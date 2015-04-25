using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Extensions
{
    public static class CollectionExtension
    {
        public static System.Collections.Generic.IEnumerable<T> Add<T>(this System.Collections.Generic.IEnumerable<T> source, params T[] toAdd)
        {
            System.Collections.Generic.List<T> list = new System.Collections.Generic.List<T>(source);
            list.AddRange(toAdd);
            return list.AsEnumerable<T>();
        }

        public static void ForEach<T>(this System.Collections.Generic.IEnumerable<T> @this, System.Action<T> action)
        {
            if (@this == null)
            {
                throw new System.ArgumentNullException("this");
            }
            if (action == null)
            {
                throw new System.ArgumentNullException("action");
            }
            foreach (T local in @this)
            {
                action(local);
            }
        }

        public static void ForEach<T>(this System.Collections.Generic.IEnumerable<T> @this, System.Action<T, int> action)
        {
            if (@this == null)
            {
                throw new System.ArgumentNullException("this");
            }
            if (action == null)
            {
                throw new System.ArgumentNullException("action");
            }
            int num = 0;
            foreach (T local in @this)
            {
                action(local, num);
                num = (int)(num + 1);
            }
        }

        public static System.Collections.Generic.IEnumerable<T> Remove<T>(this System.Collections.Generic.IEnumerable<T> source, T toRemove) where T : class
        {
            return (from item in source
                    where (bool)(toRemove != item)
                    select item);
        }

        public static void Times(this int number, System.Action<int> action)
        {
            if (action == null)
            {
                throw new System.ArgumentNullException("action");
            }
            if (number > 0)
            {
                for (int i = 0; i < number; i = (int)(i + 1))
                {
                    action(i);
                }
            }
        }

        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
                                                                               Func<T, TKey> getKey)
        {
            return from item in items
                   join otherItem in other on getKey(item)
                   equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;

        }
    }
}
