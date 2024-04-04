<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var totalSwitchNum = int.Parse(Console.ReadLine());
		
		string[] switchStatusNumsInStr = Console.ReadLine().Split(' ');
		int[] switchStatusNums = changeStrToNum(switchStatusNumsInStr);
		var numOfPeople = int.Parse(Console.ReadLine());
		
		for (var i = 0; i < numOfPeople; i++)
		{
			var personData = changeStrToNum(Console.ReadLine().Split(' ')); 
			var numForCalc = personData[1];
			// 남자
			if(personData[0] == 1)
			{
				for (var j = 1; j <= totalSwitchNum / numForCalc; j++)
				{
					var tempNum = switchStatusNums[numForCalc * j - 1];
					switchStatusNums[numForCalc * j - 1] = reverseSwitch(tempNum);
				}
			} 
			// 여자
			else
			{
				// 중간 숫자 뒤집기
				switchStatusNums[numForCalc - 1] = reverseSwitch(switchStatusNums[numForCalc - 1]);
				
				var numOfIter = 0;
				if((numForCalc - 1) > (totalSwitchNum - numForCalc))
				{
					numOfIter = totalSwitchNum - numForCalc;
				} else {
					numOfIter = numForCalc - 1;
				}
				// 좌우 대칭으로 숫자 뒤집기
				for(var j = 1; j <= numOfIter; j++)
				{
					if(switchStatusNums[((numForCalc - 1) + j)] == switchStatusNums[((numForCalc - 1) - j)])
					{
						switchStatusNums[(numForCalc -1) + j] = reverseSwitch(switchStatusNums[(numForCalc -1) + j]);
						switchStatusNums[(numForCalc -1) - j] = reverseSwitch(switchStatusNums[(numForCalc -1) - j]);
					} else 
					{
						break;
					}
					
				}	
			}	
		}
		for (var i = 0; i < switchStatusNums.Length; i++)
			{
				Console.Write($"{switchStatusNums[i]} ");
				if((i + 1)% 20 == 0)
				{
					Console.WriteLine();
				}
				
			}	
	}
	
	static int[] changeStrToNum(string[] numsInStr)
	{
		var numsInInt = new int[numsInStr.Length];
		for(var i = 0; i < numsInStr.Length; i++)
			numsInInt[i] = int.Parse(numsInStr[i]);
		
		return numsInInt;
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


