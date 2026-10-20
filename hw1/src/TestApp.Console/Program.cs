
using TestApp.Core;

if (args.Length == 0)
{
    Console.WriteLine("usage: TestApp.Console <path to folder with assemblies>");
    return;
}

try
{
    Parser parser = new Parser();
    var addInTypes = parser.ParsePath(args[0]);
    Runner runner = new Runner();
    runner.RunTests(addInTypes);
}
catch (DirectoryNotFoundException ex)
{
    Console.WriteLine(ex.Message);
}
