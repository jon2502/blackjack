using System;

public class Player : Participant {
    public int PlayerID {get; set;}
    public double Chips {get; set;} = 100;

    public List<double> Bet {get; set;} = [0];
    public List<double> Insurance {get; set;} = [0];

    public List<bool> Stand {get; set;} = [false];

    public double Retuns {get; set;} = 0;
    public bool InGame {get; set;} = true;

    public bool SetBet(string value, int i) {
        try {
            double output = Double.Parse(value);
            if (output > Chips) {
                Console.WriteLine($"{Name} balance to low");
                return false;
            } else {
                Console.WriteLine(output);
                Chips -= output;
                Bet[i] = output;
                Console.WriteLine($"{Name} bet is {Bet[0]}");
                return true;
            }
        } catch {
            Console.WriteLine($"{Name} please select a number");
            return false;
        }
    }

    public bool SetInsurance(string value, int i) {
        try {
             double output = Double.Parse(value);
            
            if (output > Chips) {
                Console.WriteLine($"{Name} balance to low");
                return false;
            } else if (output > Bet[i] / 2) {
                Console.WriteLine($"{Name} Inssurance can max be half your bet");
                return false;
            } else {
                Chips -= output;
                Insurance[i] = output; 
                return true;
            }
        } catch {
            Console.WriteLine($"{Name} please select a valid full number");
            return false;
        }
    }

    public bool PlayerBlackjack(int i) {
        bool result = Blackjack(i);
        if(result) {
            Console.WriteLine($"{Name} got BlackJack");
            Retuns = Bet[i] * 1.5;
            Stand[i] = true;
            CheckPlayerState();
            return true;
        }
        return false;
    }

    public void PlayerBust() {
        int length = Hand.Count;
        for (int i = 0; i < length; i++){
            bool result = BustCheck(i);
            if(result) {
                Console.WriteLine($"{Name} Bust");
                Stand[i] = true;
                Bet[i] = 0;
                CheckPlayerState();
            }
        }

        
    }

    public void DoubleDown() {
        
    }

    public bool Splitcheck(List<Card> hand) {
        if(hand.Count == 2 && hand[0].Value == hand[1].Value){
            return true;
        } return false;
    }

    public void Split(int i) {
        Card card = Hand[i].Last();
        Hand[i].RemoveAt(Hand[i].Count-1);
        Hand.Add([card]);
        Bet.Add(0);
        Insurance.Add(0);
        Stand.Add(false);
    }

    public void Surrender() {
        Chips += Bet[0]/2;
        Bet.Clear();
        Insurance.Clear();
        Hand.Clear();
        CheckPlayerState();
    }


    public void CheckPlayerState(){
        if(Stand.All(x => x==true) || Hand.Count <= 0){
            InGame = false;
        } else {
            InGame = true;
        }
    }
}