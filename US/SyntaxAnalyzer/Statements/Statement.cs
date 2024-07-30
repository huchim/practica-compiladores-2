namespace Compiladores.US.SyntaxAnalyzer.Statements
{
    internal class Statement : Node
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Statement"/>.
        /// </summary>
        /// <param name="kind">Tipo de nodo.</param>
        public Statement(NodeType kind) : base(kind)
        {
        }
    }
}
