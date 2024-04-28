using System;
using System.Text;

namespace Algorithm;
public class Program
{
	static void Main(string[] args)
	{
		string inputBoard = Console.ReadLine();
		Solve(inputBoard);
	}

    static void Solve(string inputBoard)
	{
        var polyomino = new Polyomino();
        var coveredBoard = polyomino.SetPolyomino(inputBoard);
        Console.WriteLine(coveredBoard);
	}

    class Polyomino
    {
        string fail = "-1";
        public string SetPolyomino(string inputBoard)
        {
            StringBuilder _coveredBoard = new StringBuilder();
            int count = 0;
            for (var i = 0; i < inputBoard.Length; i++)
            {


                if (inputBoard[i] == 'X')
                {
                    count++;
                    if (i != inputBoard.Length - 1)
                    {
                        continue;
                    }
                }

                if (count > 0)
                {
                    var tmpBoard = PlaceBlocks(count);
                    if (tmpBoard == fail)
                    {
                        return fail;
                    }
                    else
                    {
                        _coveredBoard.Append(tmpBoard);
                        count = 0;
                    }
                }
                
                if (inputBoard[i] == '.')
                {
                    _coveredBoard.Append('.');
                }
                
            }
            return _coveredBoard.ToString();
        }

        string PlaceBlocks(int count)
        {
            if (count % 2 != 0) return fail;
            StringBuilder _tmpBoard = new StringBuilder();
                
            var countA = count / 4;
            var countB = (count % 4) / 2;

            _tmpBoard.Append(new String('A', countA * 4));
            _tmpBoard.Append(new String('B', countB * 2));

            return _tmpBoard.ToString();
        }
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