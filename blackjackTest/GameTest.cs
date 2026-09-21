using Blackjack;

namespace blackjackTest;

public class GameTest {
    [Fact]
    public void PlayerBlackjackTest() {
        Game game = new Game();
        List<string> PlayerNames = new List<string> {"Aiden", "Jane"};
        game.SetPlayerNames(PlayerNames);

        game.players[0].SetBet("10");
        game.players[1].SetBet("20");

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

        game.players[0].PlayerBlackjack();
        game.players[1].PlayerBlackjack();

        Assert.True( game.players[0].InGame);
        Assert.Equal(0,  game.players[0].Retuns);

        Assert.False( game.players[1].InGame);
        Assert.Equal(30,  game.players[1].Retuns);
    }

    [Fact]
     public void PlayerBustTest(){
        Game game = new Game();
        List<string> PlayerNames = new List<string>{"Aiden", "Jane"};
        game.SetPlayerNames(PlayerNames);

        game.players[0].SetBet("10");
        game.players[1].SetBet("20");

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

        game.players[1].Hand[0].Add(ten);
        game.players[1].Hand[0].Add(king);
        game.players[1].Hand[0].Add(three);

        game.players[0].PlayerBust();
        game.players[1].PlayerBust();

        Assert.True( game.players[0].InGame);
        Assert.Equal(10,  game.players[0].Bet[0]);

        Assert.False(game.players[1].InGame);
        Assert.Equal(0,  game.players[1].Bet[0]);
    }

    [Fact]
    public void GameDone() {
        Game game = new Game();
        List<string> PlayerNames = new List<string>{"Aiden", "Jane"};
        game.SetPlayerNames(PlayerNames);

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