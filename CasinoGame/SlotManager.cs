using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics.SymbolStore;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace CasinoGame
{
    public class Game
    {
        private SlotsManager manager;
        private Thread gameThread;
        private PictureBox[] display;
        private SlotData[] currentSlots;
        private ServerAPI serverAPI;
        private string userPin;
        private int bet;
        private FrmSlotMachine frmSlotMachine;

        public Game(
            SlotsManager manager,
            PictureBox[] display,
            int bet,
            ServerAPI serverAPI,
            string userPin,
            FrmSlotMachine frmSlotMachine
        )
        {
            this.manager = manager;
            this.display = display;
            this.bet = bet;
            this.serverAPI = serverAPI;
            this.userPin = userPin;
            this.frmSlotMachine = frmSlotMachine;

            gameThread = new Thread(GameTask);
        }

        public void Start()
        {
            gameThread.Start();
        }

        private WinData CheckForWins()
        {
            for (var i = 1; i < currentSlots.Length; i++)
            {
                // three in a row win
                if (
                    i + 1 < currentSlots.Length &&
                    currentSlots[i].Equals(currentSlots[i - 1]) &&
                    currentSlots[i].Equals(currentSlots[i + 1])
                )
                {
                    return new WinData(currentSlots[i], true);
                }
                // two in a row win
                else if (currentSlots[i].Equals(currentSlots[i - 1]))
                {
                    return new WinData(currentSlots[i], false);
                }
            }

            return null;
        }

        private void RandomSlots(int cycles)
        {
            // get random slots
            var slots = manager.GetRandomSequence(cycles == 0 ? null : currentSlots);
            currentSlots = slots;

            // display slots
            for (var i = 0; i < display.Length; i++)
            {
                display[i].BackgroundImage = slots[i].image;
            }
        }

        private void GameTask()
        {
            // roll effect
            int cycles;
            for (cycles = 0; cycles < Constants.TimingConstants.NUM_ROLLS; cycles++)
            {
                RandomSlots(cycles);

                // delay for aesthetics
                var sleepTime = Constants.TimingConstants.DEFAULT_DELAY_MS;
                sleepTime +=
                    Constants.TimingConstants.SLOWDOWN_THRESHOLD <= cycles ?
                    Constants.TimingConstants.SLOWDOWN_SLOPE_MS * (cycles - Constants.TimingConstants.SLOWDOWN_THRESHOLD) : 0;
                Thread.Sleep(sleepTime);
            }

            // OLD rigging system
            /*var quantityWonRes = serverAPI.Get<double>("quantityWon");
            var quantityLostRes = serverAPI.Get<double>("quantityLost");
            if (quantityLostRes.value + quantityWonRes.value < Constants.BEGIN_RIG_THRESHOLD ||
                quantityLostRes.value <= 0)
            {
                Console.WriteLine($"Random payout: threshold not met ({quantityWonRes.value} / {quantityLostRes.value})");
                RandomSlots(cycles);
            } else
            {
                var payoutRate = quantityWonRes.value / quantityLostRes.value;
                Console.Write($"PAYOUT: {payoutRate}\t");
                if (payoutRate < Constants.FULL_PAYOUT_RANGE)
                {
                    // give full payout
                    Console.WriteLine("FULL PAYOUT");
                    var randSlot = manager.GetRandomSlot();
                    currentSlots = new SlotData[] { randSlot, randSlot, randSlot };
    
                    for (var i = 0; i < display.Length; i++)
                    {
                        display[i].BackgroundImage = currentSlots[i].image;
                    }
                    
                } else if (payoutRate < Constants.HALF_PAYOUT_RANGE) {
                    // give half payout
                    Console.WriteLine("HALF PAYOUT");
                    var winnerSlot = manager.GetRandomSlot();
                    var loserSlot = manager.GetRandomSlot(winnerSlot);
                    var rand = new Random().Next(2) == 1;
                    if (rand)
                    {
                        currentSlots = new SlotData[] { winnerSlot, winnerSlot, loserSlot };
                    } else
                    {
                        currentSlots = new SlotData[] { loserSlot, winnerSlot, winnerSlot };
                    }
    
                    for (var i = 0; i < display.Length; i++)
                    {
                        display[i].BackgroundImage = currentSlots[i].image;
                    }
                    
                } else if (payoutRate > Constants.INTENDED_PAYOUT) {
                    Console.WriteLine("NO PAYOUT");
                    // give no payout
                    for (var i = 0; i < currentSlots.Length; i++)
                    {
                        currentSlots[i] = manager.GetRandomSlot(i == 0 ? null : currentSlots[i - 1]);
                        display[i].BackgroundImage = currentSlots[i].image;
                        Console.WriteLine(CheckForWins());
                    }
                    Console.WriteLine($"Loss layout: {currentSlots[0].name}, {currentSlots[1].name}, {currentSlots[2].name}");
                } else 
                {
                    // random payout
                    Console.WriteLine("RNG PAYOUT");
                    RandomSlots(cycles);
                }
            }
            */

            // new rigging system
            var quantityWonRes = serverAPI.Get<double>("quantityWon");
            var quantityLostRes = serverAPI.Get<double>("quantityLost");
            var payoutRate = quantityWonRes.value / quantityLostRes.value;
            if (payoutRate >= Constants.MAXIMUM_PAYOUT)
            {
                Console.WriteLine($"NO PAYOUT: Payout Rate was {payoutRate}");
                // give no payout
                for (var i = 0; i < currentSlots.Length; i++)
                {
                    currentSlots[i] = manager.GetRandomSlot(i == 0 ? null : currentSlots[i - 1]);
                    display[i].BackgroundImage = currentSlots[i].image;
                    Console.WriteLine(CheckForWins());
                }
            } else
            {
                Console.WriteLine("RNG PAYOUT");
                RandomSlots(cycles);
            }

            // commit transaction
            var win = CheckForWins();
            if (win == null)
            {
                serverAPI.Transaction(userPin, bet * -1);
                var totalLost = serverAPI.Get<int>("quantityLost");
                serverAPI.Store("quantityLost", bet + totalLost.value);
                Console.WriteLine($"Bet: {bet} ; TotalLost: {totalLost.value}");

                MessageBox.Show("You lost this time :(", "How Unfortunate");
            } else
            {
                var payout = win.GetPayout(bet);
                serverAPI.Transaction(userPin, payout);
                
                var totalWon = serverAPI.Get<int>("quantityWon");
                serverAPI.Store("quantityWon", bet + totalWon.value);
                Console.WriteLine($"Bet: {bet} ; TotalWon: {totalWon.value}");
                
                WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
                player.URL = win.slotData.audio;
                player.controls.play();
                
                MessageBox.Show($"You won {payout} tokens!", "WINNER");
            }

            // DEBUG: output ending payout
            var totalWonFinal = serverAPI.Get<double>("quantityWon");
            var totalLostFinal = serverAPI.Get<double>("quantityLost");
            Console.WriteLine($"NEXT PAYOUT: {totalWonFinal.value}/{totalLostFinal.value}={totalWonFinal.value/totalLostFinal.value}");

            // update UI
            if (frmSlotMachine.GetBalanceTextbox().InvokeRequired)
            {
                frmSlotMachine.GetBalanceTextbox().Invoke(new Action(() => {
                    frmSlotMachine.UpdateUserData();
                }));

                frmSlotMachine.GetSpinButton().Invoke(new Action(() => {
                    frmSlotMachine.GetSpinButton().Enabled = true;    
                }));

                frmSlotMachine.SetQuickBetEnabled(true, true);
            } else
            {
                frmSlotMachine.UpdateUserData();
                frmSlotMachine.GetSpinButton().Enabled = true;
                frmSlotMachine.SetQuickBetEnabled(true);
            }
        }
    }

    public class SlotsManager
    {
        private SlotData[] allSlots;
        private int machineLength;
        private Random random;

        public SlotsManager(SlotData[] allSlots, int machineLength)
        {
            this.allSlots = allSlots;
            this.machineLength = machineLength;

            this.random = new Random();
        }

        public SlotData GetRandomSlot(SlotData exclude = null)
        {
            int totalWeight = 0;
            for (int i = 0; i < allSlots.Length; i++)
            {
                totalWeight += allSlots[i].weight;
            }

            int index = random.Next(totalWeight);
            int rolling = 0;
            for (int i = 0; i < allSlots.Length; i++)
            {
                rolling += allSlots[i].weight;
                if (rolling > index)
                {
                    if (exclude != null && allSlots[i].name == exclude.name)
                    {
                        return GetRandomSlot(exclude);
                    }
                    return allSlots[i];
                }
            }

            if (exclude != null && allSlots[allSlots.Length - 1].name == exclude.name)
            {
                return GetRandomSlot(exclude);
            }
            return allSlots[allSlots.Length - 1];
        }

        public SlotData[] GetRandomSequence(SlotData[] exclude = null)
        {
            SlotData[] ret = new SlotData[machineLength];
            Random rand = new Random();

            for (var i = 0; i < machineLength; i++)
            {
                var excludeSlot = exclude == null ? null : exclude[i];
                ret[i] = GetRandomSlot(excludeSlot);
            }

            return ret;
        }
    }

    public class SlotData // TODO: see if i want to fix this silly little compiler warning
    {
        public Image image { private set; get; }
        public string audio { private set; get; }
        // TODO: add sound functionality
        public string name { private set; get; }
        public int weight { private set; get; }
        public double payout { private set; get; }

        // payout should be a percent above 0, representing the amount won by the player,
        // not the total amount returned to the player
        // ex: 1 = double bet won, 2 = triple bet won, .5 = half of bet won, etc.
        public SlotData(string imagePath, string audio, string name, int weight, double payout)
        {
            this.image = Image.FromFile(imagePath);
            this.name = name;
            this.weight = weight;
            this.payout = payout;
            this.audio = audio;
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == this.GetType() && ((SlotData)obj).name == this.name;
        }
    }

    public class WinData
    {
        public SlotData slotData { private set; get; }
        public bool isFullPayout { private set; get; }

        public WinData(SlotData slotData, bool isFullPayout)
        {
            this.slotData = slotData;
            this.isFullPayout = isFullPayout;
        }

        // gets the amount of money returned to the user
        // does NOT include the initial bet
        public int GetPayout(int bet)
        {
            return (int)(slotData.payout * (isFullPayout ? 1 : 0.5) * bet);
        }
    }
}
