using Blackjack;

namespace blackjackTest;
public class GameTest {
    [Fact]
    public void PlayerBlackjackTest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount=2;
        game.SetPlayerNames();

        game.players[0].SetBet("10",0);
        game.players[1].SetBet("20",0);

        Card ace = new Card {
            Suit = "♦",
            Rank = "A",
            Value = 11,
        };
        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };

        Card two = new Card {
            Suit = "♠",
            Rank = "2",
            Value = 2,
        };

        Card five = new Card {
            Suit = "♣",
            Rank = "5",
            Value = 5,
        };

        game.players[0].Hand[0].Add(two);
        game.players[0].Hand[0].Add(five);

        game.players[1].Hand[0].Add(ace);
        game.players[1].Hand[0].Add(king);

        game.players[0].PlayerBlackjack(0);
        game.players[1].PlayerBlackjack(0);

        Assert.True( game.players[0].InGame);
        Assert.Equal(0,  game.players[0].Retuns);

        Assert.False(game.players[1].InGame);
        Assert.Equal(30,  game.players[1].Retuns);
    }

    [Fact]
     public void PlayerBustTest(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount = 2;
        game.SetPlayerNames();

        game.players[0].SetBet("10",0);
        game.players[1].SetBet("20",0);

        Card ten = new Card {
            Suit = "♦",
            Rank = "10",
            Value = 10,
        };
        Card three = new Card {
            Suit = "♦",
            Rank = "3",
            Value = 3,
        };
        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };

        Card six = new Card {
            Suit = "♠",
            Rank = "6",
            Value = 6,
        };

        Card five = new Card {
            Suit = "♣",
            Rank = "5",
            Value = 5,
        };

        Card ace1 = new Card {
            Suit = "♥",
            Rank = "A",
            Value = 11,
        };
    
        Card ace2 = new Card {
            Suit = "♣",
            Rank = "A",
            Value = 11,
        };


        game.players[0].Hand[0].Add(six);
        game.players[0].Hand[0].Add(five);
        game.players[0].Hand[0].Add(ace1);
        game.players[0].Hand[0].Add(ace2);
        
        game.players[1].Hand[0].Add(ten);
        game.players[1].Hand[0].Add(king);
        game.players[1].Hand[0].Add(three);
        
        Assert.Equal(33, game.players[0].Hand[0].Sum(card => card.Value));

        game.players[0].BustCheck(0);
        game.players[1].BustCheck(0);

        Assert.True(game.players[0].InGame);
        Assert.Equal(13, game.players[0].Hand[0].Sum(card => card.Value));
        Assert.Equal(10, game.players[0].Bet[0]);

        Assert.False(game.players[1].InGame);
        Assert.Equal(0, game.players[1].Bet[0]);
    }

    [Fact]
    public void GameDone() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount=2;
        game.SetPlayerNames();
        foreach(Player player in game.players){
            player.CheckPlayerState();
        }
        Assert.True(game.players[0].InGame);
        Assert.True(game.players[1].InGame);

        foreach(Player player in game.players){
            player.Hand.Clear();
            player.CheckPlayerState();
        }
        Assert.False(game.players[0].InGame);
        Assert.False(game.players[1].InGame);

        game.CheckIfGamesOver();
        Assert.False(game.Playing);
    }

    [Fact]
    public void CheckIfNewRoundBegins(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount = 2;
        game.SetPlayerNames();

        game.CheckIfNewRoundBegins();
        Assert.Equal(2, game.players.Count);
        Assert.Equal(2, game.PlayerCount);
        Assert.True(game.Playing);

        game.players.Clear();
        game.PlayerCount = 0;

        game.CheckIfNewRoundBegins();
        Assert.Empty(game.players);
        Assert.Equal(0, game.PlayerCount);
        Assert.False(game.Playing);
    }

    [Fact]
   public void ResultTest(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane\nJack"));
        game.PlayerCount=3;

        game.SetPlayerNames();

        
        Card ten = new Card {
            Suit = "♦",
            Rank = "10",
            Value = 10,
        };
        Card three = new Card {
            Suit = "♦",
            Rank = "3",
            Value = 3,
        };
        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };

        Card six = new Card {
            Suit = "♠",
            Rank = "6",
            Value = 6,
        };

        Card five = new Card {
            Suit = "♣",
            Rank = "5",
            Value = 5,
        };

        Card ace = new Card {
            Suit = "♥",
            Rank = "A",
            Value = 11,
        };
        game.players[0].Hand.Add([]);
        game.players[0].Bet.Add(0);

        game.players[0].SetBet("10",0);
        game.players[0].SetBet("40",1);
        game.players[1].SetBet("30",0);
        game.players[2].SetBet("100",0);


        game.dealer.Hand[0].Add(five);
        game.dealer.Hand[0].Add(six);
        game.dealer.Hand[0].Add(five);


        game.players[0].Hand[0].Add(five);
        game.players[0].Hand[0].Add(six);
        game.players[0].Hand[0].Add(five);

        game.players[0].Hand[1].Add(six);
        game.players[0].Hand[1].Add(three);
        game.players[0].Hand[1].Add(ace);

        game.players[1].Hand[0].Add(three);
        game.players[1].Hand[0].Add(ace);

        game.players[2].Hand[0].Add(five);
        game.players[2].Hand[0].Add(six);

        game.Results();
        Assert.Equal(2, game.players[0].Hand.Count);
        Assert.Equal(16, game.players[0].Hand[0].Sum(card=>card.Value));
        Assert.Equal(20, game.players[0].Hand[1].Sum(card=>card.Value));

        Assert.Equal(140, game.players[0].Chips);
        Assert.Equal(70, game.players[1].Chips);
        Assert.Equal(0, game.players[2].Chips);
    }

    [Fact]
    public void CanPlayersContinuetest(){
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\n\n\nJane\nJack"));
        game.PlayerCount = 5;
        game.SetPlayerNames();

        Assert.Equal(5, game.players.Count);

        game.players[0].Chips = 0;
        game.players[1].Chips = 0;

        game.CanPlayersContinue();

        Assert.Equal(3, game.players.Count);
    }

    [Fact]
    public void PlayersWantToContinueTest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        game.PlayerCount = 5;
        Console.SetIn(new StringReader("Aiden\n\n\nJane\nJack"));

        game.SetPlayerNames();

        Assert.Equal(5, game.players.Count);

        Console.SetIn(new StringReader("n\nn\ny\ny\ny"));

        game.DoPlayersWantContinue();
        Assert.Equal(3, game.players.Count);
    }

    [Fact]
    public void CheckIfnewPlayersJoinTest(){
                Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\n\n\nJane\nJack"));
        game.PlayerCount = 5;
        game.SetPlayerNames();

        Assert.Equal(5, game.players.Count);

        Console.SetIn(new StringReader("y\n2\njhonny\nJennifer"));
        game.CheckIfnewPlayersJoin();

        Assert.Equal(7, game.players.Count);
    }
}