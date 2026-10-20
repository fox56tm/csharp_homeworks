using TestApp.Attributes;

namespace TestApp.Core;

using System.Reflection;
using System.Diagnostics;

public class Runner
{
    private readonly IPrinter _printer;

    public Runner() : this(new ConsolePrinter())
    {
    }

    public Runner(IPrinter printer)
    {
        _printer = printer;
    }

    public void RunTests(IEnumerable<Type> addInTypes)
    {
        foreach (Type t in addInTypes)
        {
            var oneTimeSetUpmethods = t.GetMethods().Where(m => m.IsDefined(typeof(OneTimeSetUpAttribute)));
            foreach (var onesetup in oneTimeSetUpmethods)
            {
                onesetup.Invoke(null, null);
            }
            var oneSetUpmethods = t.GetMethods().Where(m => m.IsDefined(typeof(SetUpAttribute)));
            var testsMethods = t.GetMethods().Where(m => m.IsDefined(typeof(TestAttribute)));
            var tearDownMethods = t.GetMethods().Where(m => m.IsDefined(typeof(TearDownAttribute)));
            var instance = Activator.CreateInstance(t);
            foreach (var test in testsMethods)
            {
                var attribute = test.GetCustomAttribute<TestAttribute>();
                if (attribute?.Ignore != null)
                {
                    Console.WriteLine($"{t.Name}.{test.Name}: skipped ({attribute.Ignore})");
                    continue;
                }
                foreach (var setup in oneSetUpmethods)
                {
                    setup.Invoke(instance, null);
                }
                var sw = Stopwatch.StartNew();
                try
                {
                    test.Invoke(instance, null);
                    sw.Stop();
                    _printer.PrinTestsWhithoutExceptions(t, test, attribute, sw);
                }
                catch (TargetInvocationException ex)
                {
                    sw.Stop();
                    _printer.PrinTestsWithExceptions(t, test, attribute, sw, ex);
                }
                foreach (var teardown in tearDownMethods)
                {
                    teardown.Invoke(instance, null);
                }
            }
            var oneTearDownMethods = t.GetMethods().Where(m => m.IsDefined(typeof(OneTimeTearDownAttribute)));
            foreach (var onetear in oneTearDownMethods)
            {
                onetear.Invoke(null, null);
            }
        }
    }
}
