using System;
using System.Collections.Generic;

namespace BerlinClock.Classes
{
    public class TimePartConverter : ITimePartConverter
    {
        private readonly IList<string> lightStateList;
        private readonly Func<CustomTime, int> markedFunc;

        public TimePartConverter(IList<string> lightStateList, Func<CustomTime, int> markedFunc)
        {
            this.lightStateList = lightStateList;
            this.markedFunc = markedFunc;
        }

        public string Convert(CustomTime time)
        {
            string result = string.Empty;

            int markedCount = markedFunc(time);
            foreach (var lightState in lightStateList)
            {
                result += (markedCount-- > 0) ? lightState : LightState.Off;
            }

            return result;
        }
    }
}