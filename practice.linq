<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputCharArr = Console.ReadLine().ToCharArray();
		var inputIntArr = changeCharToNum(inputCharArr);
		var result = new int[10];
		
		for (var i = 0; i < inputIntArr.Length; i++)
		{
			var idx = inputIntArr[i];
			result[idx] += 1;
		}
		Console.WriteLine(getMaxNum(result));
	}
	
	static int getMaxNum(int[] numArr)
	{
		// 6, 9 예외처리
		var average = (double) (numArr[6] + numArr[9]) / 2;
		
		var tempSixAndNine = (int) Math.Ceiling(average);
		numArr[6] = 0;
		numArr[9] = 0;
		return numArr.Max() > tempSixAndNine ? numArr.Max() : tempSixAndNine;
	}
	
	static int[] changeCharToNum(char[] numsInChar)
	{
		return numsInChar.Select(x => int.Parse(x.ToString())).ToArray();
	}
	
	static int[] changeStrToNum(string[] numsInStr)
	{
		return numsInStr.Select(x => int.Parse(x)).ToArray();
	}
	
	static int reverseSwitch(int number)
	{
		if(number == 0)
		{
			return 1;	
		}
		return 0;
	}
}


