using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp
{
    /// <summary>
    /// Встреча
    /// </summary>
    internal class Meeting
    {
        /// <summary>
        /// Человек.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Название встречи.
        /// </summary>
        public string Name {  get; set; }

        /// <summary>
        /// Начало встречи.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Конец встречи.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Время напоминания.
        /// </summary>
        public DateTime ReminderTime { get; set; }

        /// <summary>
        /// Дополнительная информация.
        /// </summary>
        public string AdditionalInformation { get; set; }

        public Meeting(Person person, string name, DateTime startTime, DateTime endTime, DateTime reminderTime, string additionalInformation)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Не введено название встречи.");
            }

            if (startTime >= endTime)
            {
                throw new ArgumentException(" Дата завершения встречи должна быть позже даты её начала.");
            }

            if (startTime < DateTime.Now)
            {
                throw new ArgumentException("Начало встречи не может быть раньше текущей даты.");
            }

            Person = person;
            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            ReminderTime = reminderTime;
            AdditionalInformation = additionalInformation;
        }

    }
}
