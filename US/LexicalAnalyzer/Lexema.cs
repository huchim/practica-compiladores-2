namespace Compiladores.US.LexicalAnalyzer
{
    /// <summary>
    /// Representa un lexema en un código fuente.
    /// </summary>
    internal class Lexema
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Lexema"/>.
        /// </summary>
        /// <param name="value">Lexema.</param>
        /// <param name="position">Posición del lexema.</param>
        public Lexema(string value, int position)
        {
            Value = value;
            Position = new SourcePosition(position, value.Length);
        }

        /// <summary>
        /// Obtiene el lexema.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Obtiene la posición del lexema.
        /// </summary>
        public SourcePosition Position { get; }
    }
}
