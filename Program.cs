using System;

class Program
{
    static void Main(string[] args)
    {
        var inputStr = Console.ReadLine().Split(' ');
        var hr = int.Parse(inputStr[0]);
        var min = int.Parse(inputStr[1]);

        if(min >= 45){
            min -= 45;
        } else {
            if (hr > 0){
                hr -= 1;
            } else {
                hr = 23;
            }
            min = min + 60 - 45;
        }
        Console.WriteLine($"{hr} {min}");
    }
}
