
using TestApp.Core;

if (args.Length == 0)
{
    Console.WriteLine("usage: TestApp.Console <path to folder with assemblies>");
    return;
}

Runner r = new Runner();
r.RunTests(args[0]);
