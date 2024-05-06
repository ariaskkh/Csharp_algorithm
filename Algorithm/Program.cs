using System;
using System.Net.NetworkInformation;
using System.Text;

namespace Algorithm;
public class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine().Split(' ');
        var screenWidth = int.Parse(input[0]);
        var basketWidth = int.Parse(input[1]);
        Solve(screenWidth, basketWidth);
    }

    static void Solve(int screenWidth, int basketWidth)
    {
        var N = int.Parse(Console.ReadLine());
        int[] positions = new int[N];
        for (var i = 0; i < N; i++)
        {
            positions[i] = int.Parse(Console.ReadLine());
        }
        var basket = new Basket(screenWidth, basketWidth);
        basket.DropApples(positions);
        Console.WriteLine(basket.GetMinMoveDistance());
    }

    class Basket
    {
        int _screenWidth;
        int _basketWitdh;
        int _minMoveDistance;
        public Basket(int screenWidth, int basketWitdh)
        {
            _screenWidth = screenWidth;
            _basketWitdh = basketWitdh;
        }

        public void DropApples(int[] positions)
        {
            var screenLeft = 0;
            var screenRight = _basketWitdh;
            foreach (var position in positions)
            {
                var delta = 0;
                if (position > screenRight)
                {
                    delta = position - screenRight;
                    screenLeft += delta;
                    screenRight += delta;
                }
                else if (position < screenLeft + 1)
                {
                    delta = screenLeft + 1 - position;
                    screenLeft -= delta;
                    screenRight -= delta;
                }
                _minMoveDistance += delta;
            }
        }

        public int GetMinMoveDistance()
        {
            return _minMoveDistance;
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