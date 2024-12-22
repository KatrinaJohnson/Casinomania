namespace Casinomania
{
    partial class formSolitaire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSolitaire));
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxGameLog = new System.Windows.Forms.TextBox();
            this.labelBet = new System.Windows.Forms.Label();
            this.spinnerBet = new System.Windows.Forms.NumericUpDown();
            this.labelCoins = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerBet)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.Coral;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.Blue;
            this.buttonStart.Location = new System.Drawing.Point(563, 646);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(109, 33);
            this.buttonStart.TabIndex = 63;
            this.buttonStart.Text = "Start New Game";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxGameLog
            // 
            this.textBoxGameLog.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.textBoxGameLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGameLog.ForeColor = System.Drawing.Color.Pink;
            this.textBoxGameLog.Location = new System.Drawing.Point(12, 609);
            this.textBoxGameLog.Multiline = true;
            this.textBoxGameLog.Name = "textBoxGameLog";
            this.textBoxGameLog.ReadOnly = true;
            this.textBoxGameLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxGameLog.Size = new System.Drawing.Size(545, 70);
            this.textBoxGameLog.TabIndex = 64;
            this.textBoxGameLog.Text = "Place a bet and press \"Start New Game\". 2X payout!";
            // 
            // labelBet
            // 
            this.labelBet.AutoSize = true;
            this.labelBet.BackColor = System.Drawing.Color.Transparent;
            this.labelBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBet.ForeColor = System.Drawing.Color.Gold;
            this.labelBet.Location = new System.Drawing.Point(560, 610);
            this.labelBet.Name = "labelBet";
            this.labelBet.Size = new System.Drawing.Size(34, 16);
            this.labelBet.TabIndex = 65;
            this.labelBet.Text = "Bet:";
            // 
            // spinnerBet
            // 
            this.spinnerBet.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.spinnerBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinnerBet.ForeColor = System.Drawing.Color.Pink;
            this.spinnerBet.Location = new System.Drawing.Point(601, 609);
            this.spinnerBet.Name = "spinnerBet";
            this.spinnerBet.Size = new System.Drawing.Size(71, 29);
            this.spinnerBet.TabIndex = 66;
            // 
            // labelCoins
            // 
            this.labelCoins.AutoSize = true;
            this.labelCoins.BackColor = System.Drawing.Color.Transparent;
            this.labelCoins.ForeColor = System.Drawing.Color.Gold;
            this.labelCoins.Location = new System.Drawing.Point(12, 9);
            this.labelCoins.Name = "labelCoins";
            this.labelCoins.Size = new System.Drawing.Size(86, 13);
            this.labelCoins.TabIndex = 67;
            this.labelCoins.Text = "Welcome player!";
            // 
            // formSolitaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(684, 691);
            this.Controls.Add(this.labelCoins);
            this.Controls.Add(this.spinnerBet);
            this.Controls.Add(this.labelBet);
            this.Controls.Add(this.textBoxGameLog);
            this.Controls.Add(this.buttonStart);
            this.Name = "formSolitaire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solitaire";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.formSolitaire_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.spinnerBet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.TextBox textBoxGameLog;
        private System.Windows.Forms.Label labelBet;
        private System.Windows.Forms.NumericUpDown spinnerBet;
        private System.Windows.Forms.Label labelCoins;
    }
}