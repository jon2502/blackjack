using System;

namespace Blackjack {
    public class Program {
        static void Main(string[] args) {
            Game game = new Game();

            Console.WriteLine("Hello how may players are you: from 1 - 7");
            while (true){
                string input = Console.ReadLine() ?? "";
                bool sucsess = game.SetPlayerCount(input);
                if(sucsess == true){break;}
            }
            
            int i = 0;
            List<string> NameList = new List<string>();
            while (game.PlayerCount >  i){
                Console.WriteLine($"player {i+1} select write your name");
                string playerName = Console.ReadLine() ?? "";
                NameList.Add(playerName);
                i ++;
            }
            game.SetPlayerNames(NameList);

            while (true) {
                Deck blackjackdeck = new Deck();

                Console.WriteLine("select deck size");
                while (true){
                    string deckcount = Console.ReadLine() ?? "";
                    bool sucsess = blackjackdeck.SetDecksize(deckcount);
                    if(sucsess == true){break;}
                }
                blackjackdeck.CreateDeck();
                
                Console.WriteLine($"{game.players[0].Name}");
                foreach (Player player in game.players) {
                    Console.WriteLine($"{player.Name} place your bet");
                    while (true) {
                        string amount = Console.ReadLine() ?? "";
                            bool result = player.SetBet(amount, 0);
                            if(result == true){break;}
                    }
                }

                game.GetStartingHands(blackjackdeck);

                foreach (Player player in game.players) {
                    player.Surrender(0);
                }

                foreach (Player player in game.players) {
                    for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                        while (true) {
                            bool DidSplit = player.Split(HandIndex);
                            if (DidSplit) {
                                Card FirstCard = blackjackdeck.DrawCard();
                                player.DrawACard(FirstCard, HandIndex);

                                Card SecondCard = blackjackdeck.DrawCard();
                                player.DrawACard(SecondCard, HandIndex+1);

                                Console.WriteLine($"{player.Name} place bet for new hand");
                                while (true) {
                                    string amount = Console.ReadLine() ?? "";
                                    bool BetResult = player.SetBet(amount, HandIndex+1);
                                    if(BetResult == true){break;}
                                }
                                player.PlayerBlackjack(HandIndex);
                            }
                            else {break;}
                        }
                    }
                }

                foreach (Player player in game.players) {
                    for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                        bool DidDoubleDown = player.DoubleDown(HandIndex);
                        if (DidDoubleDown) {
                            Card card = blackjackdeck.DrawCard();
                            player.DrawACard(card,HandIndex);
                            player.DisplayHand(HandIndex);
                        }
                    }
                }

                if(game.dealer.Hand[0][0].Rank == "A" && game.dealer.Hand[0].Count == 2){
                    Console.WriteLine($"{game.dealer.Name} has an Ace would you like you place insurance?");
                    foreach (Player player in game.players) {
                        for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                            bool Output = player.DoyouWantInssurance(HandIndex);
                            if (Output) {
                                Console.WriteLine($"{player.Name} How much would you like to place into insurance?");
                                while (true) {
                                    string input = Console.ReadLine() ?? "";
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

                while(game.dealer.Hand[0].Sum(card => card.Value) >= 16) {
                    Card card = blackjackdeck.DrawCard();
                    bool result = game.dealer.Dealerhit(card);
                    if(result == true) {
                        game.Playing = false;
                        break;
                    }
                }

                while (game.Playing == true) {
                    foreach (Player player in game.players) {
                        if (player.InGame == true){
                            Console.WriteLine($"{player.Name}'s turn");
                            for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                            if (!player.Stand[HandIndex]){
                                bool Output = player.StandOrHit(HandIndex);
                                    if (Output) {
                                        Card card = blackjackdeck.DrawCard();
                                        player.DrawACard(card, HandIndex);
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

                /*bool Continue = PrintFinal();
                if (Continue) {
                    //restark
                } else {
                    Console.WriteLine($"The game is over");
                    break;
                }*/
            }
        }
    }
}