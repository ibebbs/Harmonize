using System;
using System.Collections.Generic;
using System.Linq;

namespace Bebbs.Harmonize.With
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            IEnumerator<T> enumerator = (source ?? Enumerable.Empty<T>()).GetEnumerator();

            while (enumerator.MoveNext())
            {
                action(enumerator.Current);

                yield return enumerator.Current;
            }
        }
    }
}
