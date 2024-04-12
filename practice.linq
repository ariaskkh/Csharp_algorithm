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
		var rowMaxNumber = searchRowMaxNumber(square, N);
		var columnMaxNumber = searchColumnMaxNumber(square, N);
		return Math.Max(rowMaxNumber, columnMaxNumber);
	}

	static int searchRowMaxNumber(char[,] square, int N)
	{
		char[] colors = new char[] { 'C', 'P', 'Z', 'Y' };
		var maxNumber = 0;
		foreach (var color in colors)
		{
			for (var i = 0; i < N; i++)
			{
				var swapNumber = 1;
				var tmpMaxNumber = 0;
				for (var j = 0; j < N; j++)
				{

					// 같은 색인 경우
					if (square[i, j] == color)
					{
						tmpMaxNumber += 1;
						continue;
					}
					// 다른 색인 경우 swap 가능한지 확인
					if (swapNumber == 1)
					{
						if (i - 1 >= 0 && square[i - 1, j] == color)
						{
							tmpMaxNumber += 1;
							swapNumber -= 1;
							continue;
						}
						if (i + 1 < N && square[i + 1, j] == color)
						{
							tmpMaxNumber += 1;
							swapNumber -= 1;
							continue;
						}
					}
					break;
				}
				maxNumber = Math.Max(maxNumber, tmpMaxNumber);
			}
		}
		return maxNumber;
	}

	static int searchColumnMaxNumber(char[,] square, int N)
	{
		char[] colors = new char[] { 'C', 'P', 'Z', 'Y' };
		var maxNumber = 0;
		foreach (var color in colors)
		{
			for (var i = 0; i < N; i++)
			{
				var swapNumber = 1;
				var tmpMaxNumber = 0;
				for (var j = 0; j < N; j++)
				{
					// 같은 색인 경우
					if (square[j, i] == color)
					{
						tmpMaxNumber += 1;
						continue;
					}
					// 다른 색인 경우 swap 가능한지 확인
					if (swapNumber == 1)
					{
						if (i - 1 >= 0 && square[j, i - 1] == color)
						{
							tmpMaxNumber += 1;
							swapNumber -= 1;
							continue;
						}
						if (i + 1 < N && square[j, i + 1] == color)
						{
							tmpMaxNumber += 1;
							swapNumber -= 1;
							continue;
						}
					}
					break;
				}
				maxNumber = Math.Max(maxNumber, tmpMaxNumber);
			}
		}
		return maxNumber;
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


