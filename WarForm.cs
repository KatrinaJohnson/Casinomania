using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static Casinomania.formSlots;

namespace Casinomania
{
    public partial class formWar : Form
    {

        public static string UserName = "";

        // -- Card class ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public class Card
        // -----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        {
            public string Suit { get; set; }
            public string Rank { get; set; }

            public string Color
            {
                get
                {
                    if (Suit == "diamonds" || Suit == "hearts")
                        return "red";
                    else if (Suit == "clubs" || Suit == "spades")
                        return "black";
                    else if (Rank == "black_joker")
                        return "black";
                    //else if (Rank == "red_joker")
                    //    return "red";
                    else
                        return "none";
                }
            }

            public int Value
            {
                get
                {
                    switch (Rank)
                    {
                        case "ace":
                            return 14;
                        case "king":
                            return 13;
                        case "queen":
                            return 12;
                        case "jack":
                            return 11;
                        default:
                            return int.Parse(Rank);
                    }
                }
            }
        }

        public class War
        {
            public int Coins { get; set; }
            public int Bet { get; set; }

            private List<Card> playerDeck;
            private List<Card> computerDeck;
            private List<Card> playerDiscard;
            private List<Card> computerDiscard;
            private TextBox textBox;
            private string PlayerName;
            private System.Windows.Forms.Label lableResults;
            private System.Windows.Forms.Label labelBank;
            private PictureBox pbPlayerCard;
            private PictureBox pbComputerCard;
            private Button buttonPlay;
            private Button buttonStart;
            //private Timer playFlashTimer;

            // -- War Constructor ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            public War(TextBox txtBox, /*Timer plyFlashTimer,*/ System.Windows.Forms.Label lblResults, System.Windows.Forms.Label lblBank, PictureBox pbPlyrCard, PictureBox pbCompCard, Button btnPlay, Button btnStart, string playerName)
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            {
                playerDeck = new List<Card>();
                computerDeck = new List<Card>();
                playerDiscard = new List<Card>();
                computerDiscard = new List<Card>();
                textBox = txtBox;
                PlayerName = playerName;
                lableResults = lblResults;
                pbPlayerCard = pbPlyrCard;
                pbComputerCard = pbCompCard;
                buttonPlay = btnPlay;
                buttonStart = btnStart;
                Coins = 0;
                labelBank = lblBank;
                //playFlashTimer= plyFlashTimer;
                UpdateCoinsLabel();
                CasinoCommons.GetCoins(PlayerName, "War");
            }

            public void StartGame(int bet)
            {
                List<Card> deck = InitializeDeck();
                ShuffleDeck(deck);

                // Deal the cards to the player and computer
                DealCards(deck);
                buttonPlay.Enabled = true;
                buttonStart.Enabled = false;
                Bet = bet;
                lableResults.Text = PlayerName + " bet " + Bet.ToString() + " coins.";
                UpdateCoinsLabel();
            }

            public void WriteLine(string text)
            {
                textBox.AppendText(text + "\r\n");
            }

            void PlaySound(string filename)
            {
                using (SoundPlayer player = new SoundPlayer(filename))
                {
                    player.PlaySync();
                }
            }
            
            private void UpdateCardPictures(Card player, Card computer)
            {

                pbPlayerCard.Load(player.Rank + "_of_" + player.Suit + ".png");
                pbComputerCard.Load(computer.Rank + "_of_" + computer.Suit + ".png");
            }

            public void PlayRound()
            {
                if (playerDeck.Count == 0 && playerDiscard.Count > 0) //if Player deck is empty and discard pile has cards then 
                {
                    playerDeck = playerDiscard; // assign discard pile --> player deck
                    playerDiscard.Clear(); //clear discard
                    ShuffleDeck(playerDeck); //shuffle the deck
                }

                if (computerDeck.Count == 0 && computerDiscard.Count > 0) //if the computer opponent is empty and its discard pile has cards
                {
                    computerDeck = computerDiscard;
                    computerDiscard.Clear();
                    ShuffleDeck(computerDeck);
                }

                if (playerDeck.Count == 0 || computerDeck.Count == 0)
                {
                    EndGame();
                    return;
                }

                Card playerCard = playerDeck[0];
                Card computerCard = computerDeck[0];

                playerDeck.RemoveAt(0);
                computerDeck.RemoveAt(0);

                WriteLine($"{PlayerName}'s card: {playerCard.Rank} of {playerCard.Suit}");
                WriteLine($"Computer card: {computerCard.Rank} of {computerCard.Suit}");
                UpdateCardPictures(playerCard, computerCard);

                if (playerCard.Value > computerCard.Value) //player has high card, player wins round!
                {
                    WriteLine($"{PlayerName} wins the round!");
                    lableResults.Text = PlayerName + " wins the round!";
                    playerDiscard.Add(playerCard);
                    playerDiscard.Add(computerCard);
                }
                else if (playerCard.Value < computerCard.Value) //computer has high card, computer wins round!
                {
                    WriteLine("Computer wins the round!");
                    lableResults.Text = "Computer wins the round!";
                    computerDiscard.Add(playerCard);
                    computerDiscard.Add(computerCard);
                }
                else //Round tied!
                {
                    WriteLine("It's a tie!");
                    lableResults.Text = PlayerName + "Round is tied!";
                    List<Card> warCards = new List<Card> { playerCard, computerCard };
                    PlayWarRound(warCards);
                }
            }

