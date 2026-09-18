
public class Deck {
    public  List<Card> deck = new List<Card>();

    private static readonly string [] CardSuit = {"♦","♣","♥","♠"};

    public string[] PlayingCards {get; set;} = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};

    private static readonly string [] DeafultPlayingCards= {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};

    public bool createDeck(){
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
                            Console.WriteLine(value);
                            if(value >= 1 && value <= 11) {
                                obj.Value = value;
                            }else {
                                Console.WriteLine("ERROR Value under or outside accepted parameters");
                                deck.Clear();
                                PlayingCards = DeafultPlayingCards;
                                return false;
                            }
                        } catch {
                            Console.WriteLine("ERROR parsing resorting to base deck");
                            deck.Clear();
                            PlayingCards = DeafultPlayingCards;
                            return false;
                        }
                    break;
                }
                deck.Add(obj);
            }
        }
        Console.WriteLine(deck.Count);
        if(deck.Count % 52 == 0){
            return true;
        }else {
            Console.WriteLine("ERROR Incorrect decksize, resorting to base deck");
            deck.Clear();
            PlayingCards = DeafultPlayingCards;
            return false;
        }
        
    }
}