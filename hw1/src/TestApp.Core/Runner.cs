using TestApp.Attributes;

namespace TestApp.Core;

using System.Reflection;
using System.Diagnostics;

public class Runner
{
    public void RunTests(string path)
    {
        var addInAssemblies = new List<Assembly>();
        foreach (var dll in Directory.EnumerateFiles(path, "*.dll"))
        {
            addInAssemblies.Add(Assembly.LoadFrom(dll));

        }
        var addInTypes =
            addInAssemblies
                .SelectMany(a => a.ExportedTypes)
                .Where(t => t.IsClass
                            && !t.IsAbstract && t.GetMethods().Any(m => m.IsDefined(typeof(TestAttribute))));
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
                    if (attribute?.Expected != null)
                    {
                        Console.WriteLine($"{t.Name}.{test.Name}: failed — expected {attribute.Expected.Name} ({sw.ElapsedMilliseconds} ms)");
                    }
                    else
                    {
                        Console.WriteLine($"{t.Name}.{test.Name}: passed ({sw.ElapsedMilliseconds} ms)");
                    }
                }
                catch (TargetInvocationException ex)
                {
                    sw.Stop();
                    var inner = ex.InnerException;
                    if (attribute?.Expected != null && inner != null && attribute.Expected.IsInstanceOfType(inner))
                    {
                        Console.WriteLine($"{t.Name}.{test.Name}: passed — {inner.Message} ({sw.ElapsedMilliseconds} ms)");
                    }
                    else
                    {
                        Console.WriteLine($"{t.Name}.{test.Name}: failed — {inner?.Message} ({sw.ElapsedMilliseconds} ms)");
                    }
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
