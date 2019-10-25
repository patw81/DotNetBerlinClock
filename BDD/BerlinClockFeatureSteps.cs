using System;
using BerlinClock.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BerlinClock.BDD
{
    [Binding]
    public class TheBerlinClockSteps
    {
        private ITimeConverter berlinClock = new TimeConverter(new ITimePartConverter[]
            {
                new TimePartConverter(
                    new[] {LightState.Yellow},
                    time => (time.Second % 2) == 0 ? 1 : 0),

                new TimePartConverter(
                    new[] {LightState.Red, LightState.Red, LightState.Red, LightState.Red},
                    time => time.Hour / 5),

                new TimePartConverter(
                    new[] {LightState.Red, LightState.Red, LightState.Red, LightState.Red},
                    time => time.Hour % 5),

                new TimePartConverter(
                    new[]
                    {
                        LightState.Yellow, LightState.Yellow, LightState.Red,
                        LightState.Yellow, LightState.Yellow, LightState.Red,
                        LightState.Yellow, LightState.Yellow, LightState.Red,
                        LightState.Yellow, LightState.Yellow
                    },
                    time => time.Minute / 5),

                new TimePartConverter(
                    new[] {LightState.Yellow, LightState.Yellow, LightState.Yellow, LightState.Yellow},
                    time => time.Minute % 5),
            },
            new CustomTimeParser(),
            Environment.NewLine);

        private String theTime;

        
        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(string time)
        {
            theTime = time;
        }
        
        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            Assert.AreEqual(berlinClock.ConvertTime(theTime), theExpectedBerlinClockOutput);
        }
    }
}
