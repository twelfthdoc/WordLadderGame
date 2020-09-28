using System.Collections.Generic;
using System.Linq;

namespace WordLadderGame.Common
{
    public static class HelperMethods
    {
        // Function that states whether a string uses only letters
        public static bool IsAlpha(this string s) => s.All(char.IsLetter);

        // Function that states whether an enumberable object is null or empty
        public static bool IsNullOrEmpty(this IEnumerable<object> enumerable) => enumerable == null || !enumerable.Any();

        // Function that compares two strings and states if they are similar by one letter difference
        public static bool IsSimilar(this string first, string second)
        {
            if (first.Length != second.Length) return false;

            var count = 0;

            for (var i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i]) count++;
            }

            return count == 1;
        }
    }
}