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
        public void ChangeName(Person person, string newName)
        {
            person.Name = newName;
        }

        public void ChangePhoneNumber(Person person, string phoneNumber)
        {
            person.PhoneNumber = phoneNumber;
        }
    }
}
