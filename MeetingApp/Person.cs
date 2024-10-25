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
    public class Person
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
            this.Name = name;
            this.PhoneNumber = phoneNumber;
        }
    }
}
