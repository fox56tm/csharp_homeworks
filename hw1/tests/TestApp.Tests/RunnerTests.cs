using TestApp.Core;
using TestApp.Tests.Fixtures;
using TestApp.Tests.Infrastructure;

namespace TestApp.Tests;

[Collection("Runner")]
public class RunnerTests
{
    [Fact]
    public void RunTests_PassingTest_CallsPrinterWithoutExceptions()
    {
        // Arrange
        var printer = new RecordingPrinter();
        var runner = new Runner(printer);

        // Act
        runner.RunTests(new[] { typeof(PassingFixture) });

        // Assert
        Assert.Single(printer.WithoutExceptionCalls);
    }

    [Fact]
    public void RunTests_FailingTest_CallsPrinterWithExceptions()
    {
        // Arrange
        var printer = new RecordingPrinter();
        var runner = new Runner(printer);

        // Act
        runner.RunTests(new[] { typeof(FailingFixture) });

        // Assert
        Assert.Single(printer.WithExceptionCalls);
    }

    [Fact]
    public void RunTests_IgnoredTest_DoesNotInvokeTest()
    {
        // Arrange
        IgnoredFixture.InvocationCount = 0;
        var runner = new Runner(new RecordingPrinter());

        // Act
        runner.RunTests(new[] { typeof(IgnoredFixture) });

        // Assert
        Assert.Equal(0, IgnoredFixture.InvocationCount);
    }

    [Fact]
    public void RunTests_IgnoredTest_DoesNotCallPrinter()
    {
        // Arrange
        var printer = new RecordingPrinter();
        var runner = new Runner(printer);

        // Act
        runner.RunTests(new[] { typeof(IgnoredFixture) });

        // Assert
        Assert.Empty(printer.Calls);
    }

    [Fact]
    public void RunTests_IgnoredTest_WritesSkipped()
    {
        // Arrange
        var runner = new Runner(new RecordingPrinter());

        // Act
        var output = ConsoleCapture.Capture(() => runner.RunTests(new[] { typeof(IgnoredFixture) }));

        // Assert
        Assert.Contains("IgnoredFixture.Skipped: skipped (known issue)", output);
    }

    [Fact]
    public void RunTests_TwoTests_CallsSetUpOncePerTest()
    {
        // Arrange
        LifecycleFixture.Reset();
        var runner = new Runner(new RecordingPrinter());

        // Act
        runner.RunTests(new[] { typeof(LifecycleFixture) });

        // Assert
        Assert.Equal(2, LifecycleFixture.SetUpCount);
    }

    [Fact]
    public void RunTests_TwoTests_CallsTearDownOncePerTest()
    {
        // Arrange
        LifecycleFixture.Reset();
        var runner = new Runner(new RecordingPrinter());

        // Act
        runner.RunTests(new[] { typeof(LifecycleFixture) });

        // Assert
        Assert.Equal(2, LifecycleFixture.TearDownCount);
    }

    [Fact]
    public void RunTests_TwoTests_CallsOneTimeSetUpOnce()
    {
        // Arrange
        LifecycleFixture.Reset();
        var runner = new Runner(new RecordingPrinter());

        // Act
        runner.RunTests(new[] { typeof(LifecycleFixture) });

        // Assert
        Assert.Equal(1, LifecycleFixture.OneTimeSetUpCount);
    }

    [Fact]
    public void RunTests_TwoTests_CallsOneTimeTearDownOnce()
    {
        // Arrange
        LifecycleFixture.Reset();
        var runner = new Runner(new RecordingPrinter());

        // Act
        runner.RunTests(new[] { typeof(LifecycleFixture) });

        // Assert
        Assert.Equal(1, LifecycleFixture.OneTimeTearDownCount);
    }

    [Fact]
    public void RunTests_FailingTest_CallsTearDown()
    {
        // Arrange
        FailingFixture.TearDownCount = 0;
        var runner = new Runner(new RecordingPrinter());

        // Act
        runner.RunTests(new[] { typeof(FailingFixture) });

        // Assert
        Assert.Equal(1, FailingFixture.TearDownCount);
    }
}
