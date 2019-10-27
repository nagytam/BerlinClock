
using System;

namespace BerlinClock.Classes
{
    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Pad string with n times c charater
        /// </summary>
        /// <param name="input">string</param>
        /// <param name="c">character</param>
        /// <param name="n">number of times</param>
        /// <returns>padded string</returns>
        public static string Pad(this string input, char c, int n)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            for (int i = 0; i < n; i++)
            {
                input += c;
            }

            return input;
        }

        /// <summary>
        /// Replace character at index with new value if old value is as specified 
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="index">index to be replaced</param>
        /// <param name="oldChar">value to be replaced</param>
        /// <param name="newChar">new value</param>
        /// <returns>replaced string</returns>
        public static string ReplaceAtUponValue(this string input, int index, char oldChar, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            if (input[index]==oldChar)
            {
                char[] chars = input.ToCharArray();
                chars[index] = newChar;
                return new string(chars);
            }
            else
            {
                return input;
            }
        }
    }
}
