using Compiladores.US.LexicalAnalyzer;

namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class BinaryExpression : Expression
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BinaryExpression"/>.
        /// </summary>
        public BinaryExpression() : base(NodeType.BinaryExpression)
        {
        }

        public Expression Left { get; set; }

        public Expression Right { get; set; }

        public TokenType Operator { get; set; }
    }
}
