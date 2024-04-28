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
        palindrome.MakePlaindrome(inputString);
        Console.WriteLine(palindrome.PrintPalindrome());
    }
    

    class Palindrome
    {
        string _palindrome = "";
        public void MakePlaindrome(string inputString)
        {
            var charCounts = new Dictionary<char, int>();
            var palindromeFirstHalf = new List<char>();
            var palindromeSecondHalf = new List<char>();
            char middleCharacter = default;

            charCounts = SetDictionary(charCounts, inputString);


            foreach (var item in charCounts.OrderBy(c => c.Key))
            {
                char character = item.Key;
                int count = item.Value;
                if (count % 2 != 0)
                {
                    if (middleCharacter == default)
                    {
                        middleCharacter = character;
                    }
                    else
                    {
                        _palindrome =  "I'm Sorry Hansoo";
                        return;
                    }
                }
                for (var j = 0; j < count / 2; j++)
                {
                    palindromeFirstHalf.Add(character );
                    palindromeSecondHalf.Insert(0, character );
                }
            }
            SetPalindromeString(middleCharacter, palindromeFirstHalf, palindromeSecondHalf);
        }

        void SetPalindromeString(char middleCharacter, List<char> palindromeFirstHalf, List<char> palindromeSecondHalf)
        {
            if (middleCharacter == default)
            {
                _palindrome = new string(palindromeFirstHalf.ToArray()) + new string(palindromeSecondHalf.ToArray());
            }
            else
            {
                _palindrome = new string(palindromeFirstHalf.ToArray()) + middleCharacter + new string(palindromeSecondHalf.ToArray());
            }
            
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

        public string PrintPalindrome()
        {
            return _palindrome;
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