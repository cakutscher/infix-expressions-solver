using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace InfixExpressionsSolver
{
    static class Calculator
    {
        /*
         * Algorithm adapted from a solution proposed in stackoverflow https://stackoverflow.com/a/44268877.
         * For better performance, convert infix expression into postfix before solving.
         */
        public static int Evaluate(Array infix)
        {
            var operatorstack = new Stack<string>();
            var operandstack = new Stack<int>();
            var precedence = new Dictionary<string, int> { { "(", 0 }, { "*", 1 }, { "/", 1 }, { "-", 2 }, { "+", 3 }, { ")", 4 } };

            foreach (string ch in infix)
            {
                switch (ch)
                {
                    case var digit when int.TryParse(ch, out int n):
                        operandstack.Push(Convert.ToInt32(digit.ToString()));
                        break;
                    case var op when precedence.ContainsKey(op):
                        var keepLooping = true;
                        while (keepLooping && operatorstack.Count > 0 && precedence[ch] > precedence[operatorstack.Peek()])
                        {
                            switch (operatorstack.Peek())
                            {
                                case "+":
                                    operandstack.Push(operandstack.Pop() + operandstack.Pop());
                                    break;
                                case "-":
                                    operandstack.Push(-operandstack.Pop() + operandstack.Pop());
                                    break;
                                case "*":
                                    operandstack.Push(operandstack.Pop() * operandstack.Pop());
                                    break;
                                case "/":
                                    var divisor = operandstack.Pop();
                                    operandstack.Push(operandstack.Pop() / divisor);
                                    break;
                                case "(":
                                    keepLooping = false;
                                    break;
                            }
                            if (keepLooping)
                            {
                                operatorstack.Pop();
                            }


                        }
                        if (ch == ")")
                        {
                            operatorstack.Pop();
                        }
                        else
                        {
                            operatorstack.Push(ch);
                        }

                        break;
                    default:
                        throw new ArgumentException();
                }

            }

            if (operatorstack.Count > 0 || operandstack.Count > 1)
                throw new ArgumentException();

            return operandstack.Pop();

        }

        public static void CheckBalance(String secuence)
        {
            Stack<char> stack = new Stack<char>();


            foreach (char c in secuence)
            {

                if (c == '(' || c == '{' || c == '[')
                {
                    stack.Push(c);
                    continue;
                }
                switch (c)
                {

                    case ')':
                        if (stack.Count == 0 || stack.Pop() != '(')
                        {
                            throw new ArgumentException();
                        }

                        break;
                    case '}':
                        if (stack.Count == 0 || stack.Pop() != '{')
                        {
                            throw new ArgumentException();
                        }

                        break;
                    case ']':
                        if (stack.Count == 0 || stack.Pop() != '[')
                        {
                            throw new ArgumentException();
                        }

                        break;
                }
            }
            if (stack.Count != 0)
                throw new ArgumentException();
        }

    }
}
