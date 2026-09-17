using Blackjack;

namespace blackjackTest;

public class Decktest {
    [Fact]
    public void DeckGenerationtest(){
        Deck testdeck = new Deck();
        bool result = testdeck.createDeck();
        Assert.Equal("Diamonds", testdeck.deck[1].Suit);
        Assert.True(result);
    }

    [Fact]
    public void DeckGenerationtestfail(){
        Deck testdeck = new Deck();
        testdeck.CardSuit = new string[] {"Clubs","Hearts","Spades"};
        testdeck.PlayingCards = new string[] {"3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = testdeck.createDeck();
        Assert.Empty(testdeck.deck);
        Assert.False(result);
    }

        [Fact]
    public void DeckGenerationtesparsefail(){
        string[] testdeck = {"2", "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        Deck result = Program.CreateDeck(1, testdeck);
        Console.WriteLine(result.deck.Count);
        Assert.Equal("2", result.deck[0].Rank);
        Assert.Equal("3", result.deck[1].Rank);
        Assert.Equal("4", result.deck[2].Rank);
        Assert.Equal(52, result.deck.Count);
    }

    [Fact]
    public void TestingtwoDecks() {

        string[] testdeck = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        Deck result = Program.CreateDeck(2, testdeck);
        Console.WriteLine(result.deck.Count);
        Assert.Equal("Diamonds", result.deck[0].Suit);
        Assert.Equal(104, result.deck.Count);
    }
}