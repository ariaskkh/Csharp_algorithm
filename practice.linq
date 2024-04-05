<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = int.Parse(Console.ReadLine());
		var maxStarNum = 2 * inputNum;

		for (var i = inputNum ; i > 0; i--)
		{
			var tmpSpace = (i - 1) * 2 ;
			var numOfStar = (maxStarNum - tmpSpace) / 2;

			printMnayTimes(numOfStar, '*');
			printMnayTimes(tmpSpace, ' ');
			printMnayTimes(numOfStar, '*');
			Console.WriteLine();
		}

		for (var i = 1; i < inputNum; i++)
		{
			var tmpSpace = 2 * i;
			var numOfStar = (maxStarNum - tmpSpace) / 2;

			printMnayTimes(numOfStar, '*');
			printMnayTimes(tmpSpace, ' ');
			printMnayTimes(numOfStar, '*');
			Console.WriteLine();
		}
	}

	static void printMnayTimes(int maxLength, char character)
	{
		for (var i = 0; i < maxLength; i++)
		{
			Console.Write(character);
		}
	}
	
	static int[] changeCharToNum(char[] numsInChar)
	{
		return numsInChar.Select(x => int.Parse(x.ToString())).ToArray();
	}
	
	static int[] changeStrToNum(string[] numsInStr)
	{
		return numsInStr.Select(x => int.Parse(x)).ToArray();
	}
}


