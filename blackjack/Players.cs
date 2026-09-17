
public class Players {
    public  Dictionary<int, Player> playerlist = new Dictionary<int, Player>();

    public void Add(int id, Player player){
        playerlist.Add(id, player);
    }
}