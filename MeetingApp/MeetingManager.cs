using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp
{
    /// <summary>
    /// Менеджер встреч.
    /// </summary>
    internal class MeetingManager
    {
        /// <summary>
        /// Список встреч.
        /// </summary>
        private List<Meeting> meetings;

        /// <summary>
        /// Менеджер участников.
        /// </summary>
        private PersonManager personManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="meetings">Список встреч.</param>
        public MeetingManager()
        {
            meetings = new List<Meeting>();
            personManager = new PersonManager();
            SortListByDate();
        }

        /// <summary>
        /// Добавить встречу.
        /// </summary>
        /// <param name="meeting">Объект встречи.</param>
        public void AddMeeting(Meeting meeting)
        {
            if (CheckDateIntersection(meeting.StartTime, meeting.EndTime))
            {
                throw new InvalidOperationException("Встречи пересекаются.");
            }
            meetings.Add(meeting);
            SortListByDate();
        }

        /// <summary>
        /// Удалить встречу.
        /// </summary>
        /// <param name="meeting">Объект встречи.</param>
        public void RemoveMeeting(int index)
        {
            meetings.RemoveAt(index);
        }

        /// <summary>
        /// Получить количество элементов в списке.
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return meetings.Count;
        }

        /// <summary>
        /// Получить элемент по индексу.
        /// </summary>
        /// <param name="index">Индекс.</param>
        /// <returns>Элемент под введеным индексом.</returns>
        public Meeting GetMeetingByIndex(int index)
        {
            return meetings[index];
        }

        /// <summary>
        /// Получить максимальную длину значений.
        /// </summary>
        /// <returns>Коллекция с максимальной длиной для каждого значения.</returns>
        public Dictionary<string, int> GetMaxLengthValues()
        {
            Dictionary<string, int> maxValues = new Dictionary<string, int>();
            int columnNameLength = 15;
            int columnNamePersonLength = 13;
            int columnAdditionalInformationLength = 25;
            maxValues.Add("Name", columnNameLength);
            maxValues.Add("NamePerson", columnNamePersonLength);
            maxValues.Add("AdditionalInformation", columnAdditionalInformationLength);
            var a = meetings.GetType().GetProperties();
            foreach (Meeting meeting in meetings)
            {
                if (meeting.Name.Length > maxValues["Name"])
                {
                    maxValues["Name"] = meeting.Name.Length;
                }
                if (meeting.Person.Name.Length > maxValues["NamePerson"])
                {
                    maxValues["NamePerson"] = meeting.Person.Name.Length;
                }
                if (meeting.AdditionalInformation.Length > maxValues["AdditionalInformation"])
                {
                    maxValues["AdditionalInformation"] = meeting.AdditionalInformation.Length;
                }
            }
            return maxValues;
        }

        /// <summary>
        /// Изменить название встречи.
        /// </summary>
        /// <param name="index">Индекс встречи.</param>
        /// <param name="newName">Новое название встречи.</param>
        public void ChangeNameMeeting(int index, string newName)
        {
            var meeting = meetings.ElementAt(index);
            meeting.Name = newName;
        }

        /// <summary>
        /// Изменить время встречи.
        /// </summary>
        /// <param name="index">Индекс встречи.</param>
        /// <param name="newStartTime">Начало встречи.</param>
        /// <param name="newEndTime">Конец встречи.</param>
        /// <param name="newReminderTime">Время напоминания.</param>
        /// <exception cref="InvalidOperationException">Генерируется, если встречи пересекаются.</exception>
        public void ChangeTimeMeeting(int index, DateTime newStartTime, DateTime newEndTime, DateTime newReminderTime)
        {
            if (CheckDateIntersection(newStartTime, newEndTime, index))
            {
                throw new InvalidOperationException("Встречи пересекаются.");
            }
            var meeting = meetings.ElementAt(index);
            meeting.StartTime = newStartTime;
            meeting.EndTime = newEndTime;
            meeting.ReminderTime = newReminderTime;
            SortListByDate();
        }

        /// <summary>
        /// Изменить дополнительную информацию.
        /// </summary>
        /// <param name="index">Индекс встречи.</param>
        /// <param name="newAdditionalInformation">Допольнительная иинформация.</param>
        public void ChangeAdditionalInformationMeeting(int index, string newAdditionalInformation)
        {
            var meeting = meetings.ElementAt(index);
            meeting.AdditionalInformation = newAdditionalInformation;
        }

        /// <summary>
        /// Отсортировать список встреч по началу встречи.
        /// </summary>
        private void SortListByDate()
        {
            meetings = meetings.OrderBy(p => p.StartTime).ToList();
        }

        /// <summary>
        /// Изменить имя участника.
        /// </summary>
        /// <param name="index">Индекс встречи.</param>
        /// <param name="newPersonName">Имя участника.</param>
        public void ChangePersonName(int index, string newPersonName)
        {
            var meeting = meetings.ElementAt(index);
            personManager.ChangeName(meeting.Person, newPersonName);
        }

        /// <summary>
        /// Изменить имя участника.
        /// </summary>
        /// <param name="index">Индекс встречи.</param>
        /// <param name="newPhoneNumber">Новый номер телефона участника.</param>
        public void ChangePersonPhoneNumber(int index, string newPhoneNumber)
        {
            var meeting = meetings.ElementAt(index);
            personManager.ChangePhoneNumber(meeting.Person, newPhoneNumber);
        }

        /// <summary>
        /// Проверка есть ли пересечения дат.
        /// </summary>
        /// <param name="startTime">Начало встречи.</param>
        /// <param name="endTime">Конец встречи.</param>
        /// <param name="index">Индекс встречи.</param>
        /// <returns>Возвращает true, если даты пересекаются; в противном случае возвращает false.</returns>
        public bool CheckDateIntersection(DateTime startTime, DateTime endTime, int index = -1)
        {
            bool isDateIntersection = false;
            for (int i = 0; i < meetings.Count; i++)
            {
                if (i != index)
                {
                    if (!(meetings[i].StartTime < startTime && meetings[i].StartTime < endTime || meetings[i].EndTime > startTime && meetings[i].EndTime > endTime))
                    {
                        isDateIntersection = true;
                    }
                    else if (startTime > meetings[i].StartTime && startTime < meetings[i].EndTime || endTime > meetings[i].StartTime && endTime < meetings[i].EndTime)
                    {
                        isDateIntersection = true;
                    }
                }
            }
            return isDateIntersection;
        }
    }
}
