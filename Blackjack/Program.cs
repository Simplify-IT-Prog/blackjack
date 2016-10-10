using System;

namespace CardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Which game shall we play:");
            Console.WriteLine ("(B)lackjack?");

            if (Console.ReadLine().ToUpper()=="B")
            { //Code for Blackjack; can add other card games later.
                //TODO add other card games
                Play_Blackjack();
                
            }
            
            // Pause at end
            Console.ReadLine();

            //Console.WriteLine("{0}", newCard.ToString());
            //DeckOfCards dealerDeck = new DeckOfCards(1);
        }

        public static void Play_Blackjack()
        {
            int numberCards_InitialDeal_Blackjack = 2;
            int numberPlayers = 0;
            string continuePlay;
            
            //Generate Shuffled deck
            DeckOfCards shuffled_Deck = new DeckOfCards(1);

            //Number of players
            Console.Clear();
            Console.Write("Enter the number of players: ");
            Int32.TryParse(Console.ReadLine(), out numberPlayers);

            //Initial Deal
            //Assume last player is dealer.
            CardHand[] playerHand = new CardHand[numberPlayers+1];
            initialDeal(numberCards_InitialDeal_Blackjack, numberPlayers, playerHand, shuffled_Deck);

            //For each player, display hand; only show first card of all opponents including dealer.
            Console.Clear();
            playerDecisions(playerHand, shuffled_Deck);
            dealerDecisions(playerHand, shuffled_Deck);

            DisplayGameStatus(playerHand);
            Console.WriteLine("Would you like to play again? Y/N");
            continuePlay = Console.ReadLine();
            if (continuePlay.ToUpper() == "Y")
                { Play_Blackjack(); }
            else { System.Environment.Exit(0); }

        }

        public static void initialDeal(int numberCardsPerPlayer, int numberPlayers, CardHand[] player, DeckOfCards shuffled_Deck)
        {//Deals two cards to each player in increasing order.  Also, records player's names during the process.
            string name;
            for (int j = 0; j < numberCardsPerPlayer; j++)
            {
                for (int i=0; i<= numberPlayers; i++)
                {
                    if (j == 0)
                    {
                        if (i != numberPlayers)
                        {
                            Console.WriteLine("Enter Player {0}'s name:", i+1);
                            name = Console.ReadLine ();
                        } else 
                        {
                            name = "Dealer";
                        }
                        player[i] = new CardHand(name);
                    }
                    player[i].TakeCard(shuffled_Deck.DealCard());
                }
            }
        }

        public static void playerDecisions (CardHand[] player, DeckOfCards shuffled_Deck)
        {
            for (int i = 0; i < player.GetUpperBound(0); i++) //The decision rests on i; no decisions for dealer.
            {//Print opponent's hands.
                Console.Clear();
                for (int j = 0; j <= player.GetUpperBound(0); j++)
                {
                    if (j != i)
                    {
                        writePlayerHand(j, false, player);
                    }
                }

                //Go back and print i
                Console.WriteLine();
                writePlayerHand(i, true, player);

                //Hit or Stay?
                Console.WriteLine();
                string hitChoice;
                do
                {
                    Console.WriteLine("Your current score: {0}.", player[i].score);
                    Console.Write($"{player[i].playerName}, (H)it or (S)tay: ");
                    hitChoice = Console.ReadLine().ToUpper();
                    if (hitChoice == "H")
                    {
                        player[i].TakeCard(shuffled_Deck.DealCard());
                        writePlayerHand(i, true, player);
                        WinnerStatus(player[i].score, player[player.GetUpperBound(0)].score, false);
                        if (player[i].score>=21) { break; }  //Exit if Blackjack or Bust
                    }
                } while (hitChoice.ToUpper() == "H");
                continue;
            }
        }
        public static void dealerDecisions(CardHand[] player, DeckOfCards shuffled_Deck)
        {// Automate dealer's decisions to hit or stay.
            while (player[player.GetUpperBound(0)].score < 16)
            {
                player[player.GetUpperBound(0)].TakeCard(shuffled_Deck.DealCard());
            }
        }
        public static void WinnerStatus(int score, int dealerScore, bool finalStatus)
        {
            if (score == 21)
            {
                Console.WriteLine("Blackjack!  You're a WINNER!");
            } else if(score > 21)
            {
                Console.WriteLine("Bust!  try again!");
            }
            if (finalStatus == true)
            {
                if (score < 21 && dealerScore < 21)
                {
                    Console.WriteLine((score > dealerScore) ? ("WINNER! You beat the dealer's hand.") : ("LOST! The dealer's hand beats yours."));
                } else if (score > 21 && dealerScore > 21)
                {
                    Console.WriteLine("Draw!");
                }
            }
        }
        public static void writePlayerHand(int printIndex, bool boolDisplayAllCards, CardHand[] player)
        { //Prints opponents hands and then prints player's hand
            Console.WriteLine($"{player[printIndex].playerName}'s hand:");
            if (boolDisplayAllCards == false)
            { //Opponent's hand: show only first card
                Console.WriteLine ("   {0}", player[printIndex].hand[0].ToString());
                for (int i = 1; i <= player[printIndex].hand.GetUpperBound(0); i++)
                {
                    Console.WriteLine("   * - Face Down");
                }
                
            } else
            { // Show all cards.
                for (int i = 0; i <= player[printIndex].hand.GetUpperBound(0); i++)
                {
                    Console.WriteLine("   {0}", player[printIndex].hand[i].ToString());
                }
            }
        }
        public static void DisplayGameStatus(CardHand[] player)
        {
            Console.Clear();
            for (int j = 0; j <= player.GetUpperBound(0); j++)
            {// Display each player's hand and then their game status.
                writePlayerHand(j, true, player);
                WinnerStatus(player[j].score, player[player.GetUpperBound(0)].score, true);
            }
        }
    }
}
