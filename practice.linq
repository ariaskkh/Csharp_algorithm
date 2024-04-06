<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputNum = int.Parse(Console.ReadLine());		
		var intList = new List<int>();
		var intDict = new Dictionary<int, int>();
		for (var i = 0; i < inputNum; i++)
		{
			var number = int.Parse(Console.ReadLine());
			intList.Add(number);
			
			if (intDict.ContainsKey(number))
			{
				intDict[number] += 1;
			}
			else
			{
				intDict.Add(number, 1);
			}
		}
		intList.Sort();
		
		// 평균값
		Console.WriteLine((int) Math.Round(intList.Average()));
		// 중앙값
		Console.WriteLine(intList[(inputNum - 1) / 2]);
		
		// 최빈값
		var tempList = new List<int>();
		var maxValue = intDict.Values.Max();
		foreach (var pair in intDict)
		{
			if (pair.Value == maxValue)
			{
				tempList.Add(pair.Key);
			}
		}
		tempList.Sort();
		if (tempList.Count > 1)
		{
			Console.WriteLine(tempList[1]);
		}
		else
		{
			Console.WriteLine(tempList[0]);
		}
		// 범위
		Console.WriteLine(intList.Max() - intList.Min());
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


