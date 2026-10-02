using System;

namespace Blackjack {
    public class Program {

        public static BlackJack CreateGame(){
            Deck blackjackdeck = new Deck();
            BlackJack game = new BlackJack(blackjackdeck);

            game.SetPlayerCount();
            game.SetPlayerNames();

            return game;
        }

        public static void BlackJackSetup(BlackJack game){
            game.blackjackdeck.SetDecksize();
            game.blackjackdeck.CreateDeck();
            game.SetStartingBets();
            game.GetStartingHands();
        }

        public static void BlackjackEnd(BlackJack game) {
                game.Results();
                game.CanPlayersContinue();
                game.DoPlayersWantContinue();
                game.CheckIfnewPlayersJoin();
                game.CheckIfNewRoundBegins();
        }

        static void Main(string[] args) {
            BlackJack game = CreateGame();

            while (game.Playing == true) {

                BlackJackSetup(game);

                game.DoPlayersWantToSurrender();

                game.DoPlayersWantToSplit();

                game.DoubleDownOption();

                game.CheckInsurrance();

                game.DealerHitLoop();

                game.TurnRotation();
            }
        }
    }
}