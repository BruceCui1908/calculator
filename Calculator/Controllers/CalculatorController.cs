using Calculator.Models;
using Calculator.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers;

public enum Operation
{
    Addition,
    Subtraction,
    Multiplication,
    Division,
}

public class CalculationDto
{
    public Operation? Operation { get; set; }

    public decimal? Left { get; set; }

    public decimal? Right { get; set; }

    public override string ToString()
    {
        var operation = Operation.HasValue ? Operation.ToString() : "None";
        var left = Left.HasValue ? Left.Value.ToString() : "None";
        var right = Right.HasValue ? Right.Value.ToString() : "None";
        return $"Operation: {operation}, Left: {left}, Right: {right}";
    }
}

public class CalculationValidator : AbstractValidator<CalculationDto>
{
    public CalculationValidator()
    {
        RuleFor(r => r.Operation)
            .NotNull()
            .NotEmpty()
            .WithMessage("Operation cannot be empty")
            .IsInEnum()
            .WithMessage("Invalid Operation");

        RuleFor(r => r.Left)
            .NotNull()
            .NotEmpty()
            .WithMessage("Invalid Left number");

        RuleFor(r => r.Right)
            .NotNull()
            .NotEmpty()
            .WithMessage("Invalid Right number");

        When(r => r.Operation == Operation.Division,
            () => { RuleFor(r => r.Right).Must(r => r != 0).WithMessage("Right cannot be 0"); });
    }
}

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculatorService _calculatorService;

    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ICalculatorService calculatorService, ILogger<CalculatorController> logger)
    {
        _calculatorService = calculatorService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Calculate(CalculationDto calculationDto)
    {
        decimal? result = null;
        switch (calculationDto.Operation)
        {
            case Operation.Addition:
                result = _calculatorService.Add(calculationDto.Left!.Value, calculationDto.Right!.Value);
                break;
            case Operation.Subtraction:
                result = _calculatorService.Subtract(calculationDto.Left!.Value, calculationDto.Right!.Value);
                break;
            case Operation.Multiplication:
                result = _calculatorService.Multiply(calculationDto.Left!.Value, calculationDto.Right!.Value);
                break;
            case Operation.Division:
                result = _calculatorService.Divide(calculationDto.Left!.Value, calculationDto.Right!.Value);
                break;
            default:
                break;
        }

        _logger.LogInformation("Received {CalculationDto}, result is {Result}", calculationDto, result);
        
        return Ok(CommonApiResponse<decimal?>.Success(result));
    }
}