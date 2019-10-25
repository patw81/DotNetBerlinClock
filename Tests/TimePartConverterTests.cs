using System.Collections.Generic;
using BerlinClock.Classes;
using NUnit.Framework;

namespace BerlinClock.Tests
{
    [TestFixture]
    public class TimePartConverterTests
    {
        public class MarkedFunctionTestElement
        {
            public IList<string> LightStateList { get; set; }
            public int LightStatesToConvert { get; set; }
            public string ExpectedOutput { get; set; }
        }

        [TestCaseSource(nameof(MarkedFunctionTestCases))]
        public void TimePartIsConvertedBasedOnPassedMarkedFunction(MarkedFunctionTestElement testCase)
        {
            var timePartConverter = new TimePartConverter(testCase.LightStateList, _ => testCase.LightStatesToConvert);

            string convertedTimePart = timePartConverter.Convert(default);

            Assert.AreEqual(testCase.ExpectedOutput, convertedTimePart);
        }

        private static MarkedFunctionTestElement[] MarkedFunctionTestCases =
        {
            new MarkedFunctionTestElement
            {
                LightStateList = new[] { LightState.Red, LightState.Red, LightState.Yellow, LightState.Red, LightState.Yellow },
                LightStatesToConvert = 5,
                ExpectedOutput = "RRYRY"
            },
            new MarkedFunctionTestElement
            {
                LightStateList = new[] { LightState.Red, LightState.Red, LightState.Yellow, LightState.Red, LightState.Yellow },
                LightStatesToConvert = 10,
                ExpectedOutput = "RRYRY"
            },
            new MarkedFunctionTestElement
            {
                LightStateList = new[] { LightState.Red, LightState.Red, LightState.Yellow, LightState.Red, LightState.Yellow },
                LightStatesToConvert = 3,
                ExpectedOutput = "RRYOO"
            },
            new MarkedFunctionTestElement
            {
                LightStateList = new[] { LightState.Red, LightState.Red, LightState.Yellow, LightState.Red, LightState.Yellow, LightState.Red, LightState.Red },
                LightStatesToConvert = 1,
                ExpectedOutput = "ROOOOOO"
            },
            new MarkedFunctionTestElement
            {
                LightStateList = new[] { LightState.Yellow, LightState.Red, LightState.Red, LightState.Red, LightState.Yellow, LightState.Red, LightState.Yellow },
                LightStatesToConvert = 6,
                ExpectedOutput = "YRRRYRO"
            },
        };
    }
}