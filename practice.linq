<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = int.Parse(Console.ReadLine());
		Solve(inputNum);
	}

	static void Solve(int n)
	{
		// (1, 1), (1, 4), (1, 7), (4, 1), (4, 4) => (i % 3 == 1) && (j % 3 == 1)
		// (3, 3), (3, 4) (3, 5), (4, 3), (4, 4), (4, 5), (5, 3), (5, 4), (5, 5) =>  (i / 3) % 3 == 1
		for (var i = 0; i < n; i++)
		{
			for (var j = 0; j < n; j++)
			{
				if (IsSpace(i, j))
				{
					Console.Write(' ');
				}
				else
				{
					Console.Write('*');
				}
			}
			Console.WriteLine();
		}
	}

	static bool IsSpace(int i, int j)
	{
		while (i > 0 && j > 0)
		{
			if ((i % 3 == 1) && (j % 3 == 1))
			{
				return true;
			}
			i /= 3;
			j /= 3;
		}
		return false;
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
				copyArr[i,j] = square[i,j];
			}
		}
		return copyArr;
	}

	static char[,] GetSquareArr(int N)
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

	static int[,] GetArr(int N, int M)
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
				Console.Write($"{result[i, j]} ");
			}
			Console.WriteLine();
		}
	}
}


