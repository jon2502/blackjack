using System;

public class Player : Participant {
    public int PlayerID {get; set;}
    public double Chips {get; set;} = 100;

    public List<double> Bet {get; set;} = [0];
    public List<double> Insurance {get; set;} = [0];

    public List<bool> Stand {get; set;} = [false];

    public double Retuns {get; set;} = 0;
    public bool InGame {get; set;} = true;

    public static bool PlayerOption () {
            Console.WriteLine("y : yes");
            Console.WriteLine("anyother key : continue");
            string Output = Console.ReadLine() ?? "";
            if (Output == "y" || Output== "Y") {
                return true;
            } return false;
        }

    public bool SetBet(string value, int i) {
        try {
            double output = Double.Parse(value);
            if (output > Chips) {
                Console.WriteLine($"{Name} balance to low");
                return false;
            } else {
                Chips -= output;
                Bet[i] = output;
                Console.WriteLine($"{Name} bet is {Bet[i]}");
                return true;
            }
        } catch {
            Console.WriteLine($"{Name} please select a number");
            return false;
        }
    }

    public bool DoyouWantInssurance (int i){
        Console.WriteLine($"{Name} would you like to place insurance on your hand of");
        DisplayHand(i);
        Console.WriteLine($"with a bet of {Bet[i]}");
        bool Output = PlayerOption();
        return Output;
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

    public override bool BustCheck(int index){
         int length = Hand.Count;
        for (int i = 0; i < length; i++){
            bool result = base.BustCheck(index);
            if(result) {
                Console.WriteLine($"{Name} Bust");
                Stand[i] = true;
                Bet[i] = 0;
                CheckPlayerState();
                return true;
            }
        }
        return false;
    }

    public bool DoubleDown(int i) {
        Console.WriteLine($"{Name} Would you like to doubledown for your hand of?");
        DisplayHand(i);
        bool Output = PlayerOption();
        if (Output) {
            if(Bet[i] > Chips){
                Console.WriteLine($"{Name} balance to low you cant Double down");
            } else {
                Stand[i] = true;
                Chips -= Bet[i];
                Bet[i] *= 2;
                return true;
            }
        }
        return false;
    }

    public bool Split(int i) {
        if(Hand[i].Count == 2 && Hand[i][0].Value == Hand[i][1].Value){
            if(Hand[i].FindAll(card => card.Rank == "A").Count == 2) {
                Console.WriteLine($"{Name} you have two aces so your hand will be split");
                Card card = Hand[i].Last();
                Hand[i].RemoveAt(Hand[i].Count-1);
                Hand.Add([card]);
                Bet.Add(0);
                Insurance.Add(0);
                Stand.Add(false);
                return true;
            } else {
                Console.WriteLine($"{Name} You have cards with the same value and may split them if you choce");
                DisplayHand(i);
                bool Output = PlayerOption();
                if (Output) {
                    Card card = Hand[i].Last();
                    Hand[i].RemoveAt(Hand[i].Count-1);
                    Hand.Add([card]);
                    Bet.Add(0);
                    Insurance.Add(0);
                    Stand.Add(false);
                    return true;
                }
            }
        }
        return false;
    }

    public void Surrender(int i) {
        Console.WriteLine($"{Name} would you like to surrender and get half your bet back. youyr current hand is");
        DisplayHand(i);
        bool Output = PlayerOption();
        if (Output) {
             Chips += Bet[0]/2;
            Bet.Clear();
            Insurance.Clear();
            Hand.Clear();
            Stand.Clear();
            CheckPlayerState();
        }
    }

    public bool StandOrHit(int i){
        Console.WriteLine($"{Name} would you like to Hit or stand for your hand of");
        DisplayHand(i);
        Console.WriteLine($"with a bet of {Bet[i]}");
        bool Output = PlayerOption();
        return Output;
    }

    public void CheckPlayerState(){
        if(Stand.All(x => x==true) || Hand.Count <= 0){
            InGame = false;
        } else {
            InGame = true;
        }
    }
}