using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
	class Program
	{
		/// <summary>
		/// Точка начала работы программы
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Office office = new Office();
			Scheduler sheduler = new Scheduler();
			sheduler.WorkerAction += office.OnScheduler;

			sheduler.StartEmulation(Input("Eneter pause between showing actions"));
			Console.ReadKey();
		}
		/// <summary>
		/// Корректный ввод неотрицательного числа
		/// </summary>
		/// <param name="text">Пригршение для ввода</param>
		/// <returns>Неотрицательное число типа <see = cref "uint"/></returns>
		static int Input(string text)
		{
			Console.Clear();

			int value;

			while (true)
			{
				Console.Write(text + ": ");
				if (!int.TryParse(Console.ReadLine(), out value) || value < 1)
					Console.WriteLine("\nWrong input. Try again\n");
				return value;
			}
		}
	}
}