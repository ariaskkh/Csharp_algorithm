<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = Console.ReadLine().Split(' ');
		var N = int.Parse(inputNum[0]);
		var M = int.Parse(inputNum[1]);
		var A = getArr(N, M);
		
		var K= int.Parse(Console.ReadLine().Split(' ')[1]);
		var B = getArr(M, K);
		
		var result = solve(A, B, N, M, K);
		print(result, N, K);
	}

	static void print(int[,] result, int N, int K)
	{
		for (var i = 0; i < N; i++)
		{		
			for (var j = 0; j < K; j++)
			{
				Console.Write($"{result[i,j]} ");	
			}
			Console.WriteLine();
		}
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
	
	static int[,] solve(int[,] A, int[,] B, int N, int M, int K)
	{
		int[,] result = new int[N,K];
		for (var i = 0; i < N; i++)
		{
			for (var k = 0; k < K; k++)
			{
				var tmp = 0;
				for (var j = 0; j < M; j++)		
				{
					tmp += A[i,j] * B[j,k];
				}
				result[i,k] = tmp;
			}
		}
		return result;
	}
	
	static int[] changeCharToNum(char[] numsInChar)
	{
		return numsInChar.Select(x => int.Parse(x.ToString())).ToArray();
	}
	
	static int[] changeStrToNum(string[] numsInStr)
	{
		return numsInStr.Select(x => int.Parse(x)).ToArray();
	}
}


