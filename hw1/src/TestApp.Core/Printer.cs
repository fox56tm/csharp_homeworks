using System.Diagnostics;
using System.Reflection;
using TestApp.Attributes;

class Printer
{
    public void PrinTestsWhithoutExceptions(MemberInfo method, MemberInfo test, TestAttribute? attribute, Stopwatch stopwatch)
    {
        if (attribute?.Expected != null)
        {
            Console.WriteLine($"{method.Name}.{test.Name}: failed expected {attribute.Expected.Name} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        else
        {
            Console.WriteLine($"{method.Name}.{test.Name}: passed ({stopwatch.ElapsedMilliseconds} ms)");
        }
    }

    public void PrinTestsWithExceptions(MemberInfo method, MemberInfo test, TestAttribute? attribute, Stopwatch stopwatch, TargetInvocationException ex)
    {
        var inner = ex.InnerException;
        if (attribute?.Expected != null && inner != null && attribute.Expected.IsInstanceOfType(inner))
        {
            Console.WriteLine($"{method.Name}.{test.Name}: passed {inner.Message} ({stopwatch.ElapsedMilliseconds} ms)");
        }
        else
        {
            Console.WriteLine($"{method.Name}.{test.Name}: failed {inner?.Message} ({stopwatch.ElapsedMilliseconds} ms)");
        }
    }
}
