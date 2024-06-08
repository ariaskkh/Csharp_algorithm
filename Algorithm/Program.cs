using System.Text;
using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var input = ReadLine().Split(' ').ChangeStrToInt();
        var nationNumber = input[0];
        var targetNationIndex = input[1];
        var nationList = Enumerable.Range(0, nationNumber)
            .Select(_ => ReadLine().Split(' ').ChangeStrToInt())
            .Select(data => new Nation(data[0], data[1], data[2], data[3]))  // index, 금, 은, 동
            .ToList();
        var targetNation = Solve(nationList, targetNationIndex);
        WriteLine(targetNation.Rank);
    }

    static Nation Solve(List<Nation> nationList, int targetNationIndex)
    {
        var calculator = new OlympicRankCalculator();
        calculator.Calculate(nationList);
        return calculator.GetTargetNation(targetNationIndex);
        
    }

    class OlympicRankCalculator
    {
        private List<Nation> updatedNationList;
        public void Calculate(List<Nation> nationList)
        {
            updatedNationList = nationList
                .OrderByDescending(nation => nation.GoldMedal)
                .ThenByDescending(nation => nation.SilverMedal)
                .ThenByDescending(nation => nation.BronzeMedal)
                .Aggregate(
                (nation: default(Nation), count: 1), // initial value
                (acc, curr) =>
                { // func
                    if (acc.nation == null)
                    {
                        curr.Rank = 1;
                    }
                    else
                    {
                        if (HasSameRank(acc.nation, curr))
                        {
                            curr.Rank = acc.nation.Rank;
                            acc.count++;
                        }
                        else
                        {
                            curr.Rank = acc.nation.Rank + acc.count;
                            acc.count = 1;
                        }
                    }
                    return (curr, acc.count);
                },
                (_ => nationList) // result selector
                );
        }


        bool HasSameRank(Nation prevNation, Nation currNation)
        {
            return prevNation.GoldMedal == currNation.GoldMedal
                && prevNation.SilverMedal == currNation.SilverMedal
                && prevNation.BronzeMedal == currNation.BronzeMedal;
        }

        public Nation GetTargetNation(int nationIndex)
        {
            return updatedNationList.FirstOrDefault(nation => nation.Index == nationIndex);
        }
    }

    class Nation
    {
        public int Rank { get; set; }
        public int Index { get; }
        public int GoldMedal { get; }
        public int SilverMedal { get; }
        public int BronzeMedal { get; }

        public Nation(int index, int goldMedal, int silverMedal, int bronzeMedal)
        {
            Index = index;
            GoldMedal = goldMedal;
            SilverMedal = silverMedal;
            BronzeMedal = bronzeMedal;
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

    public static Dictionary<int, int> ConvertToDictionary(this IEnumerable<int >enumerable)
    {
        return enumerable
            .GroupBy(height => height)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}