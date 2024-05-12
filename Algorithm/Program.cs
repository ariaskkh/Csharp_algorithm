using static System.Console;

namespace Algorithm;

public class Program
{
	static void Main(string[] args)
	{
		var input = ReadLine().Split(' ').Select(int.Parse).ToArray();
		var numberOfPaper = input[0];
		var opaqueThreshold = input[1];
		Solve(numberOfPaper, opaqueThreshold);
	}

	static void Solve(int numberOfPaper, int opaqueThreshold)
	{
		var drawing = new Drawing(opaqueThreshold);
		var paperVertexs = new List<((int, int), (int, int))>();
		for (var i = 0; i < numberOfPaper; i++)
		{
			var paperData = ReadLine().Split(' ').Select(int.Parse).ToArray();
			var vertex1 = (paperData[0], paperData[1]);
			var vertex2 = (paperData[2], paperData[3]);
			paperVertexs.Add((vertex1, vertex2));
		}
		drawing.PutPapers(paperVertexs);
		WriteLine(drawing.GetOpaquePiecesCount());
	}

	class Drawing
	{
		int _opaqueThreshold;
		int[][] _wholeDrawing;
		public Drawing(int opaqueThreshold)
		{
			_opaqueThreshold = opaqueThreshold;
			_wholeDrawing = new int[100][];
			for (var i = 0; i < 100; i++)
			{
				_wholeDrawing[i] = Enumerable.Repeat(0, 100).ToArray();
			}
		}


		public void PutPapers(List<((int x1, int y1), (int x2, int y2))> paperVertexs)
		{
			for (var i = 0; i < paperVertexs.Count; i++)
			{
				// 1 < x1, y1, x2, y2 <= 100;
				var x1 = paperVertexs[i].Item1.x1;
				var y1 = paperVertexs[i].Item1.y1;
				var x2 = paperVertexs[i].Item2.x2;
				var y2 = paperVertexs[i].Item2.y2;

				var x = Enumerable.Range(x1 - 1, x2 - x1 + 1);
				var y = Enumerable.Range(y1 - 1, y2 - y1 + 1);
				IterationFunction(x, y).Select(item => _wholeDrawing[item.Item1][item.Item2] += 1).ToArray();
			}
		}

		public int GetOpaquePiecesCount()
		{
			var n = Enumerable.Range(0, 100);
			var count = IterationFunction(n, n).Where(item => _wholeDrawing[item.Item1][item.Item2] > _opaqueThreshold).Count();
			return count;
		}
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

	static int[] ChangeCharToInt(char[] numsInChar)
	{
		return numsInChar.Select(x => int.Parse(x.ToString())).ToArray();
	}

	static int[] ChangeStrToInt(string[] numsInStr)
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