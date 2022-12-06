namespace CalculatorCompiler.Lexer;

public class Result
{
    public static Result<T> Empty<T>(Input reminder) => new Result<T>(reminder);

    public static Result<T> Value<T>(T value, Input reminder) => new Result<T>(value, reminder);
}