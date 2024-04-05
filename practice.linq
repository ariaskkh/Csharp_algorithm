<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = int.Parse(Console.ReadLine());
		var maxStarNum = inputNum;

		for (var i = 1; i <= inputNum; i++)
		{
			var tmpStar = i;
			var numOfSpace = maxStarNum - tmpStar;

			printMnayTimes(numOfSpace, ' ');
			printMnayTimes(tmpStar, '*');
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


