using System.Reflection;
using TestApp.Attributes;

public class Parser
{
    public IEnumerable<Type> ParsePath(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"Path not found {path}");
        }

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
        return addInTypes;
    }
}
