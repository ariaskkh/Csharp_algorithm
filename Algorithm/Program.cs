using System;
using System.Text;
class Program
{
    static void Main(string[] args)
    {
        var N = int.Parse(Console.ReadLine());
        var cards = Enumerable.Range(1, N).ToList();
        var card = new Card(cards);
        card.Execute();
        Console.WriteLine(card.Print());
    }
    class Card
    {
        readonly List<int> _cards;
        readonly List<int> _discardedCards;
        public int _lastCard { get; set; }
        public Card(List<int> cards)
        {
            _cards = cards;
            _discardedCards = new List<int>();
            _lastCard = 0;
        }

        public void Execute()
        {
            List<int> newCards = new List<int>(_cards);
            while (newCards.Count > 1)
            {
                newCards = Discard(newCards);
                newCards = MoveToBack(newCards);
            }
            _lastCard = newCards[0];
        }

        List<int> Discard(List<int> cards)
        {
            _discardedCards.Add(cards[0]);
            cards.RemoveAt(0);
            return cards;
        }

        List<int> MoveToBack(List<int> cards)
        {
            var tmpCard = cards[0];
            cards.RemoveAt(0);
            cards.Add(tmpCard);
            return cards;
        }

        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var card in _discardedCards)
            {
                sb.Append(card + " ");
            }
            sb.Append(_lastCard);
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