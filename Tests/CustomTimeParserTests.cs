using BerlinClock.Classes;
using NUnit.Framework;

namespace BerlinClock.Tests
{
    [TestFixture]
    public class CustomTimeParserTests
    {
        [TestCase("00:00:00")]
        [TestCase("10:04:24")]
        [TestCase("15:46:55")]
        [TestCase("23:59:59")]
        [TestCase("24:00:00")]
        public void CorrectFormatIsParsedCorrectly(string input)
        {
            Assert.True(new CustomTimeParser().TryParse(input, out var dummy));
        }

        [TestCase("000:00:00")]
        [TestCase("00:000:00")]
        [TestCase("00:00:000")]
        [TestCase("000000")]
        [TestCase("00:0000")]
        [TestCase("0000:00")]

        [TestCase("25:00:00")]
        [TestCase("23:60:00")]
        [TestCase("23:00:60")]
        [TestCase("24:01:00")]
        [TestCase("24:00:01")]
        public void IncorrectFormatIsNotParsedCorrectly(string input)
        {
            Assert.False(new CustomTimeParser().TryParse(input, out var dummy));
        }
    }
}