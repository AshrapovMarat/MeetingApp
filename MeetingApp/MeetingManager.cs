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

        private PersonManager personManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="meetings">Список встреч.</param>
        public MeetingManager(List<Meeting> meetings)
        {
            this.meetings = meetings;
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

        public Dictionary<string, int> GetMaxLengthValueName()
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

        public void ChangeNameMeeting(int index, string newName)
        {
            var meeting = meetings.ElementAt(index);
            meeting.Name = newName;
        }

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

        public void ChangeAdditionalInformationMeeting(int index, string newAdditionalInformation)
        {
            var meeting = meetings.ElementAt(index);
            meeting.AdditionalInformation = newAdditionalInformation;
        }

        private void SortListByDate()
        {
            meetings = meetings.OrderBy(p => p.StartTime).ToList();
        }

        public void ChangePersonName(int index, string newPersonName)
        {
            var meeting = meetings.ElementAt(index);
            meeting.Person.Name = newPersonName;
        }

        public void ChangePersonPhoneNumber(int index, string newPersonName)
        {
            var meeting = meetings.ElementAt(index);
            meeting.Person.PhoneNumber = newPersonName;
        }

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
