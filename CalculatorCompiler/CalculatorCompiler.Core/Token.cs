namespace CalculatorCompiler.Core;

public class Token
{
    public TokenType TokenType { get; set; }

    public string Lexeme { get; set; } //valor del token

    public int Line { get; set; }

    public int Column { get; set; }

    public override string ToString()
    {
        return $"Lexema: {Lexeme}, Tipo: {TokenType}, Fila: {Line}, Columna: {Column}";
    }
}