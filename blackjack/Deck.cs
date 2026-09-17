
public class Deck {
    public static List<Card> deck = new List<Card>();

    static private string[] cardSuit = {"diamonds","clubs","hearts","spades "};
    static private string[] PlayingCards = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};

    static void createDeck(){
        foreach(string Suit in cardSuit){
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
                            Console.WriteLine("error");
                            //clear deck
                            //try again
                        }
                    break;
                }
            
                deck.Add(obj);
            }
        }
        if(deck.Count == 32){
            
        }else {
            //clear
            // try again
        }
        
    }
}