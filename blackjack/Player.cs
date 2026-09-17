public class Player {
    public string Name {get; set;} = "Jhon Doe";
    public int PlayerNumber {get; set;}

    public bool PlayerState {get; set;} = true;

    public int Chips {get; set;} = 100;

        public int bet {get; set;} = 0;

    public List<Card> hand = new List<Card>();
}