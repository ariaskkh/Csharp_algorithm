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
       var coveredBoard  = new Board(inputBoard).Cover();
        Console.WriteLine(coveredBoard);
    }

    class Board
    {
        private string _inputBoard = "";
        private List<BoardX> _boardXList;
        public Board(string inputBoard)
        {
            _inputBoard = inputBoard;
            _boardXList = inputBoard.Split('.')
                .Where(block => block.Length > 0)
                .Select(block => new BoardX(block))
                .OrderByDescending(boardX => boardX.boardLength)
                .ToList();
        }

        public string Cover()
        {
            bool hasOddBoard = _boardXList.Any(x => x.isOdd);
            if (hasOddBoard)
            {
                return "-1";
            }
            
            var newBoardXList = _boardXList
                .Select(boardX => new { Input = boardX.inputBoardX, Output = boardX.Cover() })
                .ToList();

            string newBoard = _inputBoard;
            foreach (var boardX in newBoardXList)
            {
                newBoard = newBoard.Replace(boardX.Input, boardX.Output);
            }
            return newBoard;
        }
    }

    class BoardX
    {
        public string inputBoardX;
        public bool isOdd => inputBoardX.Length % 2 != 0;
        public int boardLength => inputBoardX.Length;
        
        public BoardX(string inputBoardX)
        {
            this.inputBoardX = inputBoardX;
        }

        public string Cover()
        {
            if (isOdd) return "-1";
            StringBuilder sb = new StringBuilder();
            var countA = boardLength / 4;
            var countB = boardLength % 4 / 2;

            sb.Append(new string('A', countA * 4));
            sb.Append(new string('B', countB * 2));

            return sb.ToString();
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