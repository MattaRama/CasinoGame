using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Websocket.Client;

namespace CasinoGame
{
    internal class ServerAPI
    {
        private TcpClient tcpClient;

        public ServerAPI(string host, int port) {
            tcpClient = new TcpClient(host, port);
        }

        private JObject ReadPacketSync()
        {
            var stream = tcpClient.GetStream();

            int bracketCount = 0;
            string packet = "";
            do
            {
                int c = stream.ReadByte();
                if (c == -1)
                    break;

                if ((char) c == '{')
                {
                    bracketCount++;
                } else if ((char) c == '}')
                {
                    bracketCount--;
                }

                packet += (char)c;
            } while (bracketCount != 0);

            return JsonConvert.DeserializeObject<JObject>(packet);
        }

        public GetUserDataResponse GetUserData(string pin)
        {
            var packet = PacketFactory.GetUserDataPacket(pin);
            tcpClient.GetStream().Write(packet, 0, packet.Length);
            var retPacket = ReadPacketSync();
            
            if (retPacket["type"].Value<string>() != "user.getData")
            {
                return null;
            }

            var data = retPacket["data"].Value<JObject>();
            var totals = data["totals"].Value<JObject>();

            return new GetUserDataResponse(
                data["firstName"].Value<string>(),
                data["lastName"].Value<string>(),
                data["pin"].Value<string>(),
                totals["tokens"].Value<int>(),
                totals["totalWon"].Value<int>(),
                totals["totalLost"].Value<int>(),
                totals["totalDeposit"].Value<int>(),
                totals["totalWithdraw"].Value<int>()
            );
        }

        public TransactionPacketResponse Transaction(string pin, int quantity, bool physicalCurrency = false)
        {
            var packet = PacketFactory.GetTransactionPacket(pin, quantity, physicalCurrency);
            tcpClient.GetStream().Write(packet, 0, packet.Length);
            
            var retPacket = ReadPacketSync();
            switch (retPacket["type"].Value<string>()) {
                case "error.invalidParameters":
                    return TransactionPacketResponse.InvalidParams;
                case "error.user.overdraft":
                    return TransactionPacketResponse.Overdraft;
                case "error.invalidPin":
                    return TransactionPacketResponse.InvalidPin;
                case "user.transaction":
                    return TransactionPacketResponse.Success;
                default:
                    return TransactionPacketResponse.Unknown;
            }
        }
    }

    public class PacketFactory
    {
        private static string boolWrapper(bool value)
        {
            return value ? "true" : "false";
        }

        public static byte[] GetUserDataPacket(string pin)
        {
            return Encoding.ASCII.GetBytes($"{{\"type\":\"user.getData\",\"pin\":\"{pin}\"}}");
        }

        public static byte[] GetTransactionPacket(string pin, int quantity, bool physicalCurrency)
        {
            return Encoding.ASCII.GetBytes($"{{\"pin\":\"{pin}\",\"type\":\"user.transaction\",\"quantity\":{quantity},\"physicalCurrency\":{boolWrapper(physicalCurrency)}}}");
        }
    }
}
