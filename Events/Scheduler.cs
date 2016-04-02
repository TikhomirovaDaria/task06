using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Events
{
	/// <summary>
	/// Делегат, определяющий тип события, "отправляемого" подписчикам класса <see = cref "Office"/>
	/// </summary>
	/// <param name="man">Объект класса <see = cref "Person"/>, с которым будет работать <see = cref "Office"/></param>
	/// <param name="isComing">Действие. которое будет выполнять <see = cref "Office"/> над <see = cref "Person"/>:
	/// добавить в офис/удалить из офиса</param>
	delegate void ScheduleAction(ScheduleMember man, bool isComing);

	/// <summary>
	/// Класс - планировщик жизни офиса
	/// Расписание приходов и уходов сотрудника берет из внешнего файла
	/// Печатает статистику по приходам и уходам за каждый час: 
	/// кто пришел/ушел, кто с ним поздоровался/попрощался
	/// </summary>
	class Scheduler
	{
		/// <summary>
		/// Событие, которое инициирует класс <see = cref "Office"/> обработать указанного в качестве аргумента человека
		/// </summary>
		public event ScheduleAction WorkerAction;
		/// <summary>
		/// Полный список сотрудников, с которыми можно работать
		/// </summary>
		List<ScheduleMember> workers;

		/// <summary>
		/// Конструктор по умочанию
		/// Пытается открыть указанный в переменной <see = cref "path"/> файл Namex.txt
		/// При отсутствии такой возможности генерируется исключение и вызывается обработчик исключительных ситуаций
		/// При открытии файла идет считывание и добавление всех указанных в нем сотрудников
		/// Ошибки при добавлении сотрудников также отлавливаются и передаются обработчику для дальнейшей работы
		/// Если файл был открыт, то по завершении считывания он будет закрыт
		/// </summary>
		public Scheduler()
		{
			string path = @"../../Names.txt";

			try
			{
				if (!File.Exists(path))
					throw new FileNotFoundException(string.Format("Path [{0}] is invalid", path));
			}
			catch (FileNotFoundException exFile)
			{
				ExceptionHandler(exFile);
			}

			workers = new List<ScheduleMember>();
			string person;

			System.IO.StreamReader file = new System.IO.StreamReader(path);

			while ((person = file.ReadLine()) != null)
			{
				try
				{
					workers.Add(new ScheduleMember(person));
				}
				catch (ArgumentException exArgument)
				{
					ExceptionHandler(exArgument);
				}
			}
			file.Close();
		}

		/// <summary>
		/// Обработчик исключительых ситуаций
		/// Распределяет ошибки по типам и пытается обрабатывать
		///		При возникновении ошбки <see = cref "ArgumentException"/>
		///		обработчик печатает сообщения обо всех встретившихся ошибках такого типа,
		///		с указанием функции, вызвавшей исключительную ситуцию, и, если ошибки не влияют
		///		на работоспосбность программы, говорит, что выплнение программы будет продолжена
		///		
		///		При возникновении ошибки <see = cref "FileNotFoundException"/>
		///		идет незамедлительное уведомление пользователя о невозможности открыть файл
		///		и выход и программы с кодом ошибки -1
		/// </summary>
		/// <param name="ex">Ошибка, которую надо обработать</param>
		void ExceptionHandler(Exception ex)
		{
			Console.WriteLine(ex.Message);
			if (ex is ArgumentException)
			{
				Console.WriteLine("The work will be continue\n");
				Console.ReadKey();
			}

			if (ex is FileNotFoundException)
			{
				Console.ReadKey();
				Environment.Exit(-1);
			}
		}

		/// <summary>
		/// Находит все действия, которые должны быть совершенные за час <see = cref "hour"/>
		/// и инициирует соответствующие события
		/// </summary>
		/// <param name="hour">Час, за который отслеживаются события</param>
		/// <param name="pause">Время ожидаия между совершением событий в секундах</param>
		void Actions(int hour, int pause)
		{
			foreach (ScheduleMember member in workers)
			{
				if (member.GetComeTime() == hour)
				{
					WorkerAction(member, true);
					Thread.Sleep(pause);
				}
				if (member.GetLeaveTime() == hour)
				{
					WorkerAction(member, false);
					Thread.Sleep(pause);
				}
			}
		}

		/// <summary>
		/// Инициирует эмуляцию работы офиса
		/// </summary>
		/// <param name="pause">Время ожидаия между совершением событий в секундах</param>
		public void StartEmulation(int pause)
		{
			Console.Clear();
			for (int hour = 0; hour < 25; hour++)
			{
				Actions(hour, pause * 1000);
			}
		}
	}
}