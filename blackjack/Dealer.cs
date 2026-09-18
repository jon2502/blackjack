public class Dealer : Participants {
        virtual
    public void DealerCheck(Players players) {
        if(Hand[0].Rank == "A" && Hand.Count == 2) {
            Console.WriteLine($"{Name} has an Ace would you like you place insurance?");
            //todo
            bool result = DealerBlackjack();
            if (result) {
                players.AllPlayersLose();
            }
        }
    }
    
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