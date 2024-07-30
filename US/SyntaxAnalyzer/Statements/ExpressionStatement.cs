namespace Compiladores.US.SyntaxAnalyzer.Statements
{
    internal class ExpressionStatement : Statement
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ExpressionStatement"/>.
        /// </summary>
        public ExpressionStatement() : base(NodeType.ExpressionStatement)
        {
        }

        public Expressions.Expression Expression { get; set; }
    }
}
