using System;

public class Player : Participant {
    public float Chips {get; set;} = 100;
    public List<float> Bet {get; set;} = [0];
    public List<float> Insurance {get; set;} = [0];

    public List<bool> Stand {get; set;} = [false];

    public float Retuns {get; set;} = 0;
    public bool InGame {get; set;} = true;

    public static bool PlayerOption () {
            Console.WriteLine("y : yes");
            Console.WriteLine("anyother key : no");
            string Output = Console.ReadLine() ?? "";
            if (Output == "y" || Output== "Y") {
                return true;
            } return false;
        }

    public void SetBet(int i) {
        bool Betting = true;
        while (Betting) {
            string value = Console.ReadLine() ?? "";
            try {
                float output = float.Parse(value);
                if (output <= 1) {
                    Console.WriteLine($"{Name} please select a value of 2 or greater");
                } else if (output > Chips) {
                    Console.WriteLine($"{Name} balance to low");
                } else {
                    Chips -= output;
                    Bet[i] = output;
                    Console.WriteLine($"{Name} bet is {Bet[i]}");
                    Betting = false;
                }
            } catch {
                Console.WriteLine($"{Name} please select a number");
            }
        }
        
    }

    public void SetInsurance() {
        for (int i = 0; i < Hand.Count; i++) {
             Console.WriteLine($"{Name} would you like to place insurance on your hand of");
            DisplayHand(i);
            Console.WriteLine($"with a bet of {Bet[i]}");
            bool Output = PlayerOption();
            if (Output) {
                Console.WriteLine($"{Name} How much would you like to place into insurance?");
                bool SettingInsurance = true;
                while (SettingInsurance) {
                    string value = Console.ReadLine() ?? "";
                    try {
                        float output = float.Parse(value);
                        if (output > Chips) {
                            Console.WriteLine($"{Name} balance to low");
                        } else if (output > Bet[i] / 2) {
                            Console.WriteLine($"{Name} Inssurance can max be half your bet");
                        } else {
                            Chips -= output;
                            Insurance[i] = output;
                            Console.WriteLine($"Insurance of {Insurance[i]} has been set for the hand");
                            SettingInsurance = false;
                        }
                    } catch {
                        Console.WriteLine($"{Name} please select a valid full number");
                    }
                }
            }
        }
    }

    public bool PlayerBlackjack(int i) {
        bool result = Blackjack(i);
        if(result) {
            Console.WriteLine($"{Name} got BlackJack");
            Console.WriteLine($"{Bet[i] * 1.5f} has been aded to returns");

            Retuns = Bet[i] * 1.5f;
            Stand[i] = true;
            CheckPlayerState();
            return true;
        }
        return false;
    }

    public override bool BustCheck(int index){
        bool result = base.BustCheck(index);
        if(result) {
            Console.WriteLine($"{Name} has Busted with a hand of");
            DisplayHand(index);
            Stand[index] = true;
            Bet[index] = 0;
            CheckPlayerState();
            return true;
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
                return false;
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
            if(Hand[i][0].Rank == "A" && Hand[i][1].Rank == "A") {
                Console.WriteLine($"{Name} you have two aces so your hand will be split");
                Card card = Hand[i][0];
                Hand[i].RemoveAt(0);
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
                    Card card = Hand[i][0];
                    Hand[i].RemoveAt(0);
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
            Console.WriteLine($"Surrende accepted now returning {Bet[i]/2} to your chip pool");
            Chips += Bet[i]/2;
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

    public bool Canplay(){
        double AllBets = Bet.Sum(bet => bet);
        if(Chips <= 0 && AllBets <= 0 && Retuns <= 0){
            return false;
        }
        return true;
    }

    public bool WantToContinue() {
        Console.WriteLine($"{Name} would you continue or would you like to leave the table");
        bool Output = PlayerOption();
        return Output;

    }

    public void CheckPlayerState(){
        if(Stand.All(x => x==true) || Hand.Count <= 0){
            Console.WriteLine($"{Name} has no active hands left and is now out of the game");
            InGame = false;
        } else {
            InGame = true;
        }
    }
}