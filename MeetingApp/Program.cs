namespace MeetingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MeetingApp app = new MeetingApp();
            app.Start();

            //int originStart = 4;
            //int originEnd = 8;

            //int newStart = -15;
            //int newEnd = -17;

            //if (!(originStart < newStart && originStart < newEnd || originEnd > newStart && originEnd > newEnd))
            //{
            //    Console.WriteLine("Есть переcечения.");
            //}
            //else if (newStart > originStart && newStart < originEnd || newEnd > originStart && newEnd < originEnd)
            //{
            //    Console.WriteLine("Есть переcечения.");
            //}
            //else
            //{
            //    Console.WriteLine("Нет переcечения.");
            //}
        }
    }
}
