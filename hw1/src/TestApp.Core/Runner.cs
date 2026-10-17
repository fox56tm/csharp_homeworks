using TestApp.Attributes;

namespace TestApp.Core;
using System.Reflection;
//~/Projects/csharp/csharp_facul/WebApplication2/WebApplication2/bin/Debug/net10.0/WebApplication2.dll
public class Runner
{
    public void RunTests(string path)
    {
        string addInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)
                          ?? throw new Exception("Unable to load assembly");
        var addInAssemblies = Directory.EnumerateFiles(addInDir, "*.dll");
        var addInTypes =
            addInAssemblies.Select(Assembly.Load)
                .SelectMany(a => a.ExportedTypes)
                .Where(t => t.IsClass
                            &&  !t.IsAbstract && t.IsDefined(typeof(TestAttribute)));
        foreach (Type t in addInTypes)
        {
            var instance = Activator.CreateInstance(t);
            var oneTimeSetUpmethods = t.GetMethods().Where(m => m.IsDefined(typeof(OneTimeSetUpAttribute)));
            foreach (var onesetup in oneTimeSetUpmethods)
            {
                onesetup.Invoke(instance, null);
            }
            var oneSetUpmethods = t.GetMethods().Where(m => m.IsDefined(typeof(SetUpAttribute)));
            var testsMethods = t.GetMethods().Where(m => m.IsDefined(typeof(TestAttribute)));
            var tearDownMethods = t.GetMethods().Where(m => m.IsDefined(typeof(TearDownAttribute)));
            foreach (var test in testsMethods)
            {
                foreach (var setup in oneSetUpmethods)
                {
                    setup.Invoke(instance, null);
                }
                test.Invoke(instance, null);
                foreach (var teardown in tearDownMethods)
                {
                    teardown.Invoke(instance, null);
                }
            }
            var oneTearDownMethods = t.GetMethods().Where(m => m.IsDefined(typeof(OneTimeTearDownAttribute)));
            foreach (var onetear in oneTearDownMethods)
            {
                onetear.Invoke(instance, null);
            }
        }
    }

}
