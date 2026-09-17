using Blackjack;

namespace blackjackTest;

public class UnitTest1 {
    
    [Theory]
    [InlineData("test")]
    [InlineData("-1")]
    [InlineData("8")]
    public void selectPlayeramountTestIncorrect(string value){
        int result = Program.SelectPlayeramount(value);
        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    [InlineData("6")]
    [InlineData("7")]
    public void selectPlayeramountTestIntCorrect(string value) {
        int result = Program.SelectPlayeramount(value);
        int intValue = Int32.Parse(value);
        Assert.Equal(intValue, result);
    }

    [Fact]
    public void GeneratePlayerTest(){
        Player result = Program.GeneratePlayer("Jhonny", 1);

        Assert.Equal("Jhonny", result.Name);
    }

    [Fact]
    public void GeneratePlayerTesttwo(){
        Player result = Program.GeneratePlayer("", 1);

        Assert.Equal("Jhon Doe", result.Name);
    }

    [Fact]
    public void playerDictionarytest()
    {
        Players players = new Players();
        List<Player> testlist = new List<Player>();

        testlist.Add(Program.GeneratePlayer("Aiden",0));
        testlist.Add(Program.GeneratePlayer("",1));
        testlist.Add(Program.GeneratePlayer("Jennifer",2));

        foreach(Player player in testlist){
            players.Add(player.PlayerNumber, player);
        }
    
        // Then
        Assert.Equal("Aiden", players.playerlist[0].Name);
        Assert.Equal(0, players.playerlist[0].PlayerNumber);

        Assert.Equal("Jhon Doe", players.playerlist[1].Name);
        Assert.Equal(1, players.playerlist[1].PlayerNumber);

        Assert.Equal("Jennifer", players.playerlist[2].Name);
        Assert.Equal(2, players.playerlist[2].PlayerNumber);

    }
}