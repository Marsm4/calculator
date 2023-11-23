using System;
using System.Collections.Generic;

namespace Calculator //калькулятор
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение:");
            string input = Console.ReadLine();

            double result = CalculateExpression(input);
            Console.WriteLine("Результат: " + result);
        }

        static double CalculateExpression(string input)
        {
            string[] tokens = input.Split(' ');

            Stack<double> numbersStack = new Stack<double>();
            Stack<char> operatorsStack = new Stack<char>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out double number))
                {
                    numbersStack.Push(number);
                }
                else
                {
                    char op = token[0];

                    while (operatorsStack.Count > 0 && GetPrecedence(op) <= GetPrecedence(operatorsStack.Peek()))
                    {
                        double result = ApplyOperator(numbersStack.Pop(), numbersStack.Pop(), operatorsStack.Pop());
                        numbersStack.Push(result);
                    }

                    operatorsStack.Push(op);
                }
            }

            while (operatorsStack.Count > 0)
            {
                double result = ApplyOperator(numbersStack.Pop(), numbersStack.Pop(), operatorsStack.Pop());
                numbersStack.Push(result);
            }

            return numbersStack.Pop();
        }

        static double GetPrecedence(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }

        static double ApplyOperator(double b, double a, char op)
        {
            switch (op)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                case '/':
                    return a / b;
                default:
                    throw new ArgumentException("Неверная операция");
            }
        }
    }
}
