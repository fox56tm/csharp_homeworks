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
                            && typeof(IAddIn).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()));
        foreach (Type t in addInTypes)
        {
            var addIn = Activator.CreateInstance(t) as IAddIn;
            Console.WriteLine(addIn?.DoSomething(5));
        }
    }

}
