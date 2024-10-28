using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp
{
    /// <summary>
    /// Форматировщик текстовых данных.
    /// </summary>
    internal class TextFormatter
    {
        /// <summary>
        /// Менеджер встреч.
        /// </summary>
        MeetingManager meetingManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="meetingManager">Менеджер встреч.</param>
        public TextFormatter(MeetingManager meetingManager)
        {
            this.meetingManager = meetingManager;
        }

        /// <summary>
        /// Получить информацию о встречах в виде таблицы.
        /// </summary>
        /// <param name="meetings">Массив встреч.</param>
        /// <returns>Текст с информацией о встречах в виде таблицы.</returns>
        public string GetMeetingTableInfo(Meeting[] meetings)
        {
            StringBuilder sb = new StringBuilder();

            Dictionary<string, int> maxValueFields = meetingManager.GetMaxLengthValues();
            int maxLengthFieldName = maxValueFields["Name"];
            int maxLengthFieldNamePerson = maxValueFields["NamePerson"];
            int maxLengthFieldAdditionalInformation = maxValueFields["AdditionalInformation"];

            int maxLengthFieldTime = 20;
            int maxLengthFieldPhoneNumber = 15;
            sb.Append($"|{"Порядковый номер".ToString().PadRight(16)}" +
                $"|{"Имя участника".PadRight(maxLengthFieldNamePerson)}" +
                $"|{"Номер телефона".PadRight(maxLengthFieldPhoneNumber)}" +
                $"|{"Название задачи".PadRight(maxLengthFieldName)}" +
                $"|{"Начала встречи".PadRight(maxLengthFieldTime)}" +
                $"|{"Конец встречи".PadRight(maxLengthFieldTime)}" +
                $"|{"Во сколько напомнить".PadRight(maxLengthFieldTime)}" +
                $"|{"Дополнительная информация".PadRight(maxLengthFieldAdditionalInformation)}|\n");
            int lengthStringTitle = sb.Length;
            sb.Append(new string('-', lengthStringTitle));
            sb.Append("\n");
            for (int i = 0; i < meetings.Length; i++)
            {
                sb.Append($"|{(i + 1).ToString().PadRight(16)}" +
                    $"|{meetings[i].Person.Name.PadRight(maxLengthFieldNamePerson)}" +
                    $"|{meetings[i].Person.PhoneNumber.PadRight(maxLengthFieldPhoneNumber)}" +
                    $"|{meetings[i].Name.PadRight(maxLengthFieldName)}" +
                    $"|{meetings[i].StartTime.ToString().PadRight(maxLengthFieldTime)}" +
                    $"|{meetings[i].EndTime.ToString().PadRight(maxLengthFieldTime)}" +
                    $"|{meetings[i].ReminderTime.ToString().PadRight(maxLengthFieldTime)}" +
                    $"|{meetings[i].AdditionalInformation.PadRight(maxLengthFieldAdditionalInformation)}|\n");
                sb.Append(new string('-', lengthStringTitle));
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
