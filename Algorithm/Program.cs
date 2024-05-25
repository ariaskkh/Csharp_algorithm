
using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        string word = ReadLine();
		Solve(word);
    }

    static void Solve(string word)
    {
		var converter = new WordConverter();
		converter.ConvertWord(word);
		WriteLine(converter.FirstAlphabeticalWord);
    }

	// 단어를 3가지로 나누는 모든 경우의 수를 구한다
	// 나눈 단어를 뒤집어 합친다.
	// 사전 순으로 가장 앞서는 단어를 저장해서 갱신한다 - string.Compare(a,b) a가 앞서는 경우 => -1
	class WordConverter
	{
		private string _originalWord = string.Empty;
        public string FirstAlphabeticalWord { get; set; } = string.Empty;

		public void ConvertWord(string word)
		{
			_originalWord = word;
            List<ConvertedWord> convertedWordList = GetConvertedWordList(word);
            FirstAlphabeticalWord = GetFirstAlphabeticalWord(convertedWordList);
		}
        
		private static List<ConvertedWord> GetConvertedWordList(string word)
		{
			var N = word.ToCharArray().Length;
			List<ConvertedWord> newWordList = new();
            for (var i = 1; i < N - 1; i++)
			{
				for (var j = i + 1; j < N; j++)
				{
					// 최소 1개 char 필요
					var subWordArray = new string[] {
						word[..i],
						word[i..j],
						word[j..],
						};
                    newWordList.Add(new ConvertedWord(subWordArray));
                }
			}
			return newWordList;
        }

		private static string GetFirstAlphabeticalWord(List<ConvertedWord> wordList)
		{
           ConvertedWord? firstAlphabeticalWord = null;
            foreach (ConvertedWord word in wordList)
            {
                if (firstAlphabeticalWord == null)
                {
                    firstAlphabeticalWord = word;
                }
                else
                {
                    if (string.Compare(firstAlphabeticalWord.WholeConvertedWord, word.WholeConvertedWord) > 0) // 우측이 알파벳 순서상 앞(작음)
                    {
						firstAlphabeticalWord = word;
					};
                }
            }
            return firstAlphabeticalWord?.WholeConvertedWord ?? string.Empty;
        }
    }

	interface IConvert
	{
		string Convert(string word);
	}

	class ConvertedWord : IConvert
    {
		public List<string> ConvertedSubWordList { get; } = new();
		public string WholeConvertedWord { get; }

		public ConvertedWord(string[] subWordList)
		{
			foreach (string word in subWordList)
			{
                ConvertedSubWordList.Add(Convert(word));
            }
			WholeConvertedWord = string.Join("", ConvertedSubWordList);
		}

		public string Convert(string word)
		{
			var convertedWordList = word.ToCharArray().Reverse();
			return string.Join("", convertedWordList);
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
}