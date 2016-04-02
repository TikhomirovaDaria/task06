using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
	/// <summary>
	/// Делегат, определяющий тип события <see = cref "Greeting"/>
	/// </summary>
	/// <param name="man">Объект класса <see = cref "Person"/>, которого требуется приветствовать</param>
	/// <param name="time">Время прихода сотрудника <see = cref "man"/></param>
	delegate void ComingDelegate(Person man, double time);
	/// <summary>
	/// Делегат, определяющий тип события <see = cref "Parting"/>
	/// </summary>
	/// <param name="man">Объект класса <see = cref "Person"/>, с котороым требуется попрощаться</param>
	delegate void LeavingDelegate(Person man);

	/// <summary>
	/// Класс, описывающий офис
	/// </summary>
	public class Office
	{
		/// <summary>
		/// Событие, инициализирующее процесс приветствия
		/// </summary>
		event ComingDelegate Greeting;
		/// <summary>
		/// Событие, инициализирующее процесс прощания
		/// </summary>
		event LeavingDelegate Parting;

		/// <summary>
		/// Список сотрудников, находящихся в настоящий момент в офисе
		/// </summary>
		List<Person> personsIn = new List<Person>();

		/// <summary>
		/// Приход сотрудника в офис
		/// Печатает собщение, что пришел сотрудник  <see = cref "man"/>
		/// Оповещает прочих работников о приходе нового человека
		/// Подписывает пришедшего на приветствие всех приходящих после него
		/// и добавляет в список людей, находящихся в офисе
		/// Замечание: люди с одинаковыми именами могут быть добавлены в список присутствующих. Один и тот же объект - нет
		/// </summary>
		/// <param name="man">Сотрудник - объект класса <see = cref "Person"/>, который пришел в офис</param>
		/// <param name="time">Время прихода этого сотрудника</param>
		public void AddPerson(Person man, double time)
		{
			if (personsIn.Contains(man))
				return;

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("[{0} has come]", man.Name);
			Console.ResetColor();

			if (Greeting != null)
				Greeting(man, time);

			Console.Write("\n\n");

			Greeting += man.Welcome;
			Parting += man.GoodBye;

			personsIn.Add(man);
		}

		/// <summary>
		/// Уход сотрудника из офиса
		/// Печатает собщение, что уходит сотрудник  <see = cref "man"/>
		/// Отписывает уходящего ото всех событий офиса: приветствия и прощания
		/// Удаляет его из списка присутствующих в офисе
		/// Оповещает прочих работников об уходе человека
		/// Замечание: люди с одинаковыми именами могут уходить. Один и тот же объект - нет
		/// </summary>
		/// <param name="man">Сотрудник - объект класса <see = cref "Person"/>, который пришел в офис</param>
		public void RemovePerson(Person man)
		{
			if (!personsIn.Contains(man))
				return;

			Greeting -= man.Welcome;
			Parting -= man.GoodBye;

			personsIn.Remove(man);

			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[{0} has left]", man.Name);
			Console.ResetColor();

			if (Parting != null)
				Parting(man);

			Console.Write("\n\n");
		}

		/// <summary>
		/// Обработчик события, присылаемого классом <see = cref "Scheduler"/>
		/// добавляющий или удаляющий сотрудника <see = cref "man"/>
		/// </summary>
		/// <param name="man">Сотрудник, которого следует добавить/удалить</param>
		/// <param name="isComing">Действие, которое нужно произвести 
		/// над сотрудником: true - добавить, false - удалить</param>
		public void OnScheduler(ScheduleMember man, bool isComing)
		{
			if (isComing)
				AddPerson(man.Worker, man.GetComeTime());
			else
				RemovePerson(man.Worker);
		}
	}
}