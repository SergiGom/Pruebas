using DDL.Interfaces_para_Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDL.Clase_Basica
{
    public class CollectionService : ICollectionService
    {
        public IEnumerable<T> Unique<T>(IEnumerable<T> items) => items.Distinct();

        public IEnumerable<T> TopN<T>(IEnumerable<T> items, int n, Func<T, int> selector)
            => items.OrderByDescending(selector).Take(n);

        public IEnumerable<T> Filter<T>(IEnumerable<T> items, Func<T, bool> predicate)
            => items.Where(predicate);

        public T MaxBy<T>(IEnumerable<T> items, Func<T, int> selector)
            => items.MaxBy(selector) ?? throw new ArgumentException("Secuencia vacía");

        public IEnumerable<T> Flatten<T>(IEnumerable<IEnumerable<T>> items)
            => items.SelectMany(x => x);

        public (IEnumerable<T>, IEnumerable<T>) Partition<T>(IEnumerable<T> items, Func<T, bool> predicate)
        {
            var list = items.ToList();
            return (list.Where(predicate), list.Where(x => !predicate(x)));
        }

        public IEnumerable<T> DistinctBy<T, TKey>(IEnumerable<T> items, Func<T, TKey> keySelector)
        {
            var seen = new HashSet<TKey>();
            foreach (var item in items)
                if (seen.Add(keySelector(item)))
                    yield return item;
        }

        public int CountBy<T>(IEnumerable<T> items, Func<T, bool> predicate)
            => items.Count(predicate);
    }

}
