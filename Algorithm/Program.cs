﻿using static System.Console;

namespace Algorithm;
public class Program
{
	static void Main(string[] args)
	{
		string inputString = ReadLine();
		Solve(inputString);
	}

	static void Solve(string inputString)
	{
		var generator = new PalindromeGenerator();
		generator.MakePlaindrome(inputString);
		WriteLine(generator.Palindrome);
	}


	class PalindromeGenerator
	{
		public string Palindrome { get; set; } = "";
		private string DEFAULT_PALINDROME = "I'm Sorry Hansoo";
		public void MakePlaindrome(string inputString)
		{
			var charCounts = new Dictionary<char, int>();
			var palindromeFirstHalf = new List<char>();
			char middleCharacter = default;

			charCounts = SetDictionary(charCounts, inputString); // 확장 메서드 가능?


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
						Palindrome = DEFAULT_PALINDROME;
						return;
					}
				}
				for (var j = 0; j < count / 2; j++)
				{
					palindromeFirstHalf.Add(character);
				}
			}
			SetPalindromeString(middleCharacter, palindromeFirstHalf);
		}

		void SetPalindromeString(char middleCharacter, List<char> palindromeFirstHalf)
		{
			var palindromeSecondHalf = GetSecondHalf<char>(palindromeFirstHalf);
			Palindrome = new string(palindromeFirstHalf.ToArray())
				+ (middleCharacter == default ? string.Empty : middleCharacter.ToString())
				+ new string(palindromeSecondHalf.ToArray());
		}

		List<T> GetSecondHalf<T>(List<T> list)
		{
			var secondHalfList = list.ToList();
			secondHalfList.Reverse();
			return secondHalfList;
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
}