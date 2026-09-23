public class Dealer : Participant {
    
    public bool DealerBlackjack() {
        bool result = Blackjack(0);
        if(result) {
            Console.WriteLine($"the {Name} got BlackJack with a hand of {Hand[0][0].Suit}{Hand[0][0].Rank} and {Hand[0][1].Suit}{Hand[0][1].Rank}");
            Console.WriteLine($"{Name} All players lose");
            return true;
        }
        Console.WriteLine($"the {Name}'s hand is {Hand[0][0].Suit}{Hand[0][0].Rank} and {Hand[0][1].Suit}{Hand[0][1].Rank}");
        return false;
    }
    
    public bool Dealerhit(Card card){
        int Handsum = Hand[0].Sum(card => card.Value);
        DrawACard(card, 0);
        bool result = BustCheck(0);
        if (result){
            return true;
        } else 
        return false;
    }
}