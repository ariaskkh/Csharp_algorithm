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
			var numOfInnerSpaceWithStar = (i * 2) - 1;
			var numOfInnerSpaceWith = numOfInnerSpaceWithStar - 2;
			var numOfOuterSpace = (maxStarNum - numOfInnerSpaceWithStar) / 2;
			
			if (i == 1)
			{
				printManyTimes(numOfOuterSpace, ' ');
				Console.WriteLine('*');
				continue;
			}
			
			if (i == inputNum)
			{
				printManyTimes(maxStarNum, '*');
				break;
			}

			printManyTimes(numOfOuterSpace, ' ');
			Console.Write('*');
			printManyTimes(numOfInnerSpaceWith, ' ');
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