            private void PlayWarRound(List<Card> warCards)
            {
                if (playerDeck.Count < 4 || computerDeck.Count < 4)
                {
                    EndGame();
                    return;
                }

                List<Card> playerWarCards = playerDeck.Take(3).ToList();
                List<Card> computerWarCards = computerDeck.Take(3).ToList();

                playerDeck.RemoveRange(0, 3);
                computerDeck.RemoveRange(0, 3);

                Card playerCard = playerDeck[0];
                Card computerCard = computerDeck[0];

                playerDeck.RemoveAt(0);
                computerDeck.RemoveAt(0);

                WriteLine($"{PlayerName} war card: {playerCard.Rank} of {playerCard.Suit}");
                WriteLine($"Computer war card: {computerCard.Rank} of {computerCard.Suit}");
                UpdateCardPictures(playerCard, computerCard);

                warCards.AddRange(playerWarCards);
                warCards.AddRange(computerWarCards);
                warCards.Add(playerCard);
                warCards.Add(computerCard);

                if (playerCard.Value > computerCard.Value) // Player wins the War!
                {
                    WriteLine($"{PlayerName} wins the war!");
                    lableResults.Text = PlayerName+" wins the war!";
                    playerDiscard.AddRange(warCards);
                }
                else if (playerCard.Value < computerCard.Value) // Computer wins the War!
                {
                    WriteLine("Computer wins the war!");
                    lableResults.Text = "Computer wins the war!";
                    computerDiscard.AddRange(warCards);
                }
                else //War is tied!
                {
                    WriteLine("Tied! The War continues!");
                    lableResults.Text = "Tied! The War continues!";
                    PlayWarRound(warCards);
                }
            }

            private void EndGame()
            {
                if (playerDeck.Count == 0 && playerDiscard.Count == 0) // Player Deck is out and discard pile is out
                {
                    WriteLine("Computer wins the game!");
                    PlaySound("war_explode.wav");
                    lableResults.Text = "Computer wins the game!";
                    Coins -= Bet;
                    CasinoCommons.SetCoins(PlayerName, "War", Coins); //save the player data
                }
                else if (computerDeck.Count == 0 && computerDiscard.Count == 0) // Computer opponent Deck is out and discard pile is out
                {
                    WriteLine($"{PlayerName} wins the game!");
                    PlaySound("war_winner.wav");
                    lableResults.Text = PlayerName+" wins the game! +"+ Bet.ToString() + " coins!";
                    Coins += Bet;
                    CasinoCommons.SetCoins(PlayerName, "War", Coins);//save the player data
                }
                else //draw, no one wins! Bet returned
                {
                    WriteLine("Game ended in a draw!");
                    lableResults.Text = "Game ended in a draw!";
                }

                buttonPlay.Enabled = false;
                buttonStart.Enabled = true;
                //playFlashTimer.Enabled = false;

                UpdateCoinsLabel();
            }

            public void UpdateCoinsLabel()
            {
                labelBank.Text = "Bank: " + Coins.ToString() + " coins";
            }
            
            private List<Card> InitializeDeck()
            {
                List<Card> deck = new List<Card>();
                string[] suits = { "hearts", "diamonds", "clubs", "spades" };
                string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

                foreach (string suit in suits)
                {
                    foreach (string rank in ranks)
                    {
                        deck.Add(new Card { Suit = suit, Rank = rank });
                    }
                }

                return deck;
            }

            private void ShuffleDeck(List<Card> deck)
            {
                Random random = new Random();
                int n = deck.Count;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    Card card = deck[k];
                    deck[k] = deck[n];
                    deck[n] = card;
                }
            }

            private void DealCards(List<Card> deck)
            {
                for (int i = 0; i < deck.Count; i++)
                {
                    if (i % 2 == 0) // even cards go to player's deck, starting with the player at 0
                        playerDeck.Add(deck[i]);
                    else
                        computerDeck.Add(deck[i]); //odd cards go to the compter opponent's deck
                }
            }
        }

        private War war = null;

        private void StartTheWar(int bet)
        {
            pictureBoxPlayerCard.Load("war_card_back.jpg");
            pictureBoxComputerCard.Load("war_card_back.jpg");
            war.StartGame(bet);
        }

        //Initialize data and form objects when the form is created
        public formWar()
        {
            InitializeComponent();
            war = new War(textBoxGameLog,
                          //timerFlashPlayButton, 
                          labelResults, 
                          labelBank, 
                          pictureBoxPlayerCard, 
                          pictureBoxComputerCard,
                          buttonPlay,
                          buttonStart, 
                          UserName);
            labelPlayer.Text = UserName;
            labelWelcome.Text = "Prepare for Sky Casino War " + UserName + "!";
            buttonPlay.Enabled = false;
            buttonStart.Enabled = true;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            labelBank.Text = "Bank: "+war.Coins.ToString() + " coins";
            //timerFlashPlayButton.Enabled = true;
            war.PlayRound();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            
            war.UpdateCoinsLabel();
            int bet = (int)spinnerBet.Value;

            if (bet > war.Coins)
            {
                MessageBox.Show("You don't have ennough coins for that bet!");
            }
            else if (bet > 0)
            {
                //timerFlashPlayButton.Start();
                StartTheWar(bet);
            }
            else
            {
                MessageBox.Show("Player must make a bet before playing!");
            }
        }

        private void formWar_Shown(object sender, EventArgs e)
        {
            const string game = "War";
            war.Coins = CasinoCommons.GetCoins(UserName, game);
            labelBank.Text = "Bank: " + war.Coins + "coins";
            if (war.Coins <= 0)
            {
                CasinoCommons.SetCoins(UserName, game, 1000);
                war.Coins = 1000;
            }
        }
    }
}
