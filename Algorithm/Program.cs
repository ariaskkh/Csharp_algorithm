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
        var optionsWithShortCutKey = option.GetOptionsWIthShortCutKey();

        foreach (string[] newOption in optionsWithShortCutKey)
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
        string[][] _optionsWithShortCutKey;
        List<char> _shortCutKeyList = new List<char>();

        public Option(string[][] optionArray)
        {
            _optionArray = optionArray;
            _optionsWithShortCutKey = Copy2DArray(optionArray);
        }

        public void SetShortCutKey()
        {
            for (var row = 0; row < _optionsWithShortCutKey.Length; row++)
            {
                bool hasShortCutKeySet = false;
                hasShortCutKeySet = SetShortCutKeyOfWordFirstLetter(row, hasShortCutKeySet); // set return이 있는 게 좀 걸림.
                if (!hasShortCutKeySet)
                    SetShortCutKeyOfWhole(row, hasShortCutKeySet);
            }
        }

        bool SetShortCutKeyOfWordFirstLetter(int row, bool hasShortCutKeySet)
        {
            for (var column = 0; column < _optionsWithShortCutKey[row].Length; column++)
            {
                var st = _optionsWithShortCutKey[row][column];
                var firstChar = _optionsWithShortCutKey[row][column][0];
                if (!_shortCutKeyList.Any(c => char.ToUpperInvariant(c) == char.ToUpperInvariant(firstChar)) && hasShortCutKeySet == false)
                {
                    _shortCutKeyList.Add(firstChar);
                    _optionsWithShortCutKey[row][column] = "[" + firstChar + "]" + st.Substring(1);
                    hasShortCutKeySet = true;
                }
            }
            return hasShortCutKeySet;
        }

        void SetShortCutKeyOfWhole(int row, bool hasShortCutKeySet)
        {
            for (var j = 0; j < _optionsWithShortCutKey[row].Length; j++)
            {
                int targetIndex = default(int);
                char targetChar = default(char);
                for (var k = 0; k < _optionsWithShortCutKey[row][j].Length; k++)
                {

                    char ch = _optionsWithShortCutKey[row][j][k];
                    if (!_shortCutKeyList.Any(c => char.ToUpperInvariant(c) == char.ToUpperInvariant(ch)) && hasShortCutKeySet == false)
                    {
                        _shortCutKeyList.Add(ch);
                        targetIndex = k;
                        targetChar = ch;
                        hasShortCutKeySet = true;
                    }
                }

                if (targetIndex != default(int) && targetChar != default(char) && hasShortCutKeySet == true)
                    _optionsWithShortCutKey[row][j] = _optionsWithShortCutKey[row][j].Substring(0, targetIndex) + "[" + targetChar + "]" + _optionsWithShortCutKey[row][j].Substring(targetIndex + 1);
            }
        }

        public string[][] GetOptionsWIthShortCutKey()
        {
            return _optionsWithShortCutKey;
        }
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