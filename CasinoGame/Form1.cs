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

        public int exitCode { get; private set; } = 0;

        public FrmSlotMachine()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // form setup
            nudBet.Minimum = Constants.MINIMUM_BET;
            nudBet.Maximum = Constants.MAXIMUM_BET;
            nudBet.Value = Constants.MINIMUM_BET;

            // client connection setup
            serverAPI = new ServerAPI("r443.net", 6978);

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
            txtBalance.Text = $"{latestUserData.tokens}";
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

            // commit bet
            var res = serverAPI.Transaction(activeUser, (int)nudBet.Value * -1);
            Console.WriteLine(res);
            UpdateUserData();
        }
    }
}
