﻿using static System.Console;

class Program
{
	static void Main(string[] args)
	{
		var N = int.Parse(ReadLine());
		List<Confetto> confettoList = getConfettoList(N);
		solve(N, confettoList);
	}
	static List<Confetto> getConfettoList(int N)
	{
		List<Confetto> confettoList = new();
		for (var i = 0; i < N; i++)
		{
			var coordinate = ChangeStrToNum(ReadLine().Split(' '));
			confettoList.Add(new Confetto(coordinate[0], coordinate[1]));
		}
		return confettoList;
	}

	static void solve(int N, List<Confetto> confettiList)
	{
		var paper = new Paper();
		paper.PutConfetti(confettiList);
		WriteLine(paper.GetSumOfCoveredArea());
	}

	class Paper
	{
		private const int Width = 100;
		private const int Height = 100;

		private bool[][] ConfettoCoverCheck = Enumerable.Range(0, Width).Select(_ => Enumerable.Repeat(false, Height).ToArray()).ToArray();


		public void PutConfetti(List<Confetto> confettoList)
		{
			var n1 = Enumerable.Range(0, Width);
			var n2 = Enumerable.Range(0, Height);
			IterationFunction(n1, n2).Where(item => !ConfettoCoverCheck[item.Item1][item.Item2])
					.ForEach(item => confettoList.ForEach(confetto => CheckCoveredSpot(item.Item1, item.Item2, confetto.CoordinateX, confetto.CoordinateY)));
            
			void CheckCoveredSpot(int x, int y, int confettoX, int confettoY)
            {
                if ((x >= confettoX && x < confettoX + Confetto.Width) && (y >= confettoY && y < confettoY + Confetto.Heihgt))
                {
                    ConfettoCoverCheck[x][y] = true;
                }
            }
        }

		public int GetSumOfCoveredArea()
		{
			return ConfettoCoverCheck.Select(row => row.Where(unitArea => unitArea).Count()).Sum();
		}
    }

	class Confetto
	{
		public static int Width = 10;
		public static int Heihgt = 10;
		public int CoordinateX { get; set;}
        public int CoordinateY { get; set; }

        public Confetto(int x, int y)
		{
			CoordinateX = x;
            CoordinateY = y;
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