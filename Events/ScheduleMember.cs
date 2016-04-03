using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
	/// <summary>
	/// Класс, описывающий элементы, которыми оперирует класс <see = cref "Scheduler"/> 
	/// </summary>
	public class ScheduleMember
	{
		/// <summary>
		/// Объект класса <see = cref "Person"/>, который описывает сотрудника
		/// </summary>
		public Person Worker { get; private set; }

		/// <summary>
		/// Время прихода сотрудника
		/// Замечание: так как программа показывает статистику дествий за каждый час суток,
		/// и, следовательно оперирует только часами,
		/// то надобность в переменных типа <see = cref "DateTime"/> отсутствует.
		/// Поэтому в качестве типа выбран тип <see = cref "double"/>
		/// </summary>
		double ComeTime;

		/// <summary>
		/// Время ухода сотрудника
		/// Замечание: так как программа показывает статистику дествий за каждый час суток,
		/// и, следовательно оперирует только часами,
		/// то надобность в переменных типа <see = cref "DateTime"/> отсутствует.
		/// Поэтому в качестве типа выбран тип <see = cref "double"/>
		/// </summary>
		double LeaveTime;


		/// <summary>
		/// Функция для предоставление доступа на чтение к переменной <see = cref "ComeTime"/>
		/// </summary>
		/// <returns>Возвращает значение <see = cref "ComeTime"/></returns>
		public double GetComeTime()
		{
			return ComeTime;
		}
		/// <summary>
		/// Функция для предоставление доступа на чтение к переменной <see = cref "LeaveTime"/>
		/// </summary>
		/// <returns>Возвращает значение <see = cref "LeaveTime"/></returns>
		public double GetLeaveTime()
		{
			return LeaveTime;
		}

		/// <summary>
		/// Конструктор с параметром <see = cref "worker"/></returns> типа <see = cref "string"/></returns>,
		/// который содержит через пробел 3 факта:
		///		1) имя сотрудника
		///		2) время его прихода
		///		3) время его ухода
		///	Функция режет строку в массив строк
		///	и заполняет поля уазанными значениями
		///	Замечание: числовые поля проеряются а валидность. В случае неваидных значений
		///	выбрасывается исключение <see = cref "ArgumentException"/>
		///	Имя принимается как есть
		/// </summary>
		/// <param name="worker">Строка с информацией о новом сотруднике</param>
		public ScheduleMember(string worker)
		{
			string[] members = worker.Split(' ');

			if (members.Length != 3)
				throw new ArgumentException("In Schedule member: wrong number of arguments");

			Worker = new Person(members[0]);

			NumberFormatInfo separator = new NumberFormatInfo();
			separator.NumberDecimalSeparator = ".";

			if (!Double.TryParse(members[1], NumberStyles.AllowDecimalPoint, separator, out ComeTime)
					|| !Double.TryParse(members[2], NumberStyles.AllowDecimalPoint, separator, out LeaveTime))
				throw new ArgumentException("in Schedule member: wrong Come/Leave time");

			ComeTime = Convert.ToInt32(ComeTime);
			if (ComeTime < 0 || ComeTime > 24)
				throw new ArgumentException("in Schedule member: wrong Come time");

			LeaveTime = Convert.ToInt32(LeaveTime);
			if (LeaveTime < 0 || LeaveTime > 24)
				throw new ArgumentException("in Schedule member: wrong Leave time");
		}
	}

}