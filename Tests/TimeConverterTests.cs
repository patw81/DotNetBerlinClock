using System;
using System.Collections.Generic;
using System.Linq;
using BerlinClock.Classes;
using Moq;
using NUnit.Framework;

namespace BerlinClock.Tests
{
    [TestFixture]
    public class TimeConverterTests
    {
        Mock<ITimePartConverter> partConverterMock;
        Mock<ITimeParser> parserMock;

        [SetUp]
        public void Setup()
        {
            partConverterMock = new Mock<ITimePartConverter>();
            parserMock = new Mock<ITimeParser>();
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
        public void IncorrectFormatThrowsAnException(string input)
        {
            CustomTime time;
            parserMock.Setup(p => p.TryParse(It.IsAny<string>(), out time)).Returns(false);

            var converter = new TimeConverter(new[] { partConverterMock.Object }, parserMock.Object, string.Empty);

            Assert.Throws<FormatException>(() => converter.ConvertTime(input));
        }

        [TestCase("\n\r")]
        [TestCase("---")]
        [TestCase("||")]
        public void CorrectSeparatorIsUsed(string separator)
        {
            partConverterMock.Setup(c => c.Convert(It.IsAny<CustomTime>())).Returns("dummy");

            CustomTime time;
            parserMock.Setup(p => p.TryParse(It.IsAny<string>(), out time)).Returns(true);

            var converter = new TimeConverter(new[] { partConverterMock.Object, partConverterMock.Object, partConverterMock.Object }, parserMock.Object, separator);
            string convertedTime = converter.ConvertTime(string.Empty);

            var strings = convertedTime.Split(new [] {separator}, StringSplitOptions.None);

            Assert.AreEqual(3, strings.Length);
        }

        [TestCaseSource(nameof(GivenTimeParts))]
        public void GathersAllGivenTimeParts(string[] timeParts)
        {
            var partConverterMockList = new List<Mock<ITimePartConverter>>();
            foreach (string timePart in timeParts)
            {
                var partConverterMock = new Mock<ITimePartConverter>();
                partConverterMock.Setup(c => c.Convert(It.IsAny<CustomTime>())).Returns(timePart);

                partConverterMockList.Add(partConverterMock);
            }

            CustomTime time;
            parserMock.Setup(p => p.TryParse(It.IsAny<string>(), out time)).Returns(true);

            var converter = new TimeConverter(partConverterMockList.Select(c => c.Object), parserMock.Object, Environment.NewLine);
            string convertedTime = converter.ConvertTime(string.Empty);

            var strings = convertedTime.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            Assert.AreEqual(timeParts.Length, strings.Length);
            Array.ForEach(timeParts, timePart => Assert.Contains(timePart, strings));
            Array.ForEach(strings, str => Assert.Contains(str, timeParts));
        }

        static object[] GivenTimeParts =
        {
            new[] { "aa", "bb" },
            new[] { "aa", "bb", "ccc"},
            new[] { "dfghfg", "adfas", "sgfdg" },
            new[] { "ghjgh", "sdfdstr", "dfgdfg", "sdshs" }
        };
    }
}