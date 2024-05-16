using static System.Console;

namespace Algorithm;
public class Program
{
    static void Main(string[] args)
    {
        var input = ReadLine().Split(' ');
        var screenWidth = int.Parse(input[0]);
        var basketWidth = int.Parse(input[1]);

        if (screenWidth <= basketWidth)
            return;

        var N = int.Parse(ReadLine());
        var applePositions = new int[N];
        for (var i = 0; i < N; i++)
            applePositions[i] = int.Parse(ReadLine());
        Solve(screenWidth, basketWidth, applePositions);
    }

    static void Solve(int screenWidth, int basketWidth, int[] applePositions)
    {
        var appleGame = new AppleGame(screenWidth, basketWidth);
        appleGame.DropApples(applePositions);
        WriteLine(appleGame.GetMinBasketMoveDistance());
    }

    class AppleGame
    {
        int _screenWidth;
        Basket _basket;

        public AppleGame(int screenWidth, int basketWidth)
        {
            _screenWidth = screenWidth;
            _basket = new Basket(basketWidth);
        }

        public void DropApples(int[] applePositions)
        {
            if (applePositions.Any(position => position > _screenWidth))
                return;

            foreach (var position in applePositions)
                _basket.MoveToTakeAnApple(position);
        }

        public int GetMinBasketMoveDistance()
        {
            return _basket.GetMinMoveDistance();
        }
    }

    class Basket
    {
        int _basketWidth;
        int _minMoveDistance;
        int _basketLeftPosition;
        int _basketRightPosition;
        public Basket(int basketWitdh, int basketLeftPosition = 0, int basketRightPosition = -1) // basket 위치 지정해서 들어올 수 있게 optional 처리
        {
            _basketWidth = basketWitdh;
            _basketLeftPosition = basketLeftPosition;
            _basketRightPosition = basketRightPosition != -1 ? basketRightPosition : basketWitdh;
        }

        public void MoveToTakeAnApple(int applePosition)
        {
            int MoveRight(int applePosition)
            {
                var distance = applePosition - _basketRightPosition;
                _basketLeftPosition += distance;
                _basketRightPosition += distance;
                return distance;
            }

            int MoveLeft(int applePosition)
            {
                var distance = _basketLeftPosition + 1 - applePosition;
                _basketLeftPosition -= distance;
                _basketRightPosition -= distance;
                return distance;
            }

            var distance = 0;
            if (applePosition > _basketRightPosition)
            {
                distance = MoveRight(applePosition);
            }
            else if (applePosition < _basketLeftPosition + 1)
            {
                distance = MoveLeft(applePosition);
            }
            _minMoveDistance += distance;
        }

        public int GetMinMoveDistance()
        {
            return _minMoveDistance;
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