namespace CalculatorCompiler.Lexer;

public readonly struct Position
{
    public int Absolute { get; }

    public int Line { get; }

    public int Column { get; }

    public Position(int absolute, int line, int column)
    {
        Absolute = absolute;
        Line = line;  
        Column = column;
    }

    public static Position Start => new Position(0, 0, 0); 

    //moverse dentro del string
    public Position MovePointer(char @char) //@ para usar palabra reservada
    {
        return @char == '\n' //si el char es un salto de línea
            ? new Position(Absolute + 1, Line + 1, 1) //avanzar a la siguiente línea, primera columna
            : new Position(Absolute + 1, Line, Column + 1); //avanzar una columna en la misma fila
    }
}