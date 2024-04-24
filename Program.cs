using System;

class Program
{
	static void Main(string[] args)
	{
		var N = int.Parse(Console.ReadLine());
		Solve(N);
	}

	static void Solve(int N)
	{
        var paper = new Paper(N);
        paper.PrintSmallStar(N);
        Console.WriteLine(paper.PrintToString());
	}

    public class Paper
    {
        private var _N = 0;
        private bool[][] _isStar;

        public Paper() { }
        public Paper(int N)
        {
            _N = N;
            _isStar = Enumerable.Range(row => Enumerable.Repeat(false, N).ToArray()).ToArray();
        }

        public void PrintToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _N; i++)
            {
                for (var j = 0; j < _N; j++)
                {
                    if (_isStar[i][j] = true)
                    {
                        sb.Append('*');
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        void PrintTinyStar(int row, int column)
        {
            _isStar[row][column] = true;
        }

        
        public void PrintSmallStar(int row, int column)
        {
            var starPositions = new(int Row, int Column)
           {
               // 3개 행 X 5개 열
               (0, 2),
               (1, 1), (1,3)
               (2, 1), (2, 2), (2, 3), (2, 4), (2, 5),
           };

           foreach (var position in starPositions)
            {
                PrintTinyStar(position.Row, position.Column)
            }
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