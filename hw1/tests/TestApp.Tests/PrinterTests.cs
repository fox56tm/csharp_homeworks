using System.Diagnostics;
using System.Reflection;
using TestApp.Attributes;
using TestApp.Tests.Fixtures;

namespace TestApp.Tests;

public class PrinterTests
{
    private static readonly MethodInfo TestMethod = typeof(PrinterFixture).GetMethod(nameof(PrinterFixture.TestMethod))!;

    [Fact]
    public void PrinTestsWhithoutExceptions_NoExpected_WritesPassed()
    {
        // Arrange
        var writer = new StringWriter();
        var printer = new Printer(writer);

        // Act
        printer.PrinTestsWhithoutExceptions(typeof(PrinterFixture), TestMethod, null, new Stopwatch());

        // Assert
        Assert.StartsWith("PrinterFixture.TestMethod: passed", writer.ToString());
    }

    [Fact]
    public void PrinTestsWhithoutExceptions_Expected_WritesFailedExpected()
    {
        // Arrange
        var writer = new StringWriter();
        var printer = new Printer(writer);
        var attribute = new TestAttribute { Expected = typeof(InvalidOperationException) };

        // Act
        printer.PrinTestsWhithoutExceptions(typeof(PrinterFixture), TestMethod, attribute, new Stopwatch());

        // Assert
        Assert.StartsWith("PrinterFixture.TestMethod: failed expected InvalidOperationException", writer.ToString());
    }

    [Fact]
    public void PrinTestsWithExceptions_ExpectedException_WritesPassed()
    {
        // Arrange
        var writer = new StringWriter();
        var printer = new Printer(writer);
        var attribute = new TestAttribute { Expected = typeof(InvalidOperationException) };
        var exception = new TargetInvocationException(new InvalidOperationException("boom"));

        // Act
        printer.PrinTestsWithExceptions(typeof(PrinterFixture), TestMethod, attribute, new Stopwatch(), exception);

        // Assert
        Assert.StartsWith("PrinterFixture.TestMethod: passed boom", writer.ToString());
    }

    [Fact]
    public void PrinTestsWithExceptions_WrongException_WritesFailed()
    {
        // Arrange
        var writer = new StringWriter();
        var printer = new Printer(writer);
        var attribute = new TestAttribute { Expected = typeof(InvalidOperationException) };
        var exception = new TargetInvocationException(new ArgumentException("bad"));

        // Act
        printer.PrinTestsWithExceptions(typeof(PrinterFixture), TestMethod, attribute, new Stopwatch(), exception);

        // Assert
        Assert.StartsWith("PrinterFixture.TestMethod: failed bad", writer.ToString());
    }

    [Fact]
    public void PrinTestsWithExceptions_NoExpected_WritesFailed()
    {
        // Arrange
        var writer = new StringWriter();
        var printer = new Printer(writer);
        var exception = new TargetInvocationException(new InvalidOperationException("boom"));

        // Act
        printer.PrinTestsWithExceptions(typeof(PrinterFixture), TestMethod, null, new Stopwatch(), exception);

        // Assert
        Assert.StartsWith("PrinterFixture.TestMethod: failed boom", writer.ToString());
    }
}
