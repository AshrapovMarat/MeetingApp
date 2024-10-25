using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp
{
    /// <summary>
    /// Менеджер вывода на консоль.
    /// </summary>
    internal class ConsoleOutputManager
    {
        private MeetingManager meetingManager;

        public ConsoleOutputManager(MeetingManager meetingManager)
        {
            this.meetingManager = meetingManager;
        }
        /// <summary>
        /// Вывести сообщение.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public void DisplayMessage(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Отчистить консоль.
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        public void DisplayInfoAboutMeetings(Meeting[] meetings)
        {
            Dictionary<string, int> maxValueFields = meetingManager.GetMaxLengthValueName();
            int maxLengthFieldName = maxValueFields["Name"];
            int maxLengthFieldNamePerson = maxValueFields["NamePerson"];
            int maxLengthFieldAdditionalInformation = maxValueFields["AdditionalInformation"];

            int maxLengthFieldTime = 20;
            int maxLengthFieldPhoneNumber = 15;
            string title = $"|{"Порядковый номер".ToString().PadRight(16)}" +
                $"|{"Имя участника".PadRight(maxLengthFieldNamePerson)}" +
                $"|{"Номер телефона".PadRight(maxLengthFieldPhoneNumber)}" +
                $"|{"Название задачи".PadRight(maxLengthFieldName)}" +
                $"|{"Начала встречи".PadRight(maxLengthFieldTime)}" +
                $"|{"Конец встречи".PadRight(maxLengthFieldTime)}" +
                $"|{"Во сколько напомнить".PadRight(maxLengthFieldTime)}" +
                $"|{"Дополнительная информация".PadRight(maxLengthFieldAdditionalInformation)}|";
            
            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length));
            for (int i = 0; i < meetings.Length; i++)
            {
                Console.WriteLine($"|{(i + 1).ToString().PadRight(16)}" +
                    $"|{meetings[i].Person.Name.PadRight(maxLengthFieldNamePerson)}" +
                    $"|{meetings[i].Person.PhoneNumber.PadRight(maxLengthFieldPhoneNumber)}" +
                    $"|{meetings[i].Name.PadRight(maxLengthFieldName)}" +
                    $"|{meetings[i].StartTime.ToString().PadRight(maxLengthFieldTime)}" +
                    $"|{meetings[i].EndTime.ToString().PadRight(maxLengthFieldTime)}" +
                    $"|{meetings[i].ReminderTime.ToString().PadRight(maxLengthFieldTime)}" +
                    $"|{meetings[i].AdditionalInformation.PadRight(maxLengthFieldAdditionalInformation)}|");
                Console.WriteLine(new string('-', title.Length));
            }
        }
    }
}
