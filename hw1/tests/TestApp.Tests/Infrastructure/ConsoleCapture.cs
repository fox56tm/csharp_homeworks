namespace TestApp.Tests.Infrastructure;

internal static class ConsoleCapture
{
    private static readonly object SyncRoot = new();

    public static string Capture(Action action)
    {
        lock (SyncRoot)
        {
            var original = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                action();
                return writer.ToString();
            }
            finally
            {
                Console.SetOut(original);
            }
        }
    }
}
