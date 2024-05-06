using System;
using System.Text;

namespace Algorithm;
public class Program
{
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        int myVote = int.Parse(Console.ReadLine());
        Solve(N, myVote);
    }

    static void Solve(int N, int myVote)
    {
        var voteList = GetValidVoteList(N);
        var machine = new ElectionFraudMachine();
        machine.BuyPeopleUntilWin(myVote, voteList);
        Console.WriteLine(machine.GetMinTimesToWin());
    }

    static public List<int> GetValidVoteList(int N)
    {
        List<int> voteList = new List<int>();
        for (var i = 0; i < N - 1; i++)
        {
            voteList.Add(int.Parse(Console.ReadLine()));
        }

        return voteList;
    }

    class ElectionFraudMachine
    {
        private int _count = 0;
        public void BuyPeopleUntilWin(int myVote, List<int> voteList)
        {
            if (voteList.Count == 0)
            {
                return;
            }
            var max = voteList.Max();
            while (max >= myVote)
            {
                var targetIndex = voteList.IndexOf(voteList.Max());
                voteList[targetIndex] -= 1;
                myVote += 1;
                _count++;
                max = voteList.Max();
            }
        }

        public int GetMinTimesToWin()
        {
            return _count;
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