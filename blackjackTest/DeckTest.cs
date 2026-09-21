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

        bool result = blackjackdeck.SetDecksize(input);
        int intValue = Int32.Parse(input);
        
        Assert.True(result);
        Assert.Equal(intValue, blackjackdeck.DeckSize);
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("0")]
    [InlineData("9")]
    [InlineData("10")]
    [InlineData("test")]
    public void DeckSelectionTestfail(string input){
        Deck blackjackdeck = new Deck();

        bool result =  blackjackdeck.SetDecksize(input);;
        Assert.False(result);

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
        testdeck.CreateDeck();
        testdeck.DeckSize = 1;

        Game game = new Game();
        string [] PlayerNames = {""};
        game.SetPlayerNames(PlayerNames);

        for (int i = 0; i < 100; i++){
            Card card = testdeck.DrawCard();
            game.players[0].DrawACard(card, 0);
        }

        Assert.Equal(4, testdeck.deck.Count);
    }
}