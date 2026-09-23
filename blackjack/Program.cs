using System;

namespace Blackjack {
    public class Program {

        public static bool RetrunValue () {
            Console.WriteLine("y : yes");
            Console.WriteLine("anyother key : continue");
            string Output = Console.ReadLine() ?? "";
            if (Output == "y" || Output== "Y") {
                return true;
            } return false;
        }

        static void DisplayHand(List<Card> cards){
            foreach(Card card in cards){
                Console.WriteLine($"{card.Suit}{card.Rank}");
            }
        }

        static void Main(string[] args) {
            Game game = new Game();

            Console.WriteLine("Hello how may players are you: from 1 - 7");
            while (true){
                bool sucsess = game.SetPlayerCount();
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
                Console.WriteLine($"{player.Name} would you like to surrender and get half your bet back. youyr current hand is");
                DisplayHand(player.Hand[0]);
                bool Output = RetrunValue();
                if (Output) {
                    player.Surrender();
                } else {
                    break;
                }
            }
            foreach (Player player in game.players) {
                for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                    while (true) {
                        bool result = player.Splitcheck(HandIndex);
                        if(result) {
                            if(player.Hand[HandIndex].FindAll(card => card.Rank == "A").Count == 2) {
                                Console.WriteLine($"{player.Name} you have two aces so your hand will be split");
                                player.Split(HandIndex);
                            } else {
                                Console.WriteLine($"{player.Name} You have cards with the same value and may split them if you choce");
                                DisplayHand(player.Hand[HandIndex]);
                                bool Output = RetrunValue();
                                if (Output) {
                                    player.Split(HandIndex);
                                } else {
                                    break;
                                }
                            }
                            Card FirstCard = blackjackdeck.DrawCard();
                            player.DrawACard(FirstCard, HandIndex);

                            Card SecondCard = blackjackdeck.DrawCard();
                            player.DrawACard(SecondCard, HandIndex+1);

                            Console.WriteLine($"{player.Name} place bet for new hand");
                            while (true) {
                                string amount = Console.ReadLine() ?? "";
                                    player.Bet.Add(0);
                                    bool BetResult = player.SetBet(amount, HandIndex+1);
                                    if(BetResult == true){break;}
                            }
                            player.PlayerBlackjack(HandIndex);
                        } else{
                            break;        
                        }
                    }
                }
            }

            foreach (Player player in game.players) {
                for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                    Console.WriteLine($"{player.Name} Would you like to doubledown for your hand of?");
                    DisplayHand(player.Hand[HandIndex]);
                    bool Output = RetrunValue();
                    if (Output) {
                        if(player.Bet[HandIndex] > player.Chips){
                            Console.WriteLine($"{player.Name} balance to low you cant Double down");
                        } else {
                            Card card = blackjackdeck.DrawCard();
                            player.DoubleDown(HandIndex, card);
                        }
                    } else {
                        break;
                    }
                }
            }

            if(game.dealer.Hand[0][0].Rank == "A" && game.dealer.Hand[0].Count == 2){
                Console.WriteLine($"{game.dealer.Name} has an Ace would you like you place insurance?");
                foreach (Player player in game.players) {
                    for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                        Console.WriteLine($"{player.Name} would you like to place insurance on your hand of");
                        DisplayHand(player.Hand[HandIndex]);
                        Console.WriteLine($"with a bet of {player.Bet[HandIndex]}");
                        
                        bool Output = RetrunValue();
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

            game.dealer.DealerBlackjack();

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
                        Console.WriteLine("running");
                        Console.WriteLine($"{player.Name}'s turn");
                        for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                        if (!player.Stand[HandIndex]){
                                Console.WriteLine($"{player.Name} would you like to Hit or stand for your hand of");
                                DisplayHand(player.Hand[HandIndex]);
                                Console.WriteLine($"with a bet of {player.Bet[HandIndex]}");
                                bool Output = RetrunValue();
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
            

        }
    }
}