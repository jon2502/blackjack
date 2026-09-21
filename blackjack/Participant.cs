public class Participant {
    public string Name {get; set;} = "Jhon Doe";

    public List<List<Card>> Hand = new List<List<Card>>{
        new List<Card>()
    };

    public bool InGame {get; set;} = true;

    public bool Bust {get; set;} = false;

    public void DrawACard(Card card, int i){
       Hand[i].Add(card); 
    }
    public bool Blackjack() {
        int Handsum = Hand[0].Sum(card => card.Value);
        if(Handsum == 21) {
            return true;
        }
        return false;
    }

    public bool BustCheck(int i){
        int Handsum = Hand[i].Sum(card => card.Value);
        if(Handsum > 21) {
            Bust = true;
            return true;
        }
        return false;
    }

}