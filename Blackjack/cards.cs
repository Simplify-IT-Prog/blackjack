using System;

namespace CardGames
{
    public enum Suit
    {
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }
    public enum Rank
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
    public struct Card
    {
        public Suit cardSuit { get; private set; }
        public Rank cardRank { get; private set; }

        public Card(Suit s, Rank r)
        { // One card with a specific Suit and Rank.
            cardSuit = s;
            cardRank = r;
        }
        public override string ToString()
        {
            return cardRank.ToString() + " of " + cardSuit.ToString();
        }
        public static string DisplaySuit(Card x)
        {
            Enum cardSuit = x.cardSuit;
            return cardSuit.ToString();
        }
        public static string DisplayRank(Card x)
        {
            Enum cardRank = x.cardRank;
            return cardRank.ToString();
        }

    }
    public class DeckOfCards
    {
        private Card[] deck;
        private int cardsDealt = 0;
        int numCardsPerDeck = 52; // May someday expand to include Jokers.

        public DeckOfCards(int numberOfDecks)
        { // Generates a shuffled deck of cards based on the desired number of decks.
            deck = new Card[numCardsPerDeck * numberOfDecks];

            for (int i = 0; i<numberOfDecks; i++)
            {
                foreach (int j in Enum.GetValues(typeof(Suit)))
                {
                    foreach (int k in Enum.GetValues(typeof(Rank)))
                    { // Assigns a card to each index of the deck: ordered based on listing of Suit and Rank in Enums.
                        deck[(i * (numCardsPerDeck)) + (j * (Enum.GetValues(typeof(Rank)).Length)) + k] = new Card((Suit)j, (Rank)k);
                    }
                }
            }
            Shuffle();
        }

        private void Shuffle()
        { // Shuffle entire deck.
            Random rnd = new Random();
            for (int i = (deck.Length-1); i>=0; i--)
            {
                SwapTwoCards(rnd.Next(i), i);
            }
        }
        private void SwapTwoCards(int index1, int index2)
        { // Swap the two cards at the submitted indexes from array deck.
            Card tempCard = deck[index2];
            deck[index2] = deck[index1];
            deck[index1] = tempCard;
        }

        public Card DealCard()
        { // deal first card from the shuffled deck. 

            //check to see if there are cards in the deck that can be dealt to players.  If not, shuffled deck, then issue card.
            if (cardsDealt > deck.GetUpperBound(0))
            {
                Shuffle();
                cardsDealt = 0;
                return deck[cardsDealt++];
            }
            else { return deck[cardsDealt++]; }
        }
    }
    public class CardHand
    { // A player's hand
        public Card[] hand { get; private set; }
        public string playerName { get; private set; }
        public int score { get; private set; }

        // Constructor for new hand
        public CardHand(string name)
        {
            score = 0;
            playerName = name;
        }

        // Ask dealer for card and save in playerhand
        public void TakeCard(Card dealtCard)
        {
            if (hand == null )
            {
                hand = new Card[1];
                hand[0] = dealtCard;
            } else
            {
                Card[] tempHand = hand;
                hand = new Card[tempHand.Length+1];
                tempHand.CopyTo(hand, 0);
                hand[hand.GetUpperBound(0)] = dealtCard;
            }
            DetermineScore();
        }

        // Determine player score for Blackjack
        private void DetermineScore()
        {
            score = 0;
            // Loop through cards and sum scores
            foreach (Card cardInHand in hand)
            {
                switch (cardInHand.cardRank)
                {
                    case Rank.Ace:
                        score += 11;
                        break;
                    case Rank.Two:
                        score += 2;
                        break;
                    case Rank.Three:
                        score += 3;
                        break;
                    case  Rank.Four:
                        score += 4;
                        break;
                    case Rank.Five:
                        score += 5;
                        break;
                    case Rank.Six:
                        score += 6;
                        break;
                    case Rank.Seven:
                        score += 7;
                        break;
                    case Rank.Eight:
                        score += 8;
                        break;
                    case Rank.Nine:
                        score += 9;
                        break;
                    case Rank.Ten:
                    case Rank.Jack:
                    case Rank.Queen:
                    case Rank.King:
                        score += 10;
                        break;
                    default:
                        break;
                } 
            }
        }

    }


}


