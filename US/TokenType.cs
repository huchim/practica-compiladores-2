namespace Compiladores.US
{
    internal enum TokenType
    {
        Keyword,

        KeywordClass,
        KeywordIf,
        KeywordElse,
        KeywordFor,
        KeywordForeach,
        KeywordWhile,
        KeywordFn,
        KeywordReturn,
        Var,
        KeywordConst,
        KeywordIn,
        KeywordAs,

        Identifier,
        TypeFloat,
        TypeInt,
        TypeChar,
        Comma,
        Semicolon,

        Assignment,
        GreaterThan,
        LessThan,
        Not,
        And,
        Or,

        OperatorPlus,
        OperatorMinus,
        OperatorMultiply,
        OperatorDivide,

        OpenParenthesis,
        CloseParenthesis,
        OpenBrace,
        CloseBrace,
        OpenBracket,
        CloseBracket,

        Space,
        Tab,
        LineBreak,
        Number,
        Character,
    }
}
