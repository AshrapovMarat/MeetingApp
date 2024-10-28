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
        /// <summary>
        /// Менеджер встреч.
        /// </summary>
        private MeetingManager meetingManager;

        /// <summary>
        /// Форматировщик текстовых данных.
        /// </summary>
        private TextFormatter textFormatter;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="meetingManager">Менеджер встреч.</param>
        public ConsoleOutputManager(MeetingManager meetingManager)
        {
            this.meetingManager = meetingManager;
            textFormatter = new TextFormatter(meetingManager);
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

        /// <summary>
        /// Вывести информацию о встречах.
        /// </summary>
        /// <param name="meetings">Массив встреч.</param>
        public void DisplayInfoAboutMeetings(Meeting[] meetings)
        {
            Console.WriteLine(textFormatter.GetMeetingTableInfo(meetings));
        }
    }
}
