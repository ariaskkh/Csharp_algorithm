<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = Console.ReadLine();
		int[] inputArr = ChangeStrToNum(inputNum.Split(' '));
		var N = inputArr[0];
		var M = inputArr[1];
		Console.WriteLine(Solve(N, M));
	}

	static int Solve(int N, int M)
	{
		if (N == 1)
		{
			return 1;
		}
		if (N == 2)
		{
			var result = 1 + (M - 1) / 2;
			return result > 4 ? 4 : result; 
		}
		// M = 3부터는 자유
		
		// M = 1 -> 1
		// M = 2 -> 2
		// M = 3 -> N = 3 이상일 때 3
		// M = 4 -> 4
		// M = 5 -> 4
		
		if (M < 5)
		{
			return M;
		}
		if (M == 5)
		{
			return 4;
		}
		// N이 5보다 클 때
		return 1 + 2 + (M - 5);
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


