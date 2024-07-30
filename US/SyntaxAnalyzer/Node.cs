using Compiladores.US.LexicalAnalyzer;

namespace Compiladores.US.SyntaxAnalyzer
{
    internal class Node
    {
        public Node(NodeType kind)
        {
            Kind = kind;
        }

        public static Node Empty => new Node(NodeType.Empty);

        public Lexema Location { get; set; }

        public NodeType Kind { get; }
    }
}
