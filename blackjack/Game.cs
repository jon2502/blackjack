using System;

public class Game {
    public List<Player> players = new List<Player>();

    public int PlayerCount {get; set;} = 0;

    public bool Playing {get; set;} = true;

    public Dealer dealer = new Dealer {
        Name = "Dealer",
    };
    
    public bool SetPlayerCount(string input){
        try {
            int output = Int32.Parse(input);
            Console.WriteLine(output);
            if( output >= 1 && output <= 7){
                PlayerCount = output;
                return true;
            } else {
                Console.WriteLine("Please select a number between 1 and 7");
                return false;
            }
        } catch {
            Console.WriteLine("Please select a number between 1 and 7");
            return false;
        }
    }

    public void SetPlayerNames(List<string> playernames){
        int length = playernames.Count();
        Console.WriteLine(length);
        for (int i = 0; i < length; i++){
            Player player = new Player();
            if (!string.IsNullOrWhiteSpace(playernames[i])){
                player.Name = playernames[i];
            }
            player.PlayerID = i;
            players.Add(player);
            Console.WriteLine(player.Name);
        }
        }

    public void GetStartingHands(Deck blackjackdeck) {
        int i = 0;
        while(2 > i) {
            foreach(Player player in players){
                Card Playercard = blackjackdeck.DrawCard();
                player.DrawACard(Playercard, 0);
            }
            Card Dealercard = blackjackdeck.DrawCard();
            dealer.DrawACard(Dealercard, 0);
            i++;
        }
        foreach (Player player in players) {
            bool result = player.PlayerBlackjack(0);
            if (!result){
                Console.WriteLine($"{player.Name}'s hand is {player.Hand[0][0].Suit}{player.Hand[0][0].Rank} and {player.Hand[0][1].Suit}{player.Hand[0][1].Rank}");
            }
        }
        Console.WriteLine($"the {dealer.Name} has a {dealer.Hand[0][0].Suit}{dealer.Hand[0][0].Rank}");

    }

    public void CheckInsurrance(){
        bool result = dealer.DealerBlackjack();
        if (result) {
            foreach (Player player in players) {
                player.Bet.Clear();
                player.InGame = false;
                for (int i = 0; i < player.Insurance.Count; i++)
                {
                    player.Retuns += player.Insurance[i] * 2;
                }
                player.Insurance.Clear();
                player.Hand.Clear();
                player.CheckPlayerState();
                }
        } else {
            foreach (Player player in players) {
                player.Insurance.Clear();
            }
        }
    }

    public void CheckIfGamesOver() {
        bool check = players.All(player => player.InGame == false);
        if (check) {
            Playing = false;
        }
    }

}