using System.Text.RegularExpressions;

namespace Compiladores.US
{
    internal sealed class TokenDefinition
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TokenDefinition"/>.
        /// </summary>
        /// <param name="tokenType">Tipo de token.</param>
        /// <param name="regex">Patrón del token.</param>
        /// <param name="isIgnored">Un valor que indica que el token debe ser ignorado.</param>
        public TokenDefinition(TokenType tokenType, Regex regex, bool isIgnored = false)
        {
            TokenType = tokenType;
            Regex = regex;
            IsIgnored = isIgnored;
        }

        /// <summary>
        /// Obtiene un valor que indica si el token es ignorado.
        /// </summary>
        public bool IsIgnored { get; }

        /// <summary>
        /// Obtiene el patrón regular de este token.
        /// </summary>
        public Regex Regex { get; }

        /// <summary>
        /// Obtiene el tipo de token.
        /// </summary>
        public TokenType TokenType { get; }

        public bool IsOperator { get; set; }

        public bool IsClose { get; set; }

        public bool IsOpen { get; set; }

        public bool IsWhiteSpace { get; set; }

        public bool PreserveWhiteSpace { get; set; }

        /// <summary>
        /// Obtiene un valor que indica si el terminal es una expresión regular.
        /// </summary>
        public bool IsRegularExpression { get; }

        /// <summary>
        /// Obtiene la lista de otras terminales que pueden estar a la derecha de esta terminal.
        /// </summary>
        public string[] Others { get; }

        /// <summary>
        /// Obtiene el nombre de el terminal.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Representa una fábrica de instancias de <see cref="TokenDefinition"/>.
        /// </summary>
        public static class Factory
        {
            /// <summary>
            /// Devuelve una nueva instancia de <see cref="TokenDefinition"/>.
            /// </summary>
            /// <param name="tokenType">Tipo de tokens.</param>
            /// <param name="pattern">Patrón del token.</param>
            /// <param name="isIgnored">Un valor que indica si el token debe ser ignorado.</param>
            /// <returns></returns>
            public static TokenDefinition Create(TokenType tokenType, string pattern, bool isIgnored = false)
            {
                return new TokenDefinition(tokenType, new Regex(pattern, RegexOptions.Compiled), isIgnored);
            }
        }
    }
}