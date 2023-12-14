using System.Collections.Generic;
using System.Linq;

namespace WordLadderGame.Common
{
    public static class HelperMethods
    {
        /// <summary>
        /// Inline function that determines whether a string uses only letters
        /// </summary>
        /// <param name="s">
        /// The string to be tested.
        /// </param>
        /// <returns>
        /// Returns <see langword="true"/> if string only contains letter, otherwise <see langword="false"/>
        /// </returns>
        public static bool IsAlpha(this string s) => s.All(char.IsLetter);

        /// <summary>
        /// Inline function that states whether an enumberable object is null or empty
        /// </summary>
        /// <param name="e">
        /// The enumerable to be tested.
        /// </param>
        /// <returns>
        /// Returns <see langword="true"/> if <typeparamref name="IEnumerable"></typeparamref> is <see langword="null"/> or empty, else returns <see langword="false"/>
        /// </returns>
        public static bool IsNullOrEmpty(this IEnumerable<object> e) => e == null || !e.Any();

        /// <summary>
        /// Inline function that compares two strings, and states if they are similar but with one letter difference
        /// </summary>
        /// <param name="first">
        /// First string to be tested.
        /// </param>
        /// <param name="second"></param>
        /// Second string to be tested.
        /// <returns>
        /// Returns <see langword="true"/> if the strings are similar with one letter differing, else returns <see langword="false"/>
        /// </returns>
        public static bool IsSimilar(this string first, string second)
        {
            if (first.Length != second.Length) return false;

            var count = 0;

            for (var i = 0; i < first.Length; i++)
            {
                if (first.ElementAt(i) != second.ElementAt(i)) count++;
            }

            return count == 1;
        }
    }
}