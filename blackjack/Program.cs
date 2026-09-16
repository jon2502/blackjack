using System;

namespace Blackjack {
    public class Program {
        public static int SelectPlayeramount(string input) {
            try {
                int playerCount = Int32.Parse(input);
                Console.WriteLine(playerCount);
                if( playerCount >= 1 && playerCount <= 7){
                    Console.WriteLine(playerCount);
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

        public static void GeneratePlayer(string input, int index) {
            
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
            while (playerCount >  i){
                Console.WriteLine("All players ready");
                Console.WriteLine(i);
                i ++;
            }
            Console.WriteLine("All players ready");

        }
        
    }
}








