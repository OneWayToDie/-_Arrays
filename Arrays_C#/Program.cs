//#define ARRAY_1
//#define ARRAY_11
//#define ARRAY_2
#define JAGGED_ARRAYS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays_C_
{
	class Program
	{
		static void Main(string[] args)
		{
			Random rand = new Random(0);

#if ARRAY_1
			int[] arr = { 3, 5, 8, 13, 21 };
			for (int i = 0; i < arr.Length; i++)
			{
				Console.Write(arr[i] + "\t");
			}
			Console.WriteLine();

			foreach (int i in arr)
			{
				Console.WriteLine(i + "\t");
			}
			Console.WriteLine();
#endif

#if ARRAY_11
			Console.Write("Введите размер массива: ");
			int n = Convert.ToInt32(Console.ReadLine());
			int[] arr = new int[n];
			for (int i = 0; i < arr.Length; i++)
			{
				arr[i] = rand.Next(0, 100);
			}
			for (int i = 0; i < arr.Length; i++)
			{
				Console.Write($"{arr[i]}\t");
			}
			Console.WriteLine();
			Console.WriteLine($"Сумма элементов массива: {arr.Sum()}");
			Console.WriteLine($"Среднее арифметическое массива: {arr.Average()}");
			Console.WriteLine($"Минимальное значение в массиве: {arr.Min()}");
			Console.WriteLine($"Максимальное значение в массиве: {arr.Max()}");

			//foreach (ref int i in arr)
			//{
			//	i = rand.Next(100);
			//}
			//foreach (int i in arr)
			//{
			//	Console.Write($"{i}\t");
			//}
			//Console.WriteLine();
#endif

#if ARRAY_2
			Console.Write("Введите количество строк: ");
			int rows = Convert.ToInt32(Console.ReadLine());

			Console.Write("Введите количество элементов строки: ");
			int cols = Convert.ToInt32(Console.ReadLine());

			int[,] arr = new int[rows, cols];
			Console.WriteLine($"Количество измерений: {arr.Rank}");
			Console.WriteLine($"Количество элементов: {arr.Length}");
			for (int i = 0; i < arr.GetLength(0); i++)
			{
				for (int j = 0; j < arr.GetLength(1); j++)
				{
					arr[i, j] = rand.Next(10);
				}
			}
			//foreach (int i in arr)
			//{
			//	Console.Write(i + "\t");
			//}
			//Console.WriteLine();
			for (int i = 0; i < arr.GetLength(0); i++)
			{
				for (int j = 0; j < arr.GetLength(1); j++)
				{
					Console.Write($"{arr[i, j]}\t");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine($"Сумма элементов массива: {arr.Cast<int>().Sum()}"); //Cast преобразует многомерный массив в одномерный массив
			Console.WriteLine($"Среднее арифметическое массива: {arr.Cast<int>().Average()}");
			Console.WriteLine($"Минимальное значение в массиве: {arr.Cast<int>().Min()}");
			Console.WriteLine($"Максимальное значение в массиве: {arr.Cast<int>().Max()}");  
#endif
			int[][] arr =
			{
				new int[]{ 0, 1, 1, 2},
				new int[]{ 3, 5, 8, 13, 21},
				new int[]{ 34, 55, 89},
				new int[]{ 144, 233,377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711, 28657, 46368}
			};
			for (int i = 0; i < arr.Length; i++)
			{
				for (int j = 0; j < arr[i].Length; j++)
				{
					Console.Write(arr[i][j] + "\t");
				}
				Console.WriteLine();
			}
			Console.WriteLine("\n-------------------------------------\n");
			foreach (int[] i in arr)
			{
				foreach(int j in i)
				{
					Console.Write(j + "\t");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
			int sum = 0;
			int count = 0;
			foreach (int[] i in arr)
			{
				sum += i.Sum();
				count += i.Length;
			}
			Console.WriteLine($"Сумма элементов массива: {sum}");
			Console.WriteLine($"Среднее арифметическое элементов массива: {(double)sum/count}");
			int min, max;
			min = max = arr[0][0];
			foreach (int[] i in arr)
			{
				if (i.Min() < min) min = i.Min();
				if (i.Max() > max) max = i.Max();
			}
			Console.WriteLine($"Миниимальное значение в массиве: {min}");
			Console.WriteLine($"Максимальное значение в массиве: {max}");
		}
	}
}
