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
        Student[] studentsData = new Student[N];
        for (var index = 0; index < N; index++)
        {
			int[] classData = ReadLine().Split(' ').Select(int.Parse).ToArray();
            studentsData[index] = new Student(index, classData);
        }

        var classPresidentManager = new ClassPresidentManager(studentsData);
        classPresidentManager.CheckSameClassmate();
        WriteLine(classPresidentManager.GetPresident());
    }

    class ClassPresidentManager
    {
        static int MAX_GRADE = 5;
        Student[] _studentsData;
        public ClassPresidentManager(Student[] studentsData)
        {
            _studentsData = studentsData;
        }

        public void CheckSameClassmate()
        {
            bool WasInSameClass(int[] classData1, int[] classData2)
			{
				return Enumerable.Range(0, MAX_GRADE).Any(grade => classData1[grade] == classData2[grade]);
			}

			void AddClassmate(Student student1, Student student2 )
			{
				student1.AddSameClassmate(student2);
				student2.AddSameClassmate(student1);
			}

			_studentsData.ForEach(student1 => _studentsData
					.Where(student2 => WasInSameClass(student1.ClassData, student2.ClassData))
					.ForEach(student2 => AddClassmate(student1, student2)));
        }

        public int GetPresident()
        {
			var max = _studentsData.Select(student => student.GetSameClassmateNumber()).Max();
			var president = _studentsData.Where(student => student.GetSameClassmateNumber() == max)
					.Select(student => student.StudentId).FirstOrDefault();
			return president;
        }
    }

	class Student
	{
		public int StudentId { get; } = -1;
		public int[] ClassData { get; }
		List<Student> _sameClassmateList = new List<Student>();

        public Student(int studentIndex, int[] classData)
		{
			StudentId = studentIndex + 1;
			ClassData = classData;
        }

		public void AddSameClassmate(Student student)
		{
			if (_sameClassmateList.Contains(student))
				return;
			else
				_sameClassmateList.Add(student);
        }

		public int GetSameClassmateNumber()
		{
			return _sameClassmateList.Count;
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