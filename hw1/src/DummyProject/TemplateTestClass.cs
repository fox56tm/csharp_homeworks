using TestApp.Attributes;

public class TemplateTestClass
{
    private int _dividend;

    [OneTimeSetUp]
    public static void BeforeAll()
    {
    }

    [SetUp]
    public void BeforeEach()
    {
        _dividend = 10;
    }

    [Test]
    public void Divide_ReturnsQuotient()
    {
        if (Divide(_dividend, 2) != 5)
            throw new Exception("10 / 2 must be 5");
    }

    [Test(Expected = typeof(DivideByZeroException))]
    public void Divide_ByZero_Throws()
    {
        Divide(_dividend, 0);
    }

    [Test]
    public void Divide_Fails()
    {
        if (Divide(_dividend, 2) != 4)
            throw new Exception("10 / 2 is not 4");
    }

    [Test(Ignore = "known issue")]
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

    private static int Divide(int a, int b)
    {
        return a / b;
    }
}
