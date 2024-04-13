<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = Console.ReadLine();
		var N = int.Parse(inputNum);
		var square = getSquareArr(N);
		Console.WriteLine(solve(square, N));
	}

	static int solve(char[,] square, int N)
	{
		return getMaxNumber(square, N);
	}
	
	static int getMaxNumber(char[,] square, int N)
	{
		var totalMaxNumber = 0;
		// row 방향 변경
		for (var i = 0; i < N; i++)
		{
			for (var j = 0; j < N; j++)
			{
				var newArray = get2dArrayCopy(square);
				// 마지막 번째 중복 확인
				if (j + 1 < N)
				{
					var tmp = newArray[i, j];
					newArray[i, j] = newArray[i, j + 1];
					newArray[i, j + 1] = tmp;					
				}
				var rowMax = searchRowMaxNumber(newArray, N);
				var colMax = searchColMaxNumber(newArray, N);
				var maxNumber = Math.Max(rowMax, colMax);
				totalMaxNumber = Math.Max(totalMaxNumber, maxNumber);
			}
		}

		// column 방향 변경
		for (var i = 0; i < N; i++)
		{
			for (var j = 0; j < N; j++)
			{
				var newArray = get2dArrayCopy(square);
				if (j + 1 < N)
				{
					var tmp = newArray[j, i];
					newArray[j, i] = newArray[j + 1, i];
					newArray[j + 1, i] = tmp;
				}
				var rowMax = searchRowMaxNumber(newArray, N);
				var colMax = searchColMaxNumber(newArray, N);
				var maxNumber = Math.Max(rowMax, colMax);
				totalMaxNumber = Math.Max(totalMaxNumber, maxNumber);
			}
		}
		return totalMaxNumber;
	}
	
	static int searchRowMaxNumber(char[,] square, int N)
	{
		//Console.WriteLine(square);
		char[] colors = new char[] { 'C', 'P', 'Z', 'Y' };
		var maxNumber = 0;
		var tmp = 0;
		foreach (var color in colors)
		{
			for (var i = 0; i < N; i++)
			{
				for (var j = 0; j < N; j++)
				{
					//Console.WriteLine($"{square[i, j]}, {color}, {square[i, j] == color}");
					if (square[i, j] == color)
					{
						tmp += 1;
						maxNumber = Math.Max(maxNumber, tmp);
						continue;
					}
					tmp = 0;
				}
				tmp = 0;
			}
		}
		return maxNumber;
	}

	static int searchColMaxNumber(char[,] square, int N)
	{
		char[] colors = new char[] { 'C', 'P', 'Z', 'Y' };
		var maxNumber = 0;
		var tmp = 0;
		foreach (var color in colors)
		{
			for (var i = 0; i < N; i++)
			{
				for (var j = 0; j < N; j++)
				{
					if (square[j, i] == color)
					{
						tmp += 1;
						maxNumber = Math.Max(maxNumber, tmp);
						continue;
					}
					tmp = 0;
				}
				tmp = 0;
			}
		}
		return maxNumber;
	}

	static char[,] get2dArrayCopy(char[,] square)
	{
		int rows = square.GetLength(0);
		int cols = square.GetLength(1);
		char[,] copyArr = new char[rows, cols];
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < cols; j++)
			{
				copyArr[i,j] = square[i,j];
			}
		}
		return copyArr;
	}

	static char[,] getSquareArr(int N)
	{
		char[,] resultArr = new char[N, N];
		for (var i = 0; i < N; i++)
		{
			var tmpRowArr = Console.ReadLine().ToCharArray();
			for (var j = 0; j < N; j++)
			{
				resultArr[i, j] = tmpRowArr[j];
			}
		}
		return resultArr;
	}

	static int[,] getArr(int N, int M)
	{
		int[,] resultArr = new int[N,M];
		for (var i = 0; i < N; i++)
		{
			var tmpRowArr = Console.ReadLine().Split(' ');
			for (var j = 0; j < M; j++)
			{
				resultArr[i, j] = int.Parse(tmpRowArr[j]);
			}	
		}
		return resultArr;
	}

	static int[] changeCharToNum(char[] numsInChar)
	{
		return numsInChar.Select(x => int.Parse(x.ToString())).ToArray();
	}
	
	static int[] changeStrToNum(string[] numsInStr)
	{
		return numsInStr.Select(x => int.Parse(x)).ToArray();
	}

	static void print(int[,] result, int N, int K)
	{
		for (var i = 0; i < N; i++)
		{
			for (var j = 0; j < K; j++)
			{
				Console.Write($"{result[i, j]} ");
			}
			Console.WriteLine();
		}
	}
}


