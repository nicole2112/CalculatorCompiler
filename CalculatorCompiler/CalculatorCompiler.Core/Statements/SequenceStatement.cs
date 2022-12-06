namespace CalculatorCompiler.Core.Statements;

public class SequenceStatement: Statement
{
    public Statement Current { get; set; }
    public Statement Next { get; set; }

    public SequenceStatement(Statement current, Statement next)
    {
        Current = current;
        Next = next;
    }
}