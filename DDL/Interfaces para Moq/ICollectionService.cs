using System;
using System.Collections.Generic;
using System.Text;

namespace DDL.Interfaces_para_Moq
{
    public interface ICollectionService
    {
        IEnumerable<T> Unique<T>(IEnumerable<T> items);
        IEnumerable<T> TopN<T>(IEnumerable<T> items, int n, Func<T, int> selector);
        IEnumerable<T> Filter<T>(IEnumerable<T> items, Func<T, bool> predicate);
        T MaxBy<T>(IEnumerable<T> items, Func<T, int> selector);
        IEnumerable<T> Flatten<T>(IEnumerable<IEnumerable<T>> items);
        (IEnumerable<T>, IEnumerable<T>) Partition<T>(IEnumerable<T> items, Func<T, bool> predicate);
        IEnumerable<T> DistinctBy<T, TKey>(IEnumerable<T> items, Func<T, TKey> keySelector);
        int CountBy<T>(IEnumerable<T> items, Func<T, bool> predicate);
    }

}
