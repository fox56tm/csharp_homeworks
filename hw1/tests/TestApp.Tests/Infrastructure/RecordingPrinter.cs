using System.Diagnostics;
using System.Reflection;
using TestApp.Attributes;

namespace TestApp.Tests.Infrastructure;

internal sealed class RecordingPrinter : IPrinter
{
    public List<PrinterCall> WithoutExceptionCalls { get; } = new();

    public List<PrinterCall> WithExceptionCalls { get; } = new();

    public IEnumerable<PrinterCall> Calls => WithoutExceptionCalls.Concat(WithExceptionCalls);

    public void PrinTestsWhithoutExceptions(MemberInfo method, MemberInfo test, TestAttribute? attribute, Stopwatch stopwatch)
    {
        WithoutExceptionCalls.Add(new PrinterCall(method, test, attribute, null));
    }

    public void PrinTestsWithExceptions(MemberInfo method, MemberInfo test, TestAttribute? attribute, Stopwatch stopwatch, TargetInvocationException ex)
    {
        WithExceptionCalls.Add(new PrinterCall(method, test, attribute, ex));
    }
}

internal sealed record PrinterCall(MemberInfo Method, MemberInfo Test, TestAttribute? Attribute, TargetInvocationException? Exception);
