using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
	/// <summary>
	/// Класс - описание сотрудника офиса
	/// </summary>
	public class Person
	{
		/// <summary>
		/// Имя сотрудника, представляющее собой автогенрируемое свойство
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Конструктор с параметром типа <see = cref "string"/>
		/// Создает объект класса <see = cref "Person"/> и инициализирует
		/// поле <see = cref "Name"/> значением аргумента <see = cref "name"/>
		/// </summary>
		/// <param name="name">Имя создаваемого объекта - сотрудника офиса</param>
		public Person(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("in Person: wrong name");
			Name = name;
		}

		/// <summary>
		/// Метод, определяющий форму приветствия данного сотрудника со своим пришедшим коллегой
		/// в зависимости от времени прихода последнего
		/// </summary>
		/// <param name="man">Объект типа <see = cref "Person"/> соответствующий пришедшему струднику</param>
		/// <param name="time">Время прихода этого сотрудника</param>
		public void Welcome(Person man, double time)
		{
			if (time < 12)
				Console.WriteLine("Good morning, {0}! - said {1}", man.Name, Name);
			else if (time < 17)
				Console.WriteLine("Good afternoon, {0}! - said {1}", man.Name, Name);
			else
				Console.WriteLine("Good evening, {0}! - said {1}", man.Name, Name);
		}

		/// <summary>
		/// Метод, определяющий форму прощания данного сотрудника
		/// со своим уходящим коллегой
		/// </summary>
		/// <param name="man">Объект типа <see = cref "Person"/> соответствующий уходящему струднику</param>
		public void GoodBye(Person man)
		{
			Console.WriteLine("Good bye, {0}! - said {1}", man.Name, Name);
		}
	}
}