using static System.Console;

class Program
{
    static void Main(string[] args)
    {
        string inputWord = ReadLine();
        solve(inputWord);
    }

    static void solve(string inputWord)
    {
        var handlers = new List<ICharacterHandler>
        {
            new EqualSignHandler(),
            new DashSignHandler(),
            new CharacterJHandler(),
        };
        var translator = new CroatiaTranslator(handlers);
        translator.Translate(new Word(inputWord));
        WriteLine(translator.CroatiaWordCount);
    }

    interface ITranslator
    {
        void Translate(Word word);
    }

    class CroatiaTranslator : ITranslator
    {
        List<string> _croatiaAlphabetList = new();
        List<ICharacterHandler> handlers;
        public int CroatiaWordCount => _croatiaAlphabetList.Count;

        public CroatiaTranslator(List<ICharacterHandler> handlers)
        {
            this.handlers = handlers;
        }

        public void Translate(Word word)
        {
            var originalWord = word.OriginalWord;
            for (var i = 0; i < word.OriginalWordLength; i++)
            {
                if (i == 0)
                {
                    _croatiaAlphabetList.Add(originalWord[0].ToString());
                    continue;
                }

                bool isHandled = false;

                handlers.Where(handler => 
                    handler.CanHandle(originalWord, i))
                           .ForEach(handler =>
                           {
                               handler.Handle(originalWord, i, _croatiaAlphabetList);
                               isHandled = true;
                           });

                if (!isHandled)
                {
                    _croatiaAlphabetList.Add(originalWord[i].ToString());
                }
            }
            word.CroatiaWord = string.Join("", _croatiaAlphabetList); // 사용되진 않음
        }
    }

    interface ICharacterHandler
    {
        bool CanHandle(string word, int index);
        void Handle(string word, int index, List<string> _croatiaAlphabetList);
    }

    class EqualSignHandler : ICharacterHandler
    {
        public bool CanHandle(string word, int index)
        {
            return word[index] == '=';
        }

        public void Handle(string word, int index, List<string> _croatiaAlphabetList)
        {
            // dz= 이게 z= 보다 먼저 처리되어야 함
            if (word[index - 1] == 'z' && index - 2 >= 0 && word[index - 2] == 'd')
            {
                _croatiaAlphabetList.RemoveRange(_croatiaAlphabetList.Count - 2, 2);
                _croatiaAlphabetList.Add(Utility.GetCroatiaAlphabet(word, index - 2, index));
            }
            // c=, s=, z=
            else if (word[index - 1] == 'c' || word[index - 1] == 's' || word[index - 1] == 'z')
            {
                _croatiaAlphabetList.RemoveAt(_croatiaAlphabetList.Count - 1);
                _croatiaAlphabetList.Add(Utility.GetCroatiaAlphabet(word, index - 1, index));
            }
            else
            {
                _croatiaAlphabetList.Add(Utility.GetCroatiaAlphabet(word, index, index));
            }
        }
    }

    class DashSignHandler : ICharacterHandler
    {
        public bool CanHandle(string word, int index)
        {
            return word[index] == '-';
        }

        public void Handle(string word, int index, List<string> _croatiaAlphabetList)
        {
            // c-, d-
            if (word[index - 1] == 'c' || word[index - 1] == 'd')
            {
                _croatiaAlphabetList.RemoveAt(_croatiaAlphabetList.Count - 1);
                _croatiaAlphabetList.Add(Utility.GetCroatiaAlphabet(word, index - 1, index));
            }
            else
            {
                _croatiaAlphabetList.Add(Utility.GetCroatiaAlphabet(word, index, index));
            }
        }
    }

    class CharacterJHandler : ICharacterHandler
    {
        public bool CanHandle(string word, int index)
        {
            return word[index] == 'j';
        }

        public void Handle(string word, int index, List<string> _croatiaAlphabetList)
        {
            // lj, nj
            if (word[index - 1] == 'l' || word[index - 1] == 'n')
            {
                _croatiaAlphabetList.RemoveAt(_croatiaAlphabetList.Count - 1);
                _croatiaAlphabetList.Add(Utility.GetCroatiaAlphabet(word, index - 1, index));
            }
            else
            {
                _croatiaAlphabetList.Add(Utility.GetCroatiaAlphabet(word, index, index));
            }
        }
    }

    public static class Utility
    {
        public static string GetCroatiaAlphabet(string originalWord, int startIndex, int endIndex)
        {
            return originalWord.Substring(startIndex, endIndex - startIndex + 1);
        }
    }

    class Word
    {
        public string OriginalWord { get; set; }
        public int OriginalWordLength => OriginalWord.ToCharArray().Length;
        public string CroatiaWord { get; set; }
        public Word(string word)
        {
            OriginalWord = word;
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
            var tmpRowArr = ReadLine().ToCharArray();
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
            var tmpRowArr = ReadLine().Split(' ');
            for (var j = 0; j < M; j++)
            {
                resultArr[i, j] = int.Parse(tmpRowArr[j]);
            }
        }
        return resultArr;
    }

    static void Print(int[,] result, int N, int K)
    {
        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < K; j++)
            {
                Write($"{result[i, j]} ");
            }
            WriteLine();
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

    public static int[] ChangeCharToInt(this IEnumerable<char> enumerable)
    {
        return enumerable.Select(item => int.Parse(item.ToString())).ToArray();
    }

    public static long[] ChangeCharToLong(this IEnumerable<char> enumerable)
    {
        return enumerable.Select(item => long.Parse(item.ToString())).ToArray();
    }

    public static int[] ChangeStrToInt(this IEnumerable<string> enumerable)
    {
        return enumerable.Select(x => int.Parse(x)).ToArray();
    }

    public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
    {
        var queue = new Queue<T>();
        enumerable.ForEach(item => queue.Enqueue(item));
        return queue;
    }

    public static Dictionary<int, int> ConverIntListToDict(this IEnumerable<int> enumerable)
    {
        var numberDict = new Dictionary<int, int>();
        foreach (var item in enumerable)
        {
            if (numberDict.ContainsKey(item))
            {
                numberDict[item] += 1;
            }
            else
            {
                numberDict.Add(item, 1);
            }
        }
        return numberDict;
    }
}