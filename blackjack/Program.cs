using System;

namespace Blackjack {
    public class Program {
        public static int SelectPlayeramount(string input) {
            try {
                int playerCount = Int32.Parse(input);
                if( playerCount >= 1 && playerCount <= 7){
                    return playerCount;
                } else {
                    Console.WriteLine("Select a valid option: please try again");
                    return 0;
                }
            } catch {
                Console.WriteLine("please select a number between 1 and 7");
                return 0;
            }
        }

        public static Player GeneratePlayer(string input, int index) {

            Player obj = new Player();
            if (!string.IsNullOrWhiteSpace(input)){
                obj.Name = input;
            }
            obj.PlayerNumber = index;
            return obj;
        }


        static void Main(string[] args) {
            int playerCount = 0;
            bool selectingplayeramount = true;
            Console.WriteLine("Hello how may players are you: from 1 - 7");

            while (selectingplayeramount){
                string playeroutput = Console.ReadLine() ?? "";
                playerCount = SelectPlayeramount(playeroutput);
                if(playerCount != 0){
                    selectingplayeramount = false;
                }
            }

            int i = 0;
            Players players = new Players();
            while (playerCount >  i){
                Console.WriteLine($"player {i+1} select write your name");
                string playerName = Console.ReadLine() ?? "";
                Player playerinfo = GeneratePlayer(playerName,i);
                players.Add(i, playerinfo);
                i ++;
            }
            Console.WriteLine("All players ready");
            
        }
        
    }
}








