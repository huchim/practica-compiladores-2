using Compiladores.US.LexicalAnalyzer;

namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class Identifier : Expression
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Identifier"/>.
        /// </summary>
        /// <param name="token">Token que representa el identificador.</param>
        public Identifier(Token token) : base(NodeType.Identifier)
        {
            Name = token.Lexema.Value;
        }

        public string Name { get; }
    }
}
