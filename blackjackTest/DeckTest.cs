using Blackjack;

namespace blackjackTest;

public class Decktest {
    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    [InlineData("6")]
    [InlineData("7")]
    [InlineData("8")]

    public void DeckSelectionTest(string input){
        int result = Program.SelectDeckAmount(input);
        int intValue = Int32.Parse(input);
        Assert.Equal(intValue, result);

    }

    [Theory]
    [InlineData("-1")]
    [InlineData("0")]
    [InlineData("9")]
    [InlineData("10")]
    [InlineData("test")]
    public void DeckSelectionTestfail(string input){
        int result = Program.SelectDeckAmount(input);
        Assert.Equal(0, result);

    }

    [Fact]
    public void DeckGenerationtest(){
        Deck testdeck = new Deck();
        bool result = testdeck.createDeck();
        Assert.Equal("♦", testdeck.deck[1].Suit);
        Assert.True(result);
    }

    [Fact]
    public void DeckGenerationtestfail(){
        Deck testdeck = new Deck();
        testdeck.PlayingCards = new string[] {"3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = testdeck.createDeck();
        Assert.Empty(testdeck.deck);
        Assert.False(result);
    }

    [Fact]
    public void DeckGenerationtesparsefail(){
        string[] testdeck = {"2", "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        Deck result = Program.CreateDeck(1, testdeck);
        Assert.Equal("2", result.deck[0].Rank);
        Assert.Equal("3", result.deck[1].Rank);
        Assert.Equal("4", result.deck[2].Rank);
        Assert.Equal(52, result.deck.Count);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("11")]
    [InlineData("12")]
    public void DeckGenerationValueOverandUnder(string value){
        string[] testdeck = {value, "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        Deck result = Program.CreateDeck(1, testdeck);
        Assert.Equal("2", result.deck[0].Rank);
        Assert.NotEqual(value, result.deck[0].Rank);
        Assert.Equal(52, result.deck.Count);
    }

    [Fact]
    public void TestingtwoDecks() {
        string[] testdeck = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        Deck result = Program.CreateDeck(2, testdeck);
        Assert.Equal("♦", result.deck[0].Suit);
        Assert.Equal(104, result.deck.Count);
    }
}