namespace Lazy.Core;

public class ThreadSafeLazy<T> : ILazy<T>
{
    private Func<T>? _supplier;
    private T? _value;
    private volatile bool _isCreated;
    private readonly object _sync = new();

    public ThreadSafeLazy(Func<T> supplier)
    {
        _supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));
    }

    public T Get()
    {
        if (_isCreated)
            return _value!;

        lock (_sync)
        {
            if (!_isCreated)
            {
                _value = _supplier!();
                _supplier = null;
                _isCreated = true;
            }
        }

        return _value!;
    }
}
