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
        Program.PlayersTurn("jane");
        Program.Instructions();
    }
}