using System;
using System.Text;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        var charArray = Console.ReadLine().ToCharArray();
        
        Solve(charArray);
    }

    static void Solve(char[] charArray)
    {
        Console.WriteLine(GetNumber(charArray));
    }

    static int GetNumber(char[] charArray)
    {
        if (!isValid(charArray))
        {
            return 0;
        }
        var totalCount = 0;
        var tempCount = 0;
        Stack<dynamic> stack = new Stack<dynamic>();
        foreach (char ch in charArray)
        {
            
            if (ch == '(')
            {
                stack.Push(ch);
            }
            else if (ch == '[')
            {
                stack.Push(ch);
            }
            else if (ch == ')')
            {

                if (stack.Peek().GetType() == typeof(int))
                {
                    while (stack.Peek().GetType() == typeof(int))
                    {
                        tempCount += stack.Pop();
                    }
                }
                if (stack.Peek() == '(')
                {
                    stack.Pop();
                    if (tempCount == 0)
                    {
                        stack.Push(2);
                    }
                    else
                    {
                        stack.Push(tempCount * 2);
                        tempCount = 0;
                    }
                }
            }
            else // ch == ']'
            {
                if (stack.Peek().GetType() == typeof(int))
                {
                    while (stack.Peek().GetType() == typeof(int))
                    {
                        tempCount += stack.Pop();
                    }
                }
                if (stack.Peek() == '[')
                {
                    stack.Pop();
                    if (tempCount == 0)
                    {
                        stack.Push(3);
                    }
                    else
                    {
                        stack.Push(tempCount * 3);
                        tempCount = 0;
                    }

                }
            }

        }
        totalCount = stack.Sum(x => x);
        return totalCount;
    }

    static bool isValid(char[] charArray)
    {
        if (charArray.Count(ch => ch == '(') != charArray.Count(ch => ch == ')'))
            return false;
        if (charArray.Count(ch => ch == '[') != charArray.Count(ch => ch == ']'))
            return false;
        string st = new string(charArray);
        if (st.Contains("(]") || st.Contains("[)"))
            return false;
        return true;
    }

   

    static Dictionary<char, int> SetDictionary(Dictionary<char, int > charCounts, string inputString)
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