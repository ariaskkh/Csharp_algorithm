using System.Text.RegularExpressions;
using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var input = ReadLine().Split(' ');
        var height = int.Parse(input[0]);
        var width = int.Parse(input[1]);
        var blocks = int.Parse(input[2]);
        
        int[][] groundHeight = Enumerable.Range(0, height)
            .Select(item => ReadLine().Split(' ').ChangeStrToInt())
            .ToArray();

        Solve(groundHeight, blocks);
    }

    static void Solve(int[][] groundHeight, int blocks)
    {
        var user = new User(blocks);
        var ground = new Ground(groundHeight);
        user.Flatten(ground);
        WriteLine($"{user.ConsumeTime} {ground.FlattenedGroundHeight}");
    }

    class Ground
    {
        public int[][] GroundHeightTable { get; set; }
        public int FlattenedGroundHeight { get; set; }
        private int maxHeight;
        private int minHeight;

        public Ground(int[][] groundHeight)
        {
            GroundHeightTable = groundHeight;
            UpdateHeights();
        }

        public int GetMaxHeight()
        {
            return GroundHeightTable.Max(line => line.Max());
        }

        public int GetMinHeight()
        {
            return GroundHeightTable.Min((line) => line.Min());
        }

        public int GetNumberOfMaxHeight()
        {
            return GroundHeightTable.SelectMany(height => height).Count(height => height == maxHeight);
        }

        public int GetNumberOfMinHeight()
        {
            return GroundHeightTable.SelectMany(height => height).Count(height => height == minHeight);
        }

        public void UpdateHeights()
        {
            maxHeight = GetMaxHeight();
            minHeight = GetMinHeight();
        }
    }

    class User : Digging, Filling
    {
        private int blocks;
        public int ConsumeTime { get; set; }
        public User(int blocks)
        {
            this.blocks = blocks;
        }

        public void Flatten(Ground ground)
        {
            while (true)
            {
                int maxHeight = ground.GetMaxHeight();
                int minHeight = ground.GetMinHeight();
                if (maxHeight == minHeight)
                {
                    ground.FlattenedGroundHeight = maxHeight;
                    break;
                }
                // 가장 윗쪽 다 깎아야 함
                if (blocks == 0)
                {
                    int prevMaxHeight = maxHeight;
                    while (true)
                    {
                        Dig(ground, maxHeight);
                        ground.UpdateHeights();
                        if (prevMaxHeight != ground.GetMaxHeight())
                        {
                            break;
                        }
                    }
                }
                else // block > 0
                {
                    // 가장 아랫쪽 채우기. 개수 같을 경우 아랫쪽 채워야 평탄화 높이가 높아짐
                    if (ground.GetNumberOfMaxHeight() >= ground.GetNumberOfMinHeight())
                    {
                        Fill(ground, minHeight);
                    }
                    else // 가장 윗쪽 깎기
                    {
                        Dig(ground, maxHeight);
                    }
                }
                ground.UpdateHeights();
            }
        }

        private (int x, int y) FindCoordinateOfTargetNumber(Ground ground, int targetNumber)
        {
            var heightTable = ground.GroundHeightTable;
            var n = Enumerable.Range(0, heightTable.Count());
            var m = Enumerable.Range(0, heightTable[0].Count());
            return IterationFunction(n, m).FirstOrDefault(item => heightTable[item.Item1][item.Item2] == targetNumber);
        }

        public void Dig(Ground ground, int maxHeight)
        {
            (int x, int y) target = FindCoordinateOfTargetNumber(ground, maxHeight);
            DigUnitArea(ground, target.x, target.y);
            blocks++;
            ConsumeTime += 2;
        }

        public void Fill(Ground ground, int minHeight)
        {
            (int x, int y) target = FindCoordinateOfTargetNumber(ground, minHeight);
            FillUnitArea(ground, target.x, target.y);
            blocks--;
            ConsumeTime += 1;
        }

        public void DigUnitArea(Ground ground, int x, int y)
        {
            ground.GroundHeightTable[x][y] -= 1;
            return;
        }

        public void FillUnitArea(Ground ground, int x, int y)
        {
            ground.GroundHeightTable[x][y] += 1;
            return;
        }
    }


    interface Digging
    {
        public void Dig(Ground groung, int maxHeight);
    }

    interface Filling
    {
        public void Fill(Ground groung, int minHeight);
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

    static char[,] GetSquareArr(int N)
    {
        char[,] resultArr = new char[N, N];
        for (var i = 0; i < N; i++)
        {
            var tmpRowArr = ReadLine().ToCharArray();
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
            var tmpRowArr = ReadLine().Split(' ');
            for (var j = 0; j < M; j++)
            {
                resultArr[i, j] = int.Parse(tmpRowArr[j]);
            }
        }
        return resultArr;
    }

    static void Print(int[,] result, int N, int K)
    {
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < K; j++)
            {
                Write($"{result[i, j]} ");
            }
            WriteLine();
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

    public static int[] ChangeCharToInt(this IEnumerable<char> enumerable)
    {
        return enumerable.Select(item => int.Parse(item.ToString())).ToArray();
    }

    public static long[] ChangeCharToLong(this IEnumerable<char> enumerable)
    {
        return enumerable.Select(item => long.Parse(item.ToString())).ToArray();
    }

    public static int[] ChangeStrToInt(this IEnumerable<string> enumerable)
    {
        return enumerable.Select(x => int.Parse(x)).ToArray();
    }

    public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
    {
        var queue = new Queue<T>();
        enumerable.ForEach(item => queue.Enqueue(item));
        return queue;
    }

    public static Dictionary<int, int> ConverIntListToDict(this IEnumerable<int> enumerable)
    {
        var numberDict = new Dictionary<int, int>();
        foreach (var item in enumerable)
        {
            if (numberDict.ContainsKey(item))
            {
                numberDict[item] += 1;
            }
            else
            {
                numberDict.Add(item, 1);
            }
        }
        return numberDict;
    }
}