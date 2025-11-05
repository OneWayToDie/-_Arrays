//#define CALC_IF
//#define CALC_SWITCH
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calc_CW
{
	class Program
	{
		static string expression = "";
		static readonly char[] operators = new char[] { '+', '-', '*', '/' };
		static string[] operands;
		static double[] values;
		static readonly char[] digits = "0123456789.,".ToCharArray();
		static string[] operations;
		static void Main(string[] args)
		{

			Console.Write("Введите арифметическое выражение: ");
			//string expression = "22*33/44/2*8*3";
			expression = "5+(1+(2+(22+3)*2 + (33 + 44))/(2+8)*3+1)*2 - 2";
			//string expression = "((2+3)*4 + (3+2)*4) * 3";
			//string expression = Console.ReadLine();
			expression = expression.Replace(".", ",");
			expression = expression.Replace(" ", "");
			Console.WriteLine(Explorer(expression));
			Console.WriteLine(expression);

		
#if CALC_IF
			if (expression.Contains("+"))
				Console.WriteLine($"{values[0]} + {values[1]} = {values[0] + values[1]}");
			else if (expression.Contains("-"))
				Console.WriteLine($"{values[0]} - {values[1]} = {values[0] - values[1]}");
			else if (expression.Contains("*"))
				Console.WriteLine($"{values[0]} * {values[1]} = {values[0] * values[1]}");
			else if (expression.Contains("/"))
				Console.WriteLine($"{values[0]} / {values[1]} = {values[0] / values[1]}");
			else Console.WriteLine("Error: No operation"); 
#endif

#if CALC_SWITCH
			switch (expression[expression.IndexOfAny(operators)])    //Принимает массив char
			{
				case '+': Console.WriteLine($"{values[0]} + {values[1]} = {values[0] + values[1]}"); break;
				case '-': Console.WriteLine($"{values[0]} - {values[1]} = {values[0] - values[1]}"); break;
				case '*': Console.WriteLine($"{values[0]} * {values[1]} = {values[0] * values[1]}"); break;
				case '/': Console.WriteLine($"{values[0]} / {values[1]} = {values[0] / values[1]}"); break;
			} 
#endif
		}
		static string Explorer(string expression)
		{
			for (int i = 0; i < expression.Length; i++)
			{
				if (expression[i] == '(')
				{
					for (int j = i + 1; j < expression.Length; j++)
					{
						if (expression[j] == ')')
						{
							string substring = expression.Substring(i + 1, j - i - 1);
							if (!substring.Contains('(') && !substring.Contains(')'))
							{
								double result = Calculate(substring);
								Program.expression = Program.expression.Replace($"({substring})", result.ToString());
								Explorer(Program.expression);
							}
						}
						if (expression[j] == '(')
						{
							string substring = expression.Substring(j + 1, expression.Length - j - 1);
							Explorer(substring);
						}
					}
				}
				if (expression[i] == ')')
				{
					string substring = expression.Substring(0, i);
					if (!substring.Contains('(') && !substring.Contains(')'))
					{
						double result = Calculate(substring);
						Program.expression = Program.expression.Replace($"({substring})", result.ToString());
					}
					Explorer(Program.expression);
				}
			}
			return Calculate(Program.expression).ToString();
		}

		static double Calculate(string expression)
		{
			operands = expression.Split(operators);  //strtok() - от C#
			values = new double[operands.Length];
			for (int i = 0; i < operands.Length; i++)
			{
				values[i] = Convert.ToDouble(operands[i]);
				Console.Write($"{values[i]}\t");
			}
			Console.WriteLine();

			operations = expression.Split(digits);
			operations = operations.Where(o => o != "").ToArray();  //Where(o => o != "") - запомнить(LINQ)

			while (operations[0] != "")
			{
				for (int i = 0; i < operations.Length; i++)
				{
					if (operations[i] == "*" || operations[i] == "/")
					{
						if (operations[i] == "*") values[i] *= values[i + 1];
						if (operations[i] == "/") values[i] /= values[i + 1];
						Shift(i);
						if (operations[i] == "*" || operations[i] == "/") i--;
					}
				}
				for (int i = 0; i < operations.Length; i++)
				{
					if (operations[i] == "+" || operations[i] == "-")
					{
						if (operations[i] == "+") values[i] += values[i + 1];
						if (operations[i] == "-") values[i] -= values[i + 1];
						Shift(i);
						if (operations[i] == "+" || operations[i] == "-") i--;
					}
				}
			}
			return values[0];
		}
		static void Shift(int index)
		{
			for (int i = index; i < operations.Length - 1; i++) operations[i] = operations[i + 1];
			for (int i = index + 1; i < values.Length - 1; i++) values[i] = values[i + 1];
			operations[operations.Length - 1] = "";
			values[values.Length - 1] = 0;
		}
	}
}
