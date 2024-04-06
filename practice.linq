<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = int.Parse(Console.ReadLine());		
		var digit = 0;
		
		for (var i = 1; i < 10; i++)
		{
			if(inputNum / Math.Pow(10, i) < 1)
			{
				digit = i;
				break;
			}
		}
		if (inputNum < 10)
		{
			Console.Write(inputNum);
			return;
		}
		// 120 = (9) * 1 + (90) * 2 + (120 - 100 + 1) * 3
		var answer = 0;
		for (var i = 0; i < digit - 1; i++)
		{
			answer += (int)(9 * Math.Pow(10, i)) * (i + 1);
		}
		answer += (int)(inputNum - Math.Pow(10, digit - 1) + 1) * digit;
		Console.Write(answer);
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


