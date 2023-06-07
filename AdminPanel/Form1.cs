using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CasinoGame;

namespace AdminPanel
{
    public partial class frmAdminPanel : Form
    {
        private ServerAPI serverAPI;

        public frmAdminPanel()
        {
            InitializeComponent();
            serverAPI = new ServerAPI(Constants.HOST_SRV_ADDRESS, Constants.HOST_SRV_PORT);
        }

        private bool AdminVerification()
        {
            var res = Prompt.ShowDialog("Enter the ADMIN pin", "Admin Authorization", true);
            if (!Constants.ADMIN_PINS.Contains(res.message))
            {
                MessageBox.Show("Invalid Administrator Pin", "Failed Authorization");
                return false;
            }

            return true;
        }

        // commits create user request
        private void btnCUCommit_Click(object sender, EventArgs e)
        {
            if (!AdminVerification())
            {
                return;
            }

            CreateUser();
        }

        private bool PinInUse(string pin)
        {
            var res = serverAPI.GetUserData(pin);
            return res != null;
        }

        private bool ValidPin()
        {
            // valid pin
            var validPinRes = serverAPI.GetUserData(txtTransactionPin.Text);
            if (validPinRes != null)
            {
                MessageBox.Show("Invalid Pin or pin is already in use!", "Error");
                return false;
            }

            return true;
        }

        private void CreateUser()
        {
            if (!ValidPin())
            {
                return;
            }

            // create user
            var createUserRes = serverAPI.CreateUser(txtFirstName.Text, txtLastName.Text, txtCUPin.Text);
            if (createUserRes != CreateUserResponse.Success)
            {
                MessageBox.Show($"Failed to create user: {createUserRes}", "Error");
                return;
            }

            // deposit 
            var depositRes = serverAPI.Transaction(txtCUPin.Text, (int)nudDeposit.Value);
            if (depositRes != TransactionPacketResponse.Success)
            {
                MessageBox.Show($"Created user, but failed to enter deposit", "WARN");
                return;
            }

            MessageBox.Show("Created User Successfully", "Success");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCUPin.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            nudDeposit.Value = 0;
        }

        private void btnTransactionCommit_Click(object sender, EventArgs e)
        {
            if (!AdminVerification())
            {
                return;
            }

            Transaction();
        }

        private void Transaction()
        {
            // valid pin
            if (!PinInUse(txtTransactionPin.Text))
            {
                MessageBox.Show("Could not run transaction; user doesn't exist", "Error                                                                              ");
                return;
            }

            // transaction
            var transactionRes = serverAPI.Transaction(
                txtTransactionPin.Text,
                (int)nudTransactionDeposit.Value,
                cbxPhysical.Checked
            );
            if (transactionRes != TransactionPacketResponse.Success)
            {
                MessageBox.Show($"Failed to make transaction: {transactionRes}", "ERROR");
                return;
            }

            MessageBox.Show("Commited transaction successfully", "Success");
        }

        private void btnCommitBalance_Click(object sender, EventArgs e)
        {
            if (!PinInUse(txtBalancePin.Text))
            {
                MessageBox.Show("Invalid Pin", "Error");
                return;
            }

            // get balance
            var res = serverAPI.GetUserData(txtBalancePin.Text);
            MessageBox.Show($"{res.firstName} {res.lastName}'s Balance:\n{res.tokens}", "Success");
        }

        private void btnClearBalance_Click(object sender, EventArgs e)
        {
            txtBalancePin.Text = "";
        }

        private void btnTransactionClear_Click(object sender, EventArgs e)
        {
            txtTransactionPin.Text = "";
            nudTransactionDeposit.Value = 0;
            cbxPhysical.Checked = true;
        }
    }
}
