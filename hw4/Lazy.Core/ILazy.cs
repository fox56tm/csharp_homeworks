namespace Lazy.Core;

public interface ILazy<T>
{
    T Get();
}