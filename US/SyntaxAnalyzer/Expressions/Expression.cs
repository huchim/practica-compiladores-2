namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class Expression : Node
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Expression"/>.
        /// </summary>
        /// <param name="kind">Tipo de nodo.</param>
        public Expression(NodeType kind) : base(kind)
        {
        }
    }
}
