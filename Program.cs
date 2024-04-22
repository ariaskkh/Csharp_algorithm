using System;

class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine().Split(' ');
        var N = int.Parse(input[0].ToString());
        var K = int.Parse(input[1].ToString());
        Console.Write(Solve(N, K));
    }

    public class Sieve
    {

        private int _count = 0;
        private readonly bool[] _boolArray;
        public Sieve(int N)
        {
            _boolArray = new bool[N + 1];
            _boolArray[0] = _boolArray[1] = true;
        }

        public int GetNumberOfRemoved(int targetRemovedNumber)
        {
            for (var i = 2; i <= _boolArray.Length - 1; i++)
            {
                // Prime 일 때
                if (_boolArray[i] == false)
                {
                    // Prime 배수 처리
                    for (var multiple = i; multiple <= _boolArray.Length - 1; multiple += i)
                    {
                        if (_boolArray[multiple] == false)
                        {
                            _boolArray[multiple] = true;
                            _count++;

                            if (_count == targetRemovedNumber)
                            {
                                return multiple;
                            }
                        }
                    }
                }
            }
            _count = 0;
            return _count;
        }
    }

    static int Solve(int N, int K)
    {
        Sieve sieve = new Sieve(N);
        return sieve.GetNumberOfRemoved(K);
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