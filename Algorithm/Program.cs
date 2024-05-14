using System.Text;
using static System.Console;

namespace Algorithm;

public class Program
{
    static string ERROR_MESSAGE = "error";

    enum CommandType
    {
        D = 'D',
        R = 'R'
    }

    static void Main(string[] args)
    {
        int N = int.Parse(ReadLine());
        StringBuilder sb = Solve(N);
        WriteLine(sb.ToString()); // 시간 제한 없었으면 List를 받아서 for loop으로 출력이 더 이쁜 듯.
    }

    static StringBuilder Solve(int N)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < N; i++)
        {
            string commands = ReadLine().Replace("RR", "");
            int listLength = int.Parse(ReadLine());
            string arrayStr = ReadLine();

            var filteredCommands = commands;
            var countOfD = filteredCommands.Count(ch => ch == (char) CommandType.D);

            if (listLength < countOfD)
                sb.AppendLine(ErrorHandler());
            else
                sb.AppendLine(GetACCalculatedNumberList(arrayStr, filteredCommands));
        }
        return sb;
    }

    static string ErrorHandler()
    {
        return ERROR_MESSAGE;
    }

    static string GetACCalculatedNumberList(string arrayStr, string filteredCommands)
    {
        var langACCalculator = new LangACCalculator(arrayStr);
        langACCalculator.ApplyCommands(filteredCommands);
        return langACCalculator.GetCalculatedNumberList();
    }

    class LangACCalculator
    {
        readonly List<string> _calculatedNumberList;
        public LangACCalculator(string arrayStr)
        {
            int EMPTY_ARRAY_LENGTH = 2; // []
            if (arrayStr.Length == EMPTY_ARRAY_LENGTH)
                _calculatedNumberList = new List<string>();
            else
                _calculatedNumberList = new List<string>(arrayStr.Substring(1, arrayStr.Length - 2).Split(','));

        }
        public void ApplyCommands(string commands) // ex. RD, DDR
        {
            bool isReversed = false;
            var leftDeleteCount = 0;
            var rightDeleteCount = 0;
            foreach (char command in commands)
            {
                // 다형성 class 생성으로 if문 없애는 게 가능한가?
                if (command == (char) CommandType.D)
                {
                    var commandD = new CommandD(isReversed);
                    DeleteCount deleteCount = commandD.ApplyCommand((leftDeleteCount, rightDeleteCount));
                    leftDeleteCount = deleteCount.LeftDeleteCount;
                    rightDeleteCount = deleteCount.RightDeleteCount;
                }
                else // 'R'
                {
                    isReversed = new CommandR().ApplyCommand(isReversed);
                }
            }
            RemoveLeftElements(leftDeleteCount);
            RemoveRightElements(rightDeleteCount);
            ReverseListOrder(isReversed);
        }

        void RemoveLeftElements(int leftDeleteCount)
        {
            if (leftDeleteCount > 0)
                _calculatedNumberList.RemoveRange(0, leftDeleteCount);
        }

        void RemoveRightElements(int rightDeleteCount)
        {
            if (rightDeleteCount > 0)
                _calculatedNumberList.RemoveRange(_calculatedNumberList.Count - rightDeleteCount, rightDeleteCount);
        }

        void ReverseListOrder(bool isReversed)
        {
            if (isReversed)
                _calculatedNumberList.Reverse();
        }

        public string GetCalculatedNumberList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
			sb.Append(string.Join(',', _calculatedNumberList));
			sb.Append("]");
			return sb.ToString();
		}
	}

	interface ICommand<in TInput, out TOutput>
	{
		TOutput ApplyCommand(TInput input);
	}

	class CommandR : ICommand<bool, bool>
	{
		public bool ApplyCommand(bool isReversed)
		{
			return !isReversed;
		}
	}

	class CommandD : ICommand<(int leftDeleteCount, int rightDeleteCount), DeleteCount>
	{
		readonly bool _isReversed;
		public CommandD(bool isReversed)
		{
			_isReversed = isReversed;
		}
		public DeleteCount ApplyCommand((int leftDeleteCount, int rightDeleteCount) countData)
		{
			var deleteCount = new DeleteCount(_isReversed, countData.leftDeleteCount, countData.rightDeleteCount);
			deleteCount.CalculateDeleteCount();
			return deleteCount;
		}
	}

	interface IDeleteCount
	{
		void CalculateDeleteCount();
	}

	class DeleteCount : IDeleteCount
	{
		public int LeftDeleteCount { get; set; }
		public int RightDeleteCount { get; set; }
		bool _isReversed;
		public DeleteCount(bool isReversed, int leftDeleteCount, int rightDeleteCount)
		{
			_isReversed = isReversed;
			LeftDeleteCount = leftDeleteCount;
			RightDeleteCount = rightDeleteCount;
		}

		public void CalculateDeleteCount()
		{
			if (_isReversed)
				RightDeleteCount++;
			else
				LeftDeleteCount++;
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

	static List<T> CopyList<T>(List<T> list)
	{
		var newList = new List<T>();
		for (var i = 0; i < list.Count; i++)
		{
			newList.Add(list[i]);
		}
		return newList;
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