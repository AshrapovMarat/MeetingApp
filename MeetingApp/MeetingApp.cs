using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp
{
    /// <summary>
    /// 
    /// </summary>
    internal class MeetingApp
    {
        /// <summary>
        /// Менеджер вывода на консоль.
        /// </summary>
        private List<Meeting> meetingList;
        private MeetingManager meetingManager;
        private ConsoleOutputManager console;

        public MeetingApp()
        {
            meetingList = new List<Meeting>();
            //meetingList = new List<Meeting>() { new Meeting(new Person("Кандидат", "+56789012345"), "Встреча с кандидатом", new DateTime(2025, 10, 24, 12, 0, 0), 
            //    new DateTime(2025, 10, 24, 13, 0, 0), new DateTime(2025, 10, 24, 4, 0, 0), "Встреча в кафе.sdlijflksdjflkjdslfkj"),

            //new Meeting(new Person("Кандидат ghfjdllklhg", "+12345678901"), "Встреча с кандидатом dfssdf", new DateTime(2024, 10, 24, 12, 0, 0),
            //    new DateTime(2024, 10, 24, 13, 0, 0), new DateTime(2024, 10, 24, 4, 0, 0), "Встреча в кафе.sdfsd"),

            //new Meeting(new Person("Кандидатdsflsdl", "+12345678901"), "Встреча с кандидатом sdfsdfsdf", new DateTime(2023, 10, 24, 12, 0, 0),
            //    new DateTime(2023, 10, 24, 13, 0, 0), new DateTime(2023, 10, 24, 4, 0, 0), "Встреча в кафе.sdjkhfjhsd")};
            meetingManager = new MeetingManager(meetingList);
            console = new ConsoleOutputManager(meetingManager);
        }

        /// <summary>
        /// Запуск программы.
        /// </summary>
        public void Start()
        {
            while (true)
            {
                console.Clear();
                console.DisplayMessage("1. Вывести все встречи.\n2. Добавить встречу.\n3. Изменить информацию о встречи.\n");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        DisplayAllMeeting();
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.D2:
                        AddMeeting();
                        break;
                    case ConsoleKey.D3:
                        ChangeMeeting();
                        break;
                    default:
                        console.DisplayMessage("Вы ввели не корректную команду, поробуйте снова.");
                        break;
                }
            }
        }

        /// <summary>
        /// Вывод всех встреч.
        /// </summary>
        private void DisplayAllMeeting()
        {
            console.Clear();
            Meeting[] meetingList = GetMeetingList();
            console.DisplayInfoAboutMeetings(meetingList);
        }

        /// <summary>
        /// Добавить встречу.
        /// </summary>
        private void AddMeeting()
        {
            DisplayAllMeeting();
            console.DisplayMessage("Введите имя человека с которым назначена встреча: ");
            string personName = Console.ReadLine();

            console.DisplayMessage("Введите номер телефона участника, с которым назначена встреча: ");
            string personPhoneNumer = Console.ReadLine();

            console.DisplayMessage("\nВведите название встречи: ");
            string nameMeeting = Console.ReadLine();

            Dictionary<string, DateTime> dateAboutDate = GetDates();

            console.DisplayMessage("\nВведите дополнительную информацию о встречи: ");
            string additionalInformationMeeting = Console.ReadLine();

            meetingManager.AddMeeting(new Meeting(new Person(personName, personPhoneNumer), nameMeeting, dateAboutDate["StartTime"],
                dateAboutDate["EndTime"], dateAboutDate["ReminderTime"], additionalInformationMeeting));
        }

        private Dictionary<string, DateTime> GetDates()
        {
            console.DisplayMessage("\nВведите дату начала встречи (в формате дд.мм.гггг): ");
            DateTime dateMeeting = ReadDateFromConsole();

            console.DisplayMessage("\nВведите время начала встречи (в формате чч:мм): ");
            DateTime startTimeMeeting = ReadTimeFromConsole(dateMeeting);

            console.DisplayMessage("\nВведите время завершения встречи (в формате чч:мм): ");
            DateTime endTimeMeeting = ReadTimeFromConsole(dateMeeting);

            console.DisplayMessage("\nВведите за сколько напомнить о встречи (в формате чч:мм): ");
            DateTime reminderTimeMeeting = startTimeMeeting.Add(-ReadTimeFromConsole(dateMeeting).TimeOfDay);

            Dictionary<string, DateTime> dateAboutDate = new Dictionary<string, DateTime>();
            dateAboutDate.Add("StartTime", startTimeMeeting);
            dateAboutDate.Add("EndTime", endTimeMeeting);
            dateAboutDate.Add("ReminderTime", reminderTimeMeeting);

            return dateAboutDate;
        }

        /// <summary>
        /// изменить встречу.
        /// </summary>
        private void ChangeMeeting()
        {
            DisplayAllMeeting();
            console.DisplayMessage("Выберите индекс встречи, который хотите отредактировать:");
            if (int.TryParse(Console.ReadLine(), out int indexMeeting))
            {
                indexMeeting--;
                if (indexMeeting >= meetingList.Count)
                {
                    throw new OverflowException("Был выбран не корректный индекс встречи.");
                } 
            }
            else
            {
                throw new FormatException("Был введен не корректный индекс встречи.");
            }
            console.DisplayMessage("Выберите параметр, который хотите изменить:" +
                "\n1. Имя участника." +
                "\n2. Добавить или изменить контактную информацию." +
                "\n3. Изменить название встречи." +
                "\n4. Изменить время встречи." +
                "\n5. Удалить встречу." +
                "\n6. Изменить дополнительную информацию.\n");
            ConsoleKeyInfo key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    console.DisplayMessage("Введите новое имя для участника встречи: ");
                    string newPersonName = Console.ReadLine();
                    meetingManager.ChangePersonName(indexMeeting, newPersonName);
                    break;
                case ConsoleKey.D2:
                    console.DisplayMessage("Введите новый номер телефона для участника встречи: ");
                    string newPersonPhoneNumber = Console.ReadLine();
                    meetingManager.ChangePersonPhoneNumber(indexMeeting, newPersonPhoneNumber);
                    break;
                case ConsoleKey.D3:
                    console.DisplayMessage("Введите новое название встречи: ");
                    string newNameMeeting = Console.ReadLine();
                    meetingManager.ChangeNameMeeting(indexMeeting, newNameMeeting);
                    break;
                case ConsoleKey.D4:
                    Dictionary<string, DateTime> dateAboutDate = GetDates();
                    meetingManager.ChangeTimeMeeting(indexMeeting, dateAboutDate["StartTime"],
                dateAboutDate["EndTime"], dateAboutDate["ReminderTime"]);
                    break;
                case ConsoleKey.D5:
                    meetingManager.RemoveMeeting(indexMeeting);
                    break;
                case ConsoleKey.D6:
                    console.DisplayMessage("Введите новую информацию о встречи: ");
                    string newAdditionalInformation = Console.ReadLine();
                    meetingManager.ChangeAdditionalInformationMeeting(indexMeeting, newAdditionalInformation);
                    break;
            }
        }

        /// <summary>
        /// Считать дату из консоли.
        /// </summary>
        /// <returns>Дата.</returns>
        /// <exception cref="FormatException">Генерируется, если формат введенного времени некорректный.</exception>
        private DateTime ReadDateFromConsole()
        {
            DateTime date;
            if (DateTime.TryParse(Console.ReadLine(), out date))
            {
                return date;
            }
            else
            {
                throw new FormatException("Некорректный формат даты.");
            }
        }

        /// <summary>
        /// Считать время из консоли.
        /// </summary>
        /// <returns>Время.</returns>
        /// <exception cref="FormatException">Генерируется, если формат введенной даты некорректный.</exception>
        private DateTime ReadTimeFromConsole(DateTime date)
        {
            DateTime time;
            string input = $"{date.ToShortDateString()} {Console.ReadLine()}";
            if (DateTime.TryParseExact(input, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
            {
                return time;
            }
            else
            {
                throw new FormatException("Некорректный формат времени.");
            }
        }

        private Meeting[] GetMeetingList() 
        {
            int countMeetings = meetingManager.GetCount();
            Meeting[] meetingList = new Meeting[countMeetings];
            for (int i = 0; i < countMeetings; i++)
            {
                meetingList[i] = meetingManager.GetMeetingByIndex(i);
            }

            return meetingList;
        }
    }
}
