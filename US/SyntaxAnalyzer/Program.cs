using Compiladores.US.SyntaxAnalyzer.Statements;
using System.Collections.Generic;

namespace Compiladores.US.SyntaxAnalyzer
{
    internal class Program : Node
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Program"/>.
        /// </summary>
        public Program() : base(NodeType.Program)
        {
        }

        /// <summary>
        /// Obtiene o establece el cuerpo del programa.
        /// </summary>
        public List<Node> Body { get; set; } = new List<Node>();
    }
}
