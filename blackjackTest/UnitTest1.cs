using Blackjack;

namespace blackjackTest;

public class UnitTest1 {
    
    [Theory]
    [InlineData("test")]
    [InlineData("-1")]
    [InlineData("8")]
    public void selectPlayeramountTestIncorrect(string value){
        int result = Program.SelectPlayeramount(value);
        Assert.Equal(0, result);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    [InlineData("6")]
    [InlineData("7")]
    public void selectPlayeramountTestIntCorrect(string value) {
        int result = Program.SelectPlayeramount(value);
        int intValue = Int32.Parse(value);
        Assert.Equal(intValue, result);
    }
}
