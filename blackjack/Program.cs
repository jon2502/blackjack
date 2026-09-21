using System;

namespace Blackjack {
    public class Program {
        static void Main(string[] args) {
            Game game = new Game();

            Console.WriteLine("Hello how may players are you: from 1 - 7");
            while (true){
                string playeroutput = Console.ReadLine() ?? "";
                bool sucsess = game.SetPlayerCount(playeroutput);
                if(sucsess == true){break;}
            }

            int i = 0;
            string [] NameList = {};
            while (game.PlayerCount >  i){
                Console.WriteLine($"player {i+1} select write your name");
                string playerName = Console.ReadLine() ?? "";
                NameList.Append(playerName);
                i ++;
            }

            game.SetPlayerNames(NameList);
            //bool playing = true;

            Deck blackjackdeck = new Deck();

            Console.WriteLine("select deck size");
            while (true){
                string deckcount = Console.ReadLine() ?? "";
                bool sucsess = blackjackdeck.SetDecksize(deckcount);
                if(sucsess == true){break;}
            }
            blackjackdeck.CreateDeck();
            
            foreach (Player player in game.players) {
                Console.WriteLine($"{player.Name} place your bet");
                while (true) {
                    string amount = Console.ReadLine() ?? "";
                        bool result = player.SetBet(amount);
                        if(result == true){break;}

                }
            }
            foreach (Player player in game.players){
                player.Hand.Add(new List<Card>());
            }
            game.dealer.Hand.Add(new List<Card>());
            
            game.GetStartingHands(blackjackdeck);

            foreach (Player player in game.players) {
                player.Blackjack();
            }

            if(game.dealer.Hand[0][0].Rank == "A" && game.dealer.Hand.Count == 2){
                Console.WriteLine($"{game.dealer.Name} has an Ace would you like you place insurance?");
                foreach (Player player in game.players) {
                    Console.WriteLine($"{player.Name} would you like to place insurance or surrender");
                    Console.WriteLine("i : place insurance");
                    Console.WriteLine("s : surrender");
                    Console.WriteLine("anyother key : continue");
                    while (true) {
                        string output = Console.ReadLine() ?? "";
                        if (output == "y" || output == "n"){
                            if(output == "y") {
                                Console.WriteLine($"{player.Name} How much would you like to place into insurance?");
                                while (true) {
                                    string input = Console.ReadLine() ?? "";
                                    bool sucsess = player.SetInsurance(input);
                                    if(sucsess){break;}
                                }
                            }
                            break;
                        } else {
                            Console.WriteLine($"{player.Name} please select a vaild value");
                        }
                    }

                }
            }

            //Split
            /*
            foreach(Player player in game.players){
                bool result =
                if (){
                    
                }
                if(game.dealer.Hand[0].Rank == game.dealer.Hand[0].Rank) {
                    
                }
            }*/


            //TurnSequence();

            //Console.WriteLine("game is over");
            //Console.WriteLine("play another round: y");
            //Console.WriteLine("stop playing: n");
                
            
        }
    }
}








