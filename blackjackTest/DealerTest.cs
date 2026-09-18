using Blackjack;

namespace blackjackTest;

public class DealerTest {

    [Fact]
    public void DealerBlackjackTest(){
        Player Aiden = new Player{
            PlayerID = 0,
            Name = "Aiden",
        };
        Player Jane = new Player{
            PlayerID = 1,
            Name = "Jane",
        };
        
        Players players = new Players();
        players.Add(Aiden);
        players.Add(Jane);

        Dealer dealer = new Dealer{
            Name = "dealer"
        };

        bool AideneResult = Aiden.SetBet("10");
        bool JaneResult = Jane.SetBet("20");

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

        dealer.Hand.Add(two);
        dealer.Hand.Add(king);
        
        bool result = dealer.DealerBlackjack();
        Assert.False(result);
        dealer.DealerCheck(players);
        foreach (Player player in players.playerlist){
            Assert.True(player.State);
            Assert.NotEqual(0, player.Bet);
        }
        dealer.Hand.Clear();

  
        Card ace = new Card {
            Suit = "♦",
            Rank = "A",
            Value = 11,
        };


        dealer.Hand.Add(ace);
        dealer.Hand.Add(king);

        dealer.DealerCheck(players);
        bool newresult = dealer.DealerBlackjack();
        Assert.True(newresult);
        foreach (Player player in players.playerlist){
            Assert.False(player.State);
            Assert.Equal(0, player.Bet);
        }
    }
        
}