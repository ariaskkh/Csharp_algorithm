using System;
using System.Text;
using static System.Console;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        var N = int.Parse(ReadLine());
        string[][] optionArray = new string[N][];
        for (var i = 0; i < N; i++)
        {
            optionArray[i] = ReadLine().Split(' ');
        }
        Solve(optionArray);
    }

    static void Solve(string[][] optionArray)
    {

        var option = new Option(optionArray);
        option.SetShortCutKey();
        var newOptionArray = option.GetOptions();

        foreach (string[] newOption in newOptionArray)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string word in newOption)
            {
                sb.Append(word + ' ');
            }
            WriteLine(sb);
        }
    }

    class Option
    {
        string[][] _optionArray;
        List<char> _shortCutKeyList = new List<char>();

        public Option(string[][] optionArray)
        {
            _optionArray = optionArray;
        }

        public void SetShortCutKey()
        {
            for (var i = 0; i < _optionArray.Length; i++)
            {
                bool hasShortCutKeySet = false;
                // 제일 앞 글자
                for (var j = 0; j < _optionArray[i].Length; j++)
                {
                    var st = _optionArray[i][j];
                    if (!_shortCutKeyList.Any(c => char.ToUpperInvariant(c) == char.ToUpperInvariant(st[0])) && hasShortCutKeySet == false)
                    {
                        _shortCutKeyList.Add(st[0]);
                        _optionArray[i][j] = "[" + st[0] + "]" + st.Substring(1);
                        hasShortCutKeySet = true;
                    }
                }

                if (!hasShortCutKeySet)
                {
                    // 전체 글자
                    for (var j = 0; j < _optionArray[i].Length; j++)
                    {
                        int index = default(int);
                        char ch = default(char);
                        for (var k = 0; k < _optionArray[i][j].Length; k++)
                        {

                            char tempCh = _optionArray[i][j][k];
                            if (!_shortCutKeyList.Any(ch => char.ToUpperInvariant(ch) == char.ToUpperInvariant(tempCh)) && hasShortCutKeySet == false)
                            {
                                _shortCutKeyList.Add(tempCh);
                                index = k;
                                ch = tempCh;
                                hasShortCutKeySet = true;
                            }
                        }

                        if (index != default(int) && ch != default(char) && hasShortCutKeySet == true)
                        {
                            _optionArray[i][j] = _optionArray[i][j].Substring(0, index) + "[" + ch + "]" + _optionArray[i][j].Substring(index + 1);
                        }

                    }
                }

            }
        }

        public string[][] GetOptions()
        {
            return _optionArray;
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