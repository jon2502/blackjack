using Blackjack;
using Xunit;

namespace blackjackTest;

public class PlayerTest {
    [Theory]
    [InlineData("test")]
    [InlineData("-1")]
    [InlineData("10")]
    public void SelectPlayeramountTestIncorrect(string value){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        bool result = game.SetPlayerCount();
        Console.SetIn(new StringReader(value));
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
    public void SelectPlayeramountTestIntCorrect(string value) {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader(value));
        bool result = game.SetPlayerCount();
        Assert.True(result);
    }


    [Fact]
    public void PlayerListtest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\n\nJennifer"));
        game.PlayerCount = 3;
        game.SetPlayerNames();
    
        Assert.Equal("Aiden", game.players[0].Name);

        Assert.Equal("Jhon Doe", game.players[1].Name);

        Assert.Equal("Jennifer", game.players[2].Name);

    }

    [Fact]
    public void StartingHandTest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount = 2;
        game.SetPlayerNames();

        
        game.blackjackdeck.CreateDeck();

        game.GetStartingHands();

        Assert.Equal(46, testdeck.deck.Count);
        Assert.Equal(2, game.players[0].Hand[0].Count);
        Assert.Equal(2, game.players[1].Hand[0].Count);
        Assert.Equal(2, game.dealer.Hand[0].Count);
    }

    [Fact]
    public void SetBetTest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("\nAiden\nJane\nJennifer"));
        game.PlayerCount = 4;
        game.SetPlayerNames();

        bool JhondoeResult = game.players[0].SetBet("tets",0);
        bool AideneResult = game.players[1].SetBet("150",0);
        bool JaneResult = game.players[2].SetBet("20",0);
        bool jenniferResult = game.players[3].SetBet("-1",0);
        Assert.False(JhondoeResult);
        Assert.False(AideneResult);
        Assert.True(JaneResult);
        Assert.False(jenniferResult);

        Assert.Equal(0, game.players[0].Bet[0]);
        Assert.Equal(0, game.players[1].Bet[0]);
        Assert.Equal(20, game.players[2].Bet[0]);
        Assert.Equal(0, game.players[3].Bet[0]);

        Assert.Equal(100, game.players[0].Chips);
        Assert.Equal(100, game.players[1].Chips);
        Assert.Equal(80, game.players[2].Chips);
        Assert.Equal(100, game.players[3].Chips);
    }

    [Fact]
    public void SetInsuranceTest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("\nAiden\nJane\njack\nSofia"));
        game.PlayerCount = 5;

        game.SetPlayerNames();
        game.players[2].SetBet("40",0);
        game.players[3].SetBet("20",0);
        game.players[4].SetBet("100",0);
        for (int i = 0; i < game.players.Count; i++) {
            Console.SetIn(new StringReader("y"));
            bool result = game.players[i].DoyouWantInssurance(0);
            Assert.True(result);
        }
        
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

        Assert.Equal(0, game.players[0].Insurance[0]);
        Assert.Equal(0, game.players[1].Insurance[0]);
        Assert.Equal(20, game.players[2].Insurance[0]);
        Assert.Equal(0, game.players[3].Insurance[0]);
        Assert.Equal(0, game.players[4].Insurance[0]);

        Assert.Equal(100, game.players[0].Chips);
        Assert.Equal(100, game.players[1].Chips);
        Assert.Equal(40, game.players[2].Chips);
        Assert.Equal(80, game.players[3].Chips);
        Assert.Equal(0, game.players[4].Chips);
    }

    [Fact]
     public void NoInsurranceTest(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("\nAiden\nJane"));
        game.PlayerCount = 3;
        game.SetPlayerNames();
        game.players[0].SetBet("40",0);
        game.players[1].SetBet("20",0);
        game.players[2].SetBet("100",0);;
        for (int i = 0; i < game.players.Count; i++) {
            Console.SetIn(new StringReader("n"));
            bool result = game.players[i].DoyouWantInssurance(0);
            Assert.False(result);
        }

        Assert.Equal(0, game.players[0].Insurance[0]);
        Assert.Equal(0, game.players[1].Insurance[0]);
        Assert.Equal(0, game.players[2].Insurance[0]);

        Assert.Equal(60, game.players[0].Chips);
        Assert.Equal(80, game.players[1].Chips);
        Assert.Equal(0, game.players[2].Chips);
    }

    [Fact]
     public void SurrenderTest(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader(""));
        game.PlayerCount = 1;

        game.SetPlayerNames();

        Console.SetIn(new StringReader("y"));
        game.players[0].SetBet("100",0);
        game.players[0].Surrender(0);

        Assert.Empty(game.players[0].Bet);
        Assert.Empty(game.players[0].Insurance);
        Assert.Equal(50, game.players[0].Chips);
        Assert.False(game.players[0].InGame);

    }

    [Fact]
    public void SplitTest(){
        
        Card ace = new Card {
            Suit = "♥",
            Rank = "A",
            Value = 11,
        };
        Card two = new Card {
            Suit = "♠",
            Rank = "2",
            Value = 2,
        };

        Card three = new Card {
            Suit = "♥",
            Rank = "3",
            Value = 3,
        };

        Card queen = new Card {
            Suit = "♠",
            Rank = "Q",
            Value = 10,
        };
        Card five = new Card {
            Suit = "♥",
            Rank = "5",
            Value = 5,
        };
        Card king = new Card {
            Suit = "♠",
            Rank = "K",
            Value = 10,
        };

        Deck testdeck = new Deck();

        testdeck.deck.Add(ace);
        testdeck.deck.Add(two);
        testdeck.deck.Add(three);
        testdeck.deck.Add(five);
        testdeck.deck.Add(five);
        testdeck.deck.Add(five);
        testdeck.deck.Add(five);
        testdeck.deck.Add(five);
        testdeck.deck.Add(five);
        testdeck.deck.Add(five);
        testdeck.deck.Add(five);

        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\n\nJennifer"));
        game.PlayerCount = 4;

        game.SetPlayerNames();

        game.players[0].Hand[0].Add(ace);
        game.players[0].Hand[0].Add(ace);

        game.players[1].Hand[0].Add(queen);
        game.players[1].Hand[0].Add(king);

        game.players[2].Hand[0].Add(ace);
        game.players[2].Hand[0].Add(ace);
        game.players[2].Hand[0].Add(ace);

        game.players[3].Hand[0].Add(king);
        game.players[3].Hand[0].Add(five);

        Console.SetIn(new StringReader("y"));
        foreach (Player player in game.players) {
            for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                while (true) {
                    bool DidSplit = player.Split(HandIndex);
                    if (DidSplit) {
                        game.MoveCard(player, HandIndex);
                        game.MoveCard(player, HandIndex+1);

                        bool BetResult = player.SetBet("10", HandIndex+1);
                    }
                    else {break;}
                }
            }

            if(player.Name == "Aiden") {
                Assert.Equal(3, player.Hand.Count);
            }
            if(player.Name == "Jane") {
                Assert.Equal(2, player.Hand.Count);
            }
            if(player.Name == "Jhon Doe" || player.Name == "Jennifer"){
                Assert.Single(player.Hand);
            }
        }
    }
    [Fact]
    public void NoSplitTest(){        
        Console.SetIn(new StringReader("n"));
        Deck testdeck = new Deck();

        Game game = new Game(testdeck);
        game.blackjackdeck.CreateDeck();
        Console.SetIn(new StringReader("\nAiden\nJane"));
        game.PlayerCount = 3;
        game.SetPlayerNames();

        Card queen = new Card {
            Suit = "♠",
            Rank = "Q",
            Value = 10,
        };
        Card five = new Card {
            Suit = "♥",
            Rank = "5",
            Value = 5,
        };
        Card king = new Card {
            Suit = "♠",
            Rank = "K",
            Value = 10,
        };

        game.players[0].Hand[0].Add(queen);
        game.players[0].Hand[0].Add(king);

        game.players[1].Hand[0].Add(king);
        game.players[1].Hand[0].Add(five);
        
        foreach (Player player in game.players) {
            for (int HandIndex = 0; HandIndex < player.Hand.Count; HandIndex++) {
                while (true) {
                    bool DidSplit = player.Split(HandIndex);
                    Assert.False(DidSplit);
                    break;
                }
            }
        }
    }

    [Fact]
    public void DoubleDowntest() {
        Card two = new Card {
            Suit = "♠",
            Rank = "2",
            Value = 2,
        };

        Card queen = new Card {
            Suit = "♠",
            Rank = "Q",
            Value = 10,
        };
        
        Card five = new Card {
            Suit = "♠",
            Rank = "5",
            Value = 5,
        };
        

        Deck testdeck = new Deck();
        
        testdeck.deck.Add(queen);
        testdeck.deck.Add(queen);
        testdeck.deck.Add(queen);
        testdeck.deck.Add(queen);
        testdeck.deck.Add(queen);
        testdeck.deck.Add(queen);


        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJennifer\nJack"));
        game.PlayerCount = 4;

        game.SetPlayerNames();

        game.players[0].Hand[0].Add(two);
        game.players[0].Hand[0].Add(two);

        game.players[1].Hand[0].Add(two);
        game.players[1].Hand[0].Add(two);

        game.players[2].Hand[0].Add(five);
        game.players[2].Hand[0].Add(queen);

        game.players[3].Hand[0].Add(five);
        game.players[3].Hand[0].Add(queen);

        game.players[0].SetBet("40",0);
        game.players[1].SetBet("60",0);
        game.players[2].SetBet("20",0);
        game.players[3].SetBet("10",0);

        Assert.Equal(40, game.players[0].Bet[0]);
        Assert.Equal(60, game.players[0].Chips);
        Assert.Equal(60, game.players[1].Bet[0]);
        Assert.Equal(40, game.players[1].Chips);
        Assert.Equal(20, game.players[2].Bet[0]);
        Assert.Equal(80, game.players[2].Chips);
        Assert.Equal(10, game.players[3].Bet[0]);
        Assert.Equal(90, game.players[3].Chips);
        
        Console.SetIn(new StringReader("y\ny\ny\nn"));
        game.DoubleDownOption();

        Assert.Equal(14, game.players[0].Hand[0].Sum(card => card.Value));
        Assert.Equal(80, game.players[0].Bet[0]);
        Assert.Equal(20, game.players[0].Chips);
        Assert.Equal(3, game.players[0].Hand[0].Count);
        Assert.True(game.players[0].Stand[0]);
        Assert.False(game.players[0].InGame);


        Assert.Equal(4, game.players[1].Hand[0].Sum(card => card.Value));
        Assert.Equal(60, game.players[1].Bet[0]);
        Assert.Equal(40, game.players[1].Chips);
        Assert.True(game.players[1].InGame);

        Assert.Equal(25, game.players[2].Hand[0].Sum(card => card.Value));
        Assert.True(game.players[2].Stand[0]);
        Assert.False(game.players[2].InGame);
        Assert.Equal(0, game.players[2].Bet[0]);
        Assert.Equal(3, game.players[2].Hand[0].Count);
        Assert.Equal(60, game.players[2].Chips);

        Assert.Equal(15, game.players[3].Hand[0].Sum(card => card.Value));
        Assert.False(game.players[3].Stand[0]);
        Assert.True(game.players[3].InGame);
        Assert.Equal(10, game.players[3].Bet[0]);
        Assert.Equal(90, game.players[3].Chips);
        Assert.Equal(2, game.players[3].Hand[0].Count);
    }

    [Fact]
    public void ContinueTest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);                
        Console.SetIn(new StringReader("Aiden"));
        game.PlayerCount = 1;

        game.SetPlayerNames();
        Console.SetIn(new StringReader("y"));
        bool result = game.players[0].WantToContinue();
        Assert.True(result);
    }

    [Fact]
    public void LeaveTest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        List<string> PlayerNames = new List<string> {"Aiden"};
        Console.SetIn(new StringReader("Aiden"));
        game.PlayerCount = 1;

        game.SetPlayerNames();
        Console.SetIn(new StringReader("n"));
        bool result = game.players[0].WantToContinue();
        Assert.False(result);
    }

    [Fact]
    public void HitTest(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden"));
        game.PlayerCount = 1;
        game.SetPlayerNames();
        game.blackjackdeck.CreateDeck();
        Console.SetIn(new StringReader("y"));
        bool result = game.players[0].StandOrHit(0);
        Assert.True(result);    
        
        }

    [Fact]
    public void StandTest(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Jane"));
        game.PlayerCount = 1;
        game.SetPlayerNames();
        game.blackjackdeck.CreateDeck();
        Console.SetIn(new StringReader("n"));
        bool result = game.players[0].StandOrHit(0);
        Assert.False(result);
    }

        [Fact]
    public void CanPlayTest(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden"));
        game.PlayerCount = 1;
        game.SetPlayerNames();
        bool TrueResult = game.players[0].Canplay();
        Assert.True(TrueResult);
        game.players[0].Chips = 0;
        bool FalseResult = game.players[0].Canplay();
        Assert.False(FalseResult);
    }
}