using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoGame
{
    public static class Constants
    {
        public static readonly int MAXIMUM_BET = 200;
        public static readonly int MINIMUM_BET = 5;

        public static readonly double DESIRED_PAYOUT = 0.8;

        public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1);
    }

    public static class Util
    {
        public static long GetTimeSinceEpoch()
        {
            return (long)(DateTime.UtcNow - Constants.UNIX_EPOCH).TotalSeconds;
        }
    }
}
