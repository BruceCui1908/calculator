using Calculator.Services;

namespace Calculator.Tests.Services;

public class CalculatorServiceTests
{
    [Fact]
    public void TestCalculatorServiceAdd_ShouldReturnCorrectResult()
    {
        var calService = new CalculatorService();

        var left = 12.12m;
        var right = 12.12m;

        var realResult = calService.Add(left, right);
        var expectedResult = left + right;

        Assert.Equal(expectedResult, realResult);
    }

    [Fact]
    public void TestCalculatorServiceAddWithBigNumbers_ShouldReturnCorrectResult()
    {
        var calService = new CalculatorService();

        var left = 12321323213.1212321m;
        var right = 12123213213.12123213123m;

        var realResult = calService.Add(left, right);
        var expectedResult = left + right;

        Assert.Equal(expectedResult, realResult);
    }


    [Fact]
    public void TestCalculatorServiceSubtract_ShouldReturnCorrectResult()
    {
        var calService = new CalculatorService();

        var left = 12.12m;
        var right = 12.12m;

        var realResult = calService.Subtract(left, right);
        var expectedResult = left - right;

        Assert.Equal(expectedResult, realResult);
    }

    [Fact]
    public void TestCalculatorServiceSubtractWithBigNumbers_ShouldReturnCorrectResult()
    {
        var calService = new CalculatorService();

        var left = 12.12m;
        var right = 121232132131323.12m;

        var realResult = calService.Subtract(left, right);
        var expectedResult = left - right;

        Assert.Equal(expectedResult, realResult);
    }

    [Fact]
    public void TestCalculatorServiceMultiple_ShouldReturnCorrectResult()
    {
        var calService = new CalculatorService();

        var left = 12.12m;
        var right = 12.12m;

        var realResult = calService.Multiply(left, right);
        var expectedResult = left * right;

        Assert.Equal(expectedResult, realResult);
    }

    [Fact]
    public void TestCalculatorServiceMultipleWithBigNumbers_ShouldReturnCorrectResult()
    {
        var calService = new CalculatorService();

        var left = 1221312.11231232m;
        var right = 12123123.12123123m;

        var realResult = calService.Multiply(left, right);
        var expectedResult = left * right;

        Assert.Equal(expectedResult, realResult);
    }

    [Fact]
    public void TestCalculatorServiceDivide_ThrowsArgumentException_WhenDivisorIsZero()
    {
        var calService = new CalculatorService();

        var left = 1221312.11231232m;
        var right = 0m;

        var exception = Assert.Throws<ArgumentException>(() => calService.Divide(left: left, right: right));

        Assert.Equal("The divisor cannot be null", exception.Message);
    }

    [Fact]
    public void TestCalculatorServiceDivide_ShouldReturnCorrectResult()
    {
        var calService = new CalculatorService();

        var left = 1221312.11231232m;
        var right = 123213.12313m;

        var realResult = calService.Divide(left, right);
        var expectedResult = left / right;

        Assert.Equal(expectedResult, realResult);
    }
}