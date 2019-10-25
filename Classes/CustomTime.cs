namespace BerlinClock.Classes
{
    public struct CustomTime
    {
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }

        public CustomTime(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }
    }
}