namespace CalculatorCompiler.Core;

public abstract class Expression
{
    public abstract dynamic Evaluate();

    public abstract ExpressionType GetExpressionType();
}