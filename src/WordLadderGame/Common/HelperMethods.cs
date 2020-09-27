using System.Collections.Generic;
using System.Linq;

namespace WordLadderGame.Common
{
    public static class HelperMethods
    {
        public static bool IsNullOrEmpty(this IEnumerable<object> enumerable) => enumerable == null || !enumerable.Any();
    }
}