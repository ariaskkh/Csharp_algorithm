using static System.Console;

class Program
{
	static void Main(string[] args)
	{
		var input = ReadLine().Split(' ').ChangeStrToInt();
		var sero = input[0];
		var garo = input[1];
		var blocksAvailable = input[2];
		
		// int[][]
		var groundTable = Enumerable.Range(0, sero).Select(_ => ReadLine().Split(' ').ChangeStrToInt()).ToArray();
		var ground = new Ground(groundTable);
		var user = new User(blocksAvailable);
		Solve(ground, user);
	}
	
	static void Solve(Ground ground, User user)
	{
		var flatteningData = ground.GetFlatteningData(user);
		if (flatteningData is not null)
		{
			WriteLine($"{flatteningData.TimeNeeded} {flatteningData.Height}");
		}
	}
}

public class User
{
	public int BlocksAvailable { get; init; }
	
	public User(int blocksAvailable)
	{
		BlocksAvailable = blocksAvailable;
	}
}

class Ground
{
	public int[][] _originalGroundHeight;
	public Dictionary<int, int> _groundHeightDict;
	private int _FlattenedGroundheight = 0;
	private int _minHeight;
	private int _maxHeight;
	
	public Ground(int[][] groundHeight)
	{
		_originalGroundHeight = groundHeight;
		_groundHeightDict = GetGroundHeightDict();
		_minHeight = _groundHeightDict.Keys.Min();
		_maxHeight = _groundHeightDict.Keys.Max();
	}
	
	private Dictionary<int, int> GetGroundHeightDict()
	{
		return _originalGroundHeight
					.SelectMany(h => h)
					.GroupBy(h => h)
					.ToDictionary(g => g.Key, g => g.Count());
	}
	
	public FlatteningResult GetFlatteningData(User user)
	{
		var flatteningResult = Enumerable.Range(_minHeight, _maxHeight - _minHeight + 1)
				.Select(h => CalculateFlatteningData(h, user.BlocksAvailable))
				.Where(data => data.SufficientBlock)
				.OrderBy(data => data.TimeNeeded)
				.ThenByDescending(data => data.Height)
				.FirstOrDefault();
		_FlattenedGroundheight = flatteningResult.Height;
		return flatteningResult;
	}
	
	private FlatteningResult CalculateFlatteningData(int targetHeight, int blocksAvailable)
	{
		var digNeeded = GetNumberOfDig(targetHeight);
		var fillNeeded = GetNumberOfFill(targetHeight);
		
		return new FlatteningResult
		{
			Dig = digNeeded,
			Fill = fillNeeded,
			SufficientBlock = digNeeded + blocksAvailable >= fillNeeded,
			TimeNeeded = digNeeded * 2 + fillNeeded,
			Height = targetHeight,
		};
	}
	
	private int GetNumberOfDig(int targetHeight)
	{
		return _groundHeightDict
					.Select(h => h.Key)
					.Where(h => h > targetHeight)
					.Sum(h => (h - targetHeight) * _groundHeightDict[h]);
	}
	
	private int GetNumberOfFill(int targetHeight)
	{
		return _groundHeightDict
					.Select(h => h.Key)
					.Where(h => h < targetHeight)
					.Sum(h => (targetHeight - h) * _groundHeightDict[h]);
	}
}

public class FlatteningResult
{
	public int Dig { get; init; }
	public int Fill { get; init; }
	public bool SufficientBlock { get; init; }
	public int TimeNeeded { get; init; }
	public int Height { get; init; }
}

/////////////////////////////  util 함수  ////////////////////////////////
public static class Utils
{
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