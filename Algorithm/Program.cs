using static System.Console;
using System.Text;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var N = int.Parse(ReadLine());
        var numberList = new List<int>();
        Enumerable.Range(0, N).ForEach(_ => numberList.Add(int.Parse(ReadLine())));

        Solve(numberList);
    }

    static void Solve(List<int> numberList)
    {
        StringBuilder st = new StringBuilder();
        st.AppendLine(StaticalCalculator.GetMean(numberList).ToString());
        st.AppendLine(StaticalCalculator.GetMedian(numberList).ToString());
        st.AppendLine(StaticalCalculator.GetMode(numberList).ToString());
        st.AppendLine(StaticalCalculator.GetRange(numberList).ToString());

        WriteLine(st.ToString());
    }

    static class StaticalCalculator
    {

        public static int GetMean(List<int> numberList) // 평균
        {
            return (int)Math.Round(numberList.Average());
        }

        public static int GetMedian(List<int> numberList) // 중앙값
        {
            var newNumberList = numberList.OrderBy(number => number).ToList();
            var medianIndex = (newNumberList.Count - 1) / 2;
            return newNumberList[medianIndex];
        }

        public static int GetMode(List<int> numberList) // 최빈값
        {
            var numberDict = numberList.ConverIntListToDict();
            List<int> maxNumberList = GetMaxNumberList(numberDict, numberDict.Values.Max());
            return maxNumberList.Count > 1 ? maxNumberList[1] : maxNumberList[0];
        }

        public static int GetRange(List<int> numberList) // 범위
        {
            return numberList.Max() - numberList.Min();
        }

        static private List<int> GetMaxNumberList(Dictionary<int, int> numberDict, int maxValue)
        {
            return numberDict.Where(keyValue => keyValue.Value == maxValue)
                .Select(keyValue => keyValue.Key)
                .OrderBy(number => number)
                .ToList();
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