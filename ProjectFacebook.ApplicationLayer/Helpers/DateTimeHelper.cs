using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime FirstDateOfMounth(int year, int month)
        {
            return new DateTime(year, month, 1);
        }

        public static DateTime FirstDateofMonthId(int monthId)
        {
            string monthIdText = monthId.ToString();

            if (monthIdText.Length != 6)
            {
                throw new ApplicationException("Invalid MounthId");
            }

            return FirstDateOfMounth(Int32.Parse(monthIdText.Substring(0, 4)), Int32.Parse(monthIdText.Substring(4, 2)));
        }

        public static DateTime LastDateOfMounth(int year, int month)
        {
            return FirstDateOfMounth(year, month).AddDays(1).AddDays(-1);
        }

        public static DateTime LastDateofMonthId(int monthId)
        {
            string monthIdText = monthId.ToString();

            if (monthIdText.Length != 6)
            {
                throw new ApplicationException("Invalid MounthId");
            }

            return LastDateOfMounth(Int32.Parse(monthIdText.Substring(0, 4)), Int32.Parse(monthIdText.Substring(4, 2)));
        }

        public static List<DateTime> GetDaysOfMonth(int monthId)
        {
            List<DateTime> days = new List<DateTime>();
            DateTime firstDay = FirstDateofMonthId(monthId);
            DateTime lastDay = LastDateofMonthId(monthId);

            for (DateTime day = firstDay; day <= lastDay; day = day.AddDays(1))
            {
                days.Add(day);
            }

            return days;
        }
    }
}
