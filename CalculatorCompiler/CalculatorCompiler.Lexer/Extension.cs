using System.Text;
using CalculatorCompiler.Core;

namespace CalculatorCompiler.Lexer;

internal static class Extensions
{
    public static Token ToToken(this StringBuilder lexeme, Input input, TokenType type) =>
        new Token
        {
            TokenType = type,
            Column = input.Position.Column,
            Line = input.Position.Line,
            Lexeme = lexeme.ToString()
        };
}