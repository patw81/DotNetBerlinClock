using System;
using System.Collections.Generic;
using System.Linq;

namespace BerlinClock.Classes
{
    public class TimeConverter : ITimeConverter
    {
        private readonly IEnumerable<ITimePartConverter> converters;
        private readonly ITimeParser timeParser;
        private readonly string separator;

        // using constructor parameters allows us to configure it in IoC container and test it easily
        public TimeConverter(IEnumerable<ITimePartConverter> converters, ITimeParser timeParser, string separator)
        {
            this.converters = converters;
            this.timeParser = timeParser;
            this.separator = separator;
        }

        public string ConvertTime(string aTime)
        {
            // we cannot use TimeSpan because we need to support time format 24:00:00 which is not equal to 00:00:00
            if (!timeParser.TryParse(aTime, out var time))
            {
                throw new FormatException($"Provided time '{aTime}' has incorrect format. Should be '{timeParser.TimeFormat}'");
            }

            return string.Join(separator, converters.Select(c => c.Convert(time)));
        }
    }
}
