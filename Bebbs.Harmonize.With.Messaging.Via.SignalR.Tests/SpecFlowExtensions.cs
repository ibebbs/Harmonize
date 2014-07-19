using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Bebbs.Harmonize.With.Messaging.Via.SignalR.Tests
{
    public static class SpecFlowExtensions
    {
        public static DateTime ReadDate(this TableRow row, string columnName)
        {
            string value = row[columnName];

            return DateTime.Parse(value);
        }

        public static T Read<T>(this TableRow row, string columnName)
        {
            string value = row[columnName];

            return (T) Convert.ChangeType(value, typeof(T));
        }

        public static T ReadEnum<T>(this TableRow row, string columnName)
        {
            string value = row[columnName];

            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
