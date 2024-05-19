using static System.Console;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        int N = int.Parse(ReadLine());
        Solve(N);
    }

    static void Solve(int N)
    {
        char[][] spaceArray = GetSpaceArray(N);
        var space = new Space(spaceArray);
        var count = space.GetSleepAvailableSpace();
        WriteLine(count.countRow + " " + count.countColumn);
    }

    static char[][] GetSpaceArray(int N)
    {
        // input array 둘레를 벽(wall) X로 감싸기
        // XXXXX
        // X   X
        // X   X
        // XXXXX
        char[][] spaceArrayWithWall = new char[N + 2][];
        spaceArrayWithWall[0] = (new string('X', N + 2)).ToCharArray();
        spaceArrayWithWall[N + 1] = (new string('X', N + 2)).ToCharArray();
        for (var i = 1; i < N + 1; i++)
        {
            spaceArrayWithWall[i] = ('X' + ReadLine() + 'X').ToCharArray();
        }
        return spaceArrayWithWall;
    }

    public class Space
    {
        private readonly List<Line> _spaceList;
        private readonly List<Line> _spaceLineReversed; // row, column 반전
        public Space(char[][] spaceArray)
        {
            _spaceList = ConvertSpaceArray(spaceArray);
            _spaceLineReversed = ConvertSpaceArray(GetReversed2DArray(spaceArray));
        }

        List<Line> ConvertSpaceArray(char[][] spaceArray)
        {
            var convertedSpaceArray = new List<Line>();
            foreach (var line in spaceArray)
            {
                convertedSpaceArray.Add(new Line(line));
            }
            return convertedSpaceArray;
        }

        public (int countRow, int countColumn) GetSleepAvailableSpace()
        {
            var countRow = 0;
            foreach (Line line in _spaceList)
            {
                countRow += line.GetAvailableSpace();
            }

            var countColumn = 0;
            foreach (Line line in _spaceLineReversed)
            {
                countColumn += line.GetAvailableSpace();
            }

            return (countRow, countColumn);
        }
    }

    public class Line
    {
        char[] _lineSpace;
        int _availableSpace = -1;
        public int LineSpaceCount => _lineSpace.Length;

        public Line(char[] lineSpace)
        {
            _lineSpace = lineSpace;
        }

        public int GetAvailableSpace()
        {
            if (_availableSpace != -1)
            {
                return _availableSpace;
            }
            _availableSpace = _lineSpace.Select((ch, index) => (ch, index))
                .Where(element => element.ch == 'X')
                .Select(element => element.index)
                .Aggregate(
                (count: 0, prevIndex: 0), // seed
                (acc, curr) =>  // func
                {
                    if ((curr) - acc.prevIndex > 2)
                    {
                        acc.count++;
                    }
                    acc.prevIndex = curr;
                    return acc;
                },
                (line) => line.count // resultSelector
                );

            return _availableSpace;
        }
    }


    /////////////////////////////  util 함수  ////////////////////////////////
    static T[] CopyArray<T>(T[] array)
    {
        T[] newArray = new T[array.Length];
        for (var i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }
        return newArray;
    }

    static T[][] Copy2DArray<T>(T[][] array)
    {
        T[][] newArray = new T[array.Length][];
        for (var i = 0; i < array.Length; i++)
        {
            newArray[i] = new T[array[i].Length];
            for (var j = 0; j < array[i].Length; j++)
            {
                newArray[i][j] = array[i][j];
            }
        }
        return newArray;
    }

    static T[][] Get2DArray<T>(T[][] array, T element)
    {
        T[][] newArray = new T[array.Length][];
        for (var i = 0; i < array.Length; i++)
            newArray[i] = Enumerable.Repeat(element, array.Length).ToArray();
        return newArray;
    }

    static List<T> CopyList<T>(List<T> list)
    {
        var newList = new List<T>();
        for (var i = 0; i < list.Count; i++)
        {
            newList.Add(list[i]);
        }
        return newList;
    }


    static Dictionary<char, int> SetDictionary(Dictionary<char, int> charCounts, string inputString)
    {
        foreach (var character in inputString)
        {
            if (charCounts.ContainsKey(character))
            {
                charCounts[character]++;
            }
            else
            {
                charCounts[character] = 1;
            }
        }
        return charCounts;
    }


    static IEnumerable<(T1, T2)> IterationFunction<T1, T2>(IEnumerable<T1> arr1, IEnumerable<T2> arr2)
    {
        return arr1.Join<T1, T2, bool, (T1, T2)>(arr2, item1 => true, item2 => true, (item1, item2) => (item1, item2));
    }
    static IEnumerable<(T1, T2, T3)> IterationFunction<T1, T2, T3>(IEnumerable<T1> arr1, IEnumerable<T2> arr2, IEnumerable<T3> arr3)
    {
        return arr1.Join(arr2, item1 => true, item2 => true, (item1, item2) => (item1, item2)).Join(arr3, item => true, item3 => true, (item, item3) => (item.item1, item.item2, item3));
    }

    static char[,] Get2dArrayCopy(char[,] square)
    {
        int rows = square.GetLength(0);
        int cols = square.GetLength(1);
        char[,] copyArr = new char[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copyArr[i, j] = square[i, j];
            }
        }
        return copyArr;
    }

    static public char[][] GetReversed2DArray(char[][] array)
    {
        char[][] reversedArray = new char[array[0].Length][];

        for (var i = 0; i < reversedArray.Length; i++)
        {
            reversedArray[i] = new char[array[i].Length];
            for (var j = 0; j < reversedArray.Length; j++)
            {
                reversedArray[i][j] = array[j][i];
            }
        }
        return reversedArray;
    }

    static char[,] GetSquareArr(int N)
    {
        char[,] resultArr = new char[N, N];
        for (var i = 0; i < N; i++)
        {
            var tmpRowArr = Console.ReadLine().ToCharArray();
            for (var j = 0; j < N; j++)
            {
                resultArr[i, j] = tmpRowArr[j];
            }
        }
        return resultArr;
    }

    static int[,] GetArr(int N, int M)
    {
        int[,] resultArr = new int[N, M];
        for (var i = 0; i < N; i++)
        {
            var tmpRowArr = Console.ReadLine().Split(' ');
            for (var j = 0; j < M; j++)
            {
                resultArr[i, j] = int.Parse(tmpRowArr[j]);
            }
        }
        return resultArr;
    }

    static int[] ChangeCharToNum(char[] numsInChar)
    {
        return numsInChar.Select(x => int.Parse(x.ToString())).ToArray();
    }

    static int[] ChangeStrToNum(string[] numsInStr)
    {
        return numsInStr.Select(x => int.Parse(x)).ToArray();
    }

    static void Print(int[,] result, int N, int K)
    {
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < K; j++)
            {
                Console.Write($"{result[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}


public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }

}