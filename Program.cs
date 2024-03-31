using System;

class Program
{
    static void Main(string[] args)
    {
        var inputStr = Console.ReadLine().Split(' ');
        var dice1 = int.Parse(inputStr[0]);
        var dice2 = int.Parse(inputStr[1]);
        var dice3 = int.Parse(inputStr[2]);

        var money = 0;
        // 3개 같음
        if(dice1 == dice2 & dice2 == dice3){
            money = 10000 + dice1 * 1000;
            Console.WriteLine(money);
            return;
        }
        // 2개 같음
        if((dice1 == dice2) | (dice1 == dice3)){
            money = 1000 + dice1 * 100;
            Console.WriteLine(money);
            return;
        }
        if(dice2 == dice3){
            money = 1000 + dice2 * 100;
            Console.WriteLine(money);
            return;
        }
        // 모두 다른 눈        
        int[] numbers = {dice1, dice2, dice3};
        money = numbers.Max()*100;
        Console.WriteLine(money);
    }
}
