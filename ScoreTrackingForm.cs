using Casinomania;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Casinomania
{
    public partial class formScoreTracking : Form
    {
        //CITATION: Copilot
        private void CenterRichText(RichTextBox richTextBox)
        {
            // Save the current selection
            int originalSelectionStart = richTextBox.SelectionStart;
            int originalSelectionLength = richTextBox.SelectionLength;
            richTextBox.SelectAll(); // Select all text in the RichTextBox
            richTextBox.SelectionAlignment = HorizontalAlignment.Center; // Set the alignment to center
            richTextBox.Select(originalSelectionStart, originalSelectionLength); // Restore the original selection
        }
        public void LoadScores()
        {
            richTextBoxScores.Clear();
            richTextBoxScores.AppendText(CasinoCommons.GetAllCoins("Slots"));
            richTextBoxScores.AppendText(CasinoCommons.GetAllCoins("Solitaire"));
            richTextBoxScores.AppendText(CasinoCommons.GetAllCoins("War"));
            CenterRichText(richTextBoxScores);
        }

        public formScoreTracking()
        {
            InitializeComponent();
            LoadScores();
        }
    }
}





