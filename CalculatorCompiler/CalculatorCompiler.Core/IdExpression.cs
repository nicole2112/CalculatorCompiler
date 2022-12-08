using System;
namespace CalculatorCompiler.Core
{
    public class IdExpression : Expression
    {
        public String Name { get; set; }
        public IdExpression(string name)
        {
            Name = name;
        }
    }
}

