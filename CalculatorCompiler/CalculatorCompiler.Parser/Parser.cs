using CalculatorCompiler.Core;
using CalculatorCompiler.Lexer;
using System.Linq.Expressions;
using System.Text;
using Expression = CalculatorCompiler.Core.Expression;

namespace CalculatorCompiler.Parser
{
    public class Parser
    {
        private Token lookAhead;
        private readonly IScanner scanner;

        public Parser(IScanner scanner)
        {
            this.scanner = scanner;
            this.Move();
        }

        public Statement Parse()
        {
            var code = Code();
            return code;
        }

        private void Move()
        {
            this.lookAhead = this.scanner.GetNextToken();
        }

        private void Match(TokenType tokenType)
        {
            if(this.lookAhead.TokenType != tokenType) {
                throw new ApplicationException($"Syntax error! Expected {tokenType} but found {this.lookAhead.TokenType}, Line {this.lookAhead.Line}, Column: {this.lookAhead.Column}");
            }
            this.Move();
        }
        
        private Statement Code()
        {
            var block = Block();
            return block;
        }

        private Statement Block()
        {
            Decls();
            var stmts = Stmts();
            return stmts;
        }

        private Statement Stmts()
        {
            if(this.lookAhead.TokenType == TokenType.PrintKeyword)
            {
               return new SequenceStatement(Stmt(), Stmts());
            }

            return null; 

        }

        private Statement Stmt()
        {
            var printStmt = Print_Stmt();
            return printStmt;
        }

        private Statement Print_Stmt()
        {
            Match(TokenType.PrintKeyword);
            Match(TokenType.OpenParenthesis);
            var expr = Expr();
            Match(TokenType.CloseParenthesis);
            Match(TokenType.SemicolonOperator);
            return new PrintStatement(expr);
        }

        private void Decls()
        {
            if(this.lookAhead.TokenType == TokenType.Id)
            {
                Decl();
                Decls();
            }
            
        }

        private void Decl()
        {
            Match(TokenType.Id);
            Match(TokenType.AssignationOperator);
            Expr();
            Match(TokenType.SemicolonOperator);
        }

        private Expression Expr()
        {
            var leftExpression = Factor();
            while(this.lookAhead.TokenType == TokenType.PlusOperator ||
               this.lookAhead.TokenType == TokenType.MinusOperator)
            {
                var token = this.lookAhead;
                Move();
                var rightExpression = Factor();
                leftExpression = new ArithmeticExpression(leftExpression, rightExpression, token);
            }

            return leftExpression;
        }

        private Expression Factor()
        {
            var token = this.lookAhead;

            switch(this.lookAhead.TokenType)
            {
                case TokenType.Id:
                Match(TokenType.Id);
                return new IdExpression(token.Lexeme);
  
                case TokenType.NumberConstant:
                return DateFactor();

                default:
                return TimeFactor();
            }
        }

        private Expression TimeFactor()
        {
            var token = this.lookAhead;

            switch(this.lookAhead.TokenType)
            {
                case TokenType.DateConstant:
                Match(TokenType.DateConstant);
                break;

                case TokenType.MonthConstant:
                Match(TokenType.MonthConstant);
                break;

                case TokenType.YearConstant:
                Match(TokenType.YearConstant);
                break;

                case TokenType.HourConstant: 
                Match(TokenType.HourConstant); 
                break;

                case TokenType.MinuteConstant:
                Match(TokenType.MinuteConstant);
                break;

                default:
                Match(TokenType.SecondConstant);
                break;
            }

            return new TimeFactorExpression(token);
        }

        private Expression DateFactor()
        {
            var date = new StringBuilder();
            var token = this.lookAhead;

            Match(TokenType.NumberConstant);
            date.Append(token.Lexeme);
            Match(TokenType.SlashOperator);
            date.Append('/');
            token = this.lookAhead;
            Match(TokenType.NumberConstant);
            date.Append(token.Lexeme);
            Match(TokenType.SlashOperator);
            date.Append('/');
            token = this.lookAhead;
            Match(TokenType.NumberConstant);
            date.Append(token.Lexeme);

            if (this.lookAhead.TokenType == TokenType.NumberConstant)
            {
                token = this.lookAhead;
                Match(TokenType.NumberConstant);
                date.Append(token.Lexeme);
                Match(TokenType.ColonOperator);
                date.Append(':');
                token = this.lookAhead;
                Match(TokenType.NumberConstant);
                date.Append(token.Lexeme);
                Match(TokenType.ColonOperator);
                date.Append(':');
                token = this.lookAhead;
                Match(TokenType.NumberConstant);
                date.Append(token.Lexeme);
            }

            return new DateFactorExpression(date.ToString());
        }
    }
}