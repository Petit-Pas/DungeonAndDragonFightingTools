using System.Collections.Generic;
using System.Linq;

namespace BaseToolsLibrary.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IEnumerableExtensions
    {
        public static bool None<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }
    }
}
