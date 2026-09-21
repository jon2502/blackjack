public class Dealer : Participant {
    

    /*public void DealerCheck(List<Player> players) {
      
    }*/
    
    public bool DealerBlackjack() {
        bool result = Blackjack();
        if(result) {
            Console.WriteLine($"the {Name} got BlackJack with a hand of {Hand[0][0].Suit}{Hand[0][0].Rank} and {Hand[0][1].Suit}{Hand[0][1].Rank}");
            Console.WriteLine($"{Name} All players lose");
            return true;
        }
        Console.WriteLine($"the {Name}'s hand i {Hand[0][0].Suit}{Hand[0][0].Rank} and {Hand[0][1].Suit}{Hand[0][1].Rank}");
        return false;
    }
    
    public bool Dealerhit(Card card){
        int Handsum = Hand[0].Sum(card => card.Value);
        if(Handsum <= 16) {
            DrawACard(card, 0);
            BustCheck(0);
            return false;
        }
        return true;
    }
}