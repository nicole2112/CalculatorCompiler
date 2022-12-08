using System;
namespace CalculatorCompiler.Core
{
    public class PrintStatement : Statement
    {
        public Expression Expr { get; set; }

        public PrintStatement(Expression expr)
        {
            Expr = expr;
        }
    }
}

