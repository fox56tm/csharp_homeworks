namespace MyLinq.Core;

public static class MyLinq
{
    public static IEnumerable<int> GetPrimes()
    {
        var primes = new List<int>();
        for (int n = 2; ; n++)
        {
            bool isPrime = true;
            foreach (var p in primes)
            {
                if (p * p > n) break;
                if (n % p == 0) { isPrime = false; break; }
            }
            if (isPrime)
            {
                primes.Add(n);
                yield return n;
            }
        }
    }

    public static IEnumerable<T> Take<T>(this IEnumerable<T> seq, int n)
    {
        if(n<=0) yield break;
        var i = 0;
        foreach (var item in seq)
        {
            yield return item;
            if(++i>=n) yield break;
        }
    }

    public static IEnumerable<T> Skip<T>(this IEnumerable<T> seq, int n)
    {
        var i = 0;
        foreach (var item in seq)
        {
            if(i++ >= n) yield return item;
        }
    }

    public static List<T> Map<T>(List<T> list, Func<T, T> func)
    {
        var len =  list.Count;
        var finalList = new List<T>();
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
            if (func(list[i]))  finalList.Add(list[i]);
        }
        return finalList;
    }
    public static TAccumulate Fold<T, TAccumulate>(List<T> list, TAccumulate seed, Func<TAccumulate, T, TAccumulate> func)
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
