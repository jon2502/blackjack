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
        bool result = testdeck.CreateDeck();
        Assert.Contains(testdeck.deck, card => card.Suit == "♦");
        Assert.Contains(testdeck.deck, card => card.Suit == "♣");
        Assert.Contains(testdeck.deck, card => card.Suit == "♥");
        Assert.Contains(testdeck.deck, card => card.Suit == "♠");
        Assert.True(result);
    }

    [Fact]
    public void DeckGenerationtestfail(){
        Deck testdeck = new Deck();
        testdeck.PlayingCards = new string[] {"3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = testdeck.CreateDeck();
        Assert.Empty(testdeck.deck);
        Assert.False(result);
    }

    [Fact]
    public void DeckGenerationtesparsefail(){
        string[] testList = {"2", "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        Deck testdeck = Program.CreateDeck(1, testList);
        Assert.Contains(testdeck.deck, card => card.Rank == "2");
        Assert.Contains(testdeck.deck, card => card.Rank == "3");
        Assert.Contains(testdeck.deck, card => card.Rank == "4");
        Assert.Contains(testdeck.deck, card => card.Value == 11);
        Assert.Contains(testdeck.deck, card => card.Suit == "♦");
        Assert.Equal(52, testdeck.deck.Count);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("11")]
    [InlineData("12")]
    public void DeckGenerationValueOverandUnder(string value){
        string[] testList = {value, "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        Deck testdeck = Program.CreateDeck(1, testList);
        Assert.Contains(testdeck.deck, card => card.Rank == "2");
        Assert.DoesNotContain(testdeck.deck, card => card.Rank == value);
        Assert.Equal(52, testdeck.deck.Count);
    }

    [Fact]
    public void TestingtwoDecks() {
        string[] testList = {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        Deck testdeck = Program.CreateDeck(2, testList);
        Assert.Equal(104, testdeck.deck.Count);
    }
}