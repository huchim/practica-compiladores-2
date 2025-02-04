﻿using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Compiladores.US.LexicalAnalyzer
{
    [JsonConverter(typeof(StringEnumConverter))]
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

        /// <summary>
        /// VAR
        /// </summary>
        KeywordSet,
        KeywordConst,
        KeywordIn,
        KeywordAs,

        /// <summary>
        /// ID
        /// </summary>
        Identifier,

        /// <summary>
        /// FLOAT
        /// </summary>
        Float,

        /// <summary>
        /// INTEGER
        /// </summary>
        Integer,

        /// <summary>
        /// CHAR
        /// </summary>
        TypeChar,
        Comma,

        /// <summary>
        /// SEMI
        /// </summary>
        Semicolon,

        Assignment,
        GreaterThan,
        LessThan,
        Not,
        And,
        Or,

        OperatorPlus,
        OperatorMinus,

        /// <summary>
        /// MUL
        /// </summary>
        OperatorMultiply,
        
        /// <summary>
        /// DIV
        /// </summary>
        OperatorDivide,

        /// <summary>
        /// LPAREN
        /// </summary>
        OpenParenthesis,

        /// <summary>
        /// RPAREN
        /// </summary>
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
        EOF,
        KeywordType,

        /// <summary>
        /// TYPE
        /// </summary>
        Type,
    }
}
