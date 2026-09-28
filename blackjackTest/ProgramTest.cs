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
    public void ReturnStringTest() {
        Console.SetIn(new StringReader("Jhon"));
        string Output = Program.ReturnString();
        Assert.Equal("Jhon", Output);
    }

    [Fact]
    public void CreateTest() {
        Console.SetIn(new StringReader("3\njhon\n\n"));
        BlackJack game = Program.CreateGame();
    }
}