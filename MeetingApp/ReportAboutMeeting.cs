using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp
{
    /// <summary>
    /// Отчет о встречах.
    /// </summary>
    internal class ReportAboutMeeting
    {
        /// <summary>
        /// Форматировщик текстовых данных.
        /// </summary>
        private TextFormatter textFormatter;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="textFormatter">Форматировщик текстовых данных.</param>
        public ReportAboutMeeting(TextFormatter textFormatter)
        {
            this.textFormatter = textFormatter;
        }

        /// <summary>
        /// Создать отчет.
        /// </summary>
        /// <param name="meetings">Массив встреч.</param>
        public void CreateReport(Meeting[] meetings)
        {
            string nameFile = "report.txt";
            string textWithInfoAboutMeeting = textFormatter.GetMeetingTableInfo(meetings);

            using (StreamWriter write = new StreamWriter(nameFile))
            {
                write.WriteLine(textWithInfoAboutMeeting);
            }
        }
    }
}
