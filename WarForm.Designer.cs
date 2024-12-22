namespace Casinomania
{
    partial class formWar
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formWar));
            this.textBoxGameLog = new System.Windows.Forms.TextBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.pictureBoxPlayerCard = new System.Windows.Forms.PictureBox();
            this.pictureBoxComputerCard = new System.Windows.Forms.PictureBox();
            this.labelPlayer = new System.Windows.Forms.Label();
            this.labelComputer = new System.Windows.Forms.Label();
            this.labelResults = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.spinnerBet = new System.Windows.Forms.NumericUpDown();
            this.labelBet = new System.Windows.Forms.Label();
            this.labelBank = new System.Windows.Forms.Label();
            this.timerFlashPlayButton = new System.Windows.Forms.Timer(this.components);
            this.labelWelcome = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayerCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxComputerCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerBet)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxGameLog
            // 
            this.textBoxGameLog.BackColor = System.Drawing.Color.PaleGreen;
            this.textBoxGameLog.ForeColor = System.Drawing.Color.DimGray;
            this.textBoxGameLog.Location = new System.Drawing.Point(261, 634);
            this.textBoxGameLog.Multiline = true;
            this.textBoxGameLog.Name = "textBoxGameLog";
            this.textBoxGameLog.ReadOnly = true;
            this.textBoxGameLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxGameLog.Size = new System.Drawing.Size(285, 140);
            this.textBoxGameLog.TabIndex = 0;
            this.textBoxGameLog.Text = "Place a bet and press \"Start New Game\". 2X payout!";
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonPlay.Enabled = false;
            this.buttonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPlay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.buttonPlay.Location = new System.Drawing.Point(361, 460);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 70);
            this.buttonPlay.TabIndex = 1;
            this.buttonPlay.Text = "Play Round";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // pictureBoxPlayerCard
            // 
            this.pictureBoxPlayerCard.BackColor = System.Drawing.Color.Ivory;
            this.pictureBoxPlayerCard.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlayerCard.Image")));
            this.pictureBoxPlayerCard.Location = new System.Drawing.Point(72, 44);
            this.pictureBoxPlayerCard.Name = "pictureBoxPlayerCard";
            this.pictureBoxPlayerCard.Size = new System.Drawing.Size(173, 241);
            this.pictureBoxPlayerCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlayerCard.TabIndex = 2;
            this.pictureBoxPlayerCard.TabStop = false;
            // 
            // pictureBoxComputerCard
            // 
            this.pictureBoxComputerCard.BackColor = System.Drawing.Color.Ivory;
            this.pictureBoxComputerCard.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxComputerCard.Image")));
            this.pictureBoxComputerCard.Location = new System.Drawing.Point(552, 44);
            this.pictureBoxComputerCard.Name = "pictureBoxComputerCard";
            this.pictureBoxComputerCard.Size = new System.Drawing.Size(173, 241);
            this.pictureBoxComputerCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxComputerCard.TabIndex = 3;
            this.pictureBoxComputerCard.TabStop = false;
            // 
            // labelPlayer
            // 
            this.labelPlayer.BackColor = System.Drawing.Color.Transparent;
            this.labelPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer.ForeColor = System.Drawing.Color.Gold;
            this.labelPlayer.Location = new System.Drawing.Point(72, 13);
            this.labelPlayer.Name = "labelPlayer";
            this.labelPlayer.Size = new System.Drawing.Size(173, 23);
            this.labelPlayer.TabIndex = 4;
            this.labelPlayer.Text = "Player";
            this.labelPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelComputer
            // 
            this.labelComputer.BackColor = System.Drawing.Color.Transparent;
            this.labelComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComputer.ForeColor = System.Drawing.Color.Lime;
            this.labelComputer.Location = new System.Drawing.Point(552, 13);
            this.labelComputer.Name = "labelComputer";
            this.labelComputer.Size = new System.Drawing.Size(173, 23);
            this.labelComputer.TabIndex = 5;
            this.labelComputer.Text = "Computer";
            this.labelComputer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelResults
            // 
            this.labelResults.BackColor = System.Drawing.Color.Transparent;
            this.labelResults.Font = new System.Drawing.Font("Matura MT Script Capitals", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelResults.ForeColor = System.Drawing.Color.LimeGreen;
            this.labelResults.Location = new System.Drawing.Point(252, 44);
            this.labelResults.Name = "labelResults";
            this.labelResults.Size = new System.Drawing.Size(294, 241);
            this.labelResults.TabIndex = 6;
            this.labelResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.Coral;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStart.ForeColor = System.Drawing.Color.Blue;
            this.buttonStart.Location = new System.Drawing.Point(635, 644);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 74);
            this.buttonStart.TabIndex = 7;
            this.buttonStart.Text = "Start New Game";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // spinnerBet
            // 
            this.spinnerBet.BackColor = System.Drawing.Color.MistyRose;
            this.spinnerBet.Location = new System.Drawing.Point(638, 745);
            this.spinnerBet.Name = "spinnerBet";
            this.spinnerBet.Size = new System.Drawing.Size(72, 20);
            this.spinnerBet.TabIndex = 8;
            // 
            // labelBet
            // 
            this.labelBet.AutoSize = true;
            this.labelBet.BackColor = System.Drawing.Color.Transparent;
            this.labelBet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBet.ForeColor = System.Drawing.Color.Blue;
            this.labelBet.Location = new System.Drawing.Point(595, 745);
            this.labelBet.Name = "labelBet";
            this.labelBet.Size = new System.Drawing.Size(37, 20);
            this.labelBet.TabIndex = 9;
            this.labelBet.Text = "Bet";
            // 
            // labelBank
            // 
            this.labelBank.BackColor = System.Drawing.Color.Transparent;
            this.labelBank.Font = new System.Drawing.Font("Matura MT Script Capitals", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBank.ForeColor = System.Drawing.Color.Gold;
            this.labelBank.Location = new System.Drawing.Point(12, 332);
            this.labelBank.Name = "labelBank";
            this.labelBank.Size = new System.Drawing.Size(760, 107);
            this.labelBank.TabIndex = 10;
            this.labelBank.Text = "Bank: ---- coins";
            this.labelBank.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerFlashPlayButton
            // 
            this.timerFlashPlayButton.Interval = 750;
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.BackColor = System.Drawing.Color.Transparent;
            this.labelWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWelcome.ForeColor = System.Drawing.Color.PaleGreen;
            this.labelWelcome.Location = new System.Drawing.Point(12, 764);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(59, 13);
            this.labelWelcome.TabIndex = 11;
            this.labelWelcome.Text = "Welcome";
            // 
            // formWar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 786);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.labelBank);
            this.Controls.Add(this.spinnerBet);
            this.Controls.Add(this.labelBet);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelResults);
            this.Controls.Add(this.labelComputer);
            this.Controls.Add(this.labelPlayer);
            this.Controls.Add(this.pictureBoxComputerCard);
            this.Controls.Add(this.pictureBoxPlayerCard);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.textBoxGameLog);
            this.Name = "formWar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "War!";
            this.Shown += new System.EventHandler(this.formWar_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayerCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxComputerCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinnerBet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxGameLog;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.PictureBox pictureBoxPlayerCard;
        private System.Windows.Forms.PictureBox pictureBoxComputerCard;
        private System.Windows.Forms.Label labelPlayer;
        private System.Windows.Forms.Label labelComputer;
        private System.Windows.Forms.Label labelResults;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.NumericUpDown spinnerBet;
        private System.Windows.Forms.Label labelBet;
        private System.Windows.Forms.Label labelBank;
        private System.Windows.Forms.Timer timerFlashPlayButton;
        private System.Windows.Forms.Label labelWelcome;
    }
}