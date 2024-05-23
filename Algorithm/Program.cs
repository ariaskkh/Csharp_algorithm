using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var inputNum = int.Parse(ReadLine());
        var resultArr = Solve(inputNum);
        foreach (var result in resultArr)
        {
            WriteLine(result);
        }
    }

    static int[] Solve(int N)
    {
        var orderArr = new int[N];
        for (var i = 0; i < N; i++)
        {
            var targetIndex = ChangeStrToNum(ReadLine().Split(' '))[1];
            int[] priorities = ChangeStrToNum(ReadLine().Split(' '));
			List<Document> documentList =  priorities.Select((priority, index) => new Document(priority, index)).ToList();
            var queue = new PrinterQueue(documentList);
			queue.ProcessPrint();
            orderArr[i] = queue.GetPrintOrder(targetIndex);
        }
        return orderArr;
    }

	class PrinterQueue
	{
		private List<Document> _originalDocumentList = new();
		private Queue<Document> _queue = new();
		private List<Document> _printedOrder = new();

        public PrinterQueue(List<Document> documentList)
		{
			_originalDocumentList = documentList;
            foreach (var document in documentList)
            {
                _queue.Enqueue(document);
            }
		}

		public void ProcessPrint()
		{
			var newQueue = new Queue<Document>(_queue); // deep copy
			while (newQueue.Count > 0)
			{
				var max = newQueue.Select(document => document.Priority).Max();
				if (newQueue.Peek().Priority == max)
				{
					_printedOrder.Add(newQueue.Peek());
					newQueue.Dequeue();
                }
				else
				{
					newQueue.Enqueue(newQueue.Dequeue());
				}
			}
		}

        public int GetPrintOrder(int targetIndex)
		{
			var targetDocument = _printedOrder.Find(document => document.DocumentIndex == targetIndex);
			if (targetDocument != null)
			{
                return _printedOrder.IndexOf(targetDocument) + 1;
            }
			return -1;
        }
    }

	class Document
	{
		public int DocumentIndex { get; } = -1;
		public int Priority { get; } = -1;
		public Document(int priority, int documentIndex)
		{
			Priority = priority;
			DocumentIndex = documentIndex;
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
			var tmpRowArr = ReadLine().ToCharArray();
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
			var tmpRowArr = ReadLine().Split(' ');
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
				Write($"{result[i, j]} ");
			}
			WriteLine();
		}
	}
}

public static class EnumerableExtensions
{
	public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
	{
		foreach (T item in enumeration)
		{
			action(item);
		}
	}
}