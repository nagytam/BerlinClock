
using System;

namespace BerlinClock.Classes
{
    public static class StringExtensions
    {
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
