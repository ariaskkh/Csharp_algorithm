﻿using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        var input = ReadLine().Split(' ').ChangeStrToInt();
        var N = input[0];
        var rampLength = input[1];

        var mapData = Enumerable.Range(0, N)
            .Select(_ => ReadLine()
                .Split(' ')
                .ChangeStrToInt()
                .Select(height => new UnitArea(height))
                .ToArray())
            .ToArray();

        var availableRoadCount = Solve(mapData, rampLength);
        WriteLine(availableRoadCount);
    }

    static int Solve(UnitArea[][] mapData, int rampLength)
    {
        var map = new Map(mapData, rampLength);
        map.CheckRoadAvailable();
        return map.AvailableRoadCount;
    }

    class Map
    {
        private UnitArea[][]? mapData;
        private UnitArea?[][]? mapDataReversed;
        private int rampLength;
        private bool[] rowAvailablePathTable;
        private bool[] columnAvailablePathTable;
        public int AvailableRoadCount => rowAvailablePathTable.Count(x => x) + columnAvailablePathTable.Count(x => x);

        public Map(UnitArea[][]? mapData, int rampLength)
        {
            this.mapData = mapData;
            this.rampLength = rampLength;
            SetMapDataReversed(mapData);
            rowAvailablePathTable = Enumerable.Repeat(false, mapData.Length).ToArray();
            columnAvailablePathTable = Enumerable.Repeat(false, mapData.Length).ToArray();
        }

        private void SetMapDataReversed(UnitArea[][]? mapData)
        {
            if (mapData?.Length == 0)
            {
                return;
            }

            mapDataReversed = Enumerable.Range(0, mapData.Length)
                .Select(_ => Enumerable
                    .Repeat(default(UnitArea), mapData.Length)
                    .ToArray())
                .ToArray();

            for (var i = 0; i < mapData.Length; i++)
            {
                for (var j = 0; j < mapData[0].Length; j++)
                {
                    mapDataReversed[i][j] = new UnitArea(mapData[j][i].Height);
                }
            }
        }

        public void CheckRoadAvailable()
        {
            if (mapData?.Length == 0 || mapDataReversed?.Length == 0)
            {
                return;
            }

            // 평평한 길
            CheckFlatRoad();
            // 울퉁불퉁 길
            CheckBumpyRoad();
        }

        private void CheckFlatRoad()
        {
            // row 방향 line
            for (var row = 0; row < mapData.Length; row++)
            {
                if (HasSameHeightInLine(mapData[row]))
                {
                    rowAvailablePathTable[row] = true;
                };
            }

            // column 방향 line
            for (var row = 0; row < mapDataReversed.Length; row++)
            {
                if (HasSameHeightInLine(mapDataReversed[row]))
                {
                    columnAvailablePathTable[row] = true;
                };
            }
        }

        private static bool HasSameHeightInLine(UnitArea[] line)
        {
            if (line.Length > 0)
            {
                var firstHeight = line.First().Height;
                return line.All(x => x.Height == firstHeight);
            }
            return false;
        }

        // false인 line들만 검사
        private void CheckBumpyRoad()
        {
            for (var i = 0; i < mapData!.Length; i++)
            {
                if (rowAvailablePathTable[i] == false)
                {
                    if (CanPutRamp(mapData[i]))
                    {
                        rowAvailablePathTable[i] = true;
                    }
                }
            }

            for (var i = 0; i < mapDataReversed!.Length; i++)
            {
                if (columnAvailablePathTable[i] == false)
                {
                    if (CanPutRamp(mapDataReversed[i]))
                    {
                        columnAvailablePathTable[i] = true;
                    }
                }
            }
        }

        private bool CanPutRamp(UnitArea[] line)
        {
            var lineData = ConvertLineData(line);
            for (var i = 1; i < lineData.Count; i++)
            {
                if (Math.Abs(lineData[i - 1].height - lineData[i].height) > 1)
                {
                    return false;
                }
                else
                {
                    if (lineData[i - 1].height > lineData[i].height)
                    {
                        if (lineData[i].count >= rampLength)
                        {
                            lineData[i] = (lineData[i].height, lineData[i].count - rampLength);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (lineData[i - 1].count >= rampLength)
                        {
                            lineData[i - 1] = (lineData[i - 1].height, lineData[i - 1].count - rampLength);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        // 이런식으로 나옴
        // { (1, 2),
        //   (2, 1),
        //   (1, 3) }
        private List<(int height, int count)> ConvertLineData(UnitArea[] line)
        {
            List<(int height, int count)> lineData = new();

            int tmpCount = 1;
            int tmpHeight = line[0].Height;
            for (var i = 1; i < line.Length; i++)
            {
                if (tmpHeight == line[i].Height)
                {
                    tmpCount++;
                }
                else
                {
                    lineData.Add((tmpHeight, tmpCount));
                    tmpCount = 1;
                    tmpHeight = line[i].Height;
                }
            }
            lineData.Add((tmpHeight, tmpCount));
            return lineData;
        }
    }

    class UnitArea
    {
        public int Height { get; set; }
        public bool HasRamp { get; set; }
        
        public UnitArea(int height)
        {
            Height = height;
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