using Blackjack;

namespace blackjackTest;

public class DealerTest {

    [Fact]
    public void DealerBlackjackTest(){
        
        Deck testdeck = new Deck();
        Game game = new Game(testdeck);
        Console.SetIn(new StringReader("Aiden\nJane"));
        game.PlayerCount = 2;
        game.SetPlayerNames();


        game.players[0].SetBet("10", 0);
        game.players[1].SetBet("20", 0);

        List<int> NumbList = [10, 20];

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
        bool check = game.dealer.DoesTheDealerHaveAnAce();
        Assert.False(check);
        bool result = game.dealer.DealerBlackjack();
        Assert.False(result);

        game.CheckInsurrance();

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
            
        game.players[0].Insurance = [0];
        game.players[1].Insurance = [0];

        game.players[0].SetInsurance("5", 0);
        game.players[1].SetInsurance("10", 0);
  
        Card ace = new Card {
            Suit = "♦",
            Rank = "A",
            Value = 11,
        };


        game.dealer.Hand[0].Add(ace);
        game.dealer.Hand[0].Add(king);
        
        bool newCheck = game.dealer.DoesTheDealerHaveAnAce();
        Assert.True(newCheck);
        bool newresult = game.dealer.DealerBlackjack();
        Assert.True(newresult);

        game.CheckInsurrance();

        foreach (Player player in game.players){
            Assert.False(player.InGame);
            Assert.Empty(player.Bet);
            Assert.Empty(player.Insurance);
        }
        Assert.Equal(10, game.players[0].Retuns);
        Assert.Equal(20, game.players[1].Retuns);
    }

    [Fact]
    public void DealerHitTest() {
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

        Card ace = new Card {
            Suit = "♥",
            Rank = "A",
            Value = 11,
        };
        Card five = new Card {
            Suit = "♥",
            Rank = "5",
            Value = 5,
        };

        Deck testdeck = new Deck();
        testdeck.deck.Add(two);
        testdeck.deck.Add(three);
        testdeck.deck.Add(queen);
        testdeck.deck.Add(ace);
        testdeck.deck.Add(five);

        Game game = new Game(testdeck);

        Card four = new Card {
            Suit = "♠",
            Rank = "4",
            Value = 4,
        };

        Card king = new Card {
            Suit = "♥",
            Rank = "K",
            Value = 10,
        };


        game.dealer.Hand[0].Add(four);
        game.dealer.Hand[0].Add(king);

        game.DealerHitLoop();


        Assert.Equal(3, testdeck.deck.Count);
        Assert.Equal(19, game.dealer.Hand[0].Sum(card => card.Value));

        game.dealer.Hand[0].Clear();
        game.dealer.Hand[0].Add(four);
        game.dealer.Hand[0].Add(king);
        
        game.DealerHitLoop();

        Assert.Equal(2, testdeck.deck.Count);
        Assert.Equal(24, game.dealer.Hand[0].Sum(card => card.Value));
    }

}