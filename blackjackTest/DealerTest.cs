using Blackjack;

namespace blackjackTest;

public class DealerTest {

    [Fact]
    public void DealerBlackjackTest(){
        
        List<string> PlayerNames = new List<string> {"Aiden", "Jane"};
        Game game = new Game();
        game.SetPlayerNames(PlayerNames);


        game.players[0].SetBet("10");
        game.players[1].SetBet("20");

        game.players[0].SetInsurance("5", 0);
        game.players[1].SetInsurance("10", 0);

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

        game.dealer.Hand[0].Add(two);
        game.dealer.Hand[0].Add(king);
        
        bool result = game.dealer.DealerBlackjack();
        Assert.False(result);
        game.Insurrance();
        foreach (Player player in game.players){
            Assert.True(player.InGame);
            Assert.NotEqual(0, player.Bet[0]);
        }
        Assert.Equal(10, game.players[0].Bet[0]);
        Assert.Empty(game.players[0].Insurance);
        Assert.Equal(85, game.players[0].Chips);

        Assert.Equal(20, game.players[1].Bet[0]);
        Assert.Empty(game.players[1].Insurance);
        Assert.Equal(70, game.players[1].Chips);

        game.dealer.Hand[0].Clear();
        game.players[0].Chips += 5;
        game.players[1].Chips += 10;

        game.players[0].SetInsurance("5", 0);
        game.players[1].SetInsurance("10", 0);
  
        Card ace = new Card {
            Suit = "♦",
            Rank = "A",
            Value = 11,
        };


        game.dealer.Hand[0].Add(ace);
        game.dealer.Hand[0].Add(king);

        game.Insurrance();
        bool newresult = game.dealer.DealerBlackjack();
        Assert.True(newresult);
        foreach (Player player in game.players){
            Assert.False(player.InGame);
            Assert.Empty(player.Bet);
            Assert.Empty(player.Insurance);
        }
        Assert.Equal(10, game.players[0].Retuns);
        Assert.Equal(20, game.players[1].Retuns);
    }
        
}