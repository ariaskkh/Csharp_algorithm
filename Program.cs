using System;

class Program
{
    static void Main(string[] args)
    {
        var inputStr = Console.ReadLine();
        var strList = inputStr.Split(' ');
        var A = int.Parse(strList[0].ToString());
        var B = int.Parse(strList[1].ToString());

        if(A > B){
            Console.WriteLine(">");
        } else if (A < B) {
            Console.WriteLine("<");
        } else {
            Console.WriteLine("==");
        }
    }
}
