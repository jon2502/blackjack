using System;

namespace Blackjack {
    public class Program {
        public static void PrintplayerNumber(int i){
            Console.WriteLine($"player {i+1} select write your name");
        }

        public static void PlaceYourBets(string name){
            Console.WriteLine($"{name} place your bet");
        }

        public static void PlaceYourBetFornewHand(string name){
            Console.WriteLine($"{name} place bet for new hand");
        }

        public static BlackJack CreateGame(){
            Deck blackjackdeck = new Deck();
            BlackJack game = new BlackJack(blackjackdeck);

            game.SetPlayerCount();
            game.SetPlayerNames();

            return game;
        }

        static void Main(string[] args) {
            BlackJack game = CreateGame();

            while (game.Playing == true) {
                game.blackjackdeck.SetDecksize();
                game.blackjackdeck.CreateDeck();
                
                foreach (Player player in game.players) {
                    PlaceYourBets(player.Name);
                    player.SetBet(0);
                }

                game.GetStartingHands();

                game.DoPlayersWantToSurrender();

                game.DoPlayersWantToSplit();

                game.DoubleDownOption();

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