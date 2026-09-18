public class Player : Participants {
    public int PlayerID {get; set;}
    public double Chips {get; set;} = 100;

    public bool State {get; set;} = true;


    public double Bet {get; set;} = 0;

    public double Insurance {get; set;} = 0;

    public double Retuns {get; set;} = 0;


    public bool SetBet(string value) {
        try {
            int Intamount = Int32.Parse(value);
            if (Intamount > Chips) {
                Console.WriteLine($"{Name} balance to low");
                return false;
            } else {
                Chips -= Intamount;
                Bet += Intamount;
                return true;
            }
        } catch {
            Console.WriteLine($"{Name} please select a valid full number");
            return false;
        }
    }

    public void PlayerBlackjack() {
        bool result = Blackjack();
        if(result) {
            Console.WriteLine($"{Name} got BlackJack");
            State = false;
            Retuns = Bet * 1.5;
        }
    }
}