using System.Text;
using CalculatorCompiler.Core;

namespace CalculatorCompiler.Lexer;

public class Scanner: IScanner
{
    private Input input;
    private readonly Dictionary<string, TokenType> keywords;

    public Scanner(Input input)
    {
        this.input = input;
        this.keywords = new Dictionary<string, TokenType>()
        {
            {"print", TokenType.PrintKeyword}
        };
    }

    public Token GetNextToken()
    {
        var lexeme = new StringBuilder();           
        var currentChar = GetNextChar();

        while (char.IsWhiteSpace(currentChar) || currentChar == '\n') 
        {
            currentChar = GetNextChar();
        }
        
        //Id
        if (char.IsLetter(currentChar))
        {
            lexeme.Append(currentChar);
            currentChar = PeekNextChar();
            while (char.IsLetter(currentChar))
            {
                currentChar = GetNextChar();
                lexeme.Append(currentChar);
                currentChar = PeekNextChar(); 
            }
            //keywords
            if (this.keywords.ContainsKey(lexeme.ToString())) 
            {
                return lexeme.ToToken(input, this.keywords[lexeme.ToString()]);
            }
            
            return lexeme.ToToken(input, TokenType.Id);
        } 
        
        //Number Constant
        if (char.IsDigit(currentChar))
        {
            lexeme.Append(currentChar);
            currentChar = PeekNextChar();
            while (char.IsDigit(currentChar))
            {
                currentChar = GetNextChar();
                lexeme.Append(currentChar);
                currentChar = PeekNextChar();
            }
            if (currentChar == 'd')
            {
                return lexeme.ToToken(input, TokenType.DateConstant);
            }
            else if (currentChar == 'M')
            {
                return lexeme.ToToken(input, TokenType.MonthConstant);
            }
            else if (currentChar == 'Y')
            {
                return lexeme.ToToken(input, TokenType.YearConstant);
            }
            else if (currentChar == 'h')
            {
                return lexeme.ToToken(input, TokenType.HourConstant);
            }
            else if (currentChar == 'm')
            {
                return lexeme.ToToken(input, TokenType.MinuteConstant);
            }
            else if (currentChar == 's')
            {
                return lexeme.ToToken(input, TokenType.SecondConstant);
            }
            return lexeme.ToToken(input, TokenType.NumberConstant);
        }  
                
         //operadores
         switch(currentChar)
         {
            case '+':
                lexeme.Append(currentChar);
                return lexeme.ToToken(input, TokenType.PlusOperator);
            case '-':
                lexeme.Append(currentChar);
                return lexeme.ToToken(input, TokenType.MinusOperator);
            case ':':
                lexeme.Append(currentChar);
                return lexeme.ToToken(input, TokenType.ColonOperator);
            case '(':
                lexeme.Append(currentChar);
                return lexeme.ToToken(input, TokenType.OpenParenthesis);
            case ')':
                lexeme.Append(currentChar);
                return lexeme.ToToken(input, TokenType.CloseParenthesis);
            case ';':
                lexeme.Append(currentChar);
                return lexeme.ToToken(input, TokenType.SemicolonOperator);
            case '/':
                lexeme.Append(currentChar);
                return lexeme.ToToken(input, TokenType.SlashOperator);
            case '<':
                lexeme.Append(currentChar);
                currentChar = PeekNextChar();
                if (currentChar == '=')
                {
                    GetNextChar();
                    lexeme.Append(currentChar);
                    return lexeme.ToToken(input, TokenType.AssignationOperator);
                }
                break;
            case '\0':
                lexeme.Append(currentChar);
                return lexeme.ToToken(input, TokenType.EOF);
         }
        
        throw new ApplicationException(
            $"Lexema {lexeme} invalido en la fila {input.Position.Line}, columna {input.Position.Column}");
    }
    
    private char GetNextChar() //Obtener el siguiente char y mover apuntador
    {
        var next = input.NextChar();
        input = next.Reminder;
        return next.Value;
    }

    private char PeekNextChar() //Obtener siguiente char sin mover apuntador
    {
        var next = input.NextChar();
        return next.Value;
    }
}