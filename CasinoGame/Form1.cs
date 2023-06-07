using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CasinoGame
{
    public partial class FrmSlotMachine : Form
    {
        private ServerAPI serverAPI;
        private string activeUser = null;
        private GetUserDataResponse latestUserData;
        private SlotsManager slotsManager;

        private Button[] quickBetButtons;

        public int exitCode { get; private set; } = 0;

        public FrmSlotMachine()
        {
            InitializeComponent();
            slotsManager = new SlotsManager(SlotSetupConstants.ALL_SLOTS, 3);

            quickBetButtons = new Button[] {
                btnQuick10,
                btnQuick20,
                btnQuick50,
                btnQuick100,
                btnQuick150,
                btnQuick200
            };

            for (var i = 0; i < quickBetButtons.Length; i++)
            {
                quickBetButtons[i].Click += QuickBet_Click;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // form setup
            nudBet.Minimum = Constants.MINIMUM_BET;
            nudBet.Maximum = Constants.MAXIMUM_BET;
            nudBet.Value = Constants.MINIMUM_BET;
            
            // client connection setup
            serverAPI = new ServerAPI(Constants.HOST_SRV_ADDRESS, Constants.HOST_SRV_PORT);

            // gets pin, game setup
            activeUser = GetPin();
            if (activeUser == null)
            {
                // if activeUser is null by this point, the application should be closed.
                Application.Exit();
                return;
            }

            // load form
            latestUserData = serverAPI.GetUserData(activeUser);
            txtBalance.Text = $"{latestUserData.tokens}";
            lblGreeting.Text = $"Hello, {latestUserData.firstName}!";
        }

        private string GetPin()
        {
            while (true)
            {
                var response = Prompt.ShowDialog("Enter your PIN:", "User Login");
                
                // check for exit
                if (response.exitCode != 0)
                {
                    return null; // return null should force a program exit
                }

                // check for valid pin
                Console.WriteLine($"User entered pin: {response.message}");
                if (serverAPI.GetUserData(response.message) != null)
                {
                    return response.message;
                }
            }
        }

        public void UpdateUserData()
        {
            // get latest data
            latestUserData = serverAPI.GetUserData(activeUser);

            // update form
            txtBalance.Text = $"{latestUserData.tokens}"; // TODO: cannot pass data between threads; FIX THIS
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            exitCode = -1;
            Close();
        }

        private void btnSpin_Click(object sender, EventArgs e)
        {
            UpdateUserData();

            // check for valid balance
            if (latestUserData.tokens < nudBet.Value)
            {
                MessageBox.Show("You do not have enough tokens to make this bet!", "Overdraft");
                return;
            }

            // disable buttons
            btnSpin.Enabled = false;
            SetQuickBetEnabled(false);

            // start game
            Game g = new Game(
                slotsManager, 
                new PictureBox[] { picSlot1, picSlot2, picSlot3 },
                (int)nudBet.Value,
                serverAPI,
                activeUser,
                this
            );
            g.Start();
        }

        public void SetQuickBetEnabled(bool enabled, bool doInvoke = false)
        {
            for (int i = 0; i < quickBetButtons.Length; i++)
            {
                if (doInvoke)
                {
                    quickBetButtons[i].Invoke(new Action(() => {
                        quickBetButtons[i].Enabled = enabled;
                    }));
                } else
                {
                    quickBetButtons[i].Enabled = enabled;
                }
            }

        }

        private void QuickBet_Click(object sender, EventArgs e) 
        {
            for (var i = 0; i < quickBetButtons.Length; i++)
            {
                if (((Button)sender).Equals(quickBetButtons[i]))
                {
                    nudBet.Value = Constants.QUICKBET_QUANTITIES[i];
                    break;
                }
            }

            btnSpin.PerformClick();
        }

        public TextBox GetBalanceTextbox()
        {
            return txtBalance;
        }

        public Button GetSpinButton()
        {
            return btnSpin;
        }
    }
}
