using TestApp.Attributes;

public class TemplateTestClass
{
    [OneTimeSetUp]
    public void BeforeAll()
    {
    }

    [SetUp]
    public void BeforeEach()
    {
    }

    [Test(Expected = typeof(int), Ignore = "reason")]
    public void SampleTest()
    {
    }

    [TearDown]
    public void AfterEach()
    {
    }

    [OneTimeTearDown]
    public void AfterAll()
    {
    }
}
