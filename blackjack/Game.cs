using System;

public class Game {
    public List<Player> players = new List<Player>();

    public int PlayerCount {get; set;} = 0;

    public bool Playing {get; set;} = true;

    public Dealer dealer = new Dealer {
        Name = "Dealer",
    };
    public Deck blackjackdeck;
    public Game(Deck deck)
    {
        blackjackdeck = deck;
    }
    public bool SetPlayerCount(){
        Console.WriteLine($"Hello how may players are you: from 1 - {7-PlayerCount}");
        while (true){
            string input = Console.ReadLine() ?? "";
            try {
                int output = Int32.Parse(input);
                if(output >= 1 && output <= 7-PlayerCount){
                    PlayerCount = output;
                    return true;
                } else {
                    Console.WriteLine($"Please select a number between 1 and {7-PlayerCount}");
                    return false;
                }
            } catch {
                Console.WriteLine($"Please select a number between 1 and {7-PlayerCount}");
                return false;
            }
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
            players.Add(player);
            Console.WriteLine(player.Name);
        }
    }

    public void GetStartingHands() {
        int i = 0;
        while(2 > i) {
            foreach(Player player in players){
                MoveCard(player,0);
            }
            MoveCard(dealer,0);
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

    public void DoubleDownOption(){
        foreach (Player player in players) {
            for (int i = 0; i < player.Hand.Count; i++) {
                bool DidDoubleDown = player.DoubleDown(i);
                if (DidDoubleDown) {
                    MoveCard(player, i);
                    player.DisplayHand(i);
                    player.CheckPlayerState();
                }
            }
        }
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

    public void DealerHitLoop(){
        while(dealer.Hand[0].Sum(card => card.Value) <= 16) {
            MoveCard(dealer, 0);
            bool result = dealer.Dealerhit();
            if(result == true) {
                Playing = false;
                break;
            }
        }
    }

    public void CheckIfGamesOver() {
        bool check = players.All(player => player.InGame == false);
        if (check) {
            Playing = false;
        }
    }

    public void GameCleanup(){
        int DealerHandValue = dealer.Hand[0].Sum(card => card.Value);
        for (int playerIndex = 0; playerIndex < players.Count; playerIndex++){
            for (int HandIndex = 0; HandIndex < players[playerIndex].Hand.Count; HandIndex++) {
                int handValue = players[playerIndex].Hand[HandIndex].Sum(card => card.Value);
                if(handValue > DealerHandValue) {
                    players[playerIndex].Retuns += players[playerIndex].Bet[HandIndex];
                } else if (handValue < DealerHandValue) {
                    players[playerIndex].Bet[HandIndex] = 0;
                } else {
                    players[playerIndex].Retuns += players[playerIndex].Bet[HandIndex];
                };
            }
            players[playerIndex].Chips += players[playerIndex].Retuns;
            players[playerIndex].Retuns = 0;
            bool canplay = players[playerIndex].Canplay();
            if (!canplay) {
                 Console.WriteLine($"{players[playerIndex].Name} is out of chips and cant continue");
                players.RemoveAt(playerIndex);
                playerIndex --;
            }
            bool WantToContinue = players[playerIndex].WantToContinue();
            if (!WantToContinue) {
                Console.WriteLine($"{players[playerIndex].Name} has left the table with {players[playerIndex].Chips} chips");
                players.RemoveAt(playerIndex);
                playerIndex --;
            }
        }
        PlayerCount = players.Count;
    }

  public void CheckIfNewRoundBegins() {
        if(players.Count > 0) {
            foreach(Player player in players) {
                player.Bet = [0];
                player.Insurance = [0];
                player.Stand = [false];
                player.Hand = [[]];
            }
            blackjackdeck.deck= [];
            Playing = true;
        } else {
            Playing = false;
             Console.WriteLine($"no players at the table: game over");
        }
    }

    public void MoveCard(Participant participant, int Hand){
        Card card = blackjackdeck.DrawCard();
        participant.DrawACard(card, Hand);
    }
}