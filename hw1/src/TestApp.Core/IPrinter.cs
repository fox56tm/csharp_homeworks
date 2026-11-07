using System.Diagnostics;
using System.Reflection;
using TestApp.Attributes;

public interface IPrinter
{
    void PrinTestsWhithoutExceptions(MemberInfo method, MemberInfo test, TestAttribute? attribute, Stopwatch stopwatch);

    void PrinTestsWithExceptions(MemberInfo method, MemberInfo test, TestAttribute? attribute, Stopwatch stopwatch, TargetInvocationException ex);
}
