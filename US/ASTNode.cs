using System.Collections.Generic;
using System.Diagnostics;

namespace Compiladores.US
{
    [DebuggerDisplay("{Token.Name}")]
    internal class ASTNode
    {
        public ASTNode(ASTNode parent, Token token)
        {
            Nodes = new List<ASTNode>();
            Parent = parent;
            Token = token;
        }

        /// <summary>
        /// Lista de nodos hijos.
        /// </summary>
        public List<ASTNode> Nodes { get; }

        public ASTNode Parent { get; }

        public Token Token { get; }
    }
}
