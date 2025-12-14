using static System.Console;

class Program
{
	static void Main(string[] args)
	{
		const int INPUT_COUNT = 6;
		var numberPerUnit = int.Parse(ReadLine());
		var melonAreaDatas = Enumerable.Range(0, INPUT_COUNT)
			.Select(_  => ReadLine()
				.Split(' ').Select(int.Parse).ToArray())
				.Select(x => new DirectionData(GetDirection(x[0]), x[1]))
			.ToList();
		Solve(numberPerUnit, melonAreaDatas);
	}

	
	static void Solve(int numberPerUnit, List<DirectionData> directionDataList)
	{
//		directionDataList.Dump();
		var areaService = new MelonAreaService(numberPerUnit, directionDataList);
		var count = areaService.GetTotalMelonCount();
		WriteLine(count);
	}
	
	static Direction GetDirection(int direction)
	{
		return direction switch 
		{
			1 => Direction.East,
			2 => Direction.West,
			3 => Direction.South,
			4 => Direction.North,
			_ => throw new Exception("GetDirection - invalid direction")
		};
	}
}

class MelonAreaService
{
	public List<DirectionData> _directionDataList = new();
	private int _numberPerUnit;
	
	public MelonAreaService(int numberPerUnit, List<DirectionData> directionDataList)
	{
		if (directionDataList.Count != 6)
			throw new Exception($"directionDataList must be 6. but it is {directionDataList.Count}");
		_numberPerUnit = numberPerUnit;
		_directionDataList = directionDataList;
	}
	
	public int GetTotalMelonCount()
	{
		return _numberPerUnit * GetArea();
	}
	
	private int GetArea()
	{	
		var bigWidth = 0;
		var bigHeight = 0;
		
		var newDirectionDataList = _directionDataList.ToList();
		newDirectionDataList.AddRange(_directionDataList);
		var directions = newDirectionDataList.Select(d => d.Direction).ToList();
		var smallWidth = 0;
		var smallHeight = 0;
		for (int i = 0; i < 6; i++)
		{
			if (directions[i] == directions[(i + 2) % 6] && directions[i+1] == directions[(i + 3) % 6])
			{
//				$"i: {i}".Dump();
				smallWidth = newDirectionDataList[(i + 1) % 6].Distance;
				smallHeight = newDirectionDataList[(i + 2) % 6].Distance;
				bigWidth = newDirectionDataList[(i + 4) % 6].Distance;
				bigHeight = newDirectionDataList[(i + 5) % 6].Distance;
				break;
			}
		}
		
		var bigArea = bigWidth * bigHeight;
//		bigWidth.Dump();
//		bigHeight.Dump();
//		bigArea.Dump();
//		"".Dump();
		var smallArea = smallWidth * smallHeight;
//		smallWidth.Dump();
//		smallHeight.Dump();
//		smallArea.Dump();
//		"".Dump();
		return bigArea - smallArea;
	}
}

class DirectionData
{
	public Direction Direction { get; set; }
	public int Distance { get; set; }
	
	public DirectionData(Direction direction, int distance)
	{
		Direction = direction;
		Distance = distance;
	}
}

enum Direction
{
	East = 1,
	West = 2,
	South = 3,
	North = 4
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