<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = Console.ReadLine();
		var N = int.Parse(inputNum);
		var square = GetSquareArr(N);
		Console.WriteLine(Solve(square, N));
	}

	static int Solve(char[,] square, int N)
	{
		return GetMaxNumber(square, N);
	}
	
	static int GetMaxNumber(char[,] square, int N)
	{
		var totalMaxNumber = 0;
		// row 방향 변경
		for (var i = 0; i < N; i++)
		{
			for (var j = 0; j < N; j++)
			{
				var newArray = Get2dArrayCopy(square);
				// 마지막 번째 중복 확인
				if (j + 1 < N)
				{
					var tmp = newArray[i, j];
					newArray[i, j] = newArray[i, j + 1];
					newArray[i, j + 1] = tmp;					
				}
				var rowMax = SearchMaxNumber(newArray, N, true);
				var colMax = SearchMaxNumber(newArray, N, false);
				var maxNumber = Math.Max(rowMax, colMax);
				totalMaxNumber = Math.Max(totalMaxNumber, maxNumber);
			}
		}

		// column 방향 변경
		for (var i = 0; i < N; i++)
		{
			for (var j = 0; j < N; j++)
			{
				var newArray = Get2dArrayCopy(square);
				if (j + 1 < N)
				{
					var tmp = newArray[j, i];
					newArray[j, i] = newArray[j + 1, i];
					newArray[j + 1, i] = tmp;
				}
				var rowMax = SearchMaxNumber(newArray, N, true);
				var colMax = SearchMaxNumber(newArray, N, false);
				var maxNumber = Math.Max(rowMax, colMax);
				totalMaxNumber = Math.Max(totalMaxNumber, maxNumber);
			}
		}
		return totalMaxNumber;
	}
	
	static int SearchMaxNumber(char[,] square, int N, bool isRow)
	{
		char[] colors = new char[] { 'C', 'P', 'Z', 'Y' };
		var maxNumber = 0;
		
		foreach (var color in colors)
		{
				for (var i = 0; i < N; i++)
				{
					var tmp = 0;

					 var maxNumberByLine = Enumerable.Range(0, N)
								.Select(j => isRow ? square[i, j] : square[j, i])
								.Aggregate(0, (count, curColor) =>
								{
									if (curColor == color)
									{
										tmp += 1;
										return Math.Max(count, tmp);
										
									}
									tmp = 0;
									return count;
								});

					maxNumber = Math.Max(maxNumber, maxNumberByLine);
				}
		}
		return maxNumber;
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


