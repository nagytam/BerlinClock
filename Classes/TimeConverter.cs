using BerlinClock.Classes;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BerlinClock
{
    /// <summary>
    /// Time Converter based on the Berlin Clock schema 
    /// Further information: http://www.3quarks.com/en/BerlinClock/
    /// </summary>
    public class TimeConverter : ITimeConverter
    {
        // Open points:
        // 1) efficiency (string vs. StringBuilder)
        // 2) extend testcases with input input handling

        /// <summary>
        /// Converts the specified time into the Berlin Clock string representation.
        /// </summary>
        /// <param name="aTime">the time in "hh:MM:ss" format (note, that "24:00:00 shall be treated a valid input)</param>
        /// <returns>Berlin clock string representation</returns>
        public string convertTime(string aTime)
        {
            #region Validate and parse input 
            var match = Regex.Match(aTime, "(?<hours>\\d\\d):(?<minutes>\\d\\d):(?<seconds>\\d\\d)");

            if (!match.Success)
            {
                return string.Empty; // no exceptions are expected, but an empty string on invalid input
            }

            int hours = int.Parse(match.Groups["hours"].Value);
            int minutes = int.Parse(match.Groups["minutes"].Value);
            int seconds = int.Parse(match.Groups["seconds"].Value);
            #endregion

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

            #region Fifth Row (Remaining Minutes)
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
