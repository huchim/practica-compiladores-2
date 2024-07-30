using System;

namespace Compiladores.US.LexicalAnalyzer
{
    internal class Token
    {
        public Token(TokenDefinition tokenDefinition, SourceLocation word) : this(word.Position.StartIndex, word.Position.Length, tokenDefinition, word)
        {

        }

        [Obsolete("Utilizar el constructor con SourceCodeWord")]
        public Token(int start, int endIndex, TokenDefinition symbol = null, SourceLocation lexema = null)
        {
            Symbol = symbol;
            Lexema = lexema;
            Length = endIndex - start;
            StartIndex = start;
        }

        public Token(int start, TokenDefinition symbol = null)
        {
            Symbol = symbol;
            Length = 5;
            StartIndex = start;
        }

        /// <summary>
        /// Obtiene el simbolo terminal asociado.
        /// </summary>
        public TokenDefinition Symbol { get; }
        public SourceLocation Lexema { get; }

        /// <summary>
        ///  Obtiene la longitud del token.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        ///  Obtiene el nombre de este token.
        /// </summary>
        public string Name => Symbol.TokenType.ToString();

        /// <summary>
        ///  Obtiene el índice inicial dentro de la oración.
        /// </summary>
        public int StartIndex { get; }

        public void UpdateLength(int length)
        {
            Length = length;
        }
    }
}
