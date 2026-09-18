public class Participants {
    public string Name {get; set;} = "Jhon Doe";

    public bool State {get; set;} = true;

    public List<Card> Hand = new List<Card>();

    public void DrawACard(Card card){
       Hand.Add(card); 
    }
}