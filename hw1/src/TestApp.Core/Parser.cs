using System.Reflection;

class Parser
{
    public string ParsePath(string path)
    {
        string addInDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)
                          ?? throw new Exception("Unable to load assembly");
        return addInDir;
    }
}