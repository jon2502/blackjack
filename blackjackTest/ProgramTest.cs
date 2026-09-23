using Blackjack;

namespace blackjackTest;

public class programTest {
    
   
    [Theory]
    [InlineData("y")]
    [InlineData("Y")]
    public void RetrunValueTest(string input){
        Console.SetIn(new StringReader(input));
        bool result = Program.RetrunValue();
        Assert.True(result);
    }

    [Theory]
    [InlineData("-1")]
    [InlineData("0")]
    [InlineData("9")]
    [InlineData("N")]
    public void RetrunValueFailtest(string input){
        Console.SetIn(new StringReader(input));
        bool result = Program.RetrunValue();
        Assert.False(result);
            
    }

}