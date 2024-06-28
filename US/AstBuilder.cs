using System.Collections.Generic;

namespace Compiladores.US
{
    /// <summary>
    /// Encargado de crear el árbol sintáctico abstracto.
    /// </summary>
    internal class AstBuilder
    {
        internal ASTRoot Build(IEnumerable<Token> tokens)
        {
            var root = new ASTRoot();
            ASTNode activeRoot = root;

            foreach (var token in tokens)
            {
                if (!activeRoot.Token.Symbol.PreserveWhiteSpace && token.Symbol.IsWhiteSpace)
                {
                    // Se ignora el espacio en blanco.
                    continue;
                }

                if (token.Symbol.IsOpen)
                {
                    // Este token debe ser creado un nivel más abajo, del nodo activo.
                    // Se crea un nuevo nodo que servirá como raíz para la expresión.
                    var newRoot = new ASTExpresionNode(activeRoot, new Token(token.StartIndex));
                    activeRoot.Nodes.Add(newRoot);

                    // Se actualiza el nodo activo.
                    activeRoot = newRoot;
                }

                activeRoot.Nodes.Add(new ASTNode(activeRoot, token));

                if (token.Symbol.IsClose)
                {
                    // Este token debe ser creado en el nodo padre.
                    activeRoot = activeRoot.Parent ?? root;
                }
            }

            return root;
        }
    }
}
