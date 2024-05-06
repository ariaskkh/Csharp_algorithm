using System;
using System.Net.NetworkInformation;
using System.Text;

namespace Algorithm;
public class Program
{
    static void Main(string[] args)
    {
        Solve();
    }

    static string[][] GetBoard(int size)
    {
        string[][] board = new string[size][]; ;
        for (var i = 0; i < size; i++)
        {
            board[i] = Console.ReadLine().Split(' ').ToArray();
        }
        return board;
    }

    static void Solve()
    {
        var size = 5;
        var board = GetBoard(size);
        var bingoCount = GetCount(board);
        Console.WriteLine(bingoCount);
    }

    static int GetCount(string[][] board)
    {
        var bingo = new Bingo(board);
        for (var i = 0; i < board.Length; i++)
        {
            string[] numbersCalled = Console.ReadLine().Split(' ').ToArray();
            for (var j = 0; j < board.Length; j++)
            {
                bingo.Check(numbersCalled[j]);
                if (bingo.IsBingo())
                {
                    return bingo.GetBingoCount();
                }
            }
        }
        return -1;
    }

    class Bingo
    {
        string[][] _board;
        bool[][] _checkingBoard;
        int _size => _board.Length;
        int _countForBingo = 0;

        public Bingo(string[][] board)
        {
            _board = board;
            _checkingBoard = Enumerable.Range(0, _size).Select(_ => Enumerable.Repeat(false, _size).ToArray()).ToArray();
        }

        public void Check(string number)
        {
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    if (_board[i][j] == number)
                    {
                        _checkingBoard[i][j] = true;
                    }
                }
            }

            var n = Enumerable.Range(0, _size);
            IteratonFunction(n, n).Select(tuple =>
            {
                if (_board[tuple.Item1][tuple.Item2] == number)
                    _checkingBoard[tuple.Item1][tuple.Item2] = true;
                return tuple;
            }).ToArray();
            _countForBingo++;
        }

        public bool IsBingo()
        {
            if (GetBingoCountRow() + GetBingoCountColumn() + GetBingoCountDiagonal() >= 3)
                return true;
            else
                return false;
        }

        int GetBingoCountRow()
        {
            var bingoCount = 0;
            for (var i = 0; i < _size; i++)
            {
                var tmpCount = 0;
                for (var j = 0; j < _size; j++)
                {
                    if (_checkingBoard[i][j] == true)
                        tmpCount++;
                }
                if (tmpCount == _size)
                    bingoCount++;
            }
            return bingoCount;
        }

        int GetBingoCountColumn()
        {
            var bingoCount = 0;
            for (var i = 0; i < _size; i++)
            {
                var tmpCount = 0;
                for (var j = 0; j < _size; j++)
                {
                    if (_checkingBoard[j][i] == true)
                        tmpCount++;
                }
                if (tmpCount == _size)
                    bingoCount++;
            }
            return bingoCount;
        }

        int GetBingoCountDiagonal()
        {
            var bingoCount = 0;
            var countLeftDiagonal = 0;
            for (var i = 0; i < _size; i++)
            {
                if (_checkingBoard[i][i] == true)
                    countLeftDiagonal++;
            }

            var countRightDiagonal = 0;
            for (var i = 0; i < _size; i++)
            {
                if (_checkingBoard[i][(_size - 1)- i] == true)
                    countRightDiagonal++;
            }

            if (countLeftDiagonal == _size)
                bingoCount++;
            if (countRightDiagonal == _size)
                bingoCount++;
            return bingoCount;
        }   

        public int GetBingoCount()
        {
            return _countForBingo;
        }
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