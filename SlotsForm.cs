using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Casinomania.formWar;

namespace Casinomania
{
    public partial class formSlots : Form
    {
        private static void PlayWaveFile(string filename)
        {
            // Replace "yourSound.wav" with the actual path or resource name
            using (var player = new SoundPlayer(filename))
            {
                player.Play();
            }
        }

        public class SlotMachine
        {
            // Private member variables
            private Label scorelbl;
            private int coins;
            private bool winner;
            private int WinnerMultiplier;

            // Public properties
            public int Coins
            {
                get { return coins; }
                set
                {
                    coins = value;
                    if (winner && WinnerMultiplier != 0)
                    {
                        scorelbl.Text = WinnerMultiplier.ToString() + "X WINNER!" + "\r\n" + "Bank: " + coins.ToString() + " Coins";
                    }
                    else
                        scorelbl.Text = "Bank: " + coins.ToString() + " Coins";

                }
            }
            public int Bet { get; set; }
            public int Slot1Spins { get; set; }
            public int Slot2Spins { get; set; }
            public int Slot3Spins { get; set; }

            public int Slot1 { get; set; }
            public int Slot2 { get; set; }
            public int Slot3 { get; set; }

            public bool Winner { get { return winner; } }

            // Constructor
            public SlotMachine(int coins, Label scLabel)
            {
                this.coins = coins; // Initialize the private backing field
                Slot1Spins = 0;
                Slot2Spins = 0;
                Slot3Spins = 0;
                Slot1 = 6;
                Slot2 = 6;
                Slot3 = 6;
                scorelbl = scLabel;
                scorelbl.Text = "Bank: " + coins.ToString() + " Coins";
                winner = false;
                WinnerMultiplier = 0;
            }

            // Public methods
            public void Spin()
            {
                Random rnd = new Random();
                Slot3Spins = rnd.Next(40, 60);
                Slot2Spins = rnd.Next(20, Slot3Spins - 1);
                Slot1Spins = rnd.Next(1, 19);
                winner = false;
            }

            public int NextSlot(int Slot)
            {
                if (Slot == 1)
                {
                    Slot1 += 1;
                    if (Slot1 > 6)
                        Slot1 = 0;
                    return Slot1;
                }
                else if (Slot == 2)
                {
                    Slot2 += 1;
                    if (Slot2 > 6)
                        Slot2 = 0;
                    return Slot2;
                }
                else //if (Slot == 3)
                {
                    Slot3 += 1;
                    if (Slot3 > 6)
                        Slot3 = 0;
                    return Slot3;
                }
            }

            public void CheckForPayout()
            {
                WinnerMultiplier = 0;
                if (Slot1 == Slot2 && Slot2 == Slot3) // all match, payout based on multiplier
                {
                    if      (Slot1 == 0 && Slot2 == 0 && Slot3 == 0)//
                        WinnerMultiplier = 1;
                    else if (Slot1 == 1 && Slot2 == 1 && Slot3 == 1)
                        WinnerMultiplier = 2;
                    else if (Slot1 == 2 && Slot2 == 2 && Slot3 == 2)
                        WinnerMultiplier = 3;
                    else if (Slot1 == 3 && Slot2 == 3 && Slot3 == 3)
                        WinnerMultiplier = 4;
                    else if (Slot1 == 4 && Slot2 == 4 && Slot3 == 4)
                        WinnerMultiplier = 5;
                    else if (Slot1 == 5 && Slot2 == 5 && Slot3 == 5)
                        WinnerMultiplier = 6;
                    else if (Slot1 == 6 && Slot2 == 6 && Slot3 == 6)
                        WinnerMultiplier = 7;
                    Coins += WinnerMultiplier * Bet;
                    winner = true;
                }
                else if (Slot1 == Slot2 || Slot1 == Slot3 || Slot2 == Slot3) // two match
                {
                    WinnerMultiplier = 1;
                    winner = true;
                    Coins += WinnerMultiplier * Bet;
                }
                else //Lost, take the money
                {
                    winner = false;
                    Coins -= Bet;
                }
                WinnerMultiplier = 0;
            }
        }

        public SlotMachine slotMachine;
        public static string UserName = ""; //Default user name if none is assigned

        public formSlots()
        {
            InitializeComponent();
            labelWelcome.Text = "Welcome " + UserName + ", have fun gambling in space!";
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (slotMachine == null)
                slotMachine = new SlotMachine(1000, labelBank);

            if (spinnerBet.Value > 0 && slotMachine.Coins > 0)
            {

                slotMachine.Coins = slotMachine.Coins; // Resets the payout/bank label
                buttonPlay.Enabled = false;
                slotMachine.Bet = (int)spinnerBet.Value;
                slotMachine.Spin();
                timerSlot1.Start();
                timerSlot2.Start();
                timerSlot3.Start();
            }
        }

        private Image NextImage(SlotMachine machine, int SlotIndex)
        {
            return imageListSlots.Images[machine.NextSlot(SlotIndex)];
        }

        private void timerSlot1_Tick(object sender, EventArgs e)
        {
            if (slotMachine.Slot1Spins == 0)
                timerSlot1.Stop();
            else
            {
                slotMachine.Slot1Spins -= 1;
                pictureBoxSlot1.Image = NextImage(slotMachine, 1);
            }
        }

        private void timerSlot2_Tick(object sender, EventArgs e)
        {
            if (slotMachine.Slot2Spins == 0)
                timerSlot2.Stop();
            else
            {
                slotMachine.Slot2Spins -= 1;
                pictureBoxSlot2.Image = NextImage(slotMachine, 2);
            }
        }

        private void timerSlot3_Tick(object sender, EventArgs e)
        {
            if (slotMachine.Slot3Spins == 0)
            {
                timerSlot3.Stop();
                buttonPlay.Enabled = true;
                slotMachine.CheckForPayout();
                if (slotMachine.Winner)
                {
                    CasinoCommons.SetCoins(UserName, "Slots", slotMachine.Coins);
                    for (int i = 0; i < 20; i++)
                    {
                        PlayWaveFile("laserShoot.wav");
                        Thread.Sleep(100);
                    }
                }
            }
            else
            {
                slotMachine.Slot3Spins -= 1;
                pictureBoxSlot3.Image = NextImage(slotMachine, 3);
                PlayWaveFile("powerUp.wav");
            }
        }

        private void formSlots_Activated(object sender, EventArgs e)
        {
            const string game = "Slots";
            if (slotMachine != null)
            {
                slotMachine.Coins = CasinoCommons.GetCoins(UserName, game);
                if (slotMachine.Coins <= 0)
                {
                    CasinoCommons.SetCoins(UserName, game, 1000);
                    slotMachine.Coins = 1000;
                }
            }
            else
            {
                int coins = CasinoCommons.GetCoins(UserName, game);
                if (coins <= 0)
                    coins = 1000;
                labelBank.Text = "Bank: " + coins.ToString() + "coins";
            }

        }
    }
}