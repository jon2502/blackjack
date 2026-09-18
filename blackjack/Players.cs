
public class Players {
    public  List<Player> playerlist = new List<Player>();

    public void Add(Player player){
        playerlist.Add(player);
    }

    public void Playerssetbets(){
        foreach (Player player in playerlist) {
            Console.WriteLine($"{player.Name} place your bet");
            bool betting = true;
            while (betting) {
                string amount = Console.ReadLine() ?? "";
                try {
                    int Intamount = Int32.Parse(amount);
                    if (Intamount > player.Chips) {
                        Console.WriteLine($"{player.Name} balance to low");
                    } else {
                        player.Chips -= Intamount;
                        player.Bet += Intamount;
                        betting = false;
                    }
                } catch {
                    Console.WriteLine($"{player.Name} please select a valid full number");
                }
            }
        }
    }
}