public class Participant {
    public string Name {get; set;} = "Jhon Doe";

    public List<List<Card>> Hand = [[]];

    public void DrawACard(Card card, int i){
       Hand[i].Add(card);
       BustCheck(i);
    }

    public void DisplayHand(int i){
        foreach(Card card in Hand[i]){
            Console.WriteLine($"{card.Suit}{card.Rank}");
        }
    }

    public bool Blackjack(int i) {
        int Handsum = Hand[i].Sum(card => card.Value);
        if(Handsum == 21) {
            return true;
        }
        return false;
    }

    public virtual bool BustCheck(int i){
        int AceCount = Hand[i].FindAll(card => card.Rank == "A" && card.Value == 11).Count;
        while (Hand[i].Sum(card => card.Value) > 21 && AceCount > 0) {
            Card? FoundAce = Hand[i].Find(card => card.Rank == "A" && card.Value == 11);
            FoundAce?.Value = 1;
            AceCount --;
        }
        if(Hand[i].Sum(card => card.Value) > 21) {
            return true;
        }
        return false;
    }

}