using System.Collections.Generic;

namespace Valkyrie.Extensions
{
    public static class ListExtensions
    {
        public static bool IsEmpty<T>(this List<T> self)
        {
            return self.Count == 0;
        }
    }
}
