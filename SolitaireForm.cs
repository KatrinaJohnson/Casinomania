using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static Casinomania.formSolitaire;
using static Casinomania.formWar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Casinomania
{
    public partial class formSolitaire : Form
    {
        public static string UserName = ""; //Default user name if none is assigned

        const int CardHeight = 100; //Adjust to control card height
        const int CardWidth = (int)((float)CardHeight * 2.5 / 3.5); // calculate card height to keep standard playing card ratio

        //Adjust this to control spacing between cards
        const int CardXSpacing = 20;
        const int CardYSpacing = 30;

        // ------------------------------------------------------------------------------------------------------------------------------------------------------
        public class Card // ------------------------------------------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------------------------------------------------------------
        {
            public string Suit { get; set; }
            public string Rank { get; set; }
            private bool faceUp;
            private string filename;

            public bool FaceUp
            {
                get
                {
                    return faceUp; // Simply return the private field
                }
                set
                {
                    faceUp = value;
                    filename = Rank + "_of_" + Suit + ".png";
                    if (value) // Face Up
                        pictureBox.Load(filename); // load the front
                    else
                        pictureBox.Load("solitaire_card_back.png"); // load the back
                }
            }

            public string Name { get { return Rank + " of " + Suit; } }

            public string Color
            {
                get
                {
                    if (Suit == "diamonds" || Suit == "hearts")
                        return "red";
                    else if (Suit == "clubs" || Suit == "spades")
                        return "black";
                    else
                        return "none";
                }
            }

            public void PictureToFront()
            {
                pictureBox.BringToFront();
            }

            public void PictureToBack()
            {
                pictureBox.SendToBack();
            }

            public PictureBox pictureBox;
            private formSolitaire solitaireForm;
            private static Card fromCard; //for card selection
            private static Card toCard; //for card selection
            private int rankNo;
            public int RankNo {get{return rankNo;}}

            private int getACardIndexByPictureBox(PictureBox pb)
            {
                if (solitaireForm != null && solitaireForm.solitaire != null && solitaireForm.solitaire.deck != null)
                {
                    for (int i = 0; i < solitaireForm.solitaire.deck.Count; i++)
                    {
                        Card card = solitaireForm.solitaire.deck[i];
                        if (card != null && card.pictureBox == pb)
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }

            private void WriteLine(string txt)
            {
                solitaireForm.solitaire.WriteLine(txt);
            }

            private bool IsTableauCard(Card card)
            {
                return solitaireForm.solitaire.IsTableauCard(card);
            }

            private bool IsStockCard(Card card)
            {
                return solitaireForm.solitaire.IsStockCard(card);
            }

            private bool IsFoundationCard(Card card)
            {
                return solitaireForm.solitaire.IsFoundationCard(card);
            }

            private void CheckForWinner()
            {
                //if the Foundation pile is full then player wins!
                if (solitaireForm.solitaire.FoundationPileFull()) // All foundation piles are full
                {
                    int Payout = solitaireForm.solitaire.Bet * 3;
                    solitaireForm.solitaire.Coins += Payout; //Pay out 3 times Bet
                    solitaireForm.solitaire.WriteLine("3X payout. You won" + solitaireForm.spinnerBet.Value.ToString() + "coins!!!");
                    solitaireForm.solitaire.Bet = 0; //Reset for next bet
                    solitaireForm.UpdateScoreLabel();
                }
            }

            // ----------------------------------------------------------------------------------------------------------------------------------------------
            // CLICKED CARD - LEFT MOUSE BUTTON - HANDLES WHEN USER CLICKS THE CARD ONCE --------------------------------------------------------------------
            // ----------------------------------------------------------------------------------------------------------------------------------------------
            public void CardClickHandler(object sender, EventArgs e)
            {
                Control clicked_ctrl = (Control)sender; //convert ot a Control type class
                Card clickedCard = null;
                if (clicked_ctrl.Tag != null && clicked_ctrl.Tag is Card) //make sure the clicked control Tag is not null and is tagged as a Card class type.
                {
                    clickedCard = (Card)clicked_ctrl.Tag; //set clicked card
                    WriteLine("Single clicked card: " + clickedCard.Name);

                    // Flip and skip click handling for face-down top tableau cards or clicked.
                    if (!clickedCard.FaceUp && (solitaireForm.solitaire.IsTopTableauCard(clickedCard) || solitaireForm.solitaire.TopStockCard == clickedCard))
                    {
                        clickedCard.FaceUp = true;
                        clickedCard.PictureToFront();
                        WriteLine("Flipped: " + clickedCard.Name);
                        fromCard = null;
                        toCard = null;
                        return; //Bye!
                    }

                    //Check to see if first card is selected
                    if (fromCard == null) // First click - Selecting source card here! 
                    {
                        fromCard = clickedCard; //select source card
                        WriteLine("'From' card selected: " + fromCard.Name);
                    }
                    else // Second click - Selecting target card here!
                    {
                        if (clickedCard == fromCard) // Clicked the same card twice
                        {
                            Card topTableauCard = solitaireForm.solitaire.GetTopTableauCard(fromCard); //get the top Tableaue card if it is on a Tableau stack
                            if (topTableauCard != null)
                            {
                                if (!topTableauCard.FaceUp)
                                {
                                    topTableauCard.FaceUp = true;
                                    WriteLine($"Turned over Tableau card: {topTableauCard.Name}");
                                }
                                else
                                {
                                    WriteLine(clickedCard.Name + " Tableau unselected.");
                                }
                            }
                            else
                            {
                                WriteLine("Non-Tablue card "+clickedCard.Name + " unselected.");
                            }
                            fromCard = null;
                        }
                        else // Clicked the second card! 
                        {
                            toCard = clickedCard; //select target card
                            WriteLine("'To' card clicked and selected: " + toCard.Name);

                            // Try to make the move -----------------------------------------------
                            string MoveMade = solitaireForm.solitaire.PlayMove(fromCard, toCard);
                            // --------------------------------------------------------------------

                            // Reset selection
                            fromCard = null;
                            toCard = null;
                        }
                    }
                }
                else
                {
                    solitaireForm.solitaire.WriteLine("Invalid selection");
                }

                CheckForWinner(); //Check to see if player has won!
            }

            // ----------------------------------------------------------------------------------------------------------------------------------------------
            // DOUBLE CLICKED CARD - LEFT MOUSE BUTTON - HANDLES WHEN USER DOUBLER CLICKS THE CARD ----------------------------------------------------------
            // ----------------------------------------------------------------------------------------------------------------------------------------------
            public void CardDoubleClickHandler(object sender, EventArgs e)
            {
                Control clicked_ctrl = (Control)sender;
                Card card = (Card)clicked_ctrl.Tag;
                //WriteLine("Double clicked card: " + card.Name);

                WriteLine($"Double-clicked card: {card.Name}");

                // Check if the card is a king and is in a Tableau pile
                if (IsTableauCard(card) && card.Rank == "king" && card.FaceUp)
                {
                    // Find first empty Tableau pile
                    for (int i = 0; i < solitaireForm.solitaire.tableauPiles.Length; i++)
                    {
                        if (solitaireForm.solitaire.tableauPiles[i].Count == 0)
                        {
                            // Get the source pile index
                            int sourcePileIndex = -1;
                            for (int j = 0; j < solitaireForm.solitaire.tableauPiles.Length; j++)
                            {
                                if (solitaireForm.solitaire.tableauPiles[j].Contains(card))
                                {
                                    sourcePileIndex = j;
                                    break;
                                }
                            }

                            if (sourcePileIndex != i) // Don't move to same pile
                            {
                                // Get all cards from king to end of pile
                                int kingIndex = solitaireForm.solitaire.tableauPiles[sourcePileIndex].IndexOf(card);
                                var cardsToMove = solitaireForm.solitaire.tableauPiles[sourcePileIndex]
                                    .Skip(kingIndex)
                                    .ToList();

                                // Update positions
                                int yOffset = 0;
                                foreach (var cardToMove in cardsToMove)
                                {
                                    cardToMove.pictureBox.Location = new Point(
                                        solitaireForm.solitaire.TableauPilesLocations[i].X,
                                        solitaireForm.solitaire.TableauPilesLocations[i].Y + yOffset);
                                    yOffset += CardYSpacing;
                                }

                                // Remove cards from source pile
                                solitaireForm.solitaire.tableauPiles[sourcePileIndex].RemoveRange(kingIndex, cardsToMove.Count);

                                // Add cards to target pile
                                solitaireForm.solitaire.tableauPiles[i].AddRange(cardsToMove);

                                WriteLine($"Moved {card.Name} and its stack to empty tableau column {i + 1}");
                                return;
                            }
                        }
                    }
                }

                else if (solitaireForm.solitaire.TopStockCard == card) // We are double clicking the stock pile
                {
                    bool Moved = false;
                    if (card.Rank == "ace" && card.FaceUp)
                    {
                        // Try to move ace to foundation
                        string MoveMade = solitaireForm.solitaire.PlayFoundation(card);
                        if (MoveMade == "Moved to Foundation" || MoveMade == "Moved to Empty Foundation")
                        {
                            solitaireForm.solitaire.WriteLine($"Move {card.Name} to foundation");
                            Moved = true;
                        }
                        else
                        {
                            solitaireForm.solitaire.WriteLine("Move: "+MoveMade);
                            Moved = false;
                        }
                    }
                    
                    if (card.Rank == "king" && card.FaceUp) //its a king
                    {
                        // Try to move king to Tableau
                        string MoveMade = solitaireForm.solitaire.PlayToEmptyTableau(card);
                        if (MoveMade == "Moved to Empty Tableau")
                        {
                            solitaireForm.solitaire.WriteLine($"Move: {card.Name} to Foundation");
                            Moved = true;
                        }
                        else
                        {
                            solitaireForm.solitaire.WriteLine("Move: " + MoveMade);
                            Moved = false;
                        }
                    }
                    
                    if (!Moved)
                    {
                        Card foundationCard = solitaireForm.solitaire.FindValidFoundationCard(card);
                        if (foundationCard != null) //found a valid foundation card to move to
                        {
                            string MoveMade = solitaireForm.solitaire.PlayMove(card, foundationCard); // non-ace top stock card --> foundation card
                            WriteLine(MoveMade);
                            WriteLine("Stock to Foundation: " + MoveMade);
                        }
                        else //Cycle stock card to to bottom
                        {
                            //put top card on the bottom
                            solitaireForm.solitaire.StockPile.Remove(card);
                            card.PictureToBack();
                            solitaireForm.solitaire.StockPile.Insert(0, card);

                            // Show the new top card!
                            solitaireForm.solitaire.TopStockCard.FaceUp = false;
                            solitaireForm.solitaire.TopStockCard.PictureToFront();

                            solitaireForm.solitaire.WriteLine($"Cycled {card.Name} to bottom of stock pile");
                        }
                    }
                }
                else if (IsTableauCard(card))
                {
                    if (solitaireForm.solitaire.IsTopTableauCard(card))
                    {
                        if (card.Rank == "ace") //Tableau ace goes to empty Foundation spot
                        {
                            // For face-up Aces, try to move to foundation
                            string MoveMade = solitaireForm.solitaire.PlayFoundation(card);
                            WriteLine(MoveMade);
                        }
                        else
                        {
                            // For other face-up cards, try to find a valid foundation card to move to
                            Card foundationCard = solitaireForm.solitaire.FindValidFoundationCard(card); //locate a foundation spot to put the card
                            string MoveMade = solitaireForm.solitaire.PlayMove(card, foundationCard);
                            WriteLine("Tablue to Foundation: " + MoveMade);
                        }
                    }
                }
                toCard = null; //clear to card selection
                fromCard = null; //clear from card selection
                
                CheckForWinner(); //Check to see if player has won!
            }

            //Constructor
            public Card(string suit, int rnkNo, string rankName, bool faceUp, formSolitaire solitaireForm)
            {
                Suit = suit;
                Rank = rankName;

                //Seting up pictureBox and all its properties starte here --------- Note: pictureBox is loaded by setting the 
                pictureBox = new PictureBox();
                pictureBox.Visible = true;
                pictureBox.BorderStyle = BorderStyle.Fixed3D;
                pictureBox.Location = new Point(CardXSpacing, CardYSpacing);
                pictureBox.Click += CardClickHandler; // adding the single click event handler to newly created card PictureBox
                pictureBox.DoubleClick += CardDoubleClickHandler; // adding the double click event handler to newly created card PictureBox
                pictureBox.Size = new Size(75, 125); //Card Default Size
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Tag = this;
                solitaireForm.Controls.Add(pictureBox);
                
                //addtional member assignments
                this.faceUp = faceUp;
                FaceUp = faceUp; // Call the property setter to load the appropriate image
                fromCard = null;
                toCard = null;
                rankNo = rnkNo;
                this.solitaireForm = solitaireForm;
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------
        public class Solitaire // ----------------------------------------------------------------------------------------------------------------------------------------
        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------
        {
            public List<Card> deck;
            public List<Card>[] tableauPiles;
            private List<Card>[] foundationPiles;
            private List<Card> stockPile;
            public List<Card> StockPile { get { return stockPile; } }
            public int Bet {get;set;}
            public int Coins { get; set; }

            public int StockPileCount
            {
                get
                {
                    if (stockPile == null)
                        return 0;
                    else
                        return stockPile.Count();
                }
            }

            public Card TopStockCard
            {
                get
                {
                    if (stockPile != null && stockPile.Count > 0)
                    {
                        //return stockPile[stockPile.Count - 1];
                        return stockPile.Last();
                    }
                    return null;
                }
            }

            public Card BottomStockCard
            {
                get
                {
                    if (stockPile != null)
                        if (stockPile.Count() > 0)
                            return stockPile[0];
                    return null;
                }
            }

            private List<Card> wastePile;
            private formSolitaire solitaireForm;

            private readonly string[] suits = { "hearts", "diamonds", "clubs", "spades" };
            private readonly string[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };
            private System.Windows.Forms.TextBox textBoxLog;

            private Point stockPileLocation;
            private Point wastePileLocation;
            private Point[] foundationPileLocations;
            private Point[] tableauPilesLocations;
            public Point[] TableauPilesLocations { get { return tableauPilesLocations; } }

            public bool IsTableauCard(Card card)
            {
                foreach (List<Card> tableauPile in solitaireForm.solitaire.tableauPiles)
                    foreach (Card tableauCard in tableauPile)
                        if (tableauCard == card)
                            return true;
                return false;
            }

            public bool IsStockCard(Card card)
            {
                foreach (Card stockCard in stockPile)
                    if (stockCard == card)
                        return true;
                return false;
            }

            public bool IsFoundationCard(Card card)
            {
                foreach (List<Card> foundationPile in solitaireForm.solitaire.foundationPiles)
                    foreach (Card foundationCard in foundationPile)
                        if (foundationCard == card)
                            return true;
                return false;
            }

            // -------------------------------------------------------------------------------------------------------------------------------------
            // Solitaire Constructor ---------------------------------------------------------------------------------------------------------------
            // -------------------------------------------------------------------------------------------------------------------------------------
            public Solitaire(formSolitaire form)
            {
                solitaireForm = form;
            }

            public void MoveTopStockCardToWaste()
            {
                if (stockPile.Count > 0)
                {
                    Card card = stockPile[stockPile.Count - 1];
                    stockPile.RemoveAt(stockPile.Count - 1);
                    wastePile.Add(card);
                    WriteLine($"Moved {card.Name} from stock pile to waste pile");

                    // If there are still cards in the stock pile, show the new top card
                    if (stockPile.Count > 0)
                    {
                        TopStockCard.FaceUp = false;
                        TopStockCard.PictureToBack();
                    }
                }
            }

            public void Deal(System.Windows.Forms.TextBox txtBoxGameLog)
            {
                // Clear existing cards and controls
                if (deck != null)
                {
                    foreach (var card in deck)
                    {
                        if (card.pictureBox != null)
                        {
                            solitaireForm.Controls.Remove(card.pictureBox);
                            card.pictureBox.Dispose();
                        }
                    }
                }

                // Reset all collections
                deck = null;
                tableauPiles = new List<Card>[7];
                foundationPiles = new List<Card>[4];
                stockPile = new List<Card>();
                //wastePile = new List<Card>();

                for (int i = 0; i < 7; i++)
                    tableauPiles[i] = new List<Card>();
                for (int i = 0; i < 4; i++)
                    foundationPiles[i] = new List<Card>();

                textBoxLog = txtBoxGameLog; //set the log text box

                stockPileLocation = new Point(CardXSpacing, CardYSpacing);
                wastePileLocation = new Point(stockPileLocation.X + CardWidth + CardXSpacing, stockPileLocation.Y);
                foundationPileLocations = new Point[4];
                tableauPilesLocations = new Point[7];

                deck = GenerateDeck(); //create and shuffle the deck
                ShuffleDeck();

                //Deal Tableau cards
                int cardIndex = 0;
                int colCounter = 0;
                for (int t = 0; t < 7; t++)
                {
                    tableauPilesLocations[t] = new Point(CardXSpacing + (CardWidth + CardXSpacing) * t,
                                                         wastePileLocation.Y + CardHeight + CardYSpacing);

                    for (int r = 0; r <= colCounter; r++)
                    {
                        Card card = deck[cardIndex];
                        card.pictureBox.Location = new Point(tableauPilesLocations[t].X,
                            tableauPilesLocations[t].Y + r * CardYSpacing);

                        if (r < t)
                            card.FaceUp = false;
                        else
                            card.FaceUp = true;
                        card.PictureToFront();

                        tableauPiles[t].Add(card);
                        cardIndex++;
                    }
                    colCounter++;
                }

                //Remaining cards go to the stock pile
                foreach (Card card in deck)
                {
                    if (!IsTableauCard(card))
                    {
                        stockPile.Add(card);
                        card.FaceUp = true;
                        card.PictureToFront();
                    }
                }

                //position foundation piles
                for (int f = 0; f < 4; f++)
                {
                    foundationPileLocations[f] = new Point(deck[0].pictureBox.Width + CardXSpacing + wastePileLocation.X + (CardWidth + CardXSpacing) * (f + 1),CardYSpacing);
                }

                WriteLine("New game started! Cards dealt.");
            }

            public string PlayMove(Card sourceCard, Card targetCard)
            {
                if (sourceCard == targetCard) //same card
                    return "Source card same as target. Invalid move.";

                if (sourceCard == null || targetCard == null) //either card is not set
                    return "Error: Impossible play condition! Either the source or target card is not selected!";

                if (IsValidTableauMove(sourceCard, targetCard)) //can be moved to Tableau
                {
                    MoveCardToTableau(sourceCard, targetCard);
                    WriteLine($"{sourceCard.Rank} of {sourceCard.Suit} --> {targetCard.Rank} of {targetCard.Suit}");
                    UpdateTablueColumnZOrder(targetCard);
                    return "Moved to Tableau";
                }
                else if (IsValidFoundationMove(sourceCard, targetCard)) //can be moved to Foundation
                {
                    MoveCardToFoundation(sourceCard, targetCard);
                    WriteLine($"{sourceCard.Rank} of {sourceCard.Suit} --> {targetCard.Rank} of {targetCard.Suit}");
                    return "Moved to Foundation";
                }

                return "Unhandled or invalid move.";
            }

            //Gets a foundation card that is valid for the card to play on top of
            public Card FindValidFoundationCard(Card card)
            {
                foreach (List<Card> foundationCards in foundationPiles)
                {
                    if (foundationCards.Count > 0)
                    {
                        Card topFoundationCard = foundationCards.Last();
                        if (card.Suit == topFoundationCard.Suit &&
                            Array.IndexOf(ranks, card.Rank) == Array.IndexOf(ranks, topFoundationCard.Rank) + 1)
                        {
                            return topFoundationCard;
                        }
                    }
                }
                return null;
            }

            //removes card from tableauPile
            private void PopFromTableauPile(Card sourceCard)
            {
                List<Card> pile = null;
                foreach (List<Card> tableauPile in tableauPiles)
                    if (tableauPile.Contains(sourceCard))
                    {
                        pile = tableauPile;
                        break;
                    }
                if (pile != null)
                    pile.Remove(pile.Last());
            }

            //removes card from stockPile
            private void PopFromStockPile(Card sourceCard)
            {
                if (stockPile != null)
                    if (stockPile.Contains(sourceCard))
                        stockPile.Remove(sourceCard);
            }

            public string PlayFoundation(Card sourceCard)
            {
                Card targetCard = FindValidFoundationCard(sourceCard);
                if (targetCard == null) // move to empty foundation spot
                {
                    //check if there is an empty foundation stack
                    for (int i = 0; i < foundationPiles.Length; i++)
                    {
                        if (foundationPiles[i].Count() == 0)
                        {
                            if (sourceCard.Rank == "ace")
                            {
                                foundationPiles[i].Add(sourceCard);
                                if (IsTableauCard(sourceCard))
                                    PopFromTableauPile(sourceCard);
                                else if (IsStockCard(sourceCard))
                                    PopFromStockPile(sourceCard);

                                // Set the card's location to the appropriate foundation pile location
                                sourceCard.pictureBox.Location = foundationPileLocations[i];
                                sourceCard.PictureToFront();
                                return "Moved to Empty Foundation";
                            }
                        }
                    }
                }
                return PlayMove(sourceCard, targetCard);
            }

            public string PlayToEmptyTableau(Card sourceCard)
            {
                if (sourceCard != null) // move to empty foundation spot
                {
                    //check if there is an empty tablue stack
                    for (int i = 0; i < tableauPiles.Length; i++)
                    {
                        if (tableauPiles[i].Count() == 0)
                        {
                            if (sourceCard.Rank == "king")
                            {
                                tableauPiles[i].Add(sourceCard);
                                PopFromStockPile(sourceCard);

                                sourceCard.pictureBox.Location = tableauPilesLocations[i];
                                sourceCard.PictureToFront();
                                UpdateTablueColumnZOrder(sourceCard);
                                return "Moved to Empty Tableau";
                            }
                        }
                    }
                }
                return "No empty Tableau spots";
            }

            public bool TurnOverCard(Card card)
            {
                if (!card.FaceUp  && IsTopTableauCard(card))
                {
                    card.FaceUp = true;
                    return true;
                }

                return false;
            }

            public void WriteLine(string text)
            {
                textBoxLog.AppendText(">"+ text + "\r\n");
            }

            private List<Card> GenerateDeck()
            {
                var newDeck = new List<Card>();

                foreach (var suit in suits)
                {
                    for(int i = 0;i<ranks.Length;i++)
                    {
                        Card card = new Card(suit, i, ranks[i], /*FaceUp=*/false, solitaireForm);
                        newDeck.Add(card);
                    }
                }

                return newDeck;
            }

            private void ShuffleDeck()
            {
                var random = new Random();
                deck = deck.OrderBy(x => random.Next()).ToList();
            }

            private bool IsValidTableauMove(Card sourceCard, Card targetCard)
            {
                // First check if cards are face up
                if (!sourceCard.FaceUp || !targetCard.FaceUp)
                    return false;

                bool isSourceTablueCard = IsTableauCard(sourceCard);
                bool isTargetStockCard = IsStockCard(targetCard);

                // Check if moving from stock to tableau (not allowed)
                if (isSourceTablueCard && isTargetStockCard)
                    return false;

                // Check for alternating colors
                if (sourceCard.Color == targetCard.Color)
                    return false;

                // Check for correct rank sequence (source should be one less than target)
                int sourceRank = Array.IndexOf(ranks, sourceCard.Rank);
                int targetRank = Array.IndexOf(ranks, targetCard.Rank);
                if (sourceRank != targetRank - 1)
                    return false;

                return true;
            }

            private bool IsValidFoundationMove(Card sourceCard, Card targetCard)
            {
                if (!sourceCard.FaceUp)
                    return false;

                if (targetCard == null && sourceCard.Rank == "ace")
                    return true;

                if (targetCard != null && sourceCard.Suit == targetCard.Suit &&
                    Array.IndexOf(ranks, sourceCard.Rank) == Array.IndexOf(ranks, targetCard.Rank) + 1)
                    return true;

                return false;
            }

            // Helper method to calculate the height of cards in a tableau pile
            private int GetTableauStackHeight(int tableauIndex)
            {
                int count = 0;
                for (int i = 0; i < tableauPiles.Length; i++)
                {
                    if (tableauPiles[i] != null && i >= tableauIndex)
                        count++;
                }
                return count * 25; // Adjust this value based on your card spacing
            }

            public Card GetTopTableauCard(Card card)
            {
                for (int i = 0; i < tableauPiles.Length; i++)
                {
                    if (tableauPiles[i].Contains(card))
                    {
                        var pile = tableauPiles[i];
                        if (pile.Count > 0)
                        {
                            //return pile.FirstOrDefault(c => c.FaceUp);
                            return pile[pile.Count - 1];
                        }
                    }
                }
                return null;
            }

            public int GetTableauPileIndex(Card card)
            {
                if (card != null)
                    if (IsTableauCard(card))
                        for (int i = 0; i < 7; i++)
                            foreach (Card tabCard in tableauPiles[i])
                                if (tabCard == card)
                                    return i;
                return -1;
            }

            private void UpdateTablueColumnZOrder(Card card)
            {
                int tableuPileIndex = GetTableauPileIndex(card);
                if (tableuPileIndex != -1)
                {
                    List<Card> tableauPile = tableauPiles[tableuPileIndex];
                    if (tableauPile.Count() > 1)
                        for (int i = 0; i < tableauPile.Count(); i++)
                            tableauPile[i].PictureToFront();
                }
            }

            private void MoveCardToTableau(Card sourceCard, Card targetCard)
            {
                // First check if the source card is in the stock/waste pile
                if (stockPile.Contains(sourceCard))
                {
                    var targetIndex = -1;
                    for (int i = 0; i < tableauPiles.Length; i++)
                    {
                        if (tableauPiles[i].Contains(targetCard))
                        {
                            targetIndex = i;
                            break;
                        }
                    }

                    if (targetIndex >= 0)
                    {
                        stockPile.Remove(sourceCard);

                        // Update the card's position
                        sourceCard.pictureBox.Location = new Point(
                            tableauPilesLocations[targetIndex].X,
                            targetCard != null ? targetCard.pictureBox.Location.Y + 25 : tableauPilesLocations[targetIndex].Y);

                        // Add to tableau
                        tableauPiles[targetIndex].Add(sourceCard);

                        // Ensure proper z-order for the entire tableau pile
                        foreach (var card in tableauPiles[targetIndex])
                        {
                            card.PictureToFront();
                        }
                    }
                    return;
                }

                // If source card is not in stock pile, check tableau piles
                int sourceIndex = -1;
                int sourceCardIndex = -1;
                for (int i = 0; i < tableauPiles.Length; i++)
                {
                    sourceCardIndex = tableauPiles[i].IndexOf(sourceCard);
                    if (sourceCardIndex >= 0)
                    {
                        sourceIndex = i;
                        break;
                    }
                }

                if (sourceIndex >= 0)
                {
                    int targetIndex = -1;
                    for (int i = 0; i < tableauPiles.Length; i++)
                    {
                        if (tableauPiles[i].Contains(targetCard))
                        {
                            targetIndex = i;
                            break;
                        }
                    }

                    if (targetIndex >= 0)
                    {
                        var cardsToMove = tableauPiles[sourceIndex].Skip(sourceCardIndex).ToList();

                        // Update positions
                        int yOffset = 0;
                        foreach (var card in cardsToMove)
                        {
                            card.pictureBox.Location = new Point(
                                tableauPilesLocations[targetIndex].X,
                                targetCard != null ? targetCard.pictureBox.Location.Y + 25 + yOffset : tableauPilesLocations[targetIndex].Y + yOffset);
                            yOffset += 25;
                        }

                        // Remove cards from source pile
                        tableauPiles[sourceIndex].RemoveRange(sourceCardIndex, cardsToMove.Count);

                        // Add cards to target pile
                        tableauPiles[targetIndex].AddRange(cardsToMove);
                    }
                }
                UpdateTablueColumnZOrder(targetCard);
            }

            private void MoveCardToFoundation(Card sourceCard, Card targetCard)
            {
                // Find target foundation pile index
                int targetPileIndex = -1;
                if (targetCard != null)
                {
                    for (int i = 0; i < foundationPiles.Length; i++)
                    {
                        if (foundationPiles[i].Contains(targetCard))
                        {
                            targetPileIndex = i;
                            break;
                        }
                    }
                }
                else // For aces going to empty foundation
                {
                    for (int i = 0; i < foundationPiles.Length; i++)
                    {
                        if (foundationPiles[i].Count == 0)
                        {
                            targetPileIndex = i;
                            break;
                        }
                    }
                }

                // Handle source card from Tableau
                if (IsTableauCard(sourceCard))
                {
                    int sourcePileIndex = -1;
                    for (int i = 0; i < tableauPiles.Length; i++)
                    {
                        if (tableauPiles[i].Contains(sourceCard))
                        {
                            sourcePileIndex = i;
                            break;
                        }
                    }

                    if (sourcePileIndex >= 0)
                    {
                        tableauPiles[sourcePileIndex].Remove(sourceCard);

                        if (tableauPiles[sourcePileIndex].Count > 0)
                        {
                            List<Card> tableauPile = tableauPiles[sourcePileIndex];
                            tableauPile[tableauPile.Count - 1].FaceUp = true;
                        }
                    }
                }
                // Handle source card from Stock
                else if (IsStockCard(sourceCard))
                {
                    stockPile.Remove(sourceCard);

                    // If there are remaining cards in the stock pile, show the new top card
                    if (stockPile.Count > 0)
                    {
                        TopStockCard.FaceUp = true;
                        TopStockCard.PictureToFront();
                    }
                }

                // Add card to foundation pile
                if (targetPileIndex >= 0)
                {
                    // Update card position
                    sourceCard.pictureBox.Location = foundationPileLocations[targetPileIndex];
                    sourceCard.PictureToFront();

                    // Add to foundation pile
                    foundationPiles[targetPileIndex].Add(sourceCard);
                }
            }

            public bool IsTopTableauCard(Card card)
            {
                for (int i = 0; i < tableauPiles.Length; i++)
                {
                    var pile = tableauPiles[i];
                    if (pile.Count > 0 && pile.Contains(card))
                    {
                        return pile[pile.Count - 1] == card;
                    }
                }
                return false;
            }

            public bool FoundationPileFull()
            {
                int Card_Count = 0;
                if (foundationPiles != null)
                    foreach (List<Card> pile in foundationPiles)
                        Card_Count += pile.Count;
                return Card_Count == 52; //all 52 deck cards are in the foundation
            }

            public bool FoundationPileEmpty()
            {
                int Card_Count = 0;
                if (foundationPiles != null)
                    foreach (List<Card> pile in foundationPiles)
                        Card_Count += pile.Count;
                return Card_Count == 0;
            }

            public bool FoundationPileHasCards()
            {
                int Card_Count = 0;
                if (foundationPiles != null)
                    foreach (List<Card> pile in foundationPiles)
                        Card_Count += pile.Count;
                return Card_Count > 0;
            }
        }

        public void UpdateScoreLabel()
        {
            labelCoins.Text = $"Welcome {UserName}! You have {solitaire.Coins.ToString() } coins";  //Set scoring label
        }

        public Solitaire solitaire;

        public formSolitaire()
        {
            InitializeComponent(); //Winforms controls set up - Visual Studio maintained project code
            solitaire = new Solitaire(this); //create new Solitaire game
            solitaire.Coins = CasinoCommons.GetCoins(UserName, "Solitaire"); //load user coins
            if (solitaire.Coins <= 0) //no coins! Give user 1000 coins to play with, for free
            {
                solitaire.Coins = 1000;
                CasinoCommons.SetCoins(UserName, "Solitaire", solitaire.Coins); //set user data
                textBoxGameLog.AppendText("No more coins? Here's 1000 coins! On the house! Good luck!\r\n");
            }
            UpdateScoreLabel();
        }

        public void PlayGame()
        {
            if (spinnerBet.Value > 0) //Player must bet to play
            {
                solitaire.Bet = (int)spinnerBet.Value; //Set the bet on the solitaire game
                solitaire.Deal(textBoxGameLog); // Deal game 
                spinnerBet.Value = 0;
            }
            else // no bet
            {
                MessageBox.Show("Place a bet to play!");
            }
        }

        private bool UserWantsToQuit()
        {
            return MessageBox.Show("Ok to quit game?", "Ok to quit game? You will lose " + solitaire.Bet + " coins.", MessageBoxButtons.OKCancel) == DialogResult.OK;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (spinnerBet.Value <= 0)
            {
                MessageBox.Show("User must bet to play!");
            }
            else if (solitaire.FoundationPileEmpty() && spinnerBet.Value > 0)
            {
                PlayGame();
                solitaire.Coins -= solitaire.Bet;
            }
            else if (UserWantsToQuit())
            {
                CasinoCommons.SetCoins(UserName, "Solitaire", solitaire.Coins);
            }
        }

        private void formSolitaire_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (solitaire.Bet > 0)
            {
                if (!solitaire.FoundationPileFull())
                {
                    CasinoCommons.SetCoins(UserName, "Solitaire", solitaire.Coins); // take money if user quits while playing
                }
            }
        }
    }
}