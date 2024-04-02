<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var N = int.Parse(Console.ReadLine());
		var intArr = Console.ReadLine().Split(' ');
		
		var answerArr = new int[N];
		
		for (var i = 0; i < N; i++)
		{
			var count = int.Parse(intArr[i]);
			var idxOfPerson = i + 1;
			
			for(var j = 0; j < N; j++)
			{
				if(count == 0 && answerArr[j] == 0)
				{
					answerArr[j] = idxOfPerson;
					break;
				}
				if(answerArr[j] == 0)
				{
					count--;
				}
			}
			
		}

		for (var i = 0; i < N; i++)
		{
			Console.Write($"{answerArr[i]} ");
		}	
	}
}
