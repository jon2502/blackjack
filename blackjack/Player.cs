public class Player : Participants {
    public int PlayerID {get; set;}
    public double Chips {get; set;} = 100;

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
            Console.WriteLine($"{Name} please select a number");
            return false;
        }
    }

    public void PlayerBlackjack() {
        bool result = Blackjack();
        if(result) {
            Console.WriteLine($"{Name} got BlackJack");
            InGame = false;
            Retuns = Bet * 1.5;
        }
    }

    public void DoubleDown() {
        InGame = false;
    }
    public void Split() {
        
    }

    public void Surrender() {
        InGame = false;

    }
}