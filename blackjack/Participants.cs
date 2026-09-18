public class Participants {
    public string Name {get; set;} = "Jhon Doe";

    public List<Card> Hand = new List<Card>();

    public void DrawACard(Card card){
       Hand.Add(card); 
    }
    public bool Blackjack() {
        int Handsum = Hand.Sum(card => card.Value);
        if(Handsum == 21) {
            return true;
        }
        return false;
    }

}