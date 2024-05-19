using System.Reflection;
using static Algorithm.Program;

namespace Algorithm.Test;

public class UnitTest
{
    readonly int N = 5;
    readonly char[][] spaceArray;
    //object? space;
    public UnitTest()
    {
        spaceArray = new char[N + 2][];
        spaceArray[0] = "XXXXXXX".ToCharArray();
        spaceArray[1] = "X....XX".ToCharArray();
        spaceArray[2] = "X..X..X".ToCharArray();
        spaceArray[3] = "X.....X".ToCharArray();
        spaceArray[4] = "X.XX.XX".ToCharArray();
        spaceArray[5] = "XX....X".ToCharArray();
        spaceArray[6] = "XXXXXXX".ToCharArray();
    }

    [Theory]
    [InlineData("XXXXXXX", 0)]
    [InlineData("X....XX", 1)]
    [InlineData("X..X..X", 2)]
    [InlineData("X.XX.XX", 0)]
    [InlineData("XX....X", 1)]
    public void GetAvailableSpace_Should_ReturnAvailableSpace(string lineSpace, int expectation)
    {
        Type type = typeof(Line);
        var line = Activator.CreateInstance(type, lineSpace.ToCharArray());
        MethodInfo method = type.GetMethod("GetAvailableSpace");

        var result = (int?)method.Invoke(line, null);
        Assert.Equal(expectation, result);
    }

    [Fact]
    public void GetSleepAvailableSpace_Should_ReturnRowAndColumnCounts()
    {
        var space = new Space(spaceArray);
        var result = space.GetSleepAvailableSpace();
        (int, int) expectation = (5, 4);
        Assert.Equal(expectation, result);
    }
}