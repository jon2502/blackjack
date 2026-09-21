using Blackjack;
using Xunit.Abstractions;

namespace blackjackTest;

public class PlayerTest {
    [Theory]
    [InlineData("test")]
    [InlineData("-1")]
    [InlineData("8")]
    public void selectPlayeramountTestIncorrect(string value){
        Game game = new Game();
        bool result = game.SetPlayerCount(value);
        Assert.False(result);
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
        Game game = new Game();
        bool result = game.SetPlayerCount(value);
        Assert.True(result);
    }


    [Fact]
    public void playerListtest()
    {
        Game game = new Game();
         List<string> PlayerNames = new List<string> {"Aiden", "", "Jennifer"};
        
        game.SetPlayerNames(PlayerNames);
    
        Assert.Equal("Aiden", game.players[0].Name);
        Assert.Equal(0, game.players[0].PlayerID);

        Assert.Equal("Jhon Doe", game.players[1].Name);
        Assert.Equal(1, game.players[1].PlayerID);

        Assert.Equal("Jennifer", game.players[2].Name);
        Assert.Equal(2, game.players[2].PlayerID);

    }

    [Fact]
    public void StartingHandTest() {
        Game game = new Game();
         List<string> PlayerNames = new List<string> {"Aiden", "Jennifer"};
        game.SetPlayerNames(PlayerNames);

        Deck testdeck = new Deck();
        testdeck.CreateDeck();

        game.GetStartingHands(testdeck);

        Assert.Equal(46, testdeck.deck.Count);
        Assert.Equal(2, game.players[0].Hand[0].Count);
        Assert.Equal(2, game.players[1].Hand[0].Count);
        Assert.Equal(2, game.dealer.Hand[0].Count);
    }

    [Fact]
    public void SetBetTest() {
        Game game = new Game();
        List<string> PlayerNames = new List<string> {"", "Aiden", "Jane"};
        game.SetPlayerNames(PlayerNames);



        bool JhondoeResult = game.players[0].SetBet("tets");
        bool AideneResult = game.players[1].SetBet("150");
        bool JaneResult = game.players[2].SetBet("20");

        Assert.False(JhondoeResult);
        Assert.False(AideneResult);
        Assert.True(JaneResult);

        Assert.Empty(game.players[0].Bet);
        Assert.Empty(game.players[1].Bet);
        Assert.Equal(20, game.players[2].Bet[0]);

        Assert.Equal(100, game.players[0].Chips);
        Assert.Equal(100, game.players[1].Chips);
        Assert.Equal(80, game.players[2].Chips);
    }

    [Fact]
    public void SetInsuranceTest() {
        Game game = new Game();
        List<string> PlayerNames = new List<string> {"", "Aiden", "Jane", "jack", "Sofia"};
        game.SetPlayerNames(PlayerNames);
        game.players[2].SetBet("40");
        game.players[3].SetBet("20");
        game.players[4].SetBet("100");

        bool JhondoeResult = game.players[0].SetInsurance("tets", 0);
        bool AideneResult = game.players[1].SetInsurance("150", 0);
        bool JaneResult = game.players[2].SetInsurance("20", 0);
        bool JackResult =  game.players[3].SetInsurance("20", 0);
        bool SofiaResult =  game.players[4].SetInsurance("0", 0);



        Assert.False(JhondoeResult);
        Assert.False(AideneResult);
        Assert.True(JaneResult);
        Assert.False(JackResult);
        Assert.True(SofiaResult);

        Assert.Empty(game.players[0].Insurance);
        Assert.Empty(game.players[1].Insurance);
        Assert.Equal(20, game.players[2].Insurance[0]);
        Assert.Empty(game.players[3].Insurance);
        Assert.Equal(0, game.players[4].Insurance[0]);

        Assert.Equal(100, game.players[0].Chips);
        Assert.Equal(100, game.players[1].Chips);
        Assert.Equal(40, game.players[2].Chips);
        Assert.Equal(80, game.players[3].Chips);
        Assert.Equal(0, game.players[4].Chips);


    }

    [Fact]
     public void SurrenderTest(){
        Game game = new Game();
        List<string> PlayerNames = new List<string> {""};
        game.SetPlayerNames(PlayerNames);
        game.players[0].SetBet("100");
        game.players[0].Surrender();

        Assert.Empty(game.players[0].Bet);
        Assert.Empty(game.players[0].Insurance);
        Assert.Equal(50, game.players[0].Chips);
        Assert.False(game.players[0].InGame);

    }
}