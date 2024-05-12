using System;
using System.Text;
using static System.Console;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        var N = int.Parse(ReadLine());
        var targetNumber = int.Parse(ReadLine());
        Solve(N, targetNumber);
    }

    static void Solve(int N, int targetNumber)
    {
        var snail = new SnailTable(N);
        Write(snail.GetSnailTable());
        Write(snail.GetNumberCoordinate(targetNumber));
    }

    class SnailTable
    {
        int _tableLength = 0;
        int[][] _snailTable;
        public SnailTable(int N)
        {
            _tableLength = N;
            _snailTable = new int[N][];

            for (var i = 0; i < N; i++)
            {
                _snailTable[i] = Enumerable.Repeat(0, N).ToArray();
            }

            SetSnail();
        }

        // (3,3)
        // (3-1,3)
        // (3-1,3+1)
        // (3-1+2,3+1)
        // (3-1+2,3+1-2)
        void SetSnail()
        {
            var nextX = _tableLength / 2;
            var nextY = _tableLength / 2;

            var dx = new int[] { -1, 0, 1, 0 };
            var dy = new int[] { 0, 1, 0, -1 };

            var direction = 0;
            var passedDistance = 0;
            var maxDistance = 1;
            var numberOfChangeDirection = 0;
            for (var i = 0; i < _tableLength * _tableLength; i++)
            {
                _snailTable[nextX][nextY] = i + 1;

                nextX += dx[direction % 4];
                nextY += dy[direction % 4];

                UpdateCountAndDistances(ref passedDistance, ref maxDistance, ref direction, ref numberOfChangeDirection);
            }
        }

        void UpdateCountAndDistances(ref int passedDistance, ref int maxDistance, ref int direction, ref int numberOfChangeDirection)
        {
            passedDistance++;

            // max 도달 시 방향 전환
            if (passedDistance == maxDistance)
            {
                direction++;
                passedDistance = 0;
                numberOfChangeDirection++;
            }

            // 2번 방향 전환 시 이동거리++
            if (numberOfChangeDirection == 2)
            {
                maxDistance++;
                numberOfChangeDirection = 0;
            }
        }

        public string GetSnailTable()
        {
            //return _snailTable;
            var sb = new StringBuilder();
            foreach (int[] row in _snailTable)
            {
                foreach (int number in row)
                    sb.Append($"{number} ");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public string GetNumberCoordinate(int targetNumber)
        {
            var n = Enumerable.Range(0, _tableLength);
            string[] coordinate = IterationFunction(n, n).Where(item => _snailTable[item.Item1][item.Item2] == targetNumber)
                .Select(item => $"{item.Item1 + 1} {item.Item2 + 1}").ToArray();
            return coordinate[0];
        }
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