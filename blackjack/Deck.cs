using System;

public class Deck {
    public  List<Card> deck = new List<Card>();

    public int DeckSize {get; set;} = 1;

    private  readonly string [] CardSuit = {"♦","♣","♥","♠"};

    public string[] PlayingCards {get; set;} = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};

    private static readonly string [] DefaultPlayingCards= {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};

    public bool SetDecksize(string input) {
        try {
            int Output = Int32.Parse(input);
            if( Output >= 1 && Output <= 8){
                DeckSize = Output;
                return true;
            } else {
                Console.WriteLine("Please select a number between 1 and 8");
                return false;
            }
        } catch {
            Console.WriteLine("Please select a number between 1 and 8");
            return false;
        }
    }

    public bool CreateDeck(){
        if (DeckSize > 1) {
                int i = 1;
                while (DeckSize > i) {
                PlayingCards = PlayingCards.Concat(PlayingCards).ToArray();
                i++;
            }
        }
        foreach(string Suit in CardSuit){
            foreach(string Card in PlayingCards) {
                Card obj = new Card();
                obj.Suit = Suit;
                obj.Rank = Card;
                switch (Card){
                    case"A":
                        obj.Value = 11;
                    break;
                    case "J"or "Q" or "K":
                        obj.Value = 10;
                    break;
                    default:
                        try{
                            int value = Int32.Parse(Card);
                            if(value >= 1 && value <= 11) {
                                obj.Value = value;
                            }else {
                                Console.WriteLine("ERROR Value under or outside accepted parameters");
                                deck.Clear();
                                PlayingCards = DefaultPlayingCards;
                                return false;
                            }
                        } catch {
                            Console.WriteLine("ERROR parsing resorting to base deck");
                            deck.Clear();
                            PlayingCards = DefaultPlayingCards;
                            return false;
                        }
                    break;
                }
                deck.Add(obj);
            }
    
        }
        if(deck.Count / DeckSize == 52){
            deck = deck.Shuffle().ToList();
            return true;
        }else {
            Console.WriteLine("ERROR Incorrect decksize, resorting to base deck");
            deck.Clear();
            PlayingCards = DefaultPlayingCards;
            return false;
        }
        
    }

    public Card DrawCard(){
        Card card = deck.First();
        deck.RemoveAt(0);
        return card;
        //function for removing card
    }
}