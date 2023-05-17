namespace CasinoGame
{
    partial class FrmSlotMachine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSlotMachine));
            this.picSlotBackground = new System.Windows.Forms.PictureBox();
            this.picSlot1 = new System.Windows.Forms.PictureBox();
            this.picSlot2 = new System.Windows.Forms.PictureBox();
            this.picSlot3 = new System.Windows.Forms.PictureBox();
            this.btnSpin = new System.Windows.Forms.Button();
            this.btnSignOut = new System.Windows.Forms.Button();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblGreeting = new System.Windows.Forms.Label();
            this.nudBet = new System.Windows.Forms.NumericUpDown();
            this.lblBet = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picSlotBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlot1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlot2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlot3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBet)).BeginInit();
            this.SuspendLayout();
            // 
            // picSlotBackground
            // 
            this.picSlotBackground.Image = ((System.Drawing.Image)(resources.GetObject("picSlotBackground.Image")));
            this.picSlotBackground.Location = new System.Drawing.Point(308, 12);
            this.picSlotBackground.Name = "picSlotBackground";
            this.picSlotBackground.Size = new System.Drawing.Size(604, 502);
            this.picSlotBackground.TabIndex = 0;
            this.picSlotBackground.TabStop = false;
            // 
            // picSlot1
            // 
            this.picSlot1.Location = new System.Drawing.Point(491, 256);
            this.picSlot1.Name = "picSlot1";
            this.picSlot1.Size = new System.Drawing.Size(64, 87);
            this.picSlot1.TabIndex = 1;
            this.picSlot1.TabStop = false;
            // 
            // picSlot2
            // 
            this.picSlot2.Location = new System.Drawing.Point(576, 256);
            this.picSlot2.Name = "picSlot2";
            this.picSlot2.Size = new System.Drawing.Size(64, 87);
            this.picSlot2.TabIndex = 2;
            this.picSlot2.TabStop = false;
            // 
            // picSlot3
            // 
            this.picSlot3.Location = new System.Drawing.Point(659, 256);
            this.picSlot3.Name = "picSlot3";
            this.picSlot3.Size = new System.Drawing.Size(64, 87);
            this.picSlot3.TabIndex = 3;
            this.picSlot3.TabStop = false;
            // 
            // btnSpin
            // 
            this.btnSpin.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSpin.BackgroundImage")));
            this.btnSpin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSpin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpin.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnSpin.Location = new System.Drawing.Point(778, 166);
            this.btnSpin.Name = "btnSpin";
            this.btnSpin.Size = new System.Drawing.Size(89, 187);
            this.btnSpin.TabIndex = 4;
            this.btnSpin.Text = "CLICK TO\r\nSPIN!\r\n";
            this.btnSpin.UseVisualStyleBackColor = false;
            this.btnSpin.Click += new System.EventHandler(this.btnSpin_Click);
            // 
            // btnSignOut
            // 
            this.btnSignOut.Location = new System.Drawing.Point(12, 475);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(119, 39);
            this.btnSignOut.TabIndex = 5;
            this.btnSignOut.Text = "Sign Out";
            this.btnSignOut.UseVisualStyleBackColor = true;
            this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);
            // 
            // txtBalance
            // 
            this.txtBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalance.Location = new System.Drawing.Point(12, 109);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(148, 31);
            this.txtBalance.TabIndex = 6;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(12, 81);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(148, 25);
            this.lblBalance.TabIndex = 7;
            this.lblBalance.Text = "Your Balance:";
            // 
            // lblGreeting
            // 
            this.lblGreeting.AutoSize = true;
            this.lblGreeting.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreeting.Location = new System.Drawing.Point(12, 12);
            this.lblGreeting.Name = "lblGreeting";
            this.lblGreeting.Size = new System.Drawing.Size(118, 25);
            this.lblGreeting.TabIndex = 8;
            this.lblGreeting.Text = "Hi %user%";
            // 
            // nudBet
            // 
            this.nudBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudBet.Location = new System.Drawing.Point(12, 214);
            this.nudBet.Name = "nudBet";
            this.nudBet.Size = new System.Drawing.Size(148, 31);
            this.nudBet.TabIndex = 9;
            // 
            // lblBet
            // 
            this.lblBet.AutoSize = true;
            this.lblBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBet.Location = new System.Drawing.Point(12, 186);
            this.lblBet.Name = "lblBet";
            this.lblBet.Size = new System.Drawing.Size(102, 25);
            this.lblBet.TabIndex = 10;
            this.lblBet.Text = "Your Bet:";
            // 
            // FrmSlotMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 526);
            this.Controls.Add(this.lblBet);
            this.Controls.Add(this.nudBet);
            this.Controls.Add(this.lblGreeting);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.btnSignOut);
            this.Controls.Add(this.btnSpin);
            this.Controls.Add(this.picSlot3);
            this.Controls.Add(this.picSlot2);
            this.Controls.Add(this.picSlot1);
            this.Controls.Add(this.picSlotBackground);
            this.Name = "FrmSlotMachine";
            this.Text = "Slot Machine";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSlotBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlot1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlot2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSlot3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picSlotBackground;
        private System.Windows.Forms.PictureBox picSlot1;
        private System.Windows.Forms.PictureBox picSlot2;
        private System.Windows.Forms.PictureBox picSlot3;
        private System.Windows.Forms.Button btnSpin;
        private System.Windows.Forms.Button btnSignOut;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblGreeting;
        private System.Windows.Forms.NumericUpDown nudBet;
        private System.Windows.Forms.Label lblBet;
    }
}

