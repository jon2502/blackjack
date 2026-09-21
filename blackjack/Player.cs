
public class Player : Participant {
    //public List<List<Card>> splithand  = new List<List<Card>>();

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

/*    public bool CanInsure() {
        if(Chips < Bet/2) {
            Console.WriteLine($"{Name} dosent have enough to insure");
            return false;
        }
        return true;
    }*/

    public bool SetInsurance(string value) {
        try {
            int Intamount = Int32.Parse(value);
            
            if (Intamount > Chips) {
                Console.WriteLine($"{Name} balance to low");
                return false;
            } else if (Intamount> Bet / 2) {
                Console.WriteLine($"{Name} Inssurance can max be half your bet");
                return false;
            } else {
                Chips -= Intamount;
                Insurance += Intamount;
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
            InGame = false;
            Retuns = Bet * 1.5;
        }
    }

    public void PlayerBust() {
        int length = Hand.Count;
        for (int i = 0; i < length; i++){
            bool result = BustCheck(i);
            if(result) {
                Console.WriteLine($"{Name} Bust");
                InGame = false;
                Bet = 0;
        }
        }

        
    }

    public void DoubleDown() {

        InGame = false;
    }

    /*public bool Splitcheck(List<Card> hand) {
        if(hand[0].Rank == hand[1].Rank){
            return true;
        } return false;
    }

    public void Split() {
        
    }*/

    public void Surrender() {
        InGame = false;
        Chips += Bet/2;
        Bet = 0;

    }
}