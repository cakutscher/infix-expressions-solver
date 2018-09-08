using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace InfixExpressionsSolver
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(" ** Please enter the path to your file **");
            Console.Write("Path: ");
            string pth = Console.ReadLine();
            string txt = System.IO.File.ReadAllLines(pth)[0];

            String secuence = txt.Replace('{', '(').Replace('[', '(').Replace('}', ')').Replace(']', ')');
            secuence = "(" + secuence + ")";
            string[] infix = Regex.Split(secuence, @"(?<=[{}()+*/-])|(?=[{}()+*/-])").Where(x => !string.IsNullOrEmpty(x)).ToArray();

            try
            {
                Calculator.CheckBalance(txt);
                int result = Calculator.Evaluate(infix);
                Console.WriteLine("Output: " + result);

            }
            catch
            {
                Console.WriteLine("Syntax Error");
            }
        }
    }
}
