using System;
namespace CalculatorCompiler.Core
{
    public class ArithmeticExpression : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
        public Token Token { get; set; }

        public ArithmeticExpression(Expression left, Expression right, Token token)
        {
            Left = left;
            Right = right;
            Token = token;
        }

        //Changes in code
        public override dynamic Evaluate()
        {
            var left = this.Left.Evaluate() is DateTime ? (DateTime)this.Left.Evaluate() : default;
            var right = this.Right.Evaluate();

            switch (this.GetExpressionType().Lexema)
            {
                case "Day":
                    if(Token.TokenType == TokenType.PlusOperator)
                    {
                        return left.AddDays(right);
                    }
                    else
                    {
                        return left.AddDays(-right);
                    }

                case "Month":
                    if (Token.TokenType == TokenType.PlusOperator)
                    {
                        return left.AddMonths(right);
                    }
                    else
                    {
                        return left.AddMonths(-right);
                    }

                case "Year":
                    if (Token.TokenType == TokenType.PlusOperator)
                    {
                        return left.AddYears(right);
                    }
                    else
                    {
                        return left.AddYears(-right);
                    }
                case "Hours":
                    if (Token.TokenType == TokenType.PlusOperator)
                    {
                        return left.AddHours(right);
                    }
                    else
                    {
                        return left.AddHours(-right);
                    }
                case "Minutes":
                    if (Token.TokenType == TokenType.PlusOperator)
                    {
                        return left.AddMinutes(right);
                    }
                    else
                    {
                        return left.AddMinutes(-right);
                    }

                default:
                    if (Token.TokenType == TokenType.PlusOperator)
                    {
                        return left.AddSeconds(right);
                    }
                    else
                    {
                        return left.AddSeconds(-right);
                    }
                    
            }
        }

        public override ExpressionType GetExpressionType()
        {
            throw new NotImplementedException();
        }
    }
}

