using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var input = ReadLine().Split(' ').ChangeStrToInt();
        var roomHeight = input[0];
        var roomWidth = input[1];
        var targetTime = input[2];
        var rawRoomData = Enumerable.Range(0, roomHeight)
            .Select((_, rowIndex) => ReadLine()
                .Split(' ')
                .ChangeStrToInt()
                .ToList()).ToList();

        //roomData.Where((row, rowIndex) => row.Where((area, columnIndex) => area == -1).Select(_ => (rowIndex, columnIndex)));
        var airCleanerIndices = rawRoomData.SelectMany((row, rowIndex) =>
                row.Select((value, columnIndex) => new { value, rowIndex, columnIndex })
                .Where(item => item.value == -1)
                .Select(item => (item.rowIndex, item.columnIndex)))
            .ToArray();
        var airCleaner = new AirCleaner(airCleanerIndices);

        var roomData = rawRoomData.Select(row => row.Select(value =>
                {
                    if (value > 0)
                    {
                        return new UnitArea(new FineDust(value));
                    }
                    else if (value == -1)
                    {
                        return new UnitArea(airCleaner);
                    }
                    else
                    {
                        return null;
                    }
                }).ToList())
            .ToList();

        var ConsumedTime = Solve(roomHeight, roomWidth, roomData, airCleaner, targetTime);
        WriteLine(ConsumedTime);
    }

    static int Solve(int roomHeight, int roomWidth, List<List<UnitArea>> roomData, AirCleaner airCleaner, int targetTime)
    {
        var room = new Room(roomWidth, roomHeight, roomData, airCleaner);
        room.Start(targetTime);
        return room.TotalDustAmount;
    }


    // 확산, 공청기 순환
    class Room
    {
        private int width;
        private int height;
        // 깨끗한 곳은 null, 더러운 곳은 FineDust
        //private List<List<FineDust>> dustdata = new();
        //private AirCleaner airCleaner;
        private List<List<UnitArea>> roomData = new();
        private AirCleaner airCleaner;
        public int ConsumedTime { get; set; }
        public int TotalDustAmount => roomData.Select(row => row
                .Where(x => x?.FineDust != null)
                .Select(x => x.FineDust.Amount)
                .Sum())
            .Sum();
            
        

        public Room(int width, int height, List<List<UnitArea>> roomData, AirCleaner airCleaner)
        {
            this.width = width;
            this.height = height;
            this.roomData = roomData;
            this.airCleaner = airCleaner;
        }

        public void Start(int targetTime)
        {
            var time = 0;
            while (time < targetTime)
            {
                DiffuseDust();
                CleanAir();
                time++;
            }
        }

        private void DiffuseDust()
        {
            // setDIffusionAmount 하고
            // diffuseNumber 구해서 diffuse 하기

            var n = Enumerable.Range(0, height);
            var m = Enumerable.Range(0, width);
            IterationFunction(n, m)
                .Where((x) => roomData[x.Item1][x.Item2]?.FineDust != null)
                .ForEach(x => roomData[x.Item1][x.Item2].FineDust.SetDiffusionAmount());

            for (var row = 0; row < height; row++)
            {
                for (var column = 0; column < width; column++)
                {
                    if (roomData[row][column]?.FineDust != null && roomData[row][column].FineDust.DiffusionAmount != 0)
                    {
                        var fineDust = roomData[row][column].FineDust;
                        var numberOfDiffusion = DiffuseDustFromOneArea(row, column, fineDust.DiffusionAmount);
                        if (numberOfDiffusion > 0)
                        {
                            fineDust.Amount -= numberOfDiffusion * fineDust.DiffusionAmount;
                        }
                    }
                }
            }
        }

        private int DiffuseDustFromOneArea(int row, int column, int diffusionAmount)
        {
            var dx = new int[] { 0, -1, 0, 1 };
            var dy = new int[] { 1, 0, -1, 0 };
            var numberOfDiffusion = 0;

            for (var k = 0; k < 4; k++)
            {
                var nextX = row + dx[k];
                var nextY = column + dy[k];
                
                if (nextX >= 0
                    && nextX < height
                    && nextY >= 0
                    && nextY < width)
                {
                    var nextArea = roomData[nextX][nextY];
                    if (nextArea == null)
                    {
                        roomData[nextX][nextY] = new UnitArea(new FineDust(diffusionAmount));
                        numberOfDiffusion++;
                    }
                    else if (nextArea.FineDust != null)
                    {
                        nextArea.FineDust.Amount += diffusionAmount;
                        numberOfDiffusion++;
                    }
                }
            }
            return numberOfDiffusion;
        }

        private void CleanAir()
        {
            if (airCleaner is null)
            {
                return;
            }
            CirculateUpperSide(airCleaner.UpperSide);
            CirculateUnderSide(airCleaner.UnderSide);
        }

        private void CirculateUpperSide((int row, int column) upperSide)
        {
            // 좌측 세로
            for (var row = upperSide.row - 2; row >= 0; row--)
            {
                roomData[row + 1][0] = roomData[row][0];
            }
            // 윗쪽 가로
            for (var col = 0; col < width - 1; col++)
            {
                roomData[0][col] = roomData[0][col + 1];
            }
            // 우측 세로
            for (var row = 0; row < upperSide.row; row++)
            {
                roomData[row][width - 1] = roomData[row + 1][width - 1];
            }
            // 아랫쪽 가로
            for (var col = width - 1; col > 1; col--)
            {
                roomData[upperSide.row][col] = roomData[upperSide.row][col - 1];
            }
            roomData[upperSide.row][upperSide.column + 1] = null;
        }

        private void CirculateUnderSide((int row, int column) underSide)
        {
            // 좌측 세로
            for (var row = underSide.row + 2; row < height; row++)
            {
                roomData[row - 1][0] = roomData[row][0];
            }
            // 아랫쪽 가로
            for (var col = 0; col < width - 1; col++)
            {
                roomData[height - 1][col] = roomData[height - 1][col + 1];
            }
            // 우측 세로
            for (var row = height - 1; row > underSide.row; row--)
            {
                roomData[row][width - 1] = roomData[row - 1][width - 1];
            }
            // 윗쪽 가로
            for (var col = width - 1; col > 1; col--)
            {
                roomData[underSide.row][col] = roomData[underSide.row][col - 1];
            }
            roomData[underSide.row][underSide.column + 1] = null;

        }
    }

    class UnitArea
    {
        public AirCleaner AirCleaner;
        public FineDust FineDust;

        public UnitArea(AirCleaner airCleaner)
        {
            AirCleaner = airCleaner;
            FineDust = null;
        }

        public UnitArea(FineDust fineDust)
        {
            AirCleaner = null;
            FineDust = fineDust;
        }
    }


    class AirCleaner
    {
        public (int row, int column) UpperSide;
        public (int row, int column) UnderSide;

        public AirCleaner((int row, int column)[] location)
        {
            UpperSide = location[0];
            UnderSide = location[1];
        }
    }

    class FineDust
    {
        public int Amount { get; set; }
        public int DiffusionAmount { get; set; }

        public FineDust(int amount)
        {
            Amount = amount;
        }

        public void SetDiffusionAmount()
        {
            DiffusionAmount = (int)Math.Floor((double)Amount / 5);
        }

        public void Diffuse(int numberOfDIffusionDirection)
        {
            Amount -= numberOfDIffusionDirection * DiffusionAmount;
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