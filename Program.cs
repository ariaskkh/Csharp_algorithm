using System;

class Program
{
    static void Main(string[] args)
    {
        var inputTimeNow = Console.ReadLine().Split(' ');
        var inputNeedTime = int.Parse(Console.ReadLine());
        var hr = int.Parse(inputTimeNow[0]);
        var min = int.Parse(inputTimeNow[1]);

        // 필요한 시간, 분
        var hrP = inputNeedTime / 60;
        var minP = inputNeedTime % 60;
        
        if(hrP == 0){
            if(min + minP < 60){
                min += minP;
            } else {
                if(hr == 23){
                    hr = 0;
                } else {
                    hr += 1;
                }
                min = min + minP - 60;
            }
        } else { // hrP > 0
            if(min + minP < 60){
                hr = (hr + hrP) % 24;
                min += minP;
            } else {
                hr = (hr + hrP + 1) % 24;
                min = min + minP - 60;
            }
        }
        Console.WriteLine($"{hr} {min}");
    }
}
