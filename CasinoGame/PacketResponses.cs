using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame
{
    public class GetUserDataResponse
    {
        public GetUserDataResponse(
            string firstName,
            string lastName,
            string pin,
            int tokens,
            int totalWon,
            int totalLost,
            int totalDeposit,
            int totalWithdraw
        ) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.pin = pin;
            this.tokens = tokens;
            this.totalWon = totalWon;
            this.totalLost = totalLost;
            this.totalDeposit = totalDeposit;
            this.totalWithdraw = totalWithdraw;

            // get time
            timeCreated = Util.GetTimeSinceEpoch();
        }

        public readonly string firstName;
        public readonly string lastName;
        public readonly string pin;

        public readonly int tokens;
        public readonly int totalWon;
        public readonly int totalLost;
        public readonly int totalDeposit;
        public readonly int totalWithdraw;

        public readonly long timeCreated;
    }

    public enum TransactionPacketResponse
    {
        Success,
        InvalidParams,
        InvalidPin,
        Overdraft,
        Unknown
    }
}
