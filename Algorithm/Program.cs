
using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var N = int.Parse(ReadLine());
		string[] candiesArray = Enumerable.Range(0, N).Select(i => ReadLine()).ToArray();
		Solve(candiesArray, N);
    }

    static void Solve(string[] candiesArray, int N)
    {
		var bomboni = new BomboniGame(candiesArray,N);
		bomboni.ExchangeCandies();
		WriteLine(bomboni.MaxCandyCount);
    }

	class BomboniGame
	{
		private readonly CandyLine[] _candyTable;
		private readonly int N;
		public int MaxCandyCount { get; set; }
		public BomboniGame(string[] candyTable, int N)
		{
            _candyTable = candyTable.Select(candyLine => new CandyLine(candyLine)).ToArray();
			this.N = N;
        }

        public void ExchangeCandies()
		{
			var defaultCandyTableMax = _candyTable.Select(line => line.GetMaxCandyCountOfSameColor()).Max();
            MaxCandyCount = Math.Max(MaxCandyCount, defaultCandyTableMax);

            // 열방향
            for (var row = 0; row < N; row++)
			{
				for (var column = 0; column < N - 1; column++)
				{
					var newCandyTable = swap(_candyTable, row, column, row, column + 1);
					var max = newCandyTable.Select(line => line.GetMaxCandyCountOfSameColor()).Max();

                    var reversedCandyTable = getReversedCandyPlate(newCandyTable);
                    var reversedMax = reversedCandyTable.Select(line => line.GetMaxCandyCountOfSameColor()).Max();

                    MaxCandyCount = Math.Max(MaxCandyCount, Math.Max(max, reversedMax));
                }
			}

			// 행 방향
            for (var column = 0; column < N; column++)
            {
                for (var row = 0; row < N - 1; row++)
                {
                    var newCandyTable = swap(_candyTable, row, column, row + 1, column);
                    var max = newCandyTable.Select(line => line.GetMaxCandyCountOfSameColor()).Max();

					var reversedCandyTable = getReversedCandyPlate(newCandyTable);
					var reversedMax = reversedCandyTable.Select(line => line.GetMaxCandyCountOfSameColor()).Max();

                    MaxCandyCount = Math.Max(MaxCandyCount, Math.Max(max, reversedMax));
                }
            }
        }


        private CandyLine[] getReversedCandyPlate(CandyLine[] array)
		{
			var newArray = GetCopyOfCandyArray(array);
			for (var i = 0; i < newArray.Length; i++)
			{
				for (var j = i + 1; j < newArray.Length; j++)
				{
					char tmpCandy = newArray[i].Line[j];
					newArray[i].Line[j] = newArray[j].Line[i];
					newArray[j].Line[i] = tmpCandy;
				}
			}
			return newArray;
		}

        private CandyLine[] swap(CandyLine[] array, int x1, int y1, int x2, int y2)
        {
			var newArray = GetCopyOfCandyArray(array);
            var tmpCharacter = newArray[x1].Line[y1];
            newArray[x1].Line[y1] = newArray[x2].Line[y2];
            newArray[x2].Line[y2] = tmpCharacter;
            return newArray;
        }

		private CandyLine[] GetCopyOfCandyArray(CandyLine[] array)
		{
			var newCandyLine = new CandyLine[array.Length];
			for (var i = 0; i < array.Length; i++)
			{
                newCandyLine[i] = new CandyLine(string.Join("", array[i].Line));
			}
			return newCandyLine;

		}
    }

	class CandyLine
	{
        private readonly static char[] colors = new char[] { 'C', 'P', 'Z', 'Y' };
        public char[] Line { get; set; }
		public int LineLength => Line.Length;
		private int _maxCountCache = 0;
		public CandyLine(string line)
		{
			Line = line.ToCharArray();
		}

		public int GetMaxCandyCountOfSameColor()
		{
			if (_maxCountCache > 0)
				return _maxCountCache;

			var maxCount = colors.Select(color => Line.Aggregate(
				(count: 0, tmpCount: 0), // init
				((acc, curr) =>
				{
					if (curr == color)
					{
						acc.tmpCount++;
						return (Math.Max(acc.tmpCount, acc.count), acc.tmpCount);
					}
					else
					{
						acc.tmpCount = 0;
						return acc;
					}
				}), // func
				(item => item.count) // result selector
				)
			).Max();

			_maxCountCache = maxCount;
			return maxCount;
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