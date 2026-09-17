using System;

namespace Blackjack {
    public class Program {
        public static int SelectPlayerAmount(string input) {
            try {
                int playerCount = Int32.Parse(input);
                if( playerCount >= 1 && playerCount <= 7){
                    return playerCount;
                } else {
                    Console.WriteLine("Please select a number between 1 and 7");
                    return 0;
                }
            } catch {
                Console.WriteLine("Please select a number between 1 and 7");
                return 0;
            }
        }

        public static int SelectDeckAmount(string input) {
             try {
                int DeckCount = Int32.Parse(input);
                if( DeckCount >= 1 && DeckCount <= 8){
                    return DeckCount;
                } else {
                    Console.WriteLine("Please select a number between 1 and 8");
                    return 0;
                }
            } catch {
                Console.WriteLine("Please select a number between 1 and 8");
                return 0;
            }
        }

        public static Player GeneratePlayer(string input, int index) {

            Player obj = new Player();
            if (!string.IsNullOrWhiteSpace(input)){
                obj.Name = input;
            }
            obj.PlayerID = index;
            return obj;
        }

        public static Deck CreateDeck(int deckamount, string[] basedeck){
            Deck blackjackdeck = new Deck();
            string[] newdeck = {};
            int i = 0;
            while (deckamount > i) {
                newdeck = newdeck.Concat(basedeck).ToArray();
                i++;
            }
            
            blackjackdeck.PlayingCards = newdeck;
            
            while (true) {
                bool sucsess = blackjackdeck.createDeck();
                if (sucsess) {return blackjackdeck;}
            }
        }


        static void Main(string[] args) {
            int playerCount = 0;
            bool selectingplayeramount = true;
            Console.WriteLine("Hello how may players are you: from 1 - 7");

            while (selectingplayeramount){
                string playeroutput = Console.ReadLine() ?? "";
                playerCount = SelectPlayerAmount(playeroutput);
                if(playerCount != 0){
                    selectingplayeramount = false;
                }
            }

            int i = 0;
            Players players = new Players();
            while (playerCount >  i){
                Console.WriteLine($"player {i+1} select write your name");
                string playerName = Console.ReadLine() ?? "";
                Player playerinfo = GeneratePlayer(playerName, i);
                players.Add(playerinfo);
                i ++;
            }
            Console.WriteLine("All players ready");
            Console.WriteLine("now select deck size");

            bool selectingDeckAmount = true;
            while (selectingDeckAmount){
                string deckcount = Console.ReadLine() ?? "";
                int deckcountInt = SelectDeckAmount(deckcount);
                if(deckcountInt != 0){selectingDeckAmount = false;}
            }
            

            string [] basedeck = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
            CreateDeck(1, basedeck);

            players.Playerssetbets();

            //bool GameRunning = true;

            List<Card> dealerhand = new List<Card>();


            /*while (GameRunning) {
                
            }*/
            
        }
        
    }
}








