using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var gears = GetGears();
        var turnInfoList = GetTurnInfoList();
        var score = Solve(gears, turnInfoList);
        WriteLine(score);
    }

    private static IGear[] GetGears()
    {
        return Enumerable.Range(0, 4)
            .Select(_ => ReadLine()
                .Select(ch => GetMagnetic(ch))
                .ToList())
            .Select((gearInfo, index) => new Gear(gearInfo, index + 1))
            .ToArray();
    }

    private static (int gearNumber, TurnDirection direction)[] GetTurnInfoList()
    {
        var numberOfTurn = int.Parse(ReadLine());
        return Enumerable.Range(0, numberOfTurn)
            .Select(_ => ReadLine().Split(' ').ChangeStrToInt())
            .Select(turnInfo => (turnInfo[0], GetTurnDirection(turnInfo[1]))) // gearNumber, turn direction
            .ToArray();
    }

    static int Solve(IGear[] gears, (int, TurnDirection)[] turnInfoList)
    {
        var machine = new GearMachine(gears);
        machine.RunGears(turnInfoList);
        return machine.GetScore();
    }

    enum TurnDirection
    {
        Clockwise,
        CounterClockwise
    }

    enum Magnetic
    {
        N,
        S,
    }

    private static Magnetic GetMagnetic(char magnetic)
    {
        return magnetic switch
        {
            '0' => Magnetic.N,
            '1' => Magnetic.S,
            _ => Magnetic.S,
        };
    }

    private static TurnDirection GetTurnDirection(int direction)
    {
        return direction switch
        {
            1 => TurnDirection.Clockwise,
            -1 => TurnDirection.CounterClockwise,
            _ => TurnDirection.Clockwise,
        };
    }


    class GearMachine
    {
        private IGear[] gears;
        public GearMachine(IGear[] gears)
        {
            this.gears = gears;
        }

        public void RunGears((int gearNumber, TurnDirection direction)[] turnInfo)
        {
            foreach ((int gearNumber, TurnDirection direction) in turnInfo)
            {
                TurnGears(gearNumber, direction);
            }
        }

        private void TurnGears(int gearNumber, TurnDirection direction)
        {
            var targetGear = gears[gearNumber - 1];
            var targetGearLeftContactMagnetic = targetGear.LeftContactMagnetic;
            var targetGearRIghtContactMagnetic = targetGear.RightContactMagnetic;
            TurnTargetGear(targetGear, direction);
            TurnLeftGear(gearNumber, direction, targetGearLeftContactMagnetic);
            TurnRightGear(gearNumber, direction, targetGearRIghtContactMagnetic);
        }

        private void TurnTargetGear(IGear gear, TurnDirection direction)
        {
            if (direction == TurnDirection.Clockwise)
            {
                gear.TurnClockwise();
            }
            else if (direction == TurnDirection.CounterClockwise)
            {
                gear.TurnCounterClockwise();
            }
        }

        private void TurnLeftGear(int gearNumber, TurnDirection direction, Magnetic gearLeftContactMagnetic)
        {
            var leftGearIndex = (gearNumber - 1) - 1;
            if (leftGearIndex >= 0) // 좌측 기어 존재
            {
                var leftGear = gears[leftGearIndex];
                if (leftGear.RightContactMagnetic != gearLeftContactMagnetic)
                {
                    var leftGearLeftContactMagnetic = leftGear.LeftContactMagnetic;
                    var leftGearDirection = direction == TurnDirection.Clockwise ? TurnDirection.CounterClockwise : TurnDirection.Clockwise;
                    TurnTargetGear(leftGear, leftGearDirection);
                    TurnLeftGear(gearNumber - 1, leftGearDirection, leftGearLeftContactMagnetic);
                }
            }
        }

        private void TurnRightGear(int gearNumber, TurnDirection direction, Magnetic gearRIghtContactMagnetic)
        {
            var rightGearIndex = (gearNumber - 1) + 1;
            if (rightGearIndex < gears.Length) // 우측 기어 존재
            {
                var rightGear = gears[rightGearIndex];
                if (gearRIghtContactMagnetic != rightGear.LeftContactMagnetic)
                {
                    var rightGearRightContactMagnetic = rightGear.RightContactMagnetic;
                    var rightGearDirection = direction == TurnDirection.Clockwise ? TurnDirection.CounterClockwise : TurnDirection.Clockwise;
                    TurnTargetGear(rightGear, rightGearDirection);
                    TurnRightGear(gearNumber + 1, rightGearDirection, rightGearRightContactMagnetic);
                }
            }
        }


        public int GetScore()
        {
            var score = 0;
            if (gears[0].GearInfo[0] == Magnetic.S) score += 1;
            if (gears[1].GearInfo[0] == Magnetic.S) score += 2;
            if (gears[2].GearInfo[0] == Magnetic.S) score += 4;
            if (gears[3].GearInfo[0] == Magnetic.S) score += 8;
            return score;
        }

    }

    interface IGear
    {
        public int GearNumber { get; }
        public List<Magnetic> GearInfo { get; }
        public void TurnClockwise();
        public void TurnCounterClockwise();


        public Magnetic LeftContactMagnetic { get; }
        public Magnetic RightContactMagnetic { get; }
    }

    class Gear : IGear
    {
        public int GearNumber { get; }
        /// <summary> 12시 방향부터 8개 극 </summary>
        public List<Magnetic> GearInfo { get; } = new();
        /// <summary> 9시 방향 7번째 접촉 극 </summary>
        public Magnetic LeftContactMagnetic => GearInfo[6];
        /// <summary> 3시 방향 3번째 접촉 극 </summary>
        public Magnetic RightContactMagnetic => GearInfo[2];



        public Gear(List<Magnetic> gearInfo, int gearNumber)
        {

            GearInfo = gearInfo;
            GearNumber = gearNumber;
        }

        public void TurnClockwise()
        {
            var last = GearInfo.LastOrDefault();
            GearInfo.Insert(0, last);
            GearInfo.RemoveAt(GearInfo.Count - 1);
        }

        public void TurnCounterClockwise()
        {
            var first = GearInfo.FirstOrDefault();
            GearInfo.Add(first);
            GearInfo.RemoveAt(0);
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

public class Result<T>
{
    public string Error { get; set; }
    public T Content { get; set; }

    public bool HasContent { get; set; } = false;

    public static Result<T> Success(T content)
    {
        var result = new Result<T>();
        result.Content = content;
        result.HasContent = true;
        return result;
    }

    public static Result<T> Fail(string error = "error")
    {
        var result = new Result<T>();
        result.Error = error;
        return result;
    }

    public bool IsSuccess()
    {
        return Error == null;
    }

    public bool IsError()
    {
        return Error != null;
    }
}