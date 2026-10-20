using TestApp.Attributes;

namespace TestApp.Tests.Fixtures;

public class ParserSampleTestClass
{
    [Test]
    public void TestMethod()
    {
    }
}

public class ParserClassWithoutTests
{
    public void NotATest()
    {
    }
}

public abstract class ParserAbstractTestClass
{
    [Test]
    public void TestMethod()
    {
    }
}
