using Compiladores.US.LexicalAnalyzer;

namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class FloatLiteral : Literal
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="FloatLiteral"/>.
        /// </summary>
        public FloatLiteral(Token token) : base(token, NodeType.FloatLiteral)
        {
            // Verificar que el token sea un literal entero.
            if (token.Category.TokenType != TokenType.TypeFloat)
            {
                throw new System.Exception("Unexpected token");
            }

            // Intentar convertir el valor del token a un entero.
            if (!float.TryParse(Raw, out var value))
            {
                throw new System.Exception("Valor no esperado.");
            }

            Value = value;
        }

        /// <summary>
        /// Obtiene o establece el valor del literal numérico.
        /// </summary>
        public float Value { get; }
    }
}
