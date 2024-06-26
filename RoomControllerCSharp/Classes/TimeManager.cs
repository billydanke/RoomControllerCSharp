using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomControllerCSharp.Classes
{
    public class TimeManager
    {
        public static string GetFormattedTime()
        {
            DateTime currentTime = DateTime.Now;
            string formattedTime = currentTime.ToString("h:mm tt");
            return formattedTime;
        }

        public static string GetFormattedDate()
        {
            DateTime currentTime = DateTime.Now;
            string formattedDate = currentTime.ToString("M:d:yy");
            return formattedDate;
        }

        public static string GetWeekday()
        {
            DateTime currentTime = DateTime.Now;
            string formattedWeekday = currentTime.ToString("ddd");
            return formattedWeekday;
        }

        public static string GetDayAndTime()
        {
            string combinedString = $"{GetWeekday()} {GetFormattedTime()}";
            
            return combinedString;
        }
    }
}
