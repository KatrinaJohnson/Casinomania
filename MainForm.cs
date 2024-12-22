using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Media;

namespace Casinomania
{
    public partial class formMain : Form
    {
        public string user;
        
        public formMain()
        {
            InitializeComponent();
            user = "";
            labelWelcome.Text = "";
            SoundPlayer player = new SoundPlayer("tokyo-music-walker-gotta-go.wav"); //load our song file
            player.PlayLooping(); //play and loop the music
        }

        //Citation: Prompt class from: https://stackoverflow.com/questions/5427020/prompt-dialog-in-windows-forms
        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        // New User
        private void buttonNewUser_Click(object sender, EventArgs e)
        {
            user = Prompt.ShowDialog("Enter your name!", "New User!!!");
            user = user.Trim();
            if (user != null && user.Trim() != "") 
            {
                labelWelcome.Text = "Welcome to Sky Casino, "+user+"!";
                //Create the user file!
                //if the user files does not exist then create it!
                string filename = user + ".casino";
                if (!File.Exists(user + ".casino"))
                    File.CreateText(filename);
                else
                    //the user already exists! Show error!
                    MessageBox.Show("That user name already exists, pick a different name!","ERRORZ!", MessageBoxButtons.OK);
            }
        }

        // Login
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            user = Prompt.ShowDialog("Enter your login!", "Login in!"); //Enter user login name
            user = user.Trim(); 
            if (user != null && user.Trim() != "") //make sure it is not null or blank
            {
                if (File.Exists(user + ".casino"))
                    labelWelcome.Text = "Welcome back to Sky Casino, " + user + "!"; //if there is a file then welcome user
                else
                {
                    MessageBox.Show("Login not found! Create a New User!", "ERRORZ!", MessageBoxButtons.OK);
                }
            }
        }

        // Exit program
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Score Tracking
        private void buttonScoreTracking_Click(object sender, EventArgs e)
        {
            formScoreTracking formScores = new formScoreTracking(); // Show the second form form2.Show();
            formScores.LoadScores();
            formScores.Show();
        }
        
        // Rules
        private void rulesButton_Click(object sender, EventArgs e)
        {
            formRules rules = new formRules();
            rules.Show();
        }

        // Solitaire
        private void buttonSolitaire_Click(object sender, EventArgs e)
        {
            if (user != null && user != "")
            {
                formSolitaire.UserName = user;
                formSolitaire solitaire = new formSolitaire();
                solitaire.Show();
            }
            else
                MessageBox.Show("Please log in or create a new user!");
        }

        //Slots
        private void buttonSlots_Click(object sender, EventArgs e)
        {
            if (user != null && user != "")
            {
                formSlots.UserName = user;
                formSlots slots = new formSlots();
                slots.Show();
            }
            else
                MessageBox.Show("Please log in or create a new user!");
        }

        //War
        private void buttonWar_Click(object sender, EventArgs e)
        {
            if (user != null && user != "")
            {
                formWar.UserName = user;
                formWar war = new formWar();
                war.Show();
            }
            else
                MessageBox.Show("Please log in or create a new user!");
        }

        
    }
}
