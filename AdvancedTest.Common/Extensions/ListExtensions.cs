using System.Collections.Generic;
using System.Linq;

namespace AdvancedTest.Common.Extensions
{
    /// <summary>
    /// Расширение для получение следующей и предыдущей записи в коллекции
    /// </summary>
    public static class ListExtensions
    {
        public static T GetNext<T>(this IEnumerable<T> list, T current)
        {
            try
            {
                return list.SkipWhile(x => !x.Equals(current)).Skip(1).First();
            }
            catch
            {
                return default(T);
            }
        }

        public static T GetPrevious<T>(this IEnumerable<T> list, T current)
        {
            try
            {
                return list.TakeWhile(x => !x.Equals(current)).Last();
            }
            catch
            {
                return default(T);
            }
        }
    }
}
