public class Player {
    public string Name {get; set;} = "Jhon Doe";
    public int PlayerID {get; set;}

    public bool PlayerState {get; set;} = true;

    public int Chips {get; set;} = 100;

    public int Bet {get; set;} = 0;

    public List<Card> Hand = new List<Card>();
}