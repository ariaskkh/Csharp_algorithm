<Query Kind="Program" />

using System;

void Main()
{
	var inputStr = Console.ReadLine().Split(' ');
	var N = int.Parse(inputStr[0]);
	var M = int.Parse(inputStr[1]);
	
	string[] arr = new string[N];
	// 직사각형 입력 받기
	for (var s = 0; s < N; s++)
	{
		arr[s] = Console.ReadLine();
	}

	var line = N > M ? M : N;	
	
	while(line>0)
	{
		// 행 (N)
		for (var i = 0; i <= N - line; i++)
		{
			// 열 (M)
			for (var j = 0; j <= M - line; j++)
			{
				var point1 = int.Parse(arr[i][j].ToString());
				var point2 = int.Parse(arr[i][j+line-1].ToString());
				var point3 = int.Parse(arr[i+line-1][j].ToString());
				var point4 = int.Parse(arr[i+line-1][j+line-1].ToString());
				
				if(point1 == point2 && point1 == point3 && point1 == point4)
				{
					var answer = line * line;
					Console.WriteLine(answer);
					return;
				}
			}
		}
		line -= 1;
	}
}
