<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = int.Parse(Console.ReadLine());
		var maxStarNum = 2 * inputNum - 1;

		for (var i = 1 ; i <= inputNum; i++)
		{
			var numOfOuterSpace = 0;
			if (i == 1)
			{
				numOfOuterSpace = (maxStarNum - 1) / 2;
				printManyTimes(numOfOuterSpace, ' ');
				Console.WriteLine('*');
				continue;
			}
			
			var numOfInnerSpaceWithStar = (i * 2) - 1;
			var numOfInnerSpace = numOfInnerSpaceWithStar - 2;
			numOfOuterSpace = (maxStarNum - numOfInnerSpaceWithStar) / 2;			

			printManyTimes(numOfOuterSpace, ' ');
			Console.Write('*');
			printManyTimes(numOfInnerSpace, ' ');
			Console.Write('*');
			Console.WriteLine();
		}
	}

	static void printManyTimes(int maxLength, char character)
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


