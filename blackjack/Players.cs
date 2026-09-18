
public class Players {
    public  List<Player> playerlist = new List<Player>();

    public void Add(Player player){
        playerlist.Add(player);
    }

    public void AllPlayersLose(){
        foreach(Player player in playerlist) {
            player.Bet = 0;
            player.InGame = false;
        }
    }
}