using System;
using System.Globalization;
using System.Linq;

namespace BerlinClock.Classes
{
    public struct CustomTime
    {
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }

        public CustomTime(int hour, int minute, int second)
        {
            var time = new TimeSpan(hour, minute, second);
            if (time.Hours != hour || time.Minutes != minute || time.Seconds != second || new[] { hour, minute, second}.Any(i => i < 0))
            {
                if (hour != 24 || minute != 0 || second != 0)
                {
                    throw new ArgumentOutOfRangeException($"Provided time is out of range - hour:{hour}, minute:{minute}, second:{second}. Supported range is from 00:00:00 to 24:00:00");
                }
            }

            Hour = hour;
            Minute = minute;
            Second = second;
        }
    }
}