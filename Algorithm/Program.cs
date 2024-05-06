using System;
using System.Text;
using System.Xml.Linq;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        Solve(N);
    }

    static void Solve(int N)
    {
        StringBuilder sb = new StringBuilder();
        for (var i = 0; i < N; i++)
        {
            string functions = Console.ReadLine();
            int listLength = int.Parse(Console.ReadLine());
            string arrayStr = Console.ReadLine();

            var filteredFunctions = functions.Replace("RR", "");
            var countOfD = filteredFunctions.Count(ch => ch == 'D');

            if (listLength < countOfD)
                sb.AppendLine("error");
            else
            {
                var langAC = new LangAC(arrayStr);
                langAC.SetFunction(filteredFunctions);
                sb.Append(langAC.PrintIntList());
            }
        }
        Console.WriteLine(sb.ToString());
    }

    class LangAC
    {
        List<string> _updatedIntList;
        public LangAC(string arrayStr)
        {
            int emptyArrayStringLength = 2; // []
            if (arrayStr.Length == emptyArrayStringLength)
                _updatedIntList = new List<string>();
            else
                _updatedIntList = new List<string>(arrayStr.Substring(1, arrayStr.Length - 2).Split(','));

        }
        public void SetFunction(string functions) // ex. RD, DDR
        {
            bool isReversed = false;
            var leftDeleteCount = 0;
            var rightDeleteCount = 0;
            foreach (char function in functions)
            {
                if (function == 'D')
                {
                    if (isReversed)
                        rightDeleteCount++;
                    else
                        leftDeleteCount++;
                }
                else // 'R'
                    isReversed = !isReversed;
            }

            if (leftDeleteCount > 0)
                _updatedIntList.RemoveRange(0, leftDeleteCount);
            if (rightDeleteCount > 0)
                _updatedIntList.RemoveRange(_updatedIntList.Count - rightDeleteCount, rightDeleteCount);
            if (isReversed)
                _updatedIntList.Reverse();
        }

        public string PrintIntList()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            for (var j = 0; j < _updatedIntList.Count; j++)
            {
                if (j < _updatedIntList.Count - 1)
                    sb.Append(_updatedIntList[j] + ',');
                else
                    sb.Append(_updatedIntList[j]);
            }
            sb.AppendLine("]");
            return sb.ToString();
        }
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


    static IEnumerable<(T1, T2)> IteratonFunction<T1, T2>(IEnumerable<T1> arr1, IEnumerable<T2> arr2)
    {
        return arr1.Join<T1, T2, bool, (T1, T2)>(arr2, item1 => true, item2 => true, (item1, item2) => (item1, item2));
    }
    static IEnumerable<(T1, T2, T3)> IteratonFunction<T1, T2, T3>(IEnumerable<T1> arr1, IEnumerable<T2> arr2, IEnumerable<T3> arr3)
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