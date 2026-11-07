namespace MyLinq.Core;
public static class MyLinqMapFilterFold
{
    public static List<TResult> Map<T, TResult>(List<T> list, Func<T, TResult> func)
    {
        var len = list.Count;
        var finalList = new List<TResult>();
        for (int i = 0; i < len; i++)
        {
            finalList.Add(func(list[i]));
        }

        return finalList;
    }

    public static List<T> Filter<T>(List<T> list, Func<T, bool> func)
    {
        var len = list.Count;
        var finalList = new List<T>();
        for (int i = 0; i < len; i++)
        {
            if (func(list[i])) finalList.Add(list[i]);
        }

        return finalList;
    }

    public static TAccumulate Fold<T, TAccumulate>(List<T> list, TAccumulate seed,
        Func<TAccumulate, T, TAccumulate> func)
    {
        var len = list.Count;
        var acc = seed;
        for (int i = 0; i < len; i++)
        {
            acc = func(acc, list[i]);
        }

        return acc;
    }
}
