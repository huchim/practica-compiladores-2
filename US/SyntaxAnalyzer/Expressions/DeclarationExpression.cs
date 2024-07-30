using Compiladores.US.LexicalAnalyzer;

namespace Compiladores.US.SyntaxAnalyzer.Expressions
{
    internal class DeclarationExpression : Expression
    {
        public DeclarationExpression() : base(NodeType.DeclarationExpression)
        {
        }

        public Identifier Identifier { get; set; }

        public string Name { get; set; }
    }
}
