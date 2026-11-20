namespace Lazy.Core;

public class SimpleLazy<T> : ILazy<T>
{
    private Func<T>? _supplier;
    private T? _value;

    public SimpleLazy(Func<T> supplier)
    {
        _supplier = supplier ?? throw new ArgumentNullException(nameof(supplier));
    }

    public T Get()
    {
        if (_supplier != null)
        {
            _value = _supplier();
            _supplier = null;
        }

        return _value!;
    }
}
