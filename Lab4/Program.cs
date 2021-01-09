using System;
using System.Collections.Generic;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            var passGenerator = new Generator();
            var passwords = passGenerator.GeneratePasswords();
            foreach (var str in passwords)
            {
                Console.WriteLine(str);
            }

            Console.ReadKey();
        }
    }
}
