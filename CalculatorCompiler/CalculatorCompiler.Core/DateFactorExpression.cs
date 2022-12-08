using System;
namespace CalculatorCompiler.Core
{
    public class DateFactorExpression : Expression
    {
        public String Date { get; set; }

        public DateFactorExpression(string date)
        {
            Date = date;
        }
    }
}

