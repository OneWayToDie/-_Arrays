using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] ArrayCalc = new string[3];	//Создал строчный массив, записал в него объекты строки
			Console.Write("Введите первый элемент: ");
			ArrayCalc[0] = Console.ReadLine(); //В первый объекст массива сохранил первый элемент
			Console.WriteLine("Выберите оператор: Q = '+', W = '-', E = '/', R = '*'");
			ConsoleKeyInfo key = Console.ReadKey(true);	//ConsoleKeyInfo - Для описания нажатой клавиши, ReadKey - получает нажатую клавишу

			string operation = "";	//Создал с помощью свитчей возможность пользователю выбирать операции
			switch (key.Key)
			{
			case ConsoleKey.Q:	//ConsoleKey - для определения клавиши
				Console.WriteLine("+");
				operation = "+";
				break;
			case ConsoleKey.W:
				Console.WriteLine("-");
				operation = "-";
				break;
			case ConsoleKey.E:  //ConsoleKey - для определения клавиши
				Console.WriteLine("/");
				operation = "/";
				break;
			case ConsoleKey.R:  //ConsoleKey - для определения клавиши
				Console.WriteLine("*");
				operation = "*";
				break;
			case ConsoleKey.Escape: //ConsoleKey - для определения клавиши
				return;
			default:
				Console.WriteLine("Неверный оператор.");
				return;
			}
			ArrayCalc[1] = operation;	//Во второй объект массива сохранил оператор

			Console.Write("Введите второй элемент: ");
			ArrayCalc[2] = Console.ReadLine();	//В третий объект массива сохранил второй элемент

			double result = CalculateArray(ArrayCalc);	//вызвал результирующую функцию, в которой производил вычисления
			Console.WriteLine($"Результат: {ArrayCalc[0]} {ArrayCalc[1]} {ArrayCalc[2]} = {result}");	//Вывел строку с результатами на консоль
		}
		static double CalculateArray(string[] ArrayCalc)	
		{
			double first = double.Parse(ArrayCalc[0]);	//Parse - метод, позволяющий преобразовать строку в необходимый нам тип данных
			string operation = ArrayCalc[1];
			double second = double.Parse(ArrayCalc[2]);
			double result = 0;	//Приравниваем резалт нулю для того чтобы у комплиятора была гарантия возвращения чего-то в функции
			switch (operation)
			{
				case "+":
					result = first + second;
					break;
				case "-":
					result = first - second;
					break;
				case "/":
					if (second != 0)
						result = first / second;
					else
						Console.WriteLine("Делить на ноль нельзя.");
					break;
				case "*":
					result = first * second;
					break;
			}
			return result;
		}
	}
}
