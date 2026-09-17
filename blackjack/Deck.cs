
public class Deck {
    public  List<Card> deck = new List<Card>();

    public string[] CardSuit {get; set;} = {"Diamonds","Clubs","Hearts","Spades"};
    public string[] PlayingCards {get; set;} = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};

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
                            obj.Value = Int32.Parse(Card);
                        } catch {
                            Console.WriteLine("error parsing resorting to base deck");
                            deck.Clear();
                            PlayingCards = new string[] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
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
            Console.WriteLine("error");
            deck.Clear();
            PlayingCards = new string[] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
            return false;
        }
        
    }
}