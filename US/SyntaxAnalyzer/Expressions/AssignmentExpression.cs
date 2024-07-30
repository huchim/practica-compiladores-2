namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class AssignmentExpression : Expression
    {
        public AssignmentExpression() : base(NodeType.AssignmentExpression)
        {
        }

        public Identifier Left { get; set; }

        public Expression Right { get; set; }
    }
}
