<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var N = int.Parse(Console.ReadLine());
		int [,] paper = new int[100, 100];
		int[][] confettiArr = getConfettiArr(N);
		int [,] updatedPaper = solve(paper, confettiArr, N);
		int result = getSumOf2dArr(updatedPaper);
		Console.Write(result);
	}
	
	static int getSumOf2dArr(int[,] paper)
	{
		int result = 0;
		for (var i = 0; i < 100; i++)
		{
			for (var j = 0; j < 100; j++)
			{
				result += paper[i, j];
			}
		}
		return result;
	}
	
	static int[][] getConfettiArr(int N)
	{
		int[][] confettiArr = new int[N][];
		for (var i = 0; i < N; i++)
		{
			var coordinate = changeStrToNum(Console.ReadLine().Split(' '));
			confettiArr[i] = new int[] { coordinate[0], coordinate[1] };
		}
		return confettiArr;
	}

	static int[,] solve(int[,] paper, int[][] confettiArr, int N)
	{
		for (var i = 0; i < 100; i++)
		{
			for (var j = 0; j < 100; j++)
			{
				if (paper[i, j] == 1)
				{
					continue;
				}
				// 색종이별
				for (var k = 0; k < N; k++)
				{
					var x = confettiArr[k][0];
					var y = confettiArr[k][1];
					if ((i >= x && i < x + 10) && (j >= y && j < y + 10))
					{
						paper[i,j] = 1;
					}
				}
				
			}
		}
		return paper;
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


