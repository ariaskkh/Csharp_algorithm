using System;
using System.Text;

class Program
{
	static void Main(string[] args)
	{
		var N = int.Parse(Console.ReadLine());
		Solve(N);
	}

    //PrintStars(24, 0, 0);
    //  PrintStars(12, 0, 12);
    //    PrintStars(6, 0, 18);
    //      PrintStars(3, 0, 21);
    //      PrintStars(3, 3, 18);
    //      PrintStars(3, 3, 24);
    //    PrintStars(6, 6, 12);
    //      PrintStars(3, 6, 15);
    //      PrintStars(3, 9, 12);
    //      PrintStars(3, 9, 18);
    //    PrintStars(6, 6, 24);
    //      PrintStars(3, 6, 27);
    //      PrintStars(3, 9, 24);
    //      PrintStars(3, 9, 30);
    //  PrintStars(12, 12, 0);
    //    PrintStars(6, 12, 6);
    //      PrintStars(3, 12, 9);
    //      PrintStars(3, 15, 6);
    //      PrintStars(3, 15, 12);
    //    PrintStars(6, 18, 0);
    //      PrintStars(3, 18, 3);
    //      PrintStars(3, 21, 0);
    //      PrintStars(3, 21, 6);
    //    PrintStars(6, 18, 12);
    //      PrintStars(3, 18, 15);
    //      PrintStars(3, 21, 12);
    //      PrintStars(3, 21, 18);
    //  PrintStars(12, 12, 24);
    //    PrintStars(6, 12, 30);
    //      PrintStars(3, 12, 33);
    //      PrintStars(3, 15, 30);
    //      PrintStars(3, 15, 36);
    //    PrintStars(6, 18, 24);
    //      PrintStars(3, 12, 27);
    //      PrintStars(3, 21, 24);
    //      PrintStars(3, 21, 30);
    //    PrintStars(6, 18, 36);
    //      PrintStars(3, 12, 39);
    //      PrintStars(3, 21, 36);
    //      PrintStars(3, 21, 42);

    static void Solve(int N)
	{
        var paper = new Paper(N);
        paper.PrintStar(N, 0, 0);
        Console.WriteLine(paper.GetString());
	}

    public class Paper
    {
        private bool[][] _isStar;

        public Paper() { }
        public Paper(int N)
        {
            _isStar = Enumerable.Range(0, N)
                .Select(row => Enumerable.Repeat(false, N * 2 - 1).ToArray()).ToArray();
        }

        public string GetString()
        {
            var sb = new StringBuilder();
            foreach (var row in _isStar)
            {
                sb.Append(new String(row.Select(element => element ? '*' : ' ').ToArray()));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        void PrintTinyStar(int row, int column)
        {
            _isStar[row][column] = true;
        }

       
        public void PrintSmallStar(int row, int column)
        {
            var starPositions = new (int Row, int Column)[]
           {
               // 3개 행 X 5개 열
               (0, 2),
               (1, 1), (1, 3),
               (2, 0), (2, 1), (2, 2), (2, 3), (2, 4),
           };

           foreach (var position in starPositions)
            {
                var nextRow = row + position.Row;
                var nextColumn = column + position.Column;
                PrintTinyStar(nextRow, nextColumn);
            }
        }

        public void PrintStar(int N, int row, int column)
        {
            if (N == 3)
            {
                PrintSmallStar(row, column);
                return;
            }

            var starPositions2 = new (int Row, int Column)[]
            {
                (0, 0),
                (1, -1), (1, 1)
            };

            var nextN = N / 2;
            foreach (var position in starPositions2)
            {
                var nextRow = row + position.Row * nextN;
                var nextColumn = column + position.Column * nextN + nextN;
                PrintStar(nextN, nextRow, nextColumn);
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