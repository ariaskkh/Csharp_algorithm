using static System.Console;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        var input = ReadLine().Split(' ').Select(int.Parse).ToArray() ;
        var row = input[0];
        var column = input[1];
        int[][] cheesePlate = new int[row][];
        for (var i = 0; i < cheesePlate.Length; i++)
        {
            cheesePlate[i] = ChangeCharToNum(ReadLine().ToCharArray());
        }
        Solve(row, column, cheesePlate);
    }

    static void Solve(int row, int column, int[][] cheesePlate)
    {
        var cheese = new Chesse();
        WriteLine(cheese.GetAllMeltingTime(cheesePlate));
        WriteLine(cheese.GetLastCheeseCpimt());
    }
 

    class Chesse
    {
        int _lastCheeseCount = 0;
        public int GetAllMeltingTime(int[][] cheesePlate)
        {
            var newCheesePlate = cheesePlate; // copy 해야함
            var lastCheeseCount = 0;
            var consumedTime = 0;
            while (true)
            {
                newCheesePlate = GetPlateAfterAnHour(newCheesePlate);
                int cheeseCount = newCheesePlate.Select(row => row.Count(x => x == 1)).Sum();
                
                if (cheeseCount == 0)
                {
                    break;
                }
                consumedTime++;
                _lastCheeseCount = cheeseCount;
            }
            return consumedTime;
        }

        int[][] GetPlateAfterAnHour(int[][] cheesePlate) // 이름 좀..
        {
            //var n = Enumerable.Range(1, cheesePlate.Length - 1);
            //IterationFunction(n, n).Select(item => cheesePlate[item.Item1][item.Item2]) // enumerable의 확장 메소드를 정의할수도?

            var newCheesePlate = Get2DArray(cheesePlate, '0'); // 0으로 시작하지 말고 copy를 가지고 만들자
            for (int i = 1; i < cheesePlate.Length - 1; i++)
            {
                for (int j = 1; j < cheesePlate[i].Length - 1; j++)
                {
                    var tempCount = 0;
                    var dx = new int[] { 1, 0, -1, 0 };
                    var dy = new int[] { 0, 1, 0, -1, };
                    // 4방향 모두 치즈(1) 면 그대로 치즈 아니면 녹아서 0으로 바뀜
                    for (var d = 0; d < 4; d++)
                    {
                        var x = i + dx[i];
                        var y = j + dy[j];
                        if (cheesePlate[x][y] == 1)
                        {
                            tempCount++;
                        }
                        if (tempCount == 4) //치즈로 사방 막힘
                        {
                            newCheesePlate[i][j] = 1;
                        }
                    }
                }
            }
            return newCheesePlate;
        }

        public int GetLastCheeseCpimt()
        {
            return _lastCheeseCount;
        }
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