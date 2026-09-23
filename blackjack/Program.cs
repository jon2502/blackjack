using System;

namespace Blackjack {
    public class Program {

        static bool RetrunValue () {
            Console.WriteLine("y : yes");
            Console.WriteLine("anyother key : continue");
            string Output = Console.ReadLine() ?? "";
            if (Output == "y") {
                return true;
            } return false;
        }
        static void Main(string[] args) {
            Game game = new Game();

            Console.WriteLine("Hello how may players are you: from 1 - 7");
            while (true){
                string playeroutput = Console.ReadLine() ?? "";
                bool sucsess = game.SetPlayerCount(playeroutput);
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
            Console.WriteLine($"Namelist is {NameList.Count()} long");
            game.SetPlayerNames(NameList);
            //bool playing = true;

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
            Console.WriteLine("finished");

            game.GetStartingHands(blackjackdeck);

            foreach (Player player in game.players) {
                Console.WriteLine($"{player.Name} would you like to surrender and get half your bet back");
                for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                    while (true) {
                        bool result = player.Splitcheck(player.Hand[HandIndex]);
                        if(result) {
                            if(player.Hand[HandIndex].FindAll(card => card.Rank == "A").Count == 2) {
                                Console.WriteLine($"{player.Name} you have two aces so your hand will be split");
                                player.Split(HandIndex);
                            } else {
                                
                                Console.WriteLine($"{player.Name} you have a {player.Hand[HandIndex][0].Suit}{player.Hand[HandIndex][0].Rank} and {player.Hand[HandIndex][1].Suit}{player.Hand[HandIndex][1].Rank}");
                                Console.WriteLine($"You may split them if you choce");
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

            if(game.dealer.Hand[0][0].Rank == "A" && game.dealer.Hand[0].Count == 2){
                Console.WriteLine($"{game.dealer.Name} has an Ace would you like you place insurance?");
                foreach (Player player in game.players) {
                    for (int handcount = 0; handcount < player.Hand.Count; handcount++) {
                        if(player.Hand.Count > 1)
                        {
                            Console.WriteLine($"{player.Name} would you like to place insurance on hand {handcount} with a bet of {player.Bet[i]}");
                        } else {
                            Console.WriteLine($"{player.Name} would you like to place insurance");
                        }
                        bool Output = RetrunValue();
                        if (Output) {
                            Console.WriteLine($"{player.Name} How much would you like to place into insurance?");
                            while (true) {
                                string input = Console.ReadLine() ?? "";
                                bool sucsess = player.SetInsurance(input, handcount);
                                if(sucsess){break;}
                            }
                        } else {
                            break;
                        }
                    }
                }
            }
            //continue
        }
    }
}