using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewCalc
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Напишите количество объектов массива(операции и цифры): ");
			int quantity = Convert.ToInt32(Console.ReadLine());
			string[] ArrayCalc = new string[quantity];  //Создал строчный массив, записал в него объекты строки
			Console.Write("Введите первый элемент: ");
			ArrayCalc[0] = Console.ReadLine(); //В первый объект массива сохранил первый элемент

			string operation = Operation();	//Вынес выбор операции в отдельную функцию, тут выбираем что ы бдуем делать с нашими числами
			if (operation == null) return;	//Если не выбрана операция - прекращаем работу программы
			ArrayCalc[1] = operation;   //Во второй объект массива сохранил оператор

			if (quantity == 3)	//Если число объектов = 3, то мы идём по этому алгоритму
			{
				Console.Write("Введите второй элемент: ");
				ArrayCalc[2] = Console.ReadLine();  //В третий объект массива сохранил второй элемент
				//double result = CalculateArray(ArrayCalc);  //вызвал результирующую функцию, в которой производил вычисления
				double result;	//Создал переменную для результата
				if (Staples(ArrayCalc))	//Если есть скобки в выражении, вызываю функцию, которая их обработает
				{
					result = CalculateStaples(ArrayCalc);
				}
				else //Иначе вызываю функцию, которая посчитает всё по стандарту
				{
					result = CalculateArray(ArrayCalc);
				}
				Console.WriteLine($"Результат: {ArrayCalc[0]} {ArrayCalc[1]} {ArrayCalc[2]} = {result}");   //Вывел строку с результатами на консоль
			}
			else if (quantity < 3) //Ну если квантити меньше трёх, значит у нас есть только одно число и одна операция... нуууууу... даже не знаю что тут ещё сказать.........
			{
				Console.WriteLine("Ну и что это за лажа? ты сюда считать пришёл или чаи гонять???");
			}
			else //Если квантити больше трёх, тада идём по этому алгоритму... писал его я из психушки, отличное заведение, стены прям белые-белые... ()_() - это ваши глаза, когда вы попробуете разобраться в функциях и моих отличных переменных
			{
				Console.Write("Введите элемент: ");
				ArrayCalc[2] = Console.ReadLine();	//Вводим третий элемент по старинке
				double second_result;	//Создаём переменную для подсчёта последующих "результатов"
				if (Staples(ArrayCalc))
				{
					second_result = CalculateStaples(ArrayCalc);	//Вызывается если в выражении есть скобки... ну так должно было быть по задумке... по итогу выдаёт исключения
				}	//Починил... Радио
				else 
				{
					second_result = CalculateArray(new string[] {ArrayCalc[0], ArrayCalc[1], ArrayCalc[2] });	//Если нет скобок...
				}

				for (int i = 3; i < quantity; i++)	//Цикл для прохода по нашим обЖектам, начинаю с трёх, потому что до цикла прогоняем по ним
				{
					string operation_1 = Operation();	//Вызываем функцию с выбором операции, потому что последний объект до этого - был числом
					if (operation_1 == null) return;	//Если операцию не выбрали идём куда по дальше, можно на рабочий стол
					ArrayCalc[i] = operation_1;			//Записываем операцию в массив

					Console.Write("Введите следующий элемент: ");
					string element_again = Console.ReadLine();	//Записываем новое число в переменную

					second_result = Calculate_Again(second_result, operation_1, element_again);	//Вызываем функцию для подсчёта результата с новым числом

					if (i < quantity - 1)	//обработка массива вида: число, операция, число
					{
						i++;
						ArrayCalc[i] = element_again;
					}

					Console.WriteLine($"Результат: {string.Join(" ", ArrayCalc)} = {second_result}");	//Выводим результаты на консоль, Join() - метод для объединения строк, " " является разделителем
				}
			}
		}
		//Делает всё тоже самое, что и Calculate_Array(), но без привязки к "ArrayCalc[]", кароче без привязки к объекту массива
		static double Calculate_Again(double first, string operation, string secondStr)	//Функция для обработки случая,если у нас больше трёх элементов в массиве
		{
			double second = double.Parse(secondStr);	//Преобразуем строки в дабл 
			double result = 0;
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
					{
						result = first / second;
					}
					else
					{
						Console.WriteLine("Делить на ноль нельзя.");
					}
					break;
				case "*":
					result = first * second;
					break;
			}
			return result;
		}
		static string Operation()
		{
			Console.WriteLine("Выберите оператор: Q = '+', W = '-', E = '/', R = '*'");
			ConsoleKeyInfo key = Console.ReadKey(true); //ConsoleKeyInfo - Для описания нажатой клавиши, ReadKey - получает нажатую клавишу
			switch (key.Key)
			{
				case ConsoleKey.Q:  //ConsoleKey - для определения клавиши
					Console.WriteLine("+");
					return "+";
				case ConsoleKey.W:  //ConsoleKey - для определения клавиши
					Console.WriteLine("-");
					return "-";
				case ConsoleKey.E:  //ConsoleKey - для определения клавиши
					Console.WriteLine("/");
					return "/";
				case ConsoleKey.R:  //ConsoleKey - для определения клавиши
					Console.WriteLine("*");
					return "*";
				case ConsoleKey.Escape: //ConsoleKey - для определения клавиши
					return null;
				default:
					Console.WriteLine("Неверный оператор.");
					return null;
			}
		}
		static double CalculateArray(string[] ArrayCalc)	//Функция для подсчёта только ТРЕТЬЕГО объекта(была сделана при самых первых условиях нашей задача, и изначально код не был структурирован под видоизменение, но я упрямая падла, поэтому НАСЛАЖДАЕМСЯ психушкой, господа)
		{
			double first = double.Parse(ArrayCalc[0]);  //Parse - метод, позволяющий преобразовать строку в необходимый нам тип данных
			string operation = ArrayCalc[1];
			double second = double.Parse(ArrayCalc[2]);
			double result = 0;  //Приравниваем резалт нулю для того чтобы у комплиятора была гарантия возвращения чего-то в функции
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
		static bool Staples(string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				string string_now = array[i];
				bool OpenStaples = string_now.Contains("(");	//Contains() - проверяет содержится ли указанный символ/строка в "строке"... да-да, масло-масленное, выражение мыслей - это не моё
				bool CloseStaples = string_now.Contains(")");

				if (OpenStaples || CloseStaples)
				{
					return true;	//Если в строке есть скобки, возвращаем true
				}
				else
				{
					return false;
				}
			}
			return false;
		}
		static double CalculateStaples(string[] expression)
		{
			string Expression = string.Join("", expression);	//Соединяю массив строк в одну без разделителя
			return Calculate_with_Staples(Expression);			//Передаю строку в метод, который будет проводить вычисления над ними
		}
		static double Calculate_with_Staples(string expression)
		{
			while (expression.Contains("("))	//Пока есть открывающая скобка
			{
				int open = expression.LastIndexOf('(');	//Нахожу позиции скобок(LastIndexOf() и IndexOf() - методы для поиска позиции символа в строке)
				int close = expression.IndexOf(')', open);

				if (close == -1)	//Проверяю их парность
				{
					Console.WriteLine("Непарные скобки");
					break;
				}

				string ExpressAviaLinesInToCryMoreThanSleep = expression.Substring(open + 1, close - open - 1);	//Извлекаю выражение из скобок, Substring() - позволяет вырезать часть исходной строки, в данном случае начинаем с "open + 1"
				double MoreCryLessSleepAndDieAloneInPsychiatricHospital = Calculate_My_Cry(ExpressAviaLinesInToCryMoreThanSleep);	//Вычисляю выражение внутри скобок

				//Заменяю скобки на результат вычисления
				expression = expression.Substring(0, open) + MoreCryLessSleepAndDieAloneInPsychiatricHospital.ToString() + expression.Substring(close + 1);
			}
			return Calculate_My_Cry(expression);	//Возвращаю окончательный результат
		}

		static double Calculate_My_Cry(string expression)
		{
			string[] Without_Staples_But_With_Dumbs = expression.Split(new char[] { '+', '-', '*', '/' }, StringSplitOptions.RemoveEmptyEntries); //Разбиваю выражения на числа, убирая операторы. Split() - разрезает строку на части по указанным разделителям
			//StringSplitOptions.RemoveEmptyEntries - Убирает пустые элементы из результата после раздеелния строки
			if (Without_Staples_But_With_Dumbs.Length < 2) return double.Parse(expression);	//Если есть только число - возвращаем его

			double result = double.Parse(Without_Staples_But_With_Dumbs[0]);	//Беру первое число как начальный результат
			//Индекс следующего числа
			int Index_My_Wet_Eyes = 1;  //Да-да, у меня только один глаз мокрый... другой отсох
			//Делаю проход по каждому символу
			for (int i = 0; i < expression.Length; i++)
			{	
				if ("+-*/".Contains(expression[i]))	//Проверяю символы(Contains() - проверяет есть ли в указанной строке необходимый символ)
				{
					// Проверяю наличие следующего числа
					if (Index_My_Wet_Eyes < Without_Staples_But_With_Dumbs.Length)
					{	
						double next_tear = double.Parse(Without_Staples_But_With_Dumbs[Index_My_Wet_Eyes]);//Берём мои слёзы в кулак... Ой, я хотел сказать берём следующее число
						result = Calculate_Again(result, expression[i].ToString(), next_tear.ToString());//Выполняем операцию с текущим результатом
						Index_My_Wet_Eyes++; //опа, кажется я могу приобрести кучу новых глаз... жаль, что они не помогут мне это всё развидеть (переходим к следующему числу)
					}
				}
			}
			return result;
		}
		//Мой код не обрабатывает правильную очердность операций без скобок(нет приоритета операций)
	}
}
