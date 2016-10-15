using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static void AddIfNotNull<T>(this List<T> list, T element)
    {
        if(list == null || element == null)
        {
            return;
        }
        list.Add(element);
    }

    public static void AddRangeWithoutDouplicate<K,V>(this List<KeyValuePair<K,V>> list, List<KeyValuePair<K, V>> elements)
    {
        if (list == null || elements == null)
        {
            return;
        }

        foreach (var kv in elements)
        {
            if(list.Where(l => Equals(l.Key,kv.Key)).All(l => !Equals(l.Value, kv.Value)))
            {
                list.Add(kv);
            }
        }
    }

    public static bool IsNullOrEmpty<T>(this List<T> list)
    {
        return list == null || list.Count == 0;
    }
}