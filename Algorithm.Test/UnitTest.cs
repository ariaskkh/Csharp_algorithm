using static Algorithm.Program;

namespace Algorithm.Test;

public class SpaceTestFixture
{
    public SpaceTestFixture()
    {
        int N = 5;
        char[][] spaceArray = new char[N + 2][];
        spaceArray[0] = "XXXXXXX".ToCharArray();
        spaceArray[1] = "X....XX".ToCharArray();
        spaceArray[2] = "X..X..X".ToCharArray();
        spaceArray[3] = "X.....X".ToCharArray();
        spaceArray[4] = "X.XX.XX".ToCharArray();
        spaceArray[5] = "XX....X".ToCharArray();
        spaceArray[6] = "XXXXXXX".ToCharArray();
    }
}

public class UnitTest
{
    private readonly int N = 5;
    private readonly char[][] spaceArray;
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

    [Fact]
    public void TestGetSleepAvailableSpaceForRow()
    {
        var space = new Space(spaceArray);
        var availableSpaceCountList = new List<int>();
        foreach (char[] spaceArrayRow in spaceArray)
        {
            availableSpaceCountList.Add(space.GetSleepAvailableSpaceForLine(spaceArrayRow));
        }
        var expectation = new List<int>() { 0, 1, 2, 1, 0, 1, 0 };
        Assert.Equal(expectation, availableSpaceCountList);
    }

    [Fact]
    public void TestGetReversedArray()
    {
        var space = new Space(spaceArray);
        var result = space.GetReversedArray(spaceArray);
        var expectation = new char[N + 2][];
        expectation[0] = "XXXXXXX".ToCharArray();
        expectation[1] = "X....XX".ToCharArray();
        expectation[2] = "X...X.X".ToCharArray();
        expectation[3] = "X.X.X.X".ToCharArray();
        expectation[4] = "X.....X".ToCharArray();
        expectation[5] = "XX..X.X".ToCharArray();
        expectation[6] = "XXXXXXX".ToCharArray();

        Assert.Equal(expectation, result);
    }

    [Fact]
    public void TestGetSleepAvailableSpace()
    {
        var space = new Space(spaceArray);
        (int countRow, int countColumn) result = space.GetSleepAvailableSpace();
        (int countRow, int countColumn) expectation = (5, 4);

        Assert.Equal(result.countRow, expectation.countRow);
        Assert.Equal(result.countColumn, expectation.countColumn);
    }
}