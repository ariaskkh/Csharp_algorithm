﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        // row 방향 변경
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < N; j++)
            {
                var newArray = Get2dArrayCopy(square);
                if (j + 1 < N)
                {
                    newArray = swapElement(newArray, i, j, i, j + 1);
                }
                totalMaxNumber = getTotalMaxNumber(newArray);
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
                    newArray = swapElement(newArray, j, i, j + 1, i);
                }
                totalMaxNumber = getTotalMaxNumber(newArray);
            }
        }
        return totalMaxNumber;
    }

    static int SearchMaxNumber(char[,] square, int N, bool isRow)
    {

        //return colors.Select(color =>
        //{
        //    return Enumerable.Range(0, N)
        //            .Select(i =>
        //            {
        //                return Enumerable.Range(0, N)
        //                        .Select(j => isRow ? square[i, j] : square[j, i])
        //                        .Aggregate(
        //                            (count: 0, tmpCount: 0), // Seed
        //                            (acc, curColor) => // Func
        //                            {
        //                                if (curColor == color)
        //                                {
        //                                    acc.tmpCount += 1;
        //                                    return (Math.Max(acc.count, acc.tmpCount), acc.tmpCount);

        //                                }
        //                                acc.tmpCount = 0;
        //                                return (acc.count, acc.tmpCount);
        //                            },
        //                            (acc) => acc.count // Result selector
        //                            );
        //            }).Max();

        //}).Max();

        char[] colors = new char[] { 'C', 'P', 'Z', 'Y' };
        var n = Enumerable.Range(0, N);
        var a = ForLoop<char, int, int>(colors, n, n)
            .Select(tuple => isRow ? (tuple.item1, square[tuple.Item2, tuple.Item3]) : (tuple.Item1, square[tuple.Item3, tuple.Item2]))
            .Aggregate(
                (count: 0, tmpCount: 0), // Seed
                (acc, (a, b)) => // Func
                {
                    if (a == b)
                    {
                        acc.tmpCount += 1;
                        return (Math.Max(acc.count, acc.tmpCount), acc.tmpCount);
                    }
                    acc.tmpCount = 0;
                    return (acc.count, acc.tmpCount);
                },
                (acc) => acc.count // Result Selector
                );
        return a;
    }

    static IEnumerable<(T1, T2, T3)> ForLoop<T1, T2, T3>(IEnumerable<T1> arr1, IEnumerable<T2> arr2, IEnumerable<T3> arr3)
    {
        foreach (var item1 in arr1)
        {
            foreach (var item2 in arr2)
            {
                foreach (var item3 in arr3)
                {
                    yield return (item1, item2, item3);
                }
            }
        }
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