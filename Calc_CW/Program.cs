//#define CALC_IF
//#define CALC_SWITCH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc_CW
{
	internal class Program
	{
		//15+30*8-150+1020
		//15+30*8-150+1020/2+150
		static void Main(string[] args)
		{
			Console.Write("Введите арифметическое выражение: ");
			//string expression = "22+33-44/2+8*3";
			string expression = Console.ReadLine();
			expression = expression.Replace(".", ",");
			expression = expression.Replace(" ", "");
			Console.WriteLine(expression);

			char[] operators = new char[] { '+', '-', '*', '/' };
			string[] operands = expression.Split(operators) ;  //strtok() - от C#
			double[] values = new double[operands.Length];
			for (int i = 0; i < operands.Length; i++)
			{
				values[i] = Convert.ToDouble(operands[i]);
				Console.Write($"{values[i]}\t");
			}
			Console.WriteLine();

			char[] digits = "0123456789.".ToCharArray();
			//for (int i = 0; i < digits.Length; i++)
			//{
			//	Console.Write($"{digits[i]}\t");
			//}
			//Console.WriteLine();

			string[] operations = expression.Split(digits);
			operations = operations.Where(o => o != "").ToArray();  //Where(o => o != "") - запомнить(LINQ)
			for (int i = 0; i < operations.Length; i++)
			{
				Console.Write($"{operations[i]}\t");
			}
			Console.WriteLine();

			double result = Hard_Operation(values, operations);
			Console.WriteLine($"Результат: {result}");
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
		static double Hard_Operation(double[] values, string[] operations)
		{
			double result = values[0];

			for (int i = 0; i < operations.Length; i++)
			{
				if (i < operations.Length - 1 && (operations[i + 1] == "*" || operations[i + 1] == "/"))	//Обрабатываем деления и умножения в первую очередь
				{
					double temp = values[i + 1];	//Сохраняем значения во временную переменную
					int j = i + 1;
					while (j < operations.Length && (operations[j] == "*" || operations[j] == "/"))
					{
						if (operations[j] == "*")   //Проходимся по операциям до тех пор, пока они не кончатся
							temp *= values[j + 1];
						else if (operations[j] == "/")
							temp /= values[j + 1];
						j++;
					}
					// Прошёл приоритетные операции в виде умножений и делений, теперь к результирующей переменной прибавляю или вычитаю их, в зависимости от знака
					if (operations[i] == "+")	
						result += temp;
					else if (operations[i] == "-")
						result -= temp;

					i = j - 1;	//Пропускаю пройденные операции
				}
				else //Провожу оставшиеся
				{
					if (operations[i] == "+")
						result += values[i + 1];
					else if (operations[i] == "-")
						result -= values[i + 1];
				}
			}
			return result;
		}
	}
}
