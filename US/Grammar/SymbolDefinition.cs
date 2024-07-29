using System.Collections.Generic;

namespace Compiladores.US.Grammar
{
    /// <summary>
    /// Representa una definición de un símbolo.
    /// </summary>
    internal class SymbolDefinition
    {
        /// <summary>
        /// Reglas que aplican para el token.
        /// </summary>
        private readonly TokenType[] _symbols;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SymbolDefinition"/>.
        /// </summary>
        /// <param name="symbols">Lista de simbolos.</param>
        public SymbolDefinition(TokenType[] symbols)
        {
            _symbols = symbols;
        }
    }
}
