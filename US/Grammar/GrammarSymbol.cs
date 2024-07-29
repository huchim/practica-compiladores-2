using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.US.Grammar
{
    /// <summary>
    /// Representa una regla de la gramática para un símbolo no terminal.
    /// </summary>
    internal class GrammarSymbol
    {
        /// <summary>
        /// Lista de tokens que componen la regla.
        /// </summary>
        /// <remarks>
        /// Todos los tokens deben ser contiguos.
        /// </remarks>
        private readonly List<TokenType> _tokenTypes;

        public GrammarSymbol(List<TokenType> tokenTypes)
        {
            _tokenTypes = tokenTypes;
        }


    }
}
