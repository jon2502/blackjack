public class Dealer : Participant {
    
    /*public void DealerCheck(List<Player> players) {
      
    }*/
    
    public bool DealerBlackjack() {
        bool result = Blackjack();
        if(result) {
            Console.WriteLine($"{Name} got BlackJack");
            Console.WriteLine($"{Name} All players lose");
            return true;
        }
        return false;
    }

}