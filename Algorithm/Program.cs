using System;

namespace Algorithm;
public class Program
{
    static void Main(string[] args)
    {
        string inputString = Console.ReadLine();
        Solve(inputString);
    }

    static void Solve(string inputString)
    {
        var palindrome = new Palindrome();
        var palindromeString = palindrome.makePlaindrome(inputString);
        Console.WriteLine(palindromeString);
    }


    class Palindrome
    {
        public string makePlaindrome(string inputString)
        {
            var orderedList = new List<char>(inputString.OrderBy(x => x).ToArray());

            var palindromeFirstHalf = new List<char>();
            var palindromeSecondHalf = new List<char>();
            char tmpCharacter = default;
            int count = 0;
            char odd = default;

            for (var i = 0; i <= orderedList.Count; i++)
            {
                // 그 다음 단계에서 처리해야 함
                // count 를 끝까지 처리

                if (i == 0)
                {
                    tmpCharacter = orderedList[i];
                    count++;
                    continue;
                }

                if (i < orderedList.Count && orderedList[i] == tmpCharacter)
                {
                    count++;
                    continue;
                }
                else // 다른 문자 나옴
                {
                    if (count % 2 != 0) // 그 이전까지 홀수
                    {
                        if (odd == default)
                        {
                            odd = tmpCharacter;
                        }
                        else
                        {
                            return "I'm Sorry Hansoo";
                        }
                    }

                    for (var j = 0; j < count / 2; j++)
                    {
                        palindromeFirstHalf.Add(tmpCharacter);
                        palindromeSecondHalf.Insert(0, tmpCharacter);
                    }
                    if (i < orderedList.Count)
                    {
                        tmpCharacter = orderedList[i];
                        count = 1;
                    }

                }
            }

            if (odd == default)
            {
                return new string(palindromeFirstHalf.ToArray()) + new string(palindromeSecondHalf.ToArray());
            }
            else
            {
                return new string(palindromeFirstHalf.ToArray()) + odd + new string(palindromeSecondHalf.ToArray());
            }

        }
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