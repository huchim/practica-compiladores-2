namespace Compiladores.US.LexicalAnalyzer
{
    /// <summary>
    /// Representa la posición dentro de un código fuente.
    /// </summary>
    internal class SourcePosition
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SourcePosition"/>.
        /// </summary>
        /// <param name="start">Posición inicial.</param>
        /// <param name="length">Longitud del código.</param>
        public SourcePosition(int start, int length)
        {
            Length = length;
            StartIndex = start;
        }

        /// <summary>
        /// Obtiene la longitud del código.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Obtiene la posición inicial.
        /// </summary>
        public int StartIndex { get; }
    }
}
