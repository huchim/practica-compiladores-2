using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Compiladores.US
{
    /// <summary>
    /// Representa un analizador léxico.
    /// </summary>
    internal class Scanner
    {
        /// <summary>
        /// Define los patrones que deben incluir cada token.
        /// </summary>
        private readonly List<TokenDefinition> _tokenDefinitions;

        public Scanner()
        {
            // Cada token se compone de un lexema y un tipo de token: 1|NUMBER, +|OPERATOR, +|PLUS.
            _tokenDefinitions = new List<TokenDefinition>() {
                // Palabras reservadas
                TokenDefinition.Factory.Create(TokenType.KeywordIf, "^if$"),
                TokenDefinition.Factory.Create(TokenType.KeywordElse, "^else$"),
                TokenDefinition.Factory.Create(TokenType.KeywordFor, "^for$"),
                TokenDefinition.Factory.Create(TokenType.KeywordForeach, "^foreach$"),
                TokenDefinition.Factory.Create(TokenType.KeywordWhile, "^while$"),
                TokenDefinition.Factory.Create(TokenType.KeywordFn, "^fn$"),
                TokenDefinition.Factory.Create(TokenType.KeywordReturn, "^return$"),
                TokenDefinition.Factory.Create(TokenType.Var, "^var$"),
                TokenDefinition.Factory.Create(TokenType.KeywordConst, "^const$"),
                TokenDefinition.Factory.Create(TokenType.KeywordIn, "^in$"),
                TokenDefinition.Factory.Create(TokenType.KeywordAs, "^as$"),
                TokenDefinition.Factory.Create(TokenType.KeywordClass, "^class$"),

                // Otras palabras reservadas
                TokenDefinition.Factory.Create(TokenType.Keyword, "^(false|true)$"),
                TokenDefinition.Factory.Create(TokenType.Keyword, "^(float|int|string|bool)$"),

                TokenDefinition.Factory.Create(TokenType.Identifier, "^[_a-zA-Z][_a-zA-Z0-9]*$"),
                TokenDefinition.Factory.Create(TokenType.TypeFloat, "^[0-9]+.[0-9]+$"),
                TokenDefinition.Factory.Create(TokenType.TypeInt, "^[0-9]+$"),
                TokenDefinition.Factory.Create(TokenType.Number, "^[0-9]$"),
                TokenDefinition.Factory.Create(TokenType.Character, "^[a-zA-Z]$"),

                TokenDefinition.Factory.Create(TokenType.Assignment, "^=$"),
                TokenDefinition.Factory.Create(TokenType.GreaterThan, "^>$"),
                TokenDefinition.Factory.Create(TokenType.LessThan, "^<$"),
                TokenDefinition.Factory.Create(TokenType.Not, "^!$"),
                TokenDefinition.Factory.Create(TokenType.And, "^&&$"),
                TokenDefinition.Factory.Create(TokenType.Or, "^\\|\\|$"),

                TokenDefinition.Factory.Create(TokenType.Comma, "^,$"),
                TokenDefinition.Factory.Create(TokenType.Semicolon, "^;$"),
                TokenDefinition.Factory.Create(TokenType.OperatorPlus, "^\\+$"),
                TokenDefinition.Factory.Create(TokenType.OperatorMinus, "^-$"),
                TokenDefinition.Factory.Create(TokenType.OperatorMultiply, "^\\*$"),
                TokenDefinition.Factory.Create(TokenType.OperatorDivide, "^/$"),
                TokenDefinition.Factory.Create(TokenType.OpenParenthesis, "^\\($"),
                TokenDefinition.Factory.Create(TokenType.CloseParenthesis, "^\\)$"),
                TokenDefinition.Factory.Create(TokenType.OpenBrace, "^\\{$"),
                TokenDefinition.Factory.Create(TokenType.CloseBrace, "^\\}$"),
                TokenDefinition.Factory.Create(TokenType.OpenBracket, "^\\[$"),
                TokenDefinition.Factory.Create(TokenType.CloseBracket, "^\\]$"),
            };
        }

        /// <summary>
        ///  Crea los tokens sin contexto de acuerdo al texto de entrada.
        /// </summary>
        /// <param name="sourceCode">Código fuente.</param>
        /// <returns>Lista de tokens.</returns>
        internal IEnumerable<Token> Tokenize(SourceCode sourceCode)
        {
            // Recorremos el código fuente hasta que se termine.
            Console.WriteLine("Recorriendo cada lexema del código fuente.");
            foreach (var lexema in sourceCode.GetLexemas())
            {
                // Indica si al final el texto acumulado es válido dentro de la lista de simbolos.
                var tokenFound = false;

                // Recorremos cada elemento del vocabulario.
                foreach (var symbol in _tokenDefinitions)
                {
                    if (!symbol.Regex.IsMatch(lexema.Value))
                    {
                        // No se encontró un simbolo, continuamos con el siguiente.
                        continue;
                    }

                    // Si se encontró
                    tokenFound = true;
                    yield return new Token(
                        lexema.TokenPosition.StartIndex,
                        lexema.TokenPosition.StartIndex + lexema.TokenPosition.Length,
                        symbol);

                    break;
                }

                if (!tokenFound)
                {
                    throw new Exception($"Token no reconocido");
                }
            }
        }
    }
}
