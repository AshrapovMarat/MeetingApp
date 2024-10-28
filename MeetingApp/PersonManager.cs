using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp
{
    /// <summary>
    /// Менеджер участников.
    /// </summary>
    internal class PersonManager
    {
        /// <summary>
        /// Изменить имя учатника.
        /// </summary>
        /// <param name="person">Участник.</param>
        /// <param name="newName">Новое имя.</param>
        public void ChangeName(Person person, string newName)
        {
            person.Name = newName;
        }

        /// <summary>
        /// Иззменить номер телефона участника.
        /// </summary>
        /// <param name="person">Участник.</param>
        /// <param name="phoneNumber">Новый номер теелфона.</param>
        public void ChangePhoneNumber(Person person, string phoneNumber)
        {
            person.PhoneNumber = phoneNumber;
        }
    }
}
