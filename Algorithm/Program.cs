using System.Text;
using static System.Console;

namespace Algorithm;

public class Program
{
    static void Main(string[] args)
    {
        var N = int.Parse(ReadLine());
        string[] optionArray = new string[N];
        for (var i = 0; i < N; i++)
        {
            optionArray[i] = ReadLine();
        }
        Solve(optionArray);
    }

    static void Solve(string[] optionArray)
    {
        var generator = new ShortcutKeyGenerator(optionArray);
        generator.SetShortcutKey();
        var optionsWithShortcutKey = generator.GetOptionsWithShortcutKey();
        foreach (string option in optionsWithShortcutKey)
        {
            WriteLine(option);
        }
    }

    class ShortcutKeyGenerator
    {
        Option[] _optionArray = new Option[] { };
        List<char> _shortcutKeyList = new List<char>();

        public ShortcutKeyGenerator(string[] optionArray)
        {
            _optionArray = new Option[optionArray.Length];
            for (var i = 0; i < optionArray.Length; i++)
            {
                _optionArray[i] = new Option(optionArray[i]);
            }
        }

        public void SetShortcutKey()
        {
            foreach (var option in _optionArray)
            {
                SetFirstLetterShortcutKey(option, out bool hasShortCutKeySet);
                if (!hasShortCutKeySet)
                    SetAllWordShortCutKey(option, hasShortCutKeySet);
            }
        }

        void SetFirstLetterShortcutKey(Option option, out bool hasShortcutKeySet)
        {
            hasShortcutKeySet = false;
            foreach (var word in option.OptionWords)
            {
                var firstChar = word[0];
                var firstWordIndex = 0;
                if (CanAddShortcutKey(firstChar, hasShortcutKeySet))
                {
                    _shortcutKeyList.Add(firstChar);
                    option.SetShortcutKey(firstChar, word, firstWordIndex);
                    hasShortcutKeySet = true;
                }
            }
        }

        void SetAllWordShortCutKey(Option option, bool hasShortcutKeySet)
        {
            foreach (var word in option.OptionWords)
            {
                int targetIndex = default(int);
                char targetChar = default(char);
                for (var idx = 0; idx < word.Length; idx++)
                {
                    char ch = word[idx];
                    if (CanAddShortcutKey(ch, hasShortcutKeySet))
                    {
                        _shortcutKeyList.Add(ch);
                        targetIndex = idx;
                        targetChar = ch;
                        hasShortcutKeySet = true;
                    }
                }
                if (targetIndex != default(int) && targetChar != default(char) && hasShortcutKeySet == true)
                    option.SetShortcutKey(targetChar, word, targetIndex);
            }
        }

        bool CanAddShortcutKey(char ch, bool hasShortcutKeySet)
        {
            return !_shortcutKeyList.Any(key => char.ToUpperInvariant(key) == char.ToUpperInvariant(ch)) && hasShortcutKeySet == false;
        }

        public List<string> GetOptionsWithShortcutKey()
        {
            var optionsWithShortcutKey = new List<string>();
            foreach (var option in _optionArray)
            {
                StringBuilder sb = new StringBuilder();
                var hasShortCutKeySet = false;
                foreach (var word in option.OptionWords)
                {
                    if (word == option.ShortcutKeyData.word && hasShortCutKeySet == false)
                    {
                        sb.Append(MarkShortcutKey(word, option.ShortcutKeyData.index));
                        sb.Append(' ');
                        hasShortCutKeySet = true;
                    }
                    else
                    {
                        sb.Append(word);
                        sb.Append(' ');
                    }
                }
                optionsWithShortcutKey.Add(sb.ToString());
            }
            return optionsWithShortcutKey;

            string MarkShortcutKey(string word, int index)
            {
                StringBuilder sbWord = new StringBuilder();
                sbWord.Append(word[..index]);
                sbWord.Append('[');
                sbWord.Append(word[index]);
                sbWord.Append(']');
                sbWord.Append(word[(index + 1)..]);
                return sbWord.ToString();
            }
        }
    }

    class Option
    {
        string _optionFullName;
        public string[] OptionWords;
        public int OptionWordCount => OptionWords.Length;

        public (char shortCutKey, string word, int index) ShortcutKeyData;

        public Option(string option)
        {
            _optionFullName = option;
            OptionWords = option.Split(' ');
        }

        public void SetShortcutKey(char shortCutKey, string word, int shortCutKeyIndexOfWord)
        {
            ShortcutKeyData = (shortCutKey, word, shortCutKeyIndexOfWord);
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


public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
        {
            action(item);
        }
    }

}