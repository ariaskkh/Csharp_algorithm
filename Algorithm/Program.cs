using static System.Console;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        int N = int.Parse(ReadLine());
        Solve(N);
    }

    static void Solve(int N)
    {
        int[][] studentsData = new int[N][];
        for (var i = 0; i < N; i++)
        {
            int[] classData = ReadLine().Split(' ').Select(int.Parse).ToArray();
            studentsData[i] = classData;
        }

        var classPresident = new ClassPresident(studentsData);
        classPresident.CheckSameClassmate();
        WriteLine(classPresident.GetPresident());
    }

    class ClassPresident
    {
        static int MAX_GRADE = 5;
        int[][] _originalStudentsClassData;
        bool[][] _sameClassmateTable;
        public ClassPresident(int[][] studentsClassData)
        {
            _originalStudentsClassData = studentsClassData;
            _sameClassmateTable = Enumerable.Range(0, _originalStudentsClassData.Length)
				.Select(x => Enumerable.Repeat(false, _originalStudentsClassData.Length).ToArray())
				.ToArray();
        }

        public void CheckSameClassmate()
        {
            bool WasSameClass(int student1, int student2, int grade)
			{
				if (_sameClassmateTable[student1][student2] == false
					&& _originalStudentsClassData[student1][grade] == _originalStudentsClassData[student2][grade])
					return true;
				else
					return false;
			}

			void SetSameClassMateTable(int student1, int student2)
			{
                _sameClassmateTable[student1][student2] = true;
                _sameClassmateTable[student2][student1] = true;
            }

			Enumerable.Range(0, _originalStudentsClassData.Length - 1)
				.ForEach(student1 => Enumerable.Range(student1 + 1, _originalStudentsClassData.Length - (student1 + 1))
						.ForEach(student2 => Enumerable.Range(0, MAX_GRADE)
								.Where(grade => WasSameClass(student1, student2, grade))
								.ForEach(grade => SetSameClassMateTable(student1, student2))
						)
				);
		}

        public int GetPresident()
        {
			var counts = _sameClassmateTable.Select(classmateData => classmateData.Count(isTrue => isTrue)).ToList();
            var max = counts.Max();
            var president = counts.IndexOf(max) + 1;
            return president;
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

public static class EnumerableExtensions
{
	// Select와 다르게 LINQ에서 Side effect를 위한 확장 메서드
	public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
	{
		foreach (T item in enumeration)
		{
			action(item);
		}
	}
}