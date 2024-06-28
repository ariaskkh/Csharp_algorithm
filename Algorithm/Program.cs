using static System.Console;

class Program
{
    static void Main(string[] args)
    {

        var boardSize = int.Parse(ReadLine());
        var appleCount = int.Parse(ReadLine());
        var appleList = Enumerable.Range(0, appleCount)
            .Select(_ => ReadLine().Split(' ').ChangeStrToInt())
            .Select(data => (data[0] - 1, data[1] - 1))  // row index, column index
            .ToList();
        var directionChangeCount = int.Parse(ReadLine());
        var directionChangeList = Enumerable.Range(0, directionChangeCount)
            .Select(_ => ReadLine().Split(' '))
            .Select(data => (int.Parse(data[0]), GetSnakeDirection(data[1])))  // row, column
            .ToList();

        var survivedTime = Solve(boardSize, directionChangeList, appleList);
        WriteLine(survivedTime);
    }

    private static IDirectionHandler GetSnakeDirection(string direction)
    {
        return direction switch
        {
            //"D" => SnakeDirection.Right,
            //"L" => SnakeDirection.Left,
            //_ => SnakeDirection.Right,
            "D" => new TurnRightHandler(),
            "L" => new TurnLeftHandler(),
            _ => new TurnRightHandler(),
        };
    }

    static int Solve(int boardSize, List<(int, IDirectionHandler)> directionChangeList, List<(int row, int column)> appleList)
    {
        // 보드 class, 뱀 class 생성
        // 보드에서 크기, 뱀 인스턴스, 사과 위치 정보 기억
        // 뱀 class는 위치 및 left, right 메서드 가지기
        // 뱀 이동 시 앞 추가, 뒤 빼기 -> list?

        var snake = new Snake();
        var board = new Board(boardSize, boardSize, snake, directionChangeList, appleList);
        board.Start();
        return board.SurvivedTime;

    }

    private class Board
    {
        private int width;
        private int height;
        private Snake snake;
        private List<(int time, IDirectionHandler direction)> directionChangeInfo = new();
        private List<(int row, int column)> appleList = new();
        public int SurvivedTime { get; set; } = 0;

        public Board(
            int width,
            int height,
            Snake snake,
            List<(int time, IDirectionHandler direction)> directionChangeInfo,
            List<(int, int)> appleList)
        {
            this.width = width;
            this.height = height;
            this.snake = snake;
            this.directionChangeInfo = directionChangeInfo;
            this.appleList = appleList;
        }


        public void Start()
        {
            while (true)
            {
                (int x, int y) location = snake.Move(appleList);
                SurvivedTime++;

                if (IsOutOfBoard(location.x, location.y) || snake.HitItSelf)
                {
                    break;
                }

                if (directionChangeInfo.Count > 0)
                {
                    ChangeDirection(directionChangeInfo.FirstOrDefault());
                }
            }
        }

        private void ChangeDirection((int time, IDirectionHandler handler)? directionChange)
        {
            if (directionChange?.time == SurvivedTime)
            {
                snake.TurnDirection(directionChange?.handler);
                directionChangeInfo.RemoveAt(0);
            }
        }

        private bool IsOutOfBoard(int x, int y)
        {
            return x < 0
                || x >= width
                || y < 0
                || y >= height;
        }
    }



    interface Animal
    {
        int Length { get; set; }
        public List<(int row, int column)> LocationList { get; set; }
        void TurnDirection(IDirectionHandler handler);
    }

    enum SnakeDirection
    {
        Right,
        Down,
        Left,
        Up,
    }

    private class Snake : Animal
    {
        public int Length { get; set; }
        public List<(int row, int column)> LocationList { get; set; } = new List<(int, int)>();
        private List<int> dx = new List<int>() { 0, 1, 0, -1 };
        private List<int> dy = new List<int>() { 1, 0, -1, 0 };
        private SnakeDirection direction; // 0 ~ 3
        public bool HitItSelf { get; set; } = false;

        public Snake()
        {
            Length = 1;
            direction = SnakeDirection.Right;
            LocationList.Add((0, 0));
        }

        public (int x, int y) Move(List<(int row, int column)> appleList)
        {
            // 1. 다음 칸
            (var nextX, var nextY) = GetNextLocation();

            if (LocationList.Contains((nextX, nextY)))
            {
                HitItSelf = true;
                return (nextX, nextY);
            }

            // 2.5 다음 칸 정보 추가
            LocationList.Insert(0, (nextX, nextY));

            // 3. 사과 체크
            if (appleList.Contains((nextX, nextY)))
            {
                // 꼬리 안자름
                Length++;
                appleList.Remove((nextX, nextY));
            }
            else
            {
                // 꼬리 자름
                LocationList.RemoveAt(LocationList.Count - 1);
            }
            //return Survival.Survival;
            return (nextX, nextY);
        }

        private (int nextX, int nextY) GetNextLocation()
        {
            var directionValue = GetDirectionValue();
            var nextX = LocationList.FirstOrDefault().row + dx[directionValue];
            var nextY = LocationList.FirstOrDefault().column + dy[directionValue];
            return (nextX, nextY);
        }

        private int GetDirectionValue()
        {
            return (int)Enum.Parse(typeof(SnakeDirection), direction.ToString());
        }

        public void TurnDirection(IDirectionHandler? handler)
        {
            if (handler != null)
            {
                direction = handler.Handle(direction);
            }
        }
    }

    interface IDirectionHandler
    {
        SnakeDirection Handle(SnakeDirection direction);
    }

    class TurnRightHandler : IDirectionHandler
    {
        public SnakeDirection Handle(SnakeDirection direction)
        {
            return direction switch
            {
                SnakeDirection.Right => SnakeDirection.Down,
                SnakeDirection.Down => SnakeDirection.Left,
                SnakeDirection.Left => SnakeDirection.Up,
                SnakeDirection.Up => SnakeDirection.Right,
                _ => SnakeDirection.Right
            };
        }
    }

    class TurnLeftHandler : IDirectionHandler
    {
        public SnakeDirection Handle(SnakeDirection direction)
        {
            return direction switch
            {
                SnakeDirection.Right => SnakeDirection.Up,
                SnakeDirection.Down => SnakeDirection.Right,
                SnakeDirection.Left => SnakeDirection.Down,
                SnakeDirection.Up => SnakeDirection.Left,
                _ => SnakeDirection.Right
            };
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