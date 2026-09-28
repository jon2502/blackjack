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

        public static void AskingAboutInsurance(string name) {
            Console.WriteLine($"{name} How much would you like to place into insurance?");
        }

        static void Main(string[] args) {
            Deck blackjackdeck = new Deck();
            Game game = new Game(blackjackdeck);

            game.SetPlayerCount();
            game.SetPlayerNames();

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
                    player.SetBet(0);
                }

                game.GetStartingHands();

                game.DoPlayersWantToSurrender();

                game.DoPlayersWantToSplit();

                game.DoubleDownOption();

                bool check = game.dealer.DoesTheDealerHaveAnAce();
                if(check == true){
                    foreach (Player player in game.players) {
                        for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                            bool Output = player.DoyouWantInssurance(HandIndex);
                            if (Output) {
                                AskingAboutInsurance(player.Name);
                                player.SetInsurance(HandIndex);
                                } else {
                                    break;
                            }
                        }
                    }  
                }

                game.CheckInsurrance();
                game.DealerHitLoop();

                game.TurnRotation();

                game.Results();
                game.CanPlayersContinue();
                game.DoPlayersWantContinue();
                game.CheckIfnewPlayersJoin();
                game.CheckIfNewRoundBegins();
            }
        }
    }
}