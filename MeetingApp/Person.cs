using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp
{
    /// <summary>
    /// Участник.
    /// </summary>
    internal class Person
    {
        /// <summary>
        /// Имя участник.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список контактной инфорамции.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Имя участник.</param>
        public Person(string name, string phoneNumber) 
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Не введено имя пользователя.");
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("Не введен номер телефона участника.");
            }

            Name = name;
            PhoneNumber = phoneNumber;
        }
    }
}
