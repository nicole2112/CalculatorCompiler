// See https://aka.ms/new-console-template for more information

using CalculatorCompiler.Core;
using CalculatorCompiler.Lexer;

var code = File.ReadAllText("Code.txt").Replace(Environment.NewLine, "\n").Trim(new char[] {'\uFEFF', '\u200B'}); 
var input = new Input(code);
var scanner = new Scanner(input); 
var token = scanner.GetNextToken();
while (token.TokenType != TokenType.EOF)
{
    Console.WriteLine(token);
    token = scanner.GetNextToken();
}