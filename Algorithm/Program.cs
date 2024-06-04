using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var grageList = Enumerable.Range(0, 20)
            .Select(_ => ReadLine().Split(' '))
            .Select(input => new GradeData(input[0], double.Parse(input[1]), input[2]))
            .ToList();
        Solve(grageList);
    }

    static void Solve(List<GradeData> gradeList)
    {
        var calculator = new GradeCalculator();
        WriteLine(calculator.GetAverage(gradeList));
    }

    private class GradeCalculator
    {
        public double GetAverage(List<GradeData> gradeList)
        {
            return gradeList
                .Where(data => data.Grade != "P")
                .Aggregate(
                (weightSum: 0.0, weightedGradeSum: 0.0),
                ((acc, curr) =>
                {
                    var nextWeightSum = acc.weightSum + curr.GradeWeight;
                    var nextWeightedGradeSum = acc.weightedGradeSum + curr.GradeWeight * SubjectGrade.GetSubjectGrade(curr.Grade);
                    return (nextWeightSum, nextWeightedGradeSum);
                }),
                (result => (result.weightedGradeSum / result.weightSum))
                );

            //var weightedGradeSum = gradeList
            //     .Where(data => data.Grade != "P")
            //     .Sum(data => data.GradeWeight * SubjectGrade.GetSubjectGrade(data.Grade));

            //var weightSum = gradeList
            //    .Where(data => data.Grade != "P")
            //    .Sum(data => data.GradeWeight);

            //return weightedGradeSum / weightSum;
        }
    }



    // double이라 enum이 안됨
    private static class SubjectGrade
    {
        public const double F = 0.0;
        public const double D0 = 1.0;
        public const double DPlus = 1.5;
        public const double C0 = 2.0;
        public const double CPlus = 2.5;
        public const double B0 = 3.0;
        public const double BPlus = 3.5;
        public const double A0 = 4.0;
        public const double APlus = 4.5;

        public static double GetSubjectGrade(string grade)
        {
            switch (grade)
            {
                case "F":
                    return F;
                case "D0":
                    return D0;
                case "D+":
                    return DPlus;
                case "C0":
                    return C0;
                case "C+":
                    return CPlus;
                case "B0":
                    return B0;
                case "B+":
                    return BPlus;
                case "A0":
                    return A0;
                case "A+":
                    return APlus;
                default:
                    return 0;
            }
        }
    }

    private class GradeData
    {
        public string Subject { get; set; }
        public double GradeWeight { get; set; }
        public string Grade { get; set; }

        public GradeData(string subject, double gradeWeight, string grade)
        {
            Subject = subject;
            GradeWeight = gradeWeight;
            Grade = grade;
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