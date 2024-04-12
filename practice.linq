<Query Kind="Program" />

using System;

class Program
{
	static void Main(string[] args)
	{
		var inputCharArr = Console.ReadLine().ToCharArray();
		var result = solve(inputCharArr);
		Console.WriteLine(result);
	}

	static int solve(char[] charArr)
	{
		var N = charArr.Length;
		var count = N;
		for (var i = 0; i < N; i++)
		{
			if (i == 0)
			{
				continue;
			}
			// '='
			if (charArr[i] == '=')
			{
				// dz=. 이게 z= 보다 먼저 처리되어야 함
				if (charArr[i - 1] == 'z' && i - 2 >= 0 && charArr[i - 2] == 'd')
				{
					count -= 2;
					continue;
				}
				// c=, s=, z=
				if (charArr[i - 1] == 'c' || charArr[i - 1] == 's' || charArr[i - 1] == 'z')
				{
					count -= 1;
					continue;
				}
			}
			// '-'
			if (charArr[i] == '-')
			{
				// c-, d-
				if (charArr[i - 1] == 'c' || charArr[i - 1] == 'd')
				{
					count -= 1;
					continue;
				}
			}
			// 'j'
			if (charArr[i] == 'j')
			{
				// lj, nj
				if (charArr[i - 1] == 'l' || charArr[i - 1] == 'n')
				{
					count -= 1;
					continue;
				}
			}
		}
		return count;
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


