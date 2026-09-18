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
                bool sucsess = blackjackdeck.CreateDeck();
                if (sucsess) {
                    return blackjackdeck;
                    }
            }
        }

        public static void GetStartingHands(Players players, Deck blackjackdeck){
            int i = 0;
            while(2 >= i) {
                foreach(Player player in players.playerlist){
                    Card card = blackjackdeck.DrawCard();
                    player.DrawACard(card);
                    i++;
                }
            }
        }

        public static Players Setup() {
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
                return players;
        }

        /*public bool TurnSequence() {
            return true;
        }

        static public int testfuntion() {
         
        }*/

        static void Main(string[] args) {
            Players players = Setup();

            bool playing = true;
            while (playing) {
                Console.WriteLine("select deck size");

                bool selectingDeckAmount = true;
                while (selectingDeckAmount){
                    string deckcount = Console.ReadLine() ?? "";
                    int deckcountInt = SelectDeckAmount(deckcount);
                    if(deckcountInt != 0){selectingDeckAmount = false;}
                }
                
                string [] basedeck = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
                Deck blackjackdeck = CreateDeck(1, basedeck);

                players.Playerssetbets();

                //bool GameRunning = true;

                List<Card> dealerhand = new List<Card>();
                GetStartingHands(players, blackjackdeck);

                //TurnSequence();

                Console.WriteLine("game is over");
                Console.WriteLine("play another round: y");
                Console.WriteLine("stop playing: n");
            }
        }
        
    }
}








