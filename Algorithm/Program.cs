using static System.Console;
using System.Text;

class Program
{
	static void Main(string[] args)
	{
		var input = ReadLine().Split(' ').Select(int.Parse).ToList();
		var screenWidth = input[0];
		var basketWidth = input[1];
		
		var n = int.Parse(ReadLine());
		var positions = Enumerable.Range(0, n).Select(_ => int.Parse(ReadLine())).ToList();
		Solve(new Screen(screenWidth), new Basket(basketWidth), positions);
	}
	
	static void Solve(Screen screen, Basket basket, List<int> positions)
	{
		var service = new AppleDropService(screen, basket);
		service.DropApples(positions);
		var count = service.GetMinMoveCount();
		WriteLine(count);
	}
}


class AppleDropService
{
	private int _leftBasketPosition = 0;
	private int _rightBasketPosition;
	private Screen _screen;
	private Basket _basket;
	private int _minMoveCount = 0;
	
	public AppleDropService(Screen screen, Basket basket)
	{
		_rightBasketPosition = basket.BasketWidth;
		_screen = screen;
		_basket = basket;
	}
	
	public void DropApples(List<int> positions)
	{
		foreach (var position in positions)
		{
			var delta = 0;
			if (position > _rightBasketPosition)
			{
				delta = position - _rightBasketPosition;
				_rightBasketPosition += delta;
				_leftBasketPosition += delta;
			}
			else if (position < _leftBasketPosition + 1)
			{
				delta = _leftBasketPosition + 1 - position;
				_leftBasketPosition -= delta;
				_rightBasketPosition -= delta;
			}
			_minMoveCount += delta;
		}
	}
	
	public int GetMinMoveCount()
	{
		return _minMoveCount;
	}
}

class Basket
{
	public int BasketWidth { get; init; }
	
	public Basket(int basketWidth)
	{
		BasketWidth = basketWidth;
	}
}

class Screen
{
	private int _screenWidth;
	
	public Screen(int screenWidth)
	{
		_screenWidth = screenWidth;
	}
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