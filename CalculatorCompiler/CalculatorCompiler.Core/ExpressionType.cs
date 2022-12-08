using System;
namespace CalculatorCompiler.Core
{
    public class ExpressionType
    {
        public String Lexema { get; set; }
        //public TokenType TokenType { get; set; }

        public ExpressionType(string lexema)
        {
            Lexema = lexema;

        }

        public static ExpressionType Day = new ExpressionType("Day");
        public static ExpressionType Month = new ExpressionType("Month");
        public static ExpressionType Year = new ExpressionType("Year");
        public static ExpressionType Minutes = new ExpressionType("Minutes");
        public static ExpressionType Seconds = new ExpressionType("Seconds");
        public static ExpressionType Hours = new ExpressionType("Hours");
        public static ExpressionType Date = new ExpressionType("Date");

    }
}

