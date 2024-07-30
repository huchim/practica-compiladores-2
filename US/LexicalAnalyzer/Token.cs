namespace Compiladores.US.LexicalAnalyzer
{
    /// <summary>
    /// Representa un token que corresponde a un lexema y su clasificación.
    /// </summary>
    internal class Token
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Token"/>.
        /// </summary>
        /// <param name="tokenDefinition">Definición del token.</param>
        /// <param name="word">Ubicación del código.</param>
        public Token(TokenDefinition tokenDefinition, Lexema word)
        {
            Category = tokenDefinition;
            Lexema = word;
        }

        /// <summary>
        /// Obtiene la clasificación del lexema.
        /// </summary>
        public TokenDefinition Category { get; }

        /// <summary>
        /// Obtiene o establece el lexema.
        /// </summary>
        public Lexema Lexema { get; }
    }
}
