namespace CalculatorCompiler.Core
{
    public enum TokenType
    {
        EOF,
        Id,
        PrintKeyword,
        NumberConstant,
        DateConstant,
        MonthConstant,
        YearConstant,
        HourConstant,
        MinuteConstant,
        SecondConstant,
        PlusOperator,
        MinusOperator,
        ColonOperator,
        OpenParenthesis,
        CloseParenthesis,
        SemicolonOperator,
        SlashOperator,
        AssignationOperator
    }
}