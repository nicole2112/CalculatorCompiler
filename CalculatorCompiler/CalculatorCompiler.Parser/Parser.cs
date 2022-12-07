using CalculatorCompiler.Core;
using CalculatorCompiler.Lexer;
using System.Linq.Expressions;

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

        public void Parse()
        {
            Code();
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
        
        private void Code()
        {
            Block();
        }

        private void Block()
        {
            Decls();
            Stmts();
        }

        private void Stmts()
        {
            if(this.lookAhead.TokenType == TokenType.PrintKeyword)
            {
                Stmt();
                Stmts();
            }
        }

        private void Stmt()
        {
            Print_Stmt();
        }

        private void Print_Stmt()
        {
            Match(TokenType.PrintKeyword);
            Match(TokenType.OpenParenthesis);
            Expr();
            Match(TokenType.CloseParenthesis);
            Match(TokenType.SemicolonOperator);
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

        private void Expr()
        {
            Factor();
            while(this.lookAhead.TokenType == TokenType.PlusOperator ||
               this.lookAhead.TokenType == TokenType.MinusOperator)
            {
                Move();
                Factor();
            }
        }

        private void Factor()
        {
            switch(this.lookAhead.TokenType)
            {
                case TokenType.Id:
                Match(TokenType.Id);
                break;
                
                case TokenType.NumberConstant:
                DateFactor();
                break;

                default:
                TimeFactor();
                break;
            }
        }

        private void TimeFactor()
        {
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
        }

        private void DateFactor()
        {
            Match(TokenType.NumberConstant);
            Match(TokenType.SlashOperator);
            Match(TokenType.NumberConstant);
            Match(TokenType.SlashOperator);
            Match(TokenType.NumberConstant);

            if(this.lookAhead.TokenType == TokenType.NumberConstant)
            {
                Match(TokenType.NumberConstant);
                Match(TokenType.ColonOperator);
                Match(TokenType.NumberConstant);
                Match(TokenType.ColonOperator);
                Match(TokenType.NumberConstant);
            }
        }
    }
}