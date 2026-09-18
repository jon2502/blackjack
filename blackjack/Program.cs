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

        public static bool SelectDeckAmount(string input, Deck blackjackdeck) {
             try {
                int DeckCount = Int32.Parse(input);
                if( DeckCount >= 1 && DeckCount <= 8){
                    blackjackdeck.Decksize = DeckCount;
                    return true;
                } else {
                    Console.WriteLine("Please select a number between 1 and 8");
                    return false;
                }
            } catch {
                Console.WriteLine("Please select a number between 1 and 8");
                return false;
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

        public static void GetStartingHands(Players players, Dealer dealer ,Deck blackjackdeck){
            int i = 0;
            while(2 > i) {
                foreach(Player player in players.playerlist){
                    Card Playercard = blackjackdeck.DrawCard();
                    player.DrawACard(Playercard);
                    Console.WriteLine($"{player.Name} is ");

                }
                Card Dealercard = blackjackdeck.DrawCard();
                dealer.DrawACard(Dealercard);
                dealer.DealerCheck(players);
                i++;
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

        public static Deck Generating(Deck blackjackdeck) {
            while (true) {
                bool result = blackjackdeck.CreateDeck();
                if(result == true){return blackjackdeck;}
            }
        }


        static void Main(string[] args) {
            Players players = Setup();

            //bool playing = true;

            Console.WriteLine("select deck size");
            Deck blackjackdeck = new Deck();
            bool selectingDeckAmount = true;
            while (selectingDeckAmount){
                string deckcount = Console.ReadLine() ?? "";
                bool sucsess = SelectDeckAmount(deckcount,blackjackdeck);
                if(sucsess == true){selectingDeckAmount = false;}
            }

            blackjackdeck = Generating(blackjackdeck);
            
            Dealer dealer = new Dealer {
                Name = "Dealer",
            };
            
            foreach (Player player in players.playerlist) {
                Console.WriteLine($"{player.Name} place your bet");
                while (true) {
                    string amount = Console.ReadLine() ?? "";
                        bool result = player.SetBet(amount);
                        if(result == true){break;}

                }
            }
            //bool GameRunning = true;
            GetStartingHands(players, dealer, blackjackdeck);
            foreach (Player player in players.playerlist) {
                player.Blackjack();
            }

            //TurnSequence();

            //Console.WriteLine("game is over");
            //Console.WriteLine("play another round: y");
            //Console.WriteLine("stop playing: n");
                
            
        }
    }
}








