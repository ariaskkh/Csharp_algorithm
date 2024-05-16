﻿using System.Text;
using System.Xml.Linq;
using static System.Console;

namespace Algorithm;
public class Program
{
    static void Main(string[] args)
    {
        string source = "execute";
        Solve(source);
    }

    static void Solve(string source)
    {
        // 1. 정렬
        // 2. 알파벳 별로 개수 저장
        // 3. 한개씩 꺼내서 dest에 넣고 count -= 1
        // 4. 알파벳 없을 때 (source == 0) dest 출력
        var converter = new StringConverter(source);
        converter.Convert();
        WriteLine(converter.GetConvertedString());
    }

    class StringConverter
    {
        string _originalSource;
        Dictionary<char, int> _alphabetDict = new Dictionary<char, int>();
        StringBuilder _dest = new StringBuilder();
        public StringConverter(string source)
        {
            _originalSource = source;
            SetAlphabetDict(source);
        }

        void SetAlphabetDict(string source)
        {
            foreach (char ch in source.ToCharArray().OrderBy(x => x))
            {
                if (_alphabetDict.ContainsKey(ch))
                    _alphabetDict[ch] += 1;
                else
                    _alphabetDict.Add(ch, 1);
            }
        }

        public void Convert()
        {
            while (_alphabetDict.Keys.Count != 0)
            {
                _dest.Append(GetOrderedAlphabetString(_alphabetDict));
                CalculateDic();
            }
        }

        string GetOrderedAlphabetString(Dictionary<char, int> _alphabetDict)
        {
            var keys = _alphabetDict.Select(element => element.Key);
            return string.Join("", keys);
        }

        void CalculateDic()
        {
            foreach (var element in _alphabetDict)
            {
                _alphabetDict[element.Key] -= 1;
                if (_alphabetDict[element.Key] == 0)
                {
                    _alphabetDict.Remove(element.Key);
                }
            }
        }

        public string GetConvertedString()
        {
            return _dest.ToString();
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