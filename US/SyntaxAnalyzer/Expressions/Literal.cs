using Compiladores.US.LexicalAnalyzer;

namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class Literal : Expression
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Literal"/>.
        /// </summary>
        /// <param name="token">Token que representa el literal.</param>
        public Literal(Token token, NodeType nodeType = NodeType.Literal) : base(nodeType)
        {
            Raw = token.Lexema.Source;
        }

        public string Raw { get; }
    }
}
