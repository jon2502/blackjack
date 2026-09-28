using Blackjack;

namespace blackjackTest;
public class ProgramTest {
    [Fact]
    public void Testprinting()
    {
        Program.PrintplayerNumber(0);
        Program.PlaceYourBets("Jane");
        Program.PlaceYourBetFornewHand("Jane");
    }

    [Fact]
    public void CreateTest() {
        Console.SetIn(new StringReader("3\njhon\n\n"));
        BlackJack game = Program.CreateGame();
    }
}