using System;

namespace BerlinClock.Classes
{
    public interface ITimePartConverter
    {
        string Convert(CustomTime time);
    }
}