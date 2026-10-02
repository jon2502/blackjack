using System;

public class BlackJack:Game {
    public Dealer dealer = new Dealer {
        Name = "Dealer",
    };
    public Deck blackjackdeck;

    public BlackJack(Deck deck) : base (1,7)
    {
        blackjackdeck = deck;
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

    public void SetStartingBets(){
        foreach (Player player in players) {
            Console.WriteLine($"{player.Name} place your bet");
            player.SetBet(0);
        }

    }

    public void DoPlayersWantToSurrender(){
        foreach (Player player in players) {
            player.Surrender(0);
        }
    }

    public void DoPlayersWantToSplit(){
        foreach (Player player in players) {
            int HandIndex = 0;
            while (HandIndex < player.Hand.Count) {  
                bool DidSplit = player.Split(HandIndex);
                if (DidSplit) {
                    MoveCard(player,HandIndex);
                    MoveCard(player, player.Hand.Count - 1);
                    Console.WriteLine($"{player.Name} place bet for new hand");
                    player.SetBet(player.Hand.Count - 1);
    
                    player.PlayerBlackjack(HandIndex);
                    player.PlayerBlackjack(player.Hand.Count - 1);

                } else {HandIndex++;}
            }
        }
    }

    public void DoubleDownOption(){
        foreach (Player player in players) {
            for (int i = 0; i < player.Hand.Count; i++) {
                bool DidDoubleDown = player.DoubleDown(i);
                if (DidDoubleDown) {
                    MoveCard(player, i);
                    player.DisplayHand(i);
                    player.BustCheck(i);
                    player.CheckPlayerState();
                }
            }
        }
    }

    public void CheckInsurrance(){
        bool check = dealer.DoesTheDealerHaveAnAce();
        if (check) {
            foreach (Player player in players) {
                player.SetInsurance();
            }
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

    public void TurnRotation(){
         while (Playing == true) {
            foreach (Player player in players) {
                if (player.InGame == true){
                    Console.WriteLine($"{player.Name}'s turn");
                    for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                    if (player.Stand[HandIndex] != true){
                        bool Output = player.StandOrHit(HandIndex);
                            if (Output) {
                                MoveCard(player,HandIndex);
                            } else {
                                player.Stand[HandIndex] = true;
                                player.CheckPlayerState();
                            }
                        }
                    }
                }
            }
            CheckIfGamesOver();
        }
    }

    public void CheckIfGamesOver() {
        bool check = players.All(player => player.InGame == false);
        if (check) {
            Console.WriteLine("No active players left now ending game");
            Playing = false;
        }
    }

    public void Results(){
        int DealerHandValue = dealer.Hand[0].Sum(card => card.Value);
        for (int playerIndex = 0; playerIndex < players.Count; playerIndex++){
            for (int HandIndex = 0; HandIndex < players[playerIndex].Hand.Count; HandIndex++) {
                int handValue = players[playerIndex].Hand[HandIndex].Sum(card => card.Value);
                if(handValue > DealerHandValue) {
                    players[playerIndex].Retuns += players[playerIndex].Bet[HandIndex];
                } else if (handValue < DealerHandValue) {
                    players[playerIndex].Bet[HandIndex] = 0;
                }
            }
            players[playerIndex].Chips += players[playerIndex].Retuns;
            for (int i = 0; i < players[playerIndex].Bet.Count; i++) {
                players[playerIndex].Chips += players[playerIndex].Bet[i];
            }
            if (players[playerIndex].Retuns < players[playerIndex].Bet.Sum(bet => bet)) {
                Console.WriteLine($"{players[playerIndex].Name} has {players[playerIndex].Retuns} returns and has lost {players[playerIndex].Retuns -  players[playerIndex].Bet.Sum(bet => bet)}");
            } else {
                Console.WriteLine($"{players[playerIndex].Name} has {players[playerIndex].Retuns} returns");
            }         

            players[playerIndex].Retuns = 0;
        }
    }

     public void CanPlayersContinue() {
        for (int playerIndex = 0; playerIndex < players.Count; playerIndex++){
            bool canplay = players[playerIndex].Canplay();
            if (!canplay) {
                 Console.WriteLine($"{players[playerIndex].Name} is out of chips and cant continue");
                players.RemoveAt(playerIndex);
                PlayerCount =- 1;
                playerIndex --;

            }
        }
    }
    public void DoPlayersWantContinue(){
        for (int playerIndex = 0; playerIndex < players.Count; playerIndex++) {
            bool WantToContinue = players[playerIndex].WantToContinue();
            if (WantToContinue == false) {
                Console.WriteLine($"{players[playerIndex].Name} has left the table with {players[playerIndex].Chips} chips");
                players.RemoveAt(playerIndex);
                PlayerCount -= 1;
                playerIndex --;
            }
        }
    }

    public void CheckIfnewPlayersJoin() {
        if(PlayerCount < 7) {
            Console.WriteLine("Would any new players like to join?");
            Console.WriteLine("y : yes");
            Console.WriteLine("anyother key : no");
            string input = Console.ReadLine() ?? "";
            if (input == "y" || input == "Y") {
                SetPlayerCount();
                SetPlayerNames();
            }
        }
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
            Console.WriteLine($"card Drawn: {card.Suit}{card.Rank}");
            participant.DrawACard(card, Hand);
        }
}