using Blackjack;

namespace blackjackTest;

public class GameTest {
    [Fact]
    public void PlayerBlackjackTest() {
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        List<string> PlayerNames = new List<string> {"Aiden", "Jane"};
        game.SetPlayerNames(PlayerNames);

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
        List<string> PlayerNames = new List<string>{"Aiden", "Jane"};
        game.SetPlayerNames(PlayerNames);

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
        List<string> PlayerNames = new List<string>{"Aiden", "Jane"};
        game.SetPlayerNames(PlayerNames);
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
}