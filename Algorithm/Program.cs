using System;
using System.Text;

namespace Algorithm;
public class Program
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        var N = input[0];
        var myScore = input[1];
        var maxRankNumber = input[2];

        if (N == 0)
        {
            Console.WriteLine(1);
            return;
        }
        List<int> rankList = ChangeStrToInt(Console.ReadLine().Split(' '));
        Solve(rankList, myScore, maxRankNumber);
    }

    static void Solve(List<int> rankList, int myScore, int maxRankNumber)
    {
        var rankCalculator = new RankCalculator(rankList, maxRankNumber);
        var rank = rankCalculator.GetRank(myScore);
        Console.WriteLine(rank);
    }

    class RankCalculator
    {
        int _maxRankNumber;
        List<int> _rankList;
        int _rankListLength => _rankList.Count;

        public RankCalculator(List<int> rankList, int maxRankNumber)
        {
            _rankList = rankList;
            _maxRankNumber = maxRankNumber;
        }

        public int GetRank(int score)
        {
            if (IsOutOfRank(score))
            {
                return -1;
            }

            int count = 1;
            foreach (int rank in _rankList)
            {
                if (rank > score)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
            return count;
        }

        private bool IsOutOfRank(int myScore)
        {
            // 0 <= N <= maxRankNumber
            // 10 <= maxRankNumber <= 50
            if (_rankListLength == _maxRankNumber && _rankList[_rankListLength - 1] >= myScore)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    static List<int> ChangeStrToInt(string[] numsInStr)
    {
        return numsInStr.Select(x => int.Parse(x)).ToList();
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