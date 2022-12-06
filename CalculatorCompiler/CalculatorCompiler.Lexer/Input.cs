namespace CalculatorCompiler.Lexer;

public readonly struct Input
{
    public string Source { get; }

    public int Length { get; }

    public Position Position { get; }

    public Input(string source):
        this(source, Position.Start, source.Length)
    {

    }

    public Input(string source, Position position, int length)
    {
        Source = source;
        Position = position;
        Length = length;
    }

    public Result<char> NextChar()
    {
        if(Length == 0)
        {
            return Result.Empty<char>(this);
        }

        var @char = Source[Position.Absolute]; //Obtener char en la pos actual
        return Result.Value(@char, new Input(Source, Position.MovePointer(@char), Length - 1)); //retornar char y avanzar a la siguiente pos
    }
}