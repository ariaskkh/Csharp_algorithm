using System;
using System.Text;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        Solve(N);
    }

    static void Solve(int N)
    {
        char[][] spaceArray = GetSpaceArray(N);
        var space = new Space(spaceArray);
        var count = space.GetSleepAvailableSpace();
        Console.WriteLine(count.countRow + " " + count.countColumn);
    }

    static char[][] GetSpaceArray(int N)
    {
        // input array 둘레를 벽(wall) X로 감싸기
        // XXXXX
        // X   X
        // X   X
        // XXXXX
        char[][] spaceArrayWithWall = new char[N + 2][];
        spaceArrayWithWall[0] = (new string('X', N + 2)).ToCharArray();
        spaceArrayWithWall[N+1] = (new string('X', N + 2)).ToCharArray();
        for (var i = 1; i < N + 1; i++)
        {
            spaceArrayWithWall[i] = ('X' + Console.ReadLine() + 'X').ToCharArray();
        }
        return spaceArrayWithWall;
    }

    public class Space
    {
        private readonly char[][] _spaceArray;
        private readonly char[][] _spaceArrayReversed; // row, column 반전
        public Space(char[][] spaceArray)
        {
            _spaceArray = spaceArray;
            _spaceArrayReversed = GetReversedArray(spaceArray);
        }

        public char[][] GetReversedArray(char[][] spaceArray)
        {
            char[][] reversedArray = new char[spaceArray.Length][];

            for (var i = 0; i < reversedArray.Length; i++)
            {
                reversedArray[i] = new char[spaceArray[i].Length];
                for (var j = 0; j < reversedArray.Length; j++)
                {
                    reversedArray[i][j] = spaceArray[j][i];
                }
            }
            return reversedArray;
        }
        

        public (int countRow, int countColumn) GetSleepAvailableSpace()
        {
            var countRow = 0;
            foreach (char[] row in _spaceArray)
            {
                countRow += GetSleepAvailableSpaceForLine(row);
            }

            var countColumn = 0;
            foreach (char[] row in _spaceArrayReversed)
            {
                countColumn += GetSleepAvailableSpaceForLine(row);
            }

            return (countRow, countColumn);
        }

        public int GetSleepAvailableSpaceForLine(char[] row)
        {
            var availableSpaceCount = 0;
            var unitSpaceCount = 0;
            for (var i = 1; i < row.Length; i++)
            {
                if (row[i] == 'X')
                {
                    if (unitSpaceCount >= 2)
                    {
                        availableSpaceCount++;
                    }
                    unitSpaceCount = 0;
                }
                else
                {
                    unitSpaceCount++;
                }
            }
            return availableSpaceCount;
        }
    }

    static Dictionary<char, int> SetDictionary(Dictionary<char, int > charCounts, string inputString)
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
       

    static IEnumerable<(T1, T2)> IteratonFunction<T1, T2>(IEnumerable<T1> arr1, IEnumerable<T2> arr2)
    {
        return arr1.Join<T1, T2, bool, (T1, T2)>(arr2, item1 => true, item2 => true, (item1, item2) => (item1, item2));
    }
    static IEnumerable<(T1, T2, T3)> IteratonFunction<T1, T2, T3>(IEnumerable<T1> arr1, IEnumerable<T2> arr2, IEnumerable<T3> arr3)
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