namespace MyLinq.Core;

public static class MyLinq
{
    public static IEnumerable<int> GetPrimes()
    {
        var primes = new List<int>();
        for (int n = 2;; n++)
        {
            bool isPrime = true;
            foreach (var p in primes)
            {
                if (p * p > n) break;
                if (n % p == 0)
                {
                    isPrime = false;
                    break;
                }
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
        if (n <= 0) yield break;
        var i = 0;
        foreach (var item in seq)
        {
            yield return item;
            if (++i >= n) yield break;
        }
    }

    public static IEnumerable<T> Skip<T>(this IEnumerable<T> seq, int n)
    {
        var i = 0;
        foreach (var item in seq)
        {
            if (i++ >= n) yield return item;
        }
    }
}
