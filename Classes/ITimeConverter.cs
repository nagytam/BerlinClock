using System;

namespace BerlinClock
{
    /// <summary>
    /// Time converter interface for Berlin Clock
    /// </summary>
    public interface ITimeConverter
    {
        /// <summary>
        /// Convert time to Berlin Clock representation
        /// </summary>
        /// <param name="time"></param>
        /// <returns>Berlin clock string</returns>
        string ConvertTime(String time);
    }
}
