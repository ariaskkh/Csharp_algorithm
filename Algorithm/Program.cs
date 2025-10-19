using static System.Console;

class Program
{
	static void Main(string[] args)
	{
		var input = ReadLine().ToCharArray();
		var word = new CroatiaWord(input);	
		Solve(word);
	}
	
	private static void Solve(CroatiaWord word)
	{
		var handlers = new List<ICharacterHandler> ()
		{
			new CharacterJHandler(),
			new DashHandler(),
			new EqualHandler(),
			new NormalCharacterHandler()
		};
		var translator = new CroatiaTranslator(handlers);
		var updatedWord = translator.Translate(word);
		var result = updatedWord.CroatiaAlphabetList.Count();
		WriteLine(result);
	}
}

public class CroatiaWord
{
	public char[] ConvertedWord { get; private init; }
	public List<string> CroatiaAlphabetList { get; set; } = new();
	
	public CroatiaWord(char[] convertedWord)
	{
		ConvertedWord = convertedWord;
	}
}

public interface ITranslator
{
	public CroatiaWord Translate(CroatiaWord word);
}

public class CroatiaTranslator : ITranslator
{
	List<ICharacterHandler> _handlers;
	public CroatiaTranslator(List<ICharacterHandler> handlers)
	{
		_handlers = handlers;
	}
	
	public CroatiaWord Translate(CroatiaWord word)
	{
		var convertedWord = word.ConvertedWord;
		if (convertedWord.Count() <= 0)
			throw new Exception("word is invalid");
			
		var croatiaAlphabetList = new List<string>();

		foreach (var pair in convertedWord.Select((ch, i) => new {ch, i}))
		{
			var i = pair.i;
			var ch = pair.ch;
			if (i == 0)
			{
				croatiaAlphabetList.Add(ch.ToString());
			}
			else
			{
				foreach (var handler in _handlers.Where(handler => handler.CanHandle(convertedWord, i)))
				{
					handler.Handle(convertedWord, i, croatiaAlphabetList);
				}
			}
		}
		
		word.CroatiaAlphabetList = croatiaAlphabetList;
		return word;
	}
}

public interface ICharacterHandler
{
	public bool CanHandle(char[] convertedWord, int index);
	public void Handle(char[] convertedWord, int index, List<string> alphabetList);
}

public class CharacterJHandler : ICharacterHandler
{
	private const char _identifier = 'j';
	
	public bool CanHandle(char[] convertedWord, int index)
	{
		return convertedWord[index] == _identifier;
	}
	
	public void Handle(char[] convertedWord, int index, List<string> alphabetList)
	{
		// lj, nj
		if (index > 0
			&& (convertedWord[index - 1] == 'l' || convertedWord[index - 1] == 'n'))
		{
			alphabetList[alphabetList.Count - 1] = alphabetList[alphabetList.Count - 1] + _identifier;
		}
		else
		{
			alphabetList.Add(convertedWord[index].ToString());
		}
	}
}

public class DashHandler : ICharacterHandler
{
	private const char _identifier = '-';
	
	public bool CanHandle(char[] convertedWord, int index)
	{
		return convertedWord[index] == _identifier;
	}
	
	public void Handle(char[] convertedWord, int index, List<string> alphabetList)
	{
		// c-, d-
		if (index > 0
			&& (convertedWord[index - 1] == 'c' || convertedWord[index - 1] == 'd'))
		{
			alphabetList[alphabetList.Count - 1] = alphabetList[alphabetList.Count - 1] + _identifier;
		}
		else
		{
			alphabetList.Add(convertedWord[index].ToString());
		}
	}
}


public class EqualHandler : ICharacterHandler
{
	private const char _identifier = '=';
	public bool CanHandle(char[] convertedWord, int index)
	{
		return convertedWord[index] == _identifier;
	}

	public void Handle(char[] convertedWord, int index, List<string> alphabetList)
	{
		// dz=, s=, z=
		if (index <= 0)
			return;
		
		if (index > 1 && convertedWord[index - 2] == 'd' && convertedWord[index - 1] == 'z')
		{
			alphabetList.RemoveRange(alphabetList.Count - 2, 2);
			alphabetList.Add("dz=");
		}
		else if (convertedWord[index - 1] == 's' || convertedWord[index - 1] == 'z')
		{
			alphabetList[alphabetList.Count - 1] = alphabetList[alphabetList.Count - 1] + _identifier;
		}
	}
}

public class NormalCharacterHandler : ICharacterHandler
{
	public bool CanHandle(char[] convertedWord, int index)
	{
		var identifier = convertedWord[index];
		return identifier != '='
			&& identifier != '-'
			&& identifier != 'j'
			;
	}
	
	public void Handle(char[] convertedWord, int index, List<string> alphabetList)
	{
		if (index <= 0)
			return;
			
		alphabetList.Add(convertedWord[index].ToString());
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