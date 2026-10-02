using Blackjack;

namespace blackjackTest;
public class ProgramTest {

    [Fact]
    public void gameTest() {
        Console.SetIn(new StringReader("3\njhon\nJane\nJack\n2\n20\n30\n40"));
        BlackJack game = Program.CreateGame();
        Program.BlackJackSetup(game);
    }

    [Fact]
    public void EndTest(){
        Console.SetIn(new StringReader("3\njhon\nJane\nJack\n2\n20\n30\n40\ny\ny\nn\ny\n2\nJhonny\nJackie"));
        BlackJack game = Program.CreateGame();
        Program.BlackJackSetup(game);
        Program.BlackjackEnd(game);

        Assert.True(game.Playing);
        Assert.Equal(4, game.PlayerCount);

    }
}