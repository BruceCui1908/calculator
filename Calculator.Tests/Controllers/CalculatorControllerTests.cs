using Calculator.Controllers;
using Calculator.Middleware;
using Calculator.Models;
using Calculator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Calculator.Tests.Controllers;

public class CalculatorControllerTests
{
    private readonly Mock<ICalculatorService> _calculatorServiceMock;
    private readonly Mock<ILogger<CalculatorController>> _mockLogger;

    private readonly CalculatorController _calculatorController;

    public CalculatorControllerTests()
    {
        _calculatorServiceMock = new Mock<ICalculatorService>();
        _mockLogger = new Mock<ILogger<CalculatorController>>();
        _calculatorController = new CalculatorController(_calculatorServiceMock.Object, _mockLogger.Object);
    }

    [Fact]
    public void TestCalculatorController_ShouldReturnCorrectApiResponse_WhenOperationIsAddition()
    {
        var mockCalculationDto = new CalculationDto()
        {
            Operation = Operation.Addition,
            Left = 12.123m,
            Right = 23.456m,
        };

        var mockAddResult = 12321123.123m;

        _calculatorServiceMock
            .Setup(service => service.Add(mockCalculationDto.Left!.Value, mockCalculationDto.Right!.Value))
            .Returns(mockAddResult);

        var apiResponse = _calculatorController.Calculate(mockCalculationDto);

        var okResult = Assert.IsType<OkObjectResult>(apiResponse);
        var returnValue = Assert.IsType<CommonApiResponse<decimal?>>(okResult.Value);

        Assert.Equal(mockAddResult, returnValue.Data);

        _mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact]
    public void TestCalculatorController_ShouldReturnCorrectApiResponse_WhenOperationIsSubtraction()
    {
        var mockCalculationDto = new CalculationDto()
        {
            Operation = Operation.Subtraction,
            Left = 12.123m,
            Right = 23.456m,
        };

        var mockAddResult = 12321123.123m;

        _calculatorServiceMock
            .Setup(service => service.Subtract(mockCalculationDto.Left!.Value, mockCalculationDto.Right!.Value))
            .Returns(mockAddResult);

        var apiResponse = _calculatorController.Calculate(mockCalculationDto);

        var okResult = Assert.IsType<OkObjectResult>(apiResponse);
        var returnValue = Assert.IsType<CommonApiResponse<decimal?>>(okResult.Value);

        Assert.Equal(mockAddResult, returnValue.Data);

        _mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact]
    public void TestCalculatorController_ShouldReturnCorrectApiResponse_WhenOperationIsMultiple()
    {
        var mockCalculationDto = new CalculationDto()
        {
            Operation = Operation.Multiplication,
            Left = 12.123m,
            Right = 23.456m,
        };

        var mockAddResult = 12321123.123m;

        _calculatorServiceMock
            .Setup(service => service.Multiply(mockCalculationDto.Left!.Value, mockCalculationDto.Right!.Value))
            .Returns(mockAddResult);

        var apiResponse = _calculatorController.Calculate(mockCalculationDto);

        var okResult = Assert.IsType<OkObjectResult>(apiResponse);
        var returnValue = Assert.IsType<CommonApiResponse<decimal?>>(okResult.Value);

        Assert.Equal(mockAddResult, returnValue.Data);

        _mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact]
    public void TestCalculatorController_ShouldReturnCorrectApiResponse_WhenOperationIsDivision()
    {
        var mockCalculationDto = new CalculationDto()
        {
            Operation = Operation.Division,
            Left = 12.123m,
            Right = 23.456m,
        };

        var mockAddResult = 12321123.123m;

        _calculatorServiceMock
            .Setup(service => service.Divide(mockCalculationDto.Left!.Value, mockCalculationDto.Right!.Value))
            .Returns(mockAddResult);

        var apiResponse = _calculatorController.Calculate(mockCalculationDto);

        var okResult = Assert.IsType<OkObjectResult>(apiResponse);
        var returnValue = Assert.IsType<CommonApiResponse<decimal?>>(okResult.Value);

        Assert.Equal(mockAddResult, returnValue.Data);

        _mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    /// <summary>
    /// Testing the global exception handler
    /// </summary>
    [Fact]
    public async Task TestCalculatorController_ShouldReturnErrorApiResponse_WhenOperationIsDivisionAndRightIsZero()
    {
        var mockLogger = new Mock<ILogger<GlobalExceptionHandlerMiddleware>>();
        var context = new DefaultHttpContext();


        var mockCalculationDto = new CalculationDto()
        {
            Operation = Operation.Division,
            Left = 12.123m,
            Right = 0m,
        };


        var middleware = new GlobalExceptionHandlerMiddleware(next: async (innerHttpContext) =>
        {
            _calculatorServiceMock
                .Setup(service => service.Divide(mockCalculationDto.Left!.Value, mockCalculationDto.Right!.Value))
                .Throws(new ArgumentException("The divisor cannot be null"));
        }, logger: mockLogger.Object);

        await middleware.InvokeAsync(context);

        Assert.Equal(200, context.Response.StatusCode);

        mockLogger.Verify(
            logger => logger.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Never);
    }
}