using System.Linq;
using static System.Console;

class Program
{
	static void Main(string[] args)
	{
		List<string> passwordList = new();
		while (true)
		{
			var password = ReadLine();
            if (password == "end")
				break;
			passwordList.Add(password);
        }
		solve(passwordList);
	}

	static void solve(List<string> passwordList)
	{
		PasswordData[] passwordArray = passwordList.Select(password => new PasswordData(password)).ToArray();
		var generator = new PasswordGenerator();
        foreach (var password in passwordArray)
		{
			if (generator.CheckPasswordQuality(password))
			{
				WriteLine($"<{password.Password}> is acceptable.");
			}
			else
			{
                WriteLine($"<{password.Password}> is not acceptable.");
            }
		}
    }

	interface IPasswordGenerator<T> // 이름
	{
		bool CheckHavingVowel(T password);
		bool CheckCharacterContinuousDuplication(T password);
		bool CheckConsonantOrVowelContinuousDuplication(T password);

    }

	class PasswordGenerator : IPasswordGenerator<PasswordData>
    {
        private char[] VowelArray = new char[] { 'a', 'e', 'i', 'o', 'u' };
        public bool CheckPasswordQuality(PasswordData password)
		{
			if (CheckHavingVowel(password)
				&& CheckCharacterContinuousDuplication(password)
				&& CheckConsonantOrVowelContinuousDuplication(password))
            {
                return true;
			}
			return false;
		}

        public bool CheckHavingVowel(PasswordData password)
		{
			var vowelCount = password.Password.ToCharArray().Select(ch => VowelArray.Contains(ch)).Where(ch => ch).Count();
            bool hasGoodQuality = vowelCount > 0;
			return hasGoodQuality;
        }

		public bool CheckCharacterContinuousDuplication(PasswordData password)
		{
            var passwordArray = password.Password.ToCharArray();
			char previousChar = default;
			bool hasGoodQuality = true;
			foreach (var currentChar in passwordArray)
			{
				if ((previousChar == currentChar) && (currentChar != 'e' && currentChar != 'o'))
				{
                    hasGoodQuality = false;
				}
				previousChar = currentChar;
			}
			return hasGoodQuality;
        }

		public bool CheckConsonantOrVowelContinuousDuplication(PasswordData password)
		{
            var passwordArray = password.Password.ToCharArray();
			bool wasPreviousVowel = false;
			var vowelCount = 0;
			var consonantCount = 0;
			bool hasGoodQuality = true;

			foreach (var currentChar in passwordArray)
			{

				if (IsVowel(currentChar)) // 모음
				{
					if (wasPreviousVowel == true) // 연속
					{
                        vowelCount++;
                        if (vowelCount >= 3)
                        {
                            hasGoodQuality = false;
                        }
                    } 
					else
					{
						wasPreviousVowel = true;
                        vowelCount = 1;
						consonantCount = 0;
                    }
                }
				else // 자음
				{
					if (wasPreviousVowel == false) // 연속
					{
                        consonantCount++;
                        if (consonantCount >= 3)
                        {
                            hasGoodQuality = false;
                        }
                    }
					else
					{
						wasPreviousVowel = false;
                        consonantCount = 1;
						vowelCount = 0;
                    }
				}
            }
			return hasGoodQuality;
        }
        private bool IsVowel(char ch)
        {
            return VowelArray.Contains(ch);
        }

    }

	// quality 기준이 바뀔 수 있기 때문에 PasswordData에 check로직이 들어가는 건 적절하지 않은 것 같음.
	class PasswordData
	{
		public string Password { get; }
        public PasswordData(string password)
		{
			Password = password;
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
}