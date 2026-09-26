using Blackjack;

namespace blackjackTest;
public class ProgramTest {
    [Fact]
    public void Testprinting()
    {
        Program.PrintplayerNumber(0);
        Program.Printdecksize();
        Program.PlaceYourBets("Jane");
        Program.PlaceYourBetFornewHand("Jane");
        Program.TheDealerHasAnAce("dealer");
        Program.AskingAboutInsurance("Jane");
        Program.PlayersTurn("Jane");
        Program.Instructions();
    }

    [Fact]
    public void ReturnStringTest() {
        Console.SetIn(new StringReader("Jhon"));
        string Output = Program.ReturnString();
        Assert.Equal("Jhon", Output);
    }
}