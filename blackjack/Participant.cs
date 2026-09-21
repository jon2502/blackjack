public class Participant {
    public string Name {get; set;} = "Jhon Doe";

    public List<Card> Hand = new List<Card>();

    public bool InGame {get; set;} = true;

    public bool Bust {get; set;} = false;

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

    public bool BustCheck(){
        int Handsum = Hand.Sum(card => card.Value);
        if(Handsum > 21) {
            Bust = true;
            return true;
        }
        return false;
    }

}