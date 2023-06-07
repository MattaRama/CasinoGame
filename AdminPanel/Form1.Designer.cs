namespace AdminPanel
{
    partial class frmAdminPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCreateUser = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblPin = new System.Windows.Forms.Label();
            this.txtCUPin = new System.Windows.Forms.TextBox();
            this.lblDeposit = new System.Windows.Forms.Label();
            this.nudDeposit = new System.Windows.Forms.NumericUpDown();
            this.btnCUCommit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblTransaction = new System.Windows.Forms.Label();
            this.btnTransactionClear = new System.Windows.Forms.Button();
            this.btnTransactionCommit = new System.Windows.Forms.Button();
            this.nudTransactionDeposit = new System.Windows.Forms.NumericUpDown();
            this.lblDeposit2 = new System.Windows.Forms.Label();
            this.lblPinTransaction = new System.Windows.Forms.Label();
            this.txtTransactionPin = new System.Windows.Forms.TextBox();
            this.cbxPhysical = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeposit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTransactionDeposit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCreateUser
            // 
            this.lblCreateUser.AutoSize = true;
            this.lblCreateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateUser.Location = new System.Drawing.Point(61, 9);
            this.lblCreateUser.Name = "lblCreateUser";
            this.lblCreateUser.Size = new System.Drawing.Size(161, 31);
            this.lblCreateUser.TabIndex = 0;
            this.lblCreateUser.Text = "Create User";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(140, 65);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(110, 22);
            this.txtFirstName.TabIndex = 1;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(12, 62);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(122, 25);
            this.lblFirstName.TabIndex = 2;
            this.lblFirstName.Text = "First Name:";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(12, 101);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(121, 25);
            this.lblLastName.TabIndex = 4;
            this.lblLastName.Text = "Last Name:";
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(140, 104);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(110, 22);
            this.txtLastName.TabIndex = 3;
            // 
            // lblPin
            // 
            this.lblPin.AutoSize = true;
            this.lblPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPin.Location = new System.Drawing.Point(11, 137);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(49, 25);
            this.lblPin.TabIndex = 6;
            this.lblPin.Text = "Pin:";
            // 
            // txtCUPin
            // 
            this.txtCUPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCUPin.Location = new System.Drawing.Point(66, 140);
            this.txtCUPin.Name = "txtCUPin";
            this.txtCUPin.Size = new System.Drawing.Size(184, 22);
            this.txtCUPin.TabIndex = 5;
            // 
            // lblDeposit
            // 
            this.lblDeposit.AutoSize = true;
            this.lblDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeposit.Location = new System.Drawing.Point(11, 177);
            this.lblDeposit.Name = "lblDeposit";
            this.lblDeposit.Size = new System.Drawing.Size(91, 25);
            this.lblDeposit.TabIndex = 7;
            this.lblDeposit.Text = "Deposit:";
            // 
            // nudDeposit
            // 
            this.nudDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudDeposit.Location = new System.Drawing.Point(108, 179);
            this.nudDeposit.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudDeposit.Name = "nudDeposit";
            this.nudDeposit.Size = new System.Drawing.Size(142, 26);
            this.nudDeposit.TabIndex = 8;
            // 
            // btnCUCommit
            // 
            this.btnCUCommit.Location = new System.Drawing.Point(17, 223);
            this.btnCUCommit.Name = "btnCUCommit";
            this.btnCUCommit.Size = new System.Drawing.Size(173, 23);
            this.btnCUCommit.TabIndex = 9;
            this.btnCUCommit.Text = "Commit";
            this.btnCUCommit.UseVisualStyleBackColor = true;
            this.btnCUCommit.Click += new System.EventHandler(this.btnCUCommit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(196, 223);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(54, 23);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblTransaction
            // 
            this.lblTransaction.AutoSize = true;
            this.lblTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransaction.Location = new System.Drawing.Point(355, 9);
            this.lblTransaction.Name = "lblTransaction";
            this.lblTransaction.Size = new System.Drawing.Size(165, 31);
            this.lblTransaction.TabIndex = 11;
            this.lblTransaction.Text = "Transaction:";
            // 
            // btnTransactionClear
            // 
            this.btnTransactionClear.Location = new System.Drawing.Point(491, 150);
            this.btnTransactionClear.Name = "btnTransactionClear";
            this.btnTransactionClear.Size = new System.Drawing.Size(54, 23);
            this.btnTransactionClear.TabIndex = 17;
            this.btnTransactionClear.Text = "Clear";
            this.btnTransactionClear.UseVisualStyleBackColor = true;
            // 
            // btnTransactionCommit
            // 
            this.btnTransactionCommit.Location = new System.Drawing.Point(312, 150);
            this.btnTransactionCommit.Name = "btnTransactionCommit";
            this.btnTransactionCommit.Size = new System.Drawing.Size(173, 23);
            this.btnTransactionCommit.TabIndex = 16;
            this.btnTransactionCommit.Text = "Commit";
            this.btnTransactionCommit.UseVisualStyleBackColor = true;
            this.btnTransactionCommit.Click += new System.EventHandler(this.btnTransactionCommit_Click);
            // 
            // nudTransactionDeposit
            // 
            this.nudTransactionDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTransactionDeposit.Location = new System.Drawing.Point(403, 91);
            this.nudTransactionDeposit.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudTransactionDeposit.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.nudTransactionDeposit.Name = "nudTransactionDeposit";
            this.nudTransactionDeposit.Size = new System.Drawing.Size(142, 26);
            this.nudTransactionDeposit.TabIndex = 15;
            // 
            // lblDeposit2
            // 
            this.lblDeposit2.AutoSize = true;
            this.lblDeposit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeposit2.Location = new System.Drawing.Point(306, 89);
            this.lblDeposit2.Name = "lblDeposit2";
            this.lblDeposit2.Size = new System.Drawing.Size(91, 25);
            this.lblDeposit2.TabIndex = 14;
            this.lblDeposit2.Text = "Deposit:";
            // 
            // lblPinTransaction
            // 
            this.lblPinTransaction.AutoSize = true;
            this.lblPinTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPinTransaction.Location = new System.Drawing.Point(306, 49);
            this.lblPinTransaction.Name = "lblPinTransaction";
            this.lblPinTransaction.Size = new System.Drawing.Size(49, 25);
            this.lblPinTransaction.TabIndex = 13;
            this.lblPinTransaction.Text = "Pin:";
            // 
            // txtTransactionPin
            // 
            this.txtTransactionPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransactionPin.Location = new System.Drawing.Point(361, 52);
            this.txtTransactionPin.Name = "txtTransactionPin";
            this.txtTransactionPin.Size = new System.Drawing.Size(184, 22);
            this.txtTransactionPin.TabIndex = 12;
            // 
            // cbxPhysical
            // 
            this.cbxPhysical.AutoSize = true;
            this.cbxPhysical.Checked = true;
            this.cbxPhysical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxPhysical.Location = new System.Drawing.Point(403, 127);
            this.cbxPhysical.Name = "cbxPhysical";
            this.cbxPhysical.Size = new System.Drawing.Size(153, 17);
            this.cbxPhysical.TabIndex = 18;
            this.cbxPhysical.Text = "Involves Physical Currency";
            this.cbxPhysical.UseVisualStyleBackColor = true;
            // 
            // frmAdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 347);
            this.Controls.Add(this.cbxPhysical);
            this.Controls.Add(this.btnTransactionClear);
            this.Controls.Add(this.btnTransactionCommit);
            this.Controls.Add(this.nudTransactionDeposit);
            this.Controls.Add(this.lblDeposit2);
            this.Controls.Add(this.lblPinTransaction);
            this.Controls.Add(this.txtTransactionPin);
            this.Controls.Add(this.lblTransaction);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCUCommit);
            this.Controls.Add(this.nudDeposit);
            this.Controls.Add(this.lblDeposit);
            this.Controls.Add(this.lblPin);
            this.Controls.Add(this.txtCUPin);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblCreateUser);
            this.Name = "frmAdminPanel";
            this.Text = "CasinoGame - Admin Panel (AUTHORIZED USE ONLY)";
            ((System.ComponentModel.ISupportInitialize)(this.nudDeposit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTransactionDeposit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCreateUser;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.TextBox txtCUPin;
        private System.Windows.Forms.Label lblDeposit;
        private System.Windows.Forms.NumericUpDown nudDeposit;
        private System.Windows.Forms.Button btnCUCommit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblTransaction;
        private System.Windows.Forms.Button btnTransactionClear;
        private System.Windows.Forms.Button btnTransactionCommit;
        private System.Windows.Forms.NumericUpDown nudTransactionDeposit;
        private System.Windows.Forms.Label lblDeposit2;
        private System.Windows.Forms.Label lblPinTransaction;
        private System.Windows.Forms.TextBox txtTransactionPin;
        private System.Windows.Forms.CheckBox cbxPhysical;
    }
}

