using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CasinoGame
{
    public static class Constants
    {
        public static readonly string HOST_SRV_ADDRESS = "r443.net"; // make sure this resolves to r443.net!!
        public static readonly int HOST_SRV_PORT = 6978;

        public static readonly int MAXIMUM_BET = 200;
        public static readonly int MINIMUM_BET = 5;

        public static readonly double INTENDED_PAYOUT = 0.85;
        public static readonly double HALF_PAYOUT_RANGE = 0.775;
        public static readonly double FULL_PAYOUT_RANGE = 0.75;

        public static readonly double MAXIMUM_PAYOUT = 0.825;

        public static readonly int BEGIN_RIG_THRESHOLD = 100; // begins rigging after 100 tokens are passed through

        public static readonly int[] QUICKBET_QUANTITIES = { 10, 20, 50, 100, 150, 200 };

        public static readonly string[] ADMIN_PINS = {
            "4430"
        };

        public static class TimingConstants
        {
            public static readonly int NUM_ROLLS = 20;
            public static readonly int SLOWDOWN_THRESHOLD = 15;
            public static readonly int SLOWDOWN_SLOPE_MS = 200;
            public static readonly int DEFAULT_DELAY_MS = 200;
        }

        public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1);
    }

    public static class SlotSetupConstants
    {
        public static readonly string CARD_DB_PATH = "C:\\Users\\matta\\source\\repos\\CasinoGame\\CasinoGame\\slots\\"; // make sure this turns to \\matta !!!
        public static readonly SlotData[] ALL_SLOTS = {
            new SlotData($"{CARD_DB_PATH}metalPipe.jpg", $"{CARD_DB_PATH}metalPipe.mp3", "metalPipe", 5, 1),
            new SlotData($"{CARD_DB_PATH}baller.jpg", $"{CARD_DB_PATH}baller.mp3", "baller", 1, 2),
            new SlotData($"{CARD_DB_PATH}amongus.jpg", $"{CARD_DB_PATH}amongus.mp3", "amongus", 7, 0.5),
            new SlotData($"{CARD_DB_PATH}winton.jpg", $"{CARD_DB_PATH}winton.mp3", "winton", 2, 1.25),
            new SlotData($"{CARD_DB_PATH}oof.jpg", $"{CARD_DB_PATH}oof.mp3", "oof", 1, 1)
        };
    }

    public static class Util
    {
        public static long GetTimeSinceEpoch()
        {
            return (long)(DateTime.UtcNow - Constants.UNIX_EPOCH).TotalSeconds;
        }

        public static bool SlotArrContains(SlotData[] slots, SlotData slot)
        {
            for (var i = 0; i < slots.Length; i++)
            {
                if (slots[i] != null && slots[i].Equals(slot))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool WithinRange(double value, double goalValue, double plusMinus)
        {
            return goalValue + plusMinus >= value && goalValue - plusMinus <= value;
        }
    }
}
