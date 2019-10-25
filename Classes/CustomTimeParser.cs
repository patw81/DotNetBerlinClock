using System;
using System.Text.RegularExpressions;

namespace BerlinClock.Classes
{
    /// <summary>
    /// CustomTimeParser supports time range from 00:00:00 to 24:00:00.
    /// </summary>
    public class CustomTimeParser : ITimeParser
    {
        private const string Hour = "hour";
        private const string Minute = "minute";
        private const string Second = "sec";

        private readonly Regex regex;

        public string TimeFormat { get; } = $"^((?<{Hour}>(0[0-9]|1[0-9]|2[0-3])):(?<{Minute}>[0-5][0-9]):(?<{Second}>[0-5][0-9])|(?<{Hour}>24):(?<{Minute}>00):(?<{Second}>00))$";

        public CustomTimeParser()
        {
            regex = new Regex(TimeFormat);
        }

        public bool TryParse(string input, out CustomTime result)
        {
            Match match = regex.Match(input);
            if (!match.Success)
            {
                result = default;
                return false;
            }

            var hourGroup = match.Groups[Hour];
            var minuteGroup = match.Groups[Minute];
            var secGroup = match.Groups[Second];

            int hour = int.Parse(hourGroup.Value);
            int minute = int.Parse(minuteGroup.Value);
            int sec = int.Parse(secGroup.Value);

            result = new CustomTime(hour, minute, sec);

            return true;
        }
    }
}