using static System.Console;

namespace Algorithm;
class Program
{
    static void Main(string[] args)
    {
        var input = ReadLine().Split(' ');
        var N = int.Parse(input[0].ToString());
        var KthNumberNeedToRemove = int.Parse(input[1].ToString());
        Write(Solve(N, KthNumberNeedToRemove));
    }


    static int Solve(int N, int KthNumberNeedToRemove)
    {
        Sieve sieve = new Sieve(N);
        return sieve.GetCountOfKthNumberRemoved(KthNumberNeedToRemove);
    }

    public class Sieve
    {

        private int _count = 0;
		private List<int> _primeNumberList = new(); // 이 문제에선 사용하지 않지만 생성
        private readonly bool[] _boolArray;
        public Sieve(int N)
        {
            _boolArray = new bool[N + 1];
            _boolArray[0] = _boolArray[1] = true;
        }

        public int GetCountOfKthNumberRemoved(int KthNumber)
        {
            for (var number = 2; number <= _boolArray.Length - 1; number++)
            {
                // Prime 일 때
                if (_boolArray[number] == false)
                {
					_primeNumberList.Add(number);
                    // Prime 배수 처리
                    foreach (var multiple in GetMultipleNumber(_boolArray.Length - 1, number))
					{
                        if (_boolArray[multiple] == false)
                        {
                            _boolArray[multiple] = true;
                            _count++;

                            if (_count == KthNumber)
                            {
                                return multiple;
                            }
                        }
                    }
				}
            }
            return -1;
        }

        // 배수 처리에 대한 걸 yield return 할 수 있나?
        static IEnumerable<int> GetMultipleNumber(int maxLength, int primeNumber)
        {
            int multiple = 1;
            while (primeNumber * multiple <= maxLength)
            {
                yield return primeNumber * multiple;
                multiple++;
            }
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