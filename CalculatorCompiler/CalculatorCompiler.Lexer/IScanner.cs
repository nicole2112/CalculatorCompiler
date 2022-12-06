using CalculatorCompiler.Core;

namespace CalculatorCompiler.Lexer;

public interface IScanner
{
    Token GetNextToken();
}