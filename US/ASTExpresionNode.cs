using System;
using System.Linq;

namespace Compiladores.US
{
    internal class ASTExpresionNode : ASTNode
    {
        public ASTExpresionNode(ASTNode parent, Token token) : base(parent, token)
        {
        }

        public void ValidateSyntax()
        {
            // Si no hay nodos, no se valida.
            if (Nodes.Count == 0)
            {
                return;
            }

            // Validar la sintaxis de los nodos hijos.
            foreach (var node in Nodes)
            {
                // Si es una instancia de ASTExpresionNode, entonces se debe validar la sintaxis.
                if (node is ASTExpresionNode expr)
                {
                    expr.ValidateSyntax();
                }
            }

            // Si el primer nodo es PAR_OPEN, entonces el último nodo debe ser PAR_CLOSE.
            if (Nodes[0].Token.Symbol.IsOpen && Nodes[Nodes.Count - 1]?.Token?.Symbol?.IsClose == false)
            {
                throw new Exception("Se esperaba un paréntesis de cierre en " + Token.StartIndex);
            }

            // Si el último nodo es PAR_CLOSE, entonces el primer nodo debe ser PAR_OPEN.
            if (Nodes[Nodes.Count - 1]?.Token?.Symbol?.IsClose == true && Nodes[0].Token.Symbol?.IsOpen == false)
            {
                throw new Exception("Se esperaba un paréntesis de apertura en " + Token.StartIndex);
            }

            for (var i = 0; i < Nodes.Count; i++)
            {
                // Si el nodo actual es un operador, no se permite que el siguiente sea un operador.
                if (Nodes[i].Token.Symbol.IsOperator)
                {
                    // Verificar que exista un nodo siguiente.
                    if (i + 1 > Nodes.Count - 1)
                    {
                        continue;
                    }

                    // Verificar si el siguiente nodo es un operador.
                    if (Nodes[i + 1].Token.Symbol.IsOperator)
                    {
                        throw new Exception("Se esperaba un número después del operador en " + Nodes[i + 1].Token.StartIndex);
                    }
                }
            }
        }
    }
}
