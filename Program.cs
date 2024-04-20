using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

class Program
{
   static void Main(string[] args)
	{
		var input = Console.ReadLine().Split(' ');
		var N = int.Parse(input[0].ToString());
		var K = int.Parse(input[1].ToString());
		Console.Write(Solve(N, K));
	}
	
	static int Solve(int N, int K)
	{
		bool[] intArray = Enumerable.Repeat(false, N + 1).ToArray();
		intArray[0] = intArray[1] = true;
        var removedNumber = 0;
		var count = 0;

        int getRemovedNumber()
        {
            for (var i = 2; i <= N; i++)
            {
                if (intArray[i] == false)
                {
                    // Prime 배수 처리
                    for (var multiple = 1; multiple <= (N / i); multiple++)
                    {
                        if ((i * multiple <= N) && (intArray[i * multiple] == false))
                        {
                            intArray[i * multiple] = true;
                            count++;
                            removedNumber = i * multiple;

                            if (count == K)
                            {
                                return removedNumber;
                            }
                        }
                    }
                }
            }
            return -1;
        }
        return getRemovedNumber();
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