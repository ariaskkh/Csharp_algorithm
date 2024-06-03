﻿using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        int[] inputArr = ReadLine().Split(' ').ChangeStrToInt();
        var height = inputArr[0];
        var width = inputArr[1];
       Solve(width, height);
    }

    static void Solve(int width, int height)
    {
        var chessBoard = new ChessBoard(width, height);
        chessBoard.SetKnight();
        WriteLine(chessBoard.GetMaxVisitingCountOfSickNight());
    }

    class ChessBoard
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public SickKnight SickKnight { get; set; }
        
        public ChessBoard(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void SetKnight(int startX = 0, int startY = 0)
        {
            SickKnight = new SickKnight(startX, startY);
        }

        public int GetMaxVisitingCountOfSickNight()
        {
            SickKnight.Move(Width, Height);
            return SickKnight.VisitingCount;
        }
    }

    class SickKnight
    {
        public (int x, int y) Coordinate { get; set; } = (0, 0);
        public int VisitingCount { get; set; } = 1;

        public SickKnight(int startX, int startY)
        {
            Coordinate = (startX, startY);
        }

        public void Move(int width, int height)
        {
            if (height == 1)
            {
                VisitingCount = 1;
            }
            else if (height == 2)
            {
                var result = 1 + (width - 1) / 2;
                VisitingCount = result > 4 ? 4 : result;
            }
            else if (width < 5)
            {
                VisitingCount = width;
            }
            else if (width == 5)
            {
                VisitingCount = 4;
            }
            else
            {
                VisitingCount = 1 + 2 + (width - 5);
            }
        }
        
    }

    /////////////////////////////  util 함수  ////////////////////////////////
    static T[] CopyArray<T>(T[] array)
    {
        T[] newArray = new T[array.Length];
        for (var i = 0; i < array.Length; i++)
        {
            newArray[i] = array[i];
        }
        return newArray;
    }

    static T[][] Copy2DArray<T>(T[][] array)
    {
        T[][] newArray = new T[array.Length][];
        for (var i = 0; i < array.Length; i++)
        {
            newArray[i] = new T[array[i].Length];
            for (var j = 0; j < array[i].Length; j++)
            {
                newArray[i][j] = array[i][j];
            }
        }
        return newArray;
    }

    static T[][] Get2DArray<T>(T[][] array, T element)
    {
        T[][] newArray = new T[array.Length][];
        for (var i = 0; i < array.Length; i++)
            newArray[i] = Enumerable.Repeat(element, array.Length).ToArray();
        return newArray;
    }

    static Dictionary<char, int> SetDictionary(Dictionary<char, int> charCounts, string inputString)
    {
        foreach (var character in inputString)
        {
            if (charCounts.ContainsKey(character))
            {
                charCounts[character]++;
            }
            else
            {
                charCounts[character] = 1;
            }
        }
        return charCounts;
    }


    static IEnumerable<(T1, T2)> IterationFunction<T1, T2>(IEnumerable<T1> arr1, IEnumerable<T2> arr2)
    {
        return arr1.Join<T1, T2, bool, (T1, T2)>(arr2, item1 => true, item2 => true, (item1, item2) => (item1, item2));
    }
    static IEnumerable<(T1, T2, T3)> IterationFunction<T1, T2, T3>(IEnumerable<T1> arr1, IEnumerable<T2> arr2, IEnumerable<T3> arr3)
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
            var tmpRowArr = ReadLine().ToCharArray();
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
            var tmpRowArr = ReadLine().Split(' ');
            for (var j = 0; j < M; j++)
            {
                resultArr[i, j] = int.Parse(tmpRowArr[j]);
            }
        }
        return resultArr;
    }

    static void Print(int[,] result, int N, int K)
    {
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < K; j++)
            {
                Write($"{result[i, j]} ");
            }
            WriteLine();
        }
    }
}

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }

    public static int[] ChangeCharToInt(this IEnumerable<char> enumerable)
    {
        return enumerable.Select(item => int.Parse(item.ToString())).ToArray();
    }

    public static long[] ChangeCharToLong(this IEnumerable<char> enumerable)
    {
        return enumerable.Select(item => long.Parse(item.ToString())).ToArray();
    }

    public static int[] ChangeStrToInt(this IEnumerable<string> enumerable)
    {
        return enumerable.Select(x => int.Parse(x)).ToArray();
    }

    public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
    {
        var queue = new Queue<T>();
        enumerable.ForEach(item => queue.Enqueue(item));
        return queue;
    }

    public static Dictionary<int, int> ConverIntListToDict(this IEnumerable<int> enumerable)
    {
        var numberDict = new Dictionary<int, int>();
        foreach (var item in enumerable)
        {
            if (numberDict.ContainsKey(item))
            {
                numberDict[item] += 1;
            }
            else
            {
                numberDict.Add(item, 1);
            }
        }
        return numberDict;
    }
}