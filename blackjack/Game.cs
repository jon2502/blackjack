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

    public void SetPlayerNames(string[] playernames){
        int length = playernames.Count();
        for (int i = 0; i < length; i++){
            Player player = new Player();
            if (!string.IsNullOrWhiteSpace(playernames[i])){
                player.Name = playernames[i];
            }
            player.PlayerID = i;
            players.Add(player);
        }    }

    public void GetStartingHands(Deck blackjackdeck) {
        int i = 0;
        while(2 > i) {
            foreach(Player player in players){
                Card Playercard = blackjackdeck.DrawCard();
                player.DrawACard(Playercard);
                Console.WriteLine($"{player.Name} is ");

            }
            Card Dealercard = blackjackdeck.DrawCard();
            dealer.DrawACard(Dealercard);
            i++;
        }
    }

    public void Insurrance(){
        bool result = dealer.DealerBlackjack();
        if (result) {
            foreach (Player player in players) {
            player.Bet = 0;
            player.InGame = false;
            player.Retuns = player.Insurance * 2;
            player.Insurance = 0;
        }
        } else {
            foreach (Player player in players) {
                player.Insurance = 0;
            }
        }
        
    }

}