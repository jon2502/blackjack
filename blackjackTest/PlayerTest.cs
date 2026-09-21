using Blackjack;
using Xunit.Abstractions;

namespace blackjackTest;

public class PlayerTest {

    [Theory]
    [InlineData("test")]
    [InlineData("-1")]
    [InlineData("8")]
    public void selectPlayeramountTestIncorrect(string value){
        int result = Program.SelectPlayerAmount(value);
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
        int result = Program.SelectPlayerAmount(value);
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
    public void playerListtest()
    {
        Players players = new Players();
        List<Player> testlist = new List<Player>();

        testlist.Add(Program.GeneratePlayer("Aiden",0));
        testlist.Add(Program.GeneratePlayer("",1));
        testlist.Add(Program.GeneratePlayer("Jennifer",2));
        

        foreach(Player player in testlist){
            players.Add(player);
        }
    
        Assert.Equal("Aiden", players.playerlist[0].Name);
        Assert.Equal(0, players.playerlist[0].PlayerID);

        Assert.Equal("Jhon Doe", players.playerlist[1].Name);
        Assert.Equal(1, players.playerlist[1].PlayerID);

        Assert.Equal("Jennifer", players.playerlist[2].Name);
        Assert.Equal(2, players.playerlist[2].PlayerID);

    }

    [Fact]
    public void StartingHandTest() {
        Players players = new Players();
        List<Player> testlist = new List<Player>();

        testlist.Add(Program.GeneratePlayer("Aiden",0));
        testlist.Add(Program.GeneratePlayer("",1));

        foreach(Player player in testlist){
            players.Add(player);
        }

        Deck testdeck = new Deck();
        bool result = testdeck.CreateDeck();
        Dealer dealer = new Dealer();

        Program.GetStartingHands(players, dealer, testdeck);

        Assert.Equal(46, testdeck.deck.Count);
        Assert.Equal(2, players.playerlist[0].Hand.Count);
        Assert.Equal(2, players.playerlist[1].Hand.Count);
        Assert.Equal(2, dealer.Hand.Count);
    }

    [Fact]
    public void BetTest() {
        Player JhoneDoe = new Player();
        Player Aiden = new Player();
        Player Jane = new Player();
        
        JhoneDoe.PlayerID = 0;
        
        Aiden.PlayerID = 1;
        Aiden.Name = "Aiden";

        Jane.PlayerID = 2;
        Jane.Name = "Jane";

        bool JhondoeResult = JhoneDoe.SetBet("tets");
        bool AideneResult = Aiden.SetBet("150");
        bool JaneResult = Jane.SetBet("20");

        Assert.False(JhondoeResult);
        Assert.False(AideneResult);
        Assert.True(JaneResult);

        Assert.Equal(0,JhoneDoe.Bet);
        Assert.Equal(0,Aiden.Bet);
        Assert.Equal(20,Jane.Bet);

        Assert.Equal(100,JhoneDoe.Chips);
        Assert.Equal(100,Aiden.Chips);
        Assert.Equal(80,Jane.Chips);
    }

    [Fact]
    public void InsuranceTest() {
        Player JhoneDoe = new Player();
        Player Aiden = new Player();
        Player Jane = new Player();
        
        JhoneDoe.PlayerID = 0;
        
        Aiden.PlayerID = 1;
        Aiden.Name = "Aiden";

        Jane.PlayerID = 2;
        Jane.Name = "Jane";

        bool JhondoeResult = JhoneDoe.SetInsurance("tets");
        bool AideneResult = Aiden.SetInsurance("150");
        bool JaneResult = Jane.SetInsurance("20");

        Assert.False(JhondoeResult);
        Assert.False(AideneResult);
        Assert.True(JaneResult);

        Assert.Equal(0,JhoneDoe.Insurance);
        Assert.Equal(0,Aiden.Insurance);
        Assert.Equal(20,Jane.Insurance);

        Assert.Equal(100,JhoneDoe.Chips);
        Assert.Equal(100,Aiden.Chips);
        Assert.Equal(80,Jane.Chips);
    }

    [Fact]
    public void AllPlayersloseTest() {
        
    }

}