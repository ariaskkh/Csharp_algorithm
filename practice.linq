<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = int.Parse(Console.ReadLine());
		var maxStarNum = 2 * inputNum;

		for (var i = 1 ; i <= inputNum; i++)
		{	
			for (var j = 0; j < maxStarNum; j++)
			{
				if (i % 2 == 0)
				{
					if (j % 2 == 1)	
					{
						Console.Write('*');	
					}
					else
					{
						Console.Write(' ');	
					}
				}
				else
				{
					if (j % 2 == 1)	
					{
						Console.Write(' ');	
					}
					else
					{
						Console.Write('*');	
					}
				}
			}
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


