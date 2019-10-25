using BerlinClock.Classes;
using NUnit.Framework;
using System;

namespace BerlinClock.Tests
{
    [TestFixture]
    public class CustomTimeTests
    {
        [TestCase(0, 0, 0)]
        [TestCase(10, 4, 24)]
        [TestCase(15, 46, 55)]
        [TestCase(23, 59, 59)]
        [TestCase(24, 0, 0)]
        public void TimeInRangeDoesNotThrowException(int hour, int minute, int second)
        {
            Assert.DoesNotThrow(() => new CustomTime(hour, minute, second));
        }

        [TestCase(-1, 0, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, 0, -1)]
        [TestCase(25, 0, 0)]
        [TestCase(23, 60, 0)]
        [TestCase(23, 0, 60)]
        [TestCase(24, 1, 0)]
        [TestCase(24, 0, 1)]
        public void TimeOutOfRangeThrowsException(int hour, int minute, int second)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CustomTime(hour, minute, second));
        }
    }
}
