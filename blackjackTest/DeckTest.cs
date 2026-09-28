using Blackjack;

namespace blackjackTest;

public class DeckTest {
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
        Deck blackjackdeck = new Deck();
        Console.SetIn(new StringReader($"\n10\n9\ny\n0\n-1\n{input}"));
        blackjackdeck.SetDecksize();
        int intValue = Int32.Parse(input);
        
        Assert.Equal(intValue, blackjackdeck.DeckSize);
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
        Deck testdeck = new Deck();
        testdeck.PlayingCards  = new string[]  {"2", "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = testdeck.CreateDeck();
        Assert.False(result);

        testdeck.CreateDeck();
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
        Deck testdeck = new Deck();
        testdeck.PlayingCards = new  string[] {value, "3", "4", "test", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"};
        bool result = testdeck.CreateDeck();
        Assert.False(result);

        testdeck.CreateDeck();
        Assert.Contains(testdeck.deck, card => card.Rank == "2");
        Assert.DoesNotContain(testdeck.deck, card => card.Rank == value);
        Assert.Equal(52, testdeck.deck.Count);
    }

    [Fact]
    public void TestingtwoDecks() {
        Deck testdeck = new Deck();
        testdeck.DeckSize = 2;
        testdeck.CreateDeck();
        Assert.Equal(104, testdeck.deck.Count);
    }

    [Fact]
    public void EmptyDeckTest() {
        Deck testdeck = new Deck();
        BlackJack game = new BlackJack(testdeck);
        game.blackjackdeck.CreateDeck();
        game.blackjackdeck.DeckSize = 1;

        Console.SetIn(new StringReader(""));
        game.PlayerCount = 1;
        game.SetPlayerNames();

        for (int i = 0; i < 100; i++){
            Card card = testdeck.DrawCard();
            game.players[0].DrawACard(card, 0);
        }

        Assert.Equal(4, game.blackjackdeck.deck.Count);
    }
}