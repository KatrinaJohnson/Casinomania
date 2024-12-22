namespace Casinomania
{
    partial class formScoreTracking
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
            this.richTextBoxScores = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxScores
            // 
            this.richTextBoxScores.BackColor = System.Drawing.Color.Salmon;
            this.richTextBoxScores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxScores.Font = new System.Drawing.Font("Matura MT Script Capitals", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxScores.ForeColor = System.Drawing.Color.Gold;
            this.richTextBoxScores.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxScores.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBoxScores.Name = "richTextBoxScores";
            this.richTextBoxScores.Size = new System.Drawing.Size(784, 861);
            this.richTextBoxScores.TabIndex = 0;
            this.richTextBoxScores.Text = "";
            // 
            // formScoreTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 861);
            this.Controls.Add(this.richTextBoxScores);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "formScoreTracking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Score Tracking";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxScores;
    }
}