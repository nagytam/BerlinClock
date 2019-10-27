using BerlinClock.Classes;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BerlinClock
{
    /// <summary>
    /// Time Converter based on the Berlin Clock schema 
    /// Further information: http://www.3quarks.com/en/BerlinClock/
    /// </summary>
    public class TimeConverter : ITimeConverter
    {
        /// <summary>
        /// Regex to match time
        /// </summary>
        Regex RegexTime = new Regex("(?<hours>\\d\\d):(?<minutes>\\d\\d):(?<seconds>\\d\\d)");

        /// <summary>
        /// Converts the specified time into the Berlin Clock string representation.
        /// </summary>
        /// <param name="time">the time in "hh:MM:ss" format (note, that "24:00:00 shall be treated a valid input)</param>
        /// <returns>Berlin clock string representation</returns>
        public string ConvertTime(string time)
        {
            #region Validate and parse input 
            var match = RegexTime.Match(time);

            if (!match.Success)
            {
                return $"O\nOOOO\nOOOO\nOOOOOOOOOOO\nOOOO"; // expected result upon error
            }

            int hours = int.Parse(match.Groups["hours"].Value);
            int minutes = int.Parse(match.Groups["minutes"].Value);
            int seconds = int.Parse(match.Groups["seconds"].Value);
            #endregion

            var stringBuilder = new StringBuilder(32);

            #region First Row (Seconds)
            var isYellow = (seconds % 2) == 0;
            stringBuilder.Append((isYellow) ? "Y" : "O");
            stringBuilder.Append("\n");
            #endregion

            #region Second Row (5 Hours per Lamp)
            var numberOfLamps = hours / 5;
            stringBuilder.Append(RepeatCharacters('R', numberOfLamps).Pad('O', 4 - numberOfLamps));
            stringBuilder.Append("\n");
            #endregion

            #region Third Row (1 hour per Lamp)
            numberOfLamps = hours % 5;
            var thirdRow = RepeatCharacters('R', numberOfLamps).Pad('O', 4 - numberOfLamps);
            stringBuilder.Append(thirdRow);
            stringBuilder.Append("\n");
            #endregion

            #region Fourth Row (5 minutes per Lamp)
            numberOfLamps = minutes / 5;
            var fourthRow = RepeatCharacters('Y', numberOfLamps).Pad('O', 11 - numberOfLamps);
            // make 3rd / 6th / 9th lamp red if necessary
            fourthRow = fourthRow.ReplaceAtUponValue(2, 'Y', 'R');
            fourthRow = fourthRow.ReplaceAtUponValue(5, 'Y', 'R');
            fourthRow = fourthRow.ReplaceAtUponValue(8, 'Y', 'R');
            stringBuilder.Append(fourthRow);
            stringBuilder.Append("\n");
            #endregion

            #region Fifth Row (Remaining Minutes)
            numberOfLamps = minutes % 5;
            var fifthRow = RepeatCharacters('Y', numberOfLamps).Pad('O', 4 - numberOfLamps);
            stringBuilder.Append(fifthRow);
            #endregion

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Repeat the characters N times
        /// </summary>
        /// <param name="c">character to repeat</param>
        /// <param name="n">number of times to repeat</param>
        /// <returns>string containing n times character c</returns>
        protected string RepeatCharacters(char c, int n)
        {
            var s = string.Concat(Enumerable.Repeat(c, n));
            return s;
        }

    }
}
