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
    public class ServerAPI
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
    
        public CreateUserResponse CreateUser(string firstName, string lastName, string pin)
        {
            var packet = PacketFactory.GetCreateUserPacket(firstName, lastName, pin);
            tcpClient.GetStream().Write(packet, 0, packet.Length);

            var retPacket = ReadPacketSync();
            switch (retPacket["type"].Value<string>())
            {
                case "error.invalidParameters":
                    return CreateUserResponse.InvalidParams;
                case "error.pinInUse":
                    return CreateUserResponse.PinInUse;
                case "error.invalidPinFormat":
                    return CreateUserResponse.InvalidPinFormat;
                case "user.create":
                    return CreateUserResponse.Success;
                default:
                    return CreateUserResponse.Unknown;
            }
        }

        private StoreResponse Store(byte[] packet)
        {
            tcpClient.GetStream().Write(packet, 0, packet.Length);

            var retPacket = ReadPacketSync();
            switch (retPacket["type"].Value<string>())
            {
                case "error.invalidParameters":
                    return StoreResponse.InvalidParams;
                case "db.store":
                    return StoreResponse.Success;
                default:
                    return StoreResponse.Unknown;
            }
        }

        public StoreResponse Store(string key, string value)
        {
            var packet = PacketFactory.GetStorePacket(key, value);
            return Store(packet);
        }

        public StoreResponse Store(string key, double value)
        {
            var packet = PacketFactory.GetStorePacket(key, value);
            return Store(packet);
        }

        public StoreResponse Store(string key, bool value)
        {
            var packet = PacketFactory.GetStorePacket(key, value);
            return Store(packet);
        }

        public StoreResponse Store(string key, int value)
        {
            var packet = PacketFactory.GetStorePacket(key, value);
            return Store(packet);
        }

        private JToken GetResponseRaw(string key)
        {
            var packet = PacketFactory.GetGetPacket(key);
            tcpClient.GetStream().Write(packet, 0, packet.Length);

            var retPacket = ReadPacketSync();
            switch(retPacket["type"].Value<string>())
            {
                case "error.invalidParameters":
                    return null;
                case "db.get":
                    return retPacket["value"];
                default:
                    return null;
            }
        }

        public GetResponse<T> Get<T>(string key)
        {
            var responseRaw = GetResponseRaw(key);
            if (responseRaw == null)
            {
                return null;
            }

            return new GetResponse<T>(responseRaw.Value<T>(), key);
        }
    }

    public static class PacketFactory
    {
        private static string BoolWrapper(bool value)
        {
            return value ? "true" : "false";
        }

        public static byte[] GetUserDataPacket(string pin)
        {
            return Encoding.ASCII.GetBytes($"{{\"type\":\"user.getData\",\"pin\":\"{pin}\"}}");
        }

        public static byte[] GetTransactionPacket(string pin, int quantity, bool physicalCurrency)
        {
            return Encoding.ASCII.GetBytes($"{{\"pin\":\"{pin}\",\"type\":\"user.transaction\",\"quantity\":{quantity},\"physicalCurrency\":{BoolWrapper(physicalCurrency)}}}");
        }
        public static byte[] GetCreateUserPacket(string firstName, string lastName, string pin)
        {
            return Encoding.ASCII.GetBytes(
                $"{{\"type\":\"user.create\", \"firstName\":\"{firstName}\",\"lastName\":\"{lastName}\",\"pin\":\"{pin}\"}}"
            );
        }

        public static byte[] GetStorePacket(string key, string value)
        {
            return GetStorePacketRaw(key, $"\"{value}\"");
        }

        public static byte[] GetStorePacket(string key, int value)
        {
            return GetStorePacketRaw(key, $"{value}");
        }

        public static byte[] GetStorePacket(string key, double value)
        {
            return GetStorePacketRaw(key, value.ToString());
        }

        public static byte[] GetStorePacket(string key, bool value)
        {
            return GetStorePacketRaw(key, BoolWrapper(value));
        }

        private static byte[] GetStorePacketRaw(string key, string value)
        {
            return Encoding.ASCII.GetBytes(
                $"{{\"type\":\"db.store\",\"key\":\"{key}\",\"value\":{value}}}"
            );
        }

        public static byte[] GetGetPacket(string key)
        {
            return Encoding.ASCII.GetBytes(
                $"{{\"type\": \"db.get\", \"key\":\"{key}\"}}"
            );
        }
    }
}
