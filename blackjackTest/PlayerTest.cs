using Blackjack;


namespace blackjackTest;

public class PlayerTest {

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
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader($"value\n-1\n10\n{value}"));
        game.SetPlayerCount();
    }

    [Fact]
    public void PlayerListtest() {
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
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
        BlackJack game = new BlackJack(testdeck);
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
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\ntest\n150\n0\n-1\n20"));
        game.PlayerCount = 1;
        game.SetPlayerNames();
        game.players[0].SetBet(0);

        Assert.Equal(20, game.players[0].Bet[0]);

        Assert.Equal(80, game.players[0].Chips);
    }

    [Fact]
    public void SetInsuranceTest() {
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJack\n40\n100\n60\ny\ntest\n150\n20\nn\nY\n40\n30"));
        game.PlayerCount = 3;

        game.SetPlayerNames();
        game.players[0].SetBet(0);
        game.players[1].SetBet(0);
        game.players[2].SetBet(0);

        for (int i = 0; i < game.players.Count; i++) {
            game.players[i].SetInsurance();
        }
        


        Assert.Equal(20, game.players[0].Insurance[0]);
        Assert.Equal(0, game.players[1].Insurance[0]);
        Assert.Equal(30, game.players[2].Insurance[0]);


        Assert.Equal(40, game.players[0].Chips);
        Assert.Equal(0, game.players[1].Chips);
        Assert.Equal(10, game.players[2].Chips);
    }

    [Fact]
     public void SurrenderTest(){
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("\n100\ny"));
        game.PlayerCount = 1;

        game.SetPlayerNames();

        game.players[0].SetBet(0);
        game.DoPlayersWantToSurrender();

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

        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJack\nJennifer\n20\n15\ny\n10"));
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

        Assert.Equal(4, game.players.Count);
        game.DoPlayersWantToSplit();

        Assert.Equal(3, game.players[0].Hand.Count);
        List<Card> hand1 = new List<Card>();
        hand1.Add(ace);
        hand1.Add(three);

        List<Card> hand2 = new List<Card>();
        hand2.Add(ace);
        hand2.Add(two);

        List<Card> hand3 = new List<Card>();
        hand3.Add(ace);
        hand3.Add(five);

        Assert.Equal(hand1, game.players[0].Hand[0]);
        Assert.Equal(hand2, game.players[0].Hand[1]);
        Assert.Equal(hand3, game.players[0].Hand[2]);

        Assert.Equal(2, game.players[1].Hand.Count);
        Assert.Single(game.players[2].Hand);
        Assert.Single(game.players[3].Hand);

        Assert.Equal(3, game.players[0].Bet.Count);

        Assert.Equal(0, game.players[0].Bet[0]);
        Assert.Equal(20, game.players[0].Bet[1]);
        Assert.Equal(15, game.players[0].Bet[2]);
        Assert.Equal(10, game.players[1].Bet[1]);
        Assert.Equal(0, game.players[2].Bet.Sum(bet=>bet));
        Assert.Equal(0, game.players[3].Bet.Sum(bet=>bet));    
    }
    
    [Fact]
    public void NoSplitTest(){        
        Deck testdeck = new Deck();

        BlackJack game = new BlackJack(testdeck);
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
                    Console.SetIn(new StringReader("n"));
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


        BlackJack game = new BlackJack(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJennifer\nJack\n40\n60\n20\n10"));
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

        game.players[0].SetBet(0);
        game.players[1].SetBet(0);
        game.players[2].SetBet(0);
        game.players[3].SetBet(0);

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
        BlackJack game = new BlackJack(testdeck);                
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
        BlackJack game = new BlackJack(testdeck);
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
        BlackJack game = new BlackJack(testdeck);
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
        BlackJack game = new BlackJack(testdeck);
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
        BlackJack game = new BlackJack(testdeck);
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