namespace Compiladores.US
{
    internal class Token
    {
        public Token(int start, Symbol symbol = null)
        {
            Symbol = symbol ?? new Symbol("EXPR", "");
            Length = Symbol.Length;
            StartIndex = start;
        }

        /// <summary>
        /// Obtiene el simbolo terminal asociado.
        /// </summary>
        public Symbol Symbol { get; }

        /// <summary>
        ///  Obtiene la longitud del token.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        ///  Obtiene el nombre de este token.
        /// </summary>
        public string Name => Symbol.Name ?? "--";

        /// <summary>
        ///  Obtiene el índice inicial dentro de la oración.
        /// </summary>
        public int StartIndex { get; }

        public void UpdateLength(int length)
        {
            Length = length;
        }
    }
}
