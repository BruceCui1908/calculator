namespace Calculator.Services;

public interface ICalculatorService
{
    decimal Add(decimal left, decimal right);
    decimal Subtract(decimal left, decimal right);
    decimal Multiply(decimal left, decimal right);
    decimal Divide(decimal left, decimal right);
}

public class CalculatorService : ICalculatorService
{
    public decimal Add(decimal left, decimal right)
    {
        return Decimal.Add(left, right);
    }

    public decimal Subtract(decimal left, decimal right)
    {
        return Decimal.Subtract(left, right);
    }

    public decimal Multiply(decimal left, decimal right)
    {
        return Decimal.Multiply(left, right);
    }

    public decimal Divide(decimal left, decimal right)
    {
        if (right == Decimal.Zero)
        {
            throw new ArgumentException("The divisor cannot be null");
        }

        return Decimal.Divide(left, right);
    }
}