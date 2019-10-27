using BerlinClock.Classes;
using System;
using System.Linq;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        // Specification: http://www.3quarks.com/en/BerlinClock/
        public string convertTime(string aTime)
        {
            //var date = DateTime.Parse(aTime); // cannot be used as "24:00:00" shall be treated as valid input
            //int hours = date.Hour;
            //int minutes = date.Minute;
            //int seconds = date.Second;
            int hours = int.Parse(aTime.Substring(0, 2));
            int minutes = int.Parse(aTime.Substring(3, 2));
            int seconds = int.Parse(aTime.Substring(6, 2));

            #region First Row (Seconds)
            var isYellow = (seconds % 2) == 0;
            var firstRow = (isYellow) ? "Y" : "O";
            #endregion

            #region Second Row (5 Hours per Lamp)
            var numberOfLamps = hours / 5;
            var secondRow = RepeatCharacters('R', numberOfLamps).Pad('O', 4 - numberOfLamps);
            //var secondRow = "RRRR";
            #endregion

            #region Third Row (1 hour per Lamp)
            numberOfLamps = hours % 5;
            var thirdRow = RepeatCharacters('R', numberOfLamps).Pad('O', 4 - numberOfLamps);
            // var thirdRow = "RRRO";
            #endregion

            #region Fourth Row (5 minutes per Lamp)
            numberOfLamps = minutes / 5;
            var fourthRow = RepeatCharacters('Y', numberOfLamps).Pad('O', 11 - numberOfLamps);
            // make 3rd / 6th / 9th lamp red if necessary
            fourthRow = fourthRow.ReplaceAtUponValue(2, 'Y', 'R');
            fourthRow = fourthRow.ReplaceAtUponValue(5, 'Y', 'R');
            fourthRow = fourthRow.ReplaceAtUponValue(8, 'Y', 'R');
            //  var fourthRow = "YYRYYRYYRYY";
            #endregion

            #region Fifth Row ()
            numberOfLamps = minutes % 5;
            var fifthRow = RepeatCharacters('Y', numberOfLamps).Pad('O', 4 - numberOfLamps);
            // var fifthRow = "YYYY";
            #endregion

            #region Generate results
            var clock = firstRow + Environment.NewLine + secondRow + Environment.NewLine + thirdRow + Environment.NewLine + fourthRow + Environment.NewLine + fifthRow;
            return clock;
            #endregion
        }

        protected string RepeatCharacters(char c, int n)
        {
            var s = string.Concat(Enumerable.Repeat(c, n));
            return s;
        }

    }
}
