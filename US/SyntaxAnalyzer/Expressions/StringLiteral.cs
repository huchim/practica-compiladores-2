using Compiladores.US.LexicalAnalyzer;

namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class StringLiteral : Literal
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="StringLiteral"/>.
        /// </summary>
        public StringLiteral(Token token) : base(token, NodeType.StringLiteral)
        {
            // Verificar que el token sea un literal de cadena.
            if (token.Category.TokenType != TokenType.TypeChar)
            {
                throw new System.Exception("Unexpected token");
            }

            Value = Raw;
        }

        /// <summary>
        /// Obtiene o establece el valor del literal de cadena.
        /// </summary>
        public string Value { get; }
    }
}
