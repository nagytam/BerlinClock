using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BerlinClock
{
    [Binding]
    public class TheBerlinClockSteps
    {
        private ITimeConverter berlinClock = new TimeConverter();
        private string theTime;

        
        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(string time)
        {
            theTime = time;
        }
        
        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string expectedBerlinClockOutput)
        {
            var actualBerlinClockOutput = berlinClock.ConvertTime(theTime);
            Assert.AreEqual(expectedBerlinClockOutput, actualBerlinClockOutput);
        }

    }
}
