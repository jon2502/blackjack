public class Game {
    public List<Player> players = new List<Player>();

    public int MinPlayers{get; set;}

    public int MaxPlayers{get; set;}
    
    public Game(int max, int min)
    {
        MaxPlayers = max;
        MinPlayers = min;
    }

    public int PlayerCount {get; set;} = 0;

    public bool Playing {get; set;} = true;

    public void SetPlayerCount(){
        bool Setting = true;
        Console.WriteLine($"Hello how may players are you: from {MinPlayers} - {MaxPlayers-PlayerCount}");
        while (Setting){
            string input = Console.ReadLine() ?? "";
            try {
                int output = Int32.Parse(input);
                if(output >= 1 && output <= 7-PlayerCount){
                    PlayerCount += output;
                    Setting = false;
                } else {
                    Console.WriteLine($"Please select a number between {MinPlayers} - {MaxPlayers-PlayerCount}");
                }
            } catch {
                Console.WriteLine($"Please select a number between {MinPlayers} - {MaxPlayers-PlayerCount}");
            }
        }
    }

    public void SetPlayerNames(){
        for (int i = players.Count; i < PlayerCount; i++) {
            Player player = new Player();
            Console.WriteLine($"player {i+1}: write your name");

            string playername = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(playername)){
                player.Name = playername;
            }
            players.Add(player);
        }
    }
}