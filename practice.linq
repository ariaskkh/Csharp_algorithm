<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var X = int.Parse(Console.ReadLine());
		var upperNum = 0;
		var underNum = 0;
		
		// X 최대 범위 1000일 때 합 고려하여 20000으로
		for (var i = 1; i <20000; i++)
		{
			var totalNumOfBox = i * (i + 1)/2;
			var dif = 0;
			if(totalNumOfBox >= X)
			{
				// 짝수 => i / 1
				dif = totalNumOfBox - X;
				if (i % 2 == 0)
				{
					upperNum = i - dif;
					underNum = 1 + dif;
				} else {
					
					upperNum = 1 + dif;
					underNum = i - dif;
				}
				break;
			}
		}
		Console.WriteLine($"{upperNum}/{underNum}");
	}
}
