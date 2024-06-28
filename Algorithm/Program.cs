using System.Text;
using static System.Console;

enum Calculation
{
    Add,
    Remove,
    Check,
    Toggle,
    All,
    Empty,
}

class Program
{
    static void Main(string[] args)
    {
        var n = int.Parse(ReadLine());
        List<TaskUnit> taskList = new();
        //var taskList = Enumerable
        //    .Range(0, n)
        //    .Select(_ => ReadLine().Split(' '))
        //    .Select(item => new Task(item[0], int.Parse(item[1] ?? default )))
        //    .ToList();
        for (var i = 0; i < n; i++)
        {
            var result = ReadLine().Split(' ');
            if (result.Length == 1)
            {
                taskList.Add(new TaskUnit(result[0], default));
            }
            else
            {
                taskList.Add(new TaskUnit(result[0], int.Parse(result[1])));
            }
        }
        var checkList = Solve(taskList);
        var sb = new StringBuilder();
        checkList.ForEach(item => sb.AppendLine(item.ToString()));
        WriteLine(sb.ToString());
    }

    static List<int> Solve(List<TaskUnit> taskList)
    {
        var calculator = new Calculator();
        calculator.Calculate(taskList);
        return calculator.CheckList;
    }

    class Calculator
    {
        private List<int> list = new();
        public List<int> CheckList { get; set; } = new();

        public void Calculate(List<TaskUnit> taskList)
        {
            foreach (var task in taskList)
            {
                switch (task.CalcType)
                {
                    case Calculation.Add:
                        Add(task.Number);
                        break;
                    case Calculation.Remove:
                        Remove(task.Number);
                        break;
                    case Calculation.Check:
                        Check(task.Number);
                        break;
                    case Calculation.Toggle:
                        Toggle(task.Number);
                        break;
                    case Calculation.All:
                        All();
                        break;
                    case Calculation.Empty:
                        Empty();
                        break;
                }
            }
        }


        private void Add(int number)
        {
            if (!list.Contains(number))
                list.Add(number);
        }

        private void Remove(int number)
        {
            if (list.Contains(number))
            {
                list.Remove(number);
            }
        }

        private void Check(int number)
        {
            if (list.Contains(number))
            {
                CheckList.Add(1);
            }
            else
            {
                CheckList.Add(0);
            }
        }

        private void Toggle(int number)
        {
            if (list.Contains(number))
            {
                list.Remove(number);
            }
            else
            {
                list.Add(number);
            }
        }

        private void Empty()
        {
            list = new();
        }

        private void All()
        {
            list = Enumerable.Range(1, 20).ToList();
        }
    }

    class TaskUnit
    {
        public Calculation CalcType { get; set; }
        public int Number { get; set; }
        public TaskUnit(string calcType, int? number)
        {
            SetCalcTpye(calcType);
            Number = number ?? default;
        }
        private void SetCalcTpye(string calcType)
        {
            switch (calcType)
            {
                case "add":
                    CalcType = Calculation.Add;
                    break;
                case "remove":
                    CalcType = Calculation.Remove;
                    break;
                case "check":
                    CalcType = Calculation.Check;
                    break;
                case "toggle":
                    CalcType = Calculation.Toggle;
                    break;
                case "all":
                    CalcType = Calculation.All;
                    break;
                case "empty":
                    CalcType = Calculation.Empty;
                    break;
            }
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

    public static int[] ChangeCharToInt(this IEnumerable<char> enumerable)
    {
        return enumerable.Select(item => int.Parse(item.ToString())).ToArray();
    }

    public static long[] ChangeCharToLong(this IEnumerable<char> enumerable)
    {
        return enumerable.Select(item => long.Parse(item.ToString())).ToArray();
    }

    public static int[] ChangeStrToInt(this IEnumerable<string> enumerable)
    {
        return enumerable.Select(x => int.Parse(x)).ToArray();
    }

    public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
    {
        var queue = new Queue<T>();
        enumerable.ForEach(item => queue.Enqueue(item));
        return queue;
    }

    public static Dictionary<int, int> ConvertToDictionary(this IEnumerable<int> enumerable)
    {
        return enumerable
            .GroupBy(height => height)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}