using Compiladores.US.LexicalAnalyzer;

namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class IntegerLiteral : Literal
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="IntegerLiteral"/>.
        /// </summary>
        public IntegerLiteral(Token token) : base(token, NodeType.IntegerLiteral)
        {
            // Verificar que el token sea un literal entero.
            if (token.Category.TokenType != TokenType.Integer)
            {
                throw new System.Exception("Unexpected token");
            }

            // Intentar convertir el valor del token a un entero.
            if (!int.TryParse(Raw, out var value))
            {
                throw new System.Exception("Valor no esperado.");
            }

            Value = value;
        }

        /// <summary>
        /// Obtiene o establece el valor del literal numérico.
        /// </summary>
        public int Value { get; }
    }
}
