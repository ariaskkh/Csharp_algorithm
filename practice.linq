<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = int.Parse(Console.ReadLine());
		var resultArr = Solve(inputNum);
		foreach(var result in resultArr)
		{
			Console.WriteLine(result);
		}
	}

	static int[] Solve(int N)
	{
		var orderArr = new int[N];
		for (var i = 0; i < N; i++)
		{
			var targetIdx = ChangeStrToNum(Console.ReadLine().Split(' '))[1];
			int[] priorities = ChangeStrToNum(Console.ReadLine().Split(' '));
			var queue = new Queue<int>();
			SetQueue(queue, priorities);
			orderArr[i] = GetPrintOrder(queue, targetIdx);
		}
		return orderArr;
	}
	
	static Queue<int> SetQueue(Queue<int> queue, int[] priorities)
	{
		
		foreach (var priority in priorities)
		{
			queue.Enqueue(priority);
		}
		return queue;
	}
	
	static int GetPrintOrder(Queue<int> queue, int targetIdx)
	{
		var count = 0;
		while (true)
		{
			if (queue.Peek() == queue.Max())
			{
				if (targetIdx == 0)
				{
					break;
				}
				queue.Dequeue();
				count++;
				targetIdx--;
				continue;
			}
			queue.Enqueue(queue.Dequeue());
			if (targetIdx == 0)
			{
				targetIdx = queue.Count - 1;
			}
			else
			{
				targetIdx--;
			}
		}
		// index + 1 번째로 return
		return count + 1;
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


