using System;
namespace CalculatorCompiler.Core
{
    public class TimeFactorExpression : Expression
    {
        public Token Time { get; set; }

        public TimeFactorExpression(Token time)
        {
            Time = time;
        }
    }
}

