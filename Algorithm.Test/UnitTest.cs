using static Algorithm.Program;

namespace Algorithm.Test;

public class UnitTest
{
    [Fact]
    public void TestPrintSmallStar()
    {
        var paper = new Paper(3);
        paper.PrintSmallStar(0, 0);
        var result = paper.GetString();
        var expectation = @"  *  
 * * 
*****
";

        Assert.Equal(expectation, result);
    }

    [Fact]
    public void TestPrintStar()
    {
        var paper = new Paper(6);
        paper.PrintStar(6, 0, 0);
        var result = paper.GetString();
        var expectation = @"     *     
    * *    
   *****   
  *     *  
 * *   * * 
***** *****
";

        Assert.Equal(expectation, result);
    }
}