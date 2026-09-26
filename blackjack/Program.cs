using System;

namespace Blackjack {
    public class Program {

        public static string ReturnString(){
            string input = Console.ReadLine() ?? "";
            return input;
        } 

        public static void PrintplayerNumber(int i){
            Console.WriteLine($"player {i+1} select write your name");
        }

        public static void Printdecksize(){
            Console.WriteLine("select deck size");
        }

        public static void PlaceYourBets(string name){
            Console.WriteLine($"{name} place your bet");
        }

        public static void PlaceYourBetFornewHand(string name){
            Console.WriteLine($"{name} place bet for new hand");
        }

        public static void TheDealerHasAnAce(string dealer) {
            Console.WriteLine($"the {dealer} has an Ace would you like you place insurance?");
        }
        public static void AskingAboutInsurance(string name) {
            Console.WriteLine($"{name} How much would you like to place into insurance?");
        }

        public static void PlayersTurn(string name){
            Console.WriteLine($"{name}'s turn");
        }
        public static void Instructions(){
            Console.WriteLine("Would any new players like to join?");
            Console.WriteLine("y : yes");
            Console.WriteLine("anyother key : no");
        }

        static void Main(string[] args) {
            Deck blackjackdeck = new Deck();
            Game game = new Game(blackjackdeck);

            game.SetPlayerCount();
            
            List<string> NameList = new List<string>();
            for (int i = 0; i < game.PlayerCount; i++) {
                PrintplayerNumber(i);
                string playerName = ReturnString();
                NameList.Add(playerName);
            }

            game.SetPlayerNames(NameList);

            while (game.Playing == true) {
            Printdecksize();
                while (true){
                    string deckcount = ReturnString();
                    bool sucsess = game.blackjackdeck.SetDecksize(deckcount);
                    if(sucsess == true){break;}
                }
                game.blackjackdeck.CreateDeck();
                
                Console.WriteLine($"{game.players[0].Name}");
                foreach (Player player in game.players) {
                    PlaceYourBets(player.Name);
                    while (true) {
                        string amount = ReturnString();
                        bool result = player.SetBet(amount, 0);
                        if(result == true){break;}
                    }
                }

                game.GetStartingHands();

                foreach (Player player in game.players) {
                    player.Surrender(0);
                }

                foreach (Player player in game.players) {
                    for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                        while (true) {
                            bool DidSplit = player.Split(HandIndex);
                            if (DidSplit) {
                                game.MoveCard(player,HandIndex);
                                game.MoveCard(player,HandIndex+1);
                                PlaceYourBetFornewHand(player.Name);
                                while (true) {
                                    string amount = ReturnString();
                                    bool BetResult = player.SetBet(amount, HandIndex+1);
                                    if(BetResult == true){break;}
                                }
                                player.PlayerBlackjack(HandIndex);
                            }
                            else {break;}
                        }
                    }
                }

                game.DoubleDownOption();

                if(game.dealer.Hand[0][0].Rank == "A" && game.dealer.Hand[0].Count == 2){
                    TheDealerHasAnAce(game.dealer.Name);
                    foreach (Player player in game.players) {
                        for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                            bool Output = player.DoyouWantInssurance(HandIndex);
                            if (Output) {
                                AskingAboutInsurance(player.Name);
                                while (true) {
                                    string input = ReturnString();
                                    bool sucsess = player.SetInsurance(input, HandIndex);
                                    if(sucsess){break;}
                                }
                            } else {
                                break;
                            }
                        }
                    }
                }

                game.CheckInsurrance();
                game.DealerHitLoop();

                while (game.Playing == true) {
                    foreach (Player player in game.players) {
                        if (player.InGame == true){
                            PlayersTurn(player.Name);
                            for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                            if (player.Stand[HandIndex] != true){
                                bool Output = player.StandOrHit(HandIndex);
                                    if (Output) {
                                        game.MoveCard(player,HandIndex);
                                    } else {
                                        player.Stand[HandIndex] = true;
                                        player.CheckPlayerState();
                                    }
                                }
                            }
                        }
                    }
                    game.CheckIfGamesOver();
                }

                game.GameCleanup();
                if(game.PlayerCount < 7) {
                    Instructions();
                     string Output1 = ReturnString();
                    if (Output1 == "y" || Output1 == "Y") {
                        game.SetPlayerCount();
                    }
                }
                game.CheckIfNewRoundBegins();
            }
        }
    }
}