using System;
using System.Collections.Generic;
using NumberUtilityLib;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int> { 15, 3, 2 , 4 ,5 , 6 , 7, 8 ,  9 , 10  };

            list = ListNumberUtility.KLargestTop(list, 3);

            foreach (var x in list)
            {
                Console.Write(x );
                Console.Write(" ");
            }
            Console.WriteLine("End!");
        }
    }
}
