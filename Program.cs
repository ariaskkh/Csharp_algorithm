using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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

        char[,] swapElement(char[,] newArray, int x1, int y1, int x2, int y2)
        {
            var tmp = newArray[x1, y1];
            newArray[x1, y1] = newArray[x2, y2];
            newArray[x2, y2] = tmp;
            return newArray;
        }

        int getTotalMaxNumber(char[,] newArray)
        {
            var rowMax = SearchMaxNumber(newArray, N, true);
            var colMax = SearchMaxNumber(newArray, N, false);
            var maxNumber = Math.Max(rowMax, colMax);
            return Math.Max(totalMaxNumber, maxNumber);
        }

        IEnumerable<int> n = Enumerable.Range(0, N);

        // 변경 없는 square
        totalMaxNumber = IteratonFunction<int, int>(n, n)
            .Where(item => item.Item2 + 1 < N)
            .Select(item => Get2dArrayCopy(square))
            .Select(getTotalMaxNumber)
            .Max();
        // row 방향 변경
        totalMaxNumber = IteratonFunction<int, int>(n, n)
            .Where(item => item.Item2 + 1 < N)
            .Select(item => swapElement(Get2dArrayCopy(square), item.Item1, item.Item2, item.Item1, item.Item2 + 1))
            .Select(getTotalMaxNumber)
            .Max();
        // column 방향 변경
        totalMaxNumber = IteratonFunction<int, int>(n, n)
            .Where(item => item.Item2 + 1 < N)
            .Select(item => swapElement(Get2dArrayCopy(square), item.Item2, item.Item1, item.Item2 + 1, item.Item1))
            .Select(getTotalMaxNumber)
            .Max();
        return totalMaxNumber;
    }


    static int SearchMaxNumber(char[,] square, int N, bool isRow)
    {
        char[] colors = new char[] { 'C', 'P', 'Z', 'Y' };
        var n = Enumerable.Range(0, N);
        return IteratonFunction<char, int>(colors, n)
            .Select(tuple =>
            {
                // 특정 열(혹은 행)을 순회하는 j의 순서는 보장해야 함
                return n.Select(j => isRow ? (tuple.Item1, square[tuple.Item2, j]) : (tuple.Item1, square[j, tuple.Item2]))
                    .Aggregate(
                    (count: 0, tmpCount: 0), // Seed
                    (acc, current) => // Func
                    {
                        if (current.Item1 == current.Item2)
                        {
                            acc.tmpCount += 1;
                            return (Math.Max(acc.count, acc.tmpCount), acc.tmpCount);
                        }
                        acc.tmpCount = 0;
                        return (acc.count, acc.tmpCount);
                    },
                    (acc) => acc.count // Result Selector
                    );

            }).Max();
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