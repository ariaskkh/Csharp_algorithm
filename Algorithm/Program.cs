using static System.Console;

class Program
{
	static void Main(string[] args)
	{
		var gradeInputs = Enumerable.Range(0, 20)
			.Select(_ => ReadLine().Split(' '))
			.Select(i => new GradeData(i[0], double.Parse(i[1]), i[2]))
			.ToList();
		var result = GradeCalculator.GetAverage(gradeInputs);
		WriteLine(result);
	}
}

public static class GradeCalculator
{
	public static double GetAverage(List<GradeData> gradeDataList)
	{
		var garaDataListWithoutP = gradeDataList.Where(d => d.SubjectScore != "P").ToList();
		var totalWeight = garaDataListWithoutP
				.Sum(d => d.SubjectWeight);
		var scoreSum = garaDataListWithoutP
			.Select(d => d.SubjectWeight * d.SubjectScoreNumber)
				.Sum();
		return scoreSum / totalWeight;
	}
}

public class GradeData
{
	private readonly static double AP = 4.5;
	private readonly static double A0 = 4.0;
	private readonly static double BP = 3.5;
	private readonly static double B0 = 3.0;
	private readonly static double CP = 2.5;
	private readonly static double C0 = 2.0;
	private readonly static double DP = 1.5;
	private readonly static double D0 = 1.0;
	private readonly static double F = 0.0;
	private readonly static double P = -1.0;

	public GradeData(string subjectName, double subjectWeight, string subjectScore)
	{
		SubjectName = subjectName;
		SubjectWeight = subjectWeight;
		SubjectScore = subjectScore;
	}

	public string SubjectName { get; init; }
	public double SubjectWeight { get; init; }
	public string SubjectScore { get; init; }
	public double SubjectScoreNumber => SubjectScore switch
	{
		"A+" => AP,
		"A0" => A0,
		"B+" => BP,
		"B0" => B0,
		"C+" => CP,
		"C0" => C0,
		"D+" => DP,
		"D0" => D0,
		"F" => F,
		"P" => P,
		_ => 0.0
	};
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

    public static Dictionary<int, int> ConvertToDictionary(this IEnumerable<int> enumerable)
    {
        return enumerable
            .GroupBy(height => height)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}

public class Result<T>
{
    public string Error { get; set; }
    public T Content { get; set; }

    public bool HasContent { get; set; } = false;

    public static Result<T> Success(T content)
    {
        var result = new Result<T>();
        result.Content = content;
        result.HasContent = true;
        return result;
    }

    public static Result<T> Fail(string error = "error")
    {
        var result = new Result<T>();
        result.Error = error;
        return result;
    }

    public bool IsSuccess()
    {
        return Error == null;
    }

    public bool IsError()
    {
        return Error != null;
    }
}