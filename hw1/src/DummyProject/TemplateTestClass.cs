using TestApp.Attributes;

public class TemplateTestClass
{
    [OneTimeSetUp]
    public static void BeforeAll()
    {
    }

    [SetUp]
    public void BeforeEach()
    {
    }

    [Test(Expected = typeof(InvalidOperationException))]
    public void SampleTest()
    {
    }

    [Test(Ignore = "reason")]
    public void SkippedTest()
    {
    }

    [TearDown]
    public void AfterEach()
    {
    }

    [OneTimeTearDown]
    public static void AfterAll()
    {
    }
}
