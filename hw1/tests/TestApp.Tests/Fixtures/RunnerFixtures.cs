using TestApp.Attributes;

namespace TestApp.Tests.Fixtures;

public class PassingFixture
{
    [Test]
    public void Passes()
    {
    }
}

public class FailingFixture
{
    public static int TearDownCount { get; set; }

    [Test]
    public void Fails()
    {
        throw new InvalidOperationException("boom");
    }

    [TearDown]
    public void TearDown()
    {
        TearDownCount++;
    }
}

public class IgnoredFixture
{
    public static int InvocationCount { get; set; }

    [Test(Ignore = "known issue")]
    public void Skipped()
    {
        InvocationCount++;
    }
}

public class LifecycleFixture
{
    public static int OneTimeSetUpCount { get; set; }

    public static int SetUpCount { get; set; }

    public static int TearDownCount { get; set; }

    public static int OneTimeTearDownCount { get; set; }

    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        OneTimeSetUpCount++;
    }

    [SetUp]
    public void SetUp()
    {
        SetUpCount++;
    }

    [Test]
    public void FirstTest()
    {
    }

    [Test]
    public void SecondTest()
    {
    }

    [TearDown]
    public void TearDown()
    {
        TearDownCount++;
    }

    [OneTimeTearDown]
    public static void OneTimeTearDown()
    {
        OneTimeTearDownCount++;
    }

    public static void Reset()
    {
        OneTimeSetUpCount = 0;
        SetUpCount = 0;
        TearDownCount = 0;
        OneTimeTearDownCount = 0;
    }
}
