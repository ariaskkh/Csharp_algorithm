using System;

class Program
{
    static void Main(string[] args)
    {
        var inputX = int.Parse(Console.ReadLine());
        var inputY = int.Parse(Console.ReadLine());
        if(inputX > 0){
            if(inputY > 0){
                Console.WriteLine(1);
            } else {
                Console.WriteLine(4);
            }
        } else {
            if(inputY > 0){
                Console.WriteLine(2);
            } else {
                Console.WriteLine(3);
            }
        }
    }
}
