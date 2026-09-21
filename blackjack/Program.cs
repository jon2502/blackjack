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

            game.GetStartingHands(blackjackdeck);
            foreach (Player player in game.players) {
                player.Blackjack();
            }

            //TurnSequence();

            //Console.WriteLine("game is over");
            //Console.WriteLine("play another round: y");
            //Console.WriteLine("stop playing: n");
                
            
        }
    }
}








