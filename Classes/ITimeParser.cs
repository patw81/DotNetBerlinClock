using System;

namespace BerlinClock.Classes
{
    public interface ITimeParser
    {
        string TimeFormat { get; }

        bool TryParse(string input, out CustomTime result);
    }
}