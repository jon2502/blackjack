using Blackjack;

namespace blackjackTest;

public class GameTest {
    [Fact]
    public void PlayerBlackjackTest() {
        Player Aiden = new Player();
        Player Jane = new Player();
        
        Aiden.PlayerID = 0;
        Aiden.Name = "Aiden";

        Jane.PlayerID = 1;
        Jane.Name = "Jane";
        
        bool AideneResult = Aiden.SetBet("10");
        bool JaneResult = Jane.SetBet("20");

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

        Aiden.Hand.Add(two);
        Aiden.Hand.Add(five);

        Jane.Hand.Add(ace);
        Jane.Hand.Add(king);

        Aiden.PlayerBlackjack();
        Jane.PlayerBlackjack();

        Assert.True(Aiden.State);
        Assert.Equal(0, Aiden.Retuns);

        Assert.False(Jane.State);
        Assert.Equal(30, Jane.Retuns);

    }


}