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
				PrintStar(i, j, n / 3);
			}
			Console.WriteLine();
		}
	}

	static void PrintStar(int i, int j, int n)
	{
		if (((i / n) % 3 == 1) && ((j / n) % 3 == 1))
		{
			Console.Write(' ');	
		}
		else
		{
            if (n < 3)
            {
                Console.Write('*');
                return;
            }
            PrintStar(i, j, n / 3);	
        }
	}

    static IEnumerable<(T1, T2)> IteratonFunction<T1, T2>(IEnumerable<T1> arr1, IEnumerable<T2> arr2)
    {
        return arr1.Join<T1, T2, bool, (T1, T2)>(arr2, item1 => true, item2 => true, (item1, item2) => (item1, item2));
    }
    static IEnumerable<(T1, T2, T3)> ForLoop<T1, T2, T3>(IEnumerable<T1> arr1, IEnumerable<T2> arr2, IEnumerable<T3> arr3)
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
        int[,] resultArr = new int[N, M];
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