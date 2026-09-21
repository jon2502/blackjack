using Blackjack;

namespace blackjackTest;

public class DealerTest {

    [Fact]
    public void DealerBlackjackTest(){
        string [] PlayerNames = {"Aiden", "Jane"};
        Player Aiden = new Player{
            PlayerID = 0,
            Name = "Aiden",
        };
        Player Jane = new Player{
            PlayerID = 1,
            Name = "Jane",
        };
        
        Game game = new Game();
        game.SetPlayerNames(PlayerNames);


        game.players[0].SetBet("10");
        game.players[1].SetBet("20");

        Card two = new Card {
            Suit = "♠",
            Rank = "2",
            Value = 2,
        };

        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };

        game.dealer.Hand.Add(two);
        game.dealer.Hand.Add(king);
        
        bool result = game.dealer.DealerBlackjack();
        Assert.False(result);
        game.Insurrance();
        foreach (Player player in game.players){
            Assert.True(player.InGame);
            Assert.NotEqual(0, player.Bet);
        }
        game.dealer.Hand.Clear();

  
        Card ace = new Card {
            Suit = "♦",
            Rank = "A",
            Value = 11,
        };


        game.dealer.Hand.Add(ace);
        game.dealer.Hand.Add(king);

        game.Insurrance();
        bool newresult = game.dealer.DealerBlackjack();
        Assert.True(newresult);
        foreach (Player player in game.players){
            Assert.False(player.InGame);
            Assert.Equal(0, player.Bet);
        }
    }
        
}