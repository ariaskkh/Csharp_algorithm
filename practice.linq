<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = Console.ReadLine().Split(' ');		
		var N = int.Parse(inputNum[0]);
		var M = int.Parse(inputNum[1]);
		int[,] innputArr = getArr(N, M);
		
		var numOfProblems = int.Parse(Console.ReadLine());
		
		var result = new List<int>();
		
		for (var i = 0; i < numOfProblems; i++)
		{
			result.Add(getSumOfProblem(innputArr));
		}
		
		foreach (int sum in result)
		{
			Console.WriteLine(sum);
		}
	}
	static int getSumOfProblem(int[,] innputArr)
	{
		int[] problemArr = changeStrToNum(Console.ReadLine().Split(' '));
		var xStart = problemArr[0];
		var yStart = problemArr[1];
		var xEnd = problemArr[2];
		var yEnd = problemArr[3];
		
		var result = 0;
		
		for (var i = xStart - 1; i <= xEnd - 1; i++)
		{
			for (var j = yStart - 1; j <= yEnd - 1; j++)
			{
				result += innputArr[i, j];
			}	
		}
		return result;
			
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
}


