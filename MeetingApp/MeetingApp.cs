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
    /// Приложение для управления личными встречами.
    /// </summary>
    internal class MeetingApp
    {
        /// <summary>
        /// Менеджер встреч.
        /// </summary>
        private MeetingManager meetingManager;

        /// <summary>
        /// Менеджер вывода в консоль.
        /// </summary>
        private ConsoleOutputManager console;

        /// <summary>
        /// Форматировщик текстовых данных.
        /// </summary>
        private TextFormatter textFormatter;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public MeetingApp()
        {
            meetingManager = new MeetingManager();
            console = new ConsoleOutputManager(meetingManager);
            textFormatter = new TextFormatter(meetingManager);
        }

        /// <summary>
        /// Запуск программы.
        /// </summary>
        public void Start()
        {
            while (true)
            {
                console.Clear();
                CheckNotificationNeedForMeeting();
                console.DisplayMessage("1. Вывести все встречи.\n2. Добавить встречу.\n3. Изменить информацию о встречи.\n" +
                    "4. Создать отчет о встречах.\n5. Закрыть программу.");
                ConsoleKeyInfo key = Console.ReadKey(true);
                try
                {
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
                        case ConsoleKey.D4:
                            CreateReport();
                            Console.ReadKey();
                            break;
                        case ConsoleKey.D5:
                            CloseProgram();
                            break;
                        default:
                            console.Clear(); 
                            console.DisplayMessage("Вы ввели не корректную команду, поробуйте снова.");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (FormatException ex)
                {
                    console.DisplayMessage(GetTextNotification(ex));
                    Console.ReadKey();
                }
                catch (OverflowException ex)
                {
                    console.DisplayMessage(GetTextNotification(ex));
                    Console.ReadKey();
                }
                catch (ArgumentException ex)
                {
                    console.DisplayMessage(GetTextNotification(ex));
                    Console.ReadKey();
                }
                catch (InvalidOperationException ex)
                {
                    console.DisplayMessage(GetTextNotification(ex));
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    console.DisplayMessage($"Ошибка: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Вывод всех встреч.
        /// </summary>
        private void DisplayAllMeeting()
        {
            console.Clear();
            Meeting[] meetingList = GetMeetings();
            console.DisplayInfoAboutMeetings(meetingList);
        }

        /// <summary>
        /// Добавить встречу.
        /// </summary>
        private void AddMeeting()
        {
            DisplayAllMeeting();
            console.DisplayMessage("Введите имя участника с которым назначена встреча: ");
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

        /// <summary>
        /// Получить коллекцию дат.
        /// </summary>
        /// <returns>Коллекция дат.</returns>
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
        /// Изменить встречу.
        /// </summary>
        private void ChangeMeeting()
        {
            Meeting[] meetingList = GetMeetings();
            DisplayAllMeeting();
            console.DisplayMessage("Выберите индекс встречи, который хотите отредактировать:");
            if (int.TryParse(Console.ReadLine(), out int indexMeeting))
            {
                indexMeeting--;
                if (indexMeeting > 0 && indexMeeting >= meetingList.Length)
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
                "\n2. Изменить контактную информацию." +
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
        /// Закрыть программу.
        /// </summary>
        private void CloseProgram()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Создать отчет.
        /// </summary>
        private void CreateReport()
        {
            ReportAboutMeeting report = new ReportAboutMeeting(textFormatter);

            Meeting[] meetingList = GetMeetings();
            report.CreateReport(meetingList);
            console.Clear();
            console.DisplayMessage("Отчет успешно создался в папке с программой.");
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

        /// <summary>
        /// Получить массив встреч.
        /// </summary>
        /// <returns>Массив встреч.</returns>
        private Meeting[] GetMeetings()
        {
            int countMeetings = meetingManager.GetCount();
            Meeting[] meetingList = new Meeting[countMeetings];
            for (int i = 0; i < countMeetings; i++)
            {
                meetingList[i] = meetingManager.GetMeetingByIndex(i);
            }

            return meetingList;
        }

        /// <summary>
        /// Проверка надо ли напомнить пользователю о встречи.
        /// </summary>
        private void CheckNotificationNeedForMeeting()
        {
            Meeting[] meetingList = GetMeetings();
            foreach (var meeting in meetingList)
            {
                if (DateTime.Now > meeting.ReminderTime && DateTime.Now < meeting.StartTime)
                {
                    console.DisplayMessage($"У Вас встреча {meeting.Name} c {meeting.Person.Name} в {meeting.ReminderTime.ToUniversalTime()}.\n");
                }
            }
        }

        private string GetTextNotification(Exception ex)
        {
            string text = $"Уведомление: {ex.Message} Попробуйте еще раз.";
            return text;
        }
    }
}
