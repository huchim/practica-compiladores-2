using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

namespace Compiladores.US
{
    /// <summary>
    /// Representa un símbolo terminal.
    /// </summary>
    /// <remarks>
    /// Algunos morfemas son evaluadas como expresiones regulares
    /// y otras como una cadena fija de texto.
    /// </remarks>
    internal class Symbol
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Symbol"/>.
        /// </summary>
        public Symbol(): this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Symbol"/>.
        /// </summary>
        /// <param name="name">Nombre.</param>
        /// <param name="value">Valor.</param>
        /// <param name="isRegularExpression">Indica si el valor es una expresión regular.</param>
        /// <param name="others">Otros valores.</param>
        public Symbol(
            string name,
            string value,
            bool isRegularExpression = true,
            string[] others = null)
        {
            Name = name;
            Value = value;
            IsRegularExpression = isRegularExpression;
            Others = others ?? new string[0];
        }

        /// <summary>
        /// Obtiene o establece el terminador del token.
        /// </summary>
        public string Terminator { get; set; }

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
        /// Obtiene la longitud de el terminal.
        /// </summary>
        public int Length => IsRegularExpression ? 1 : Value.Length;

        /// <summary>
        /// Obtiene el nombre de el terminal.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Obtiene la lista de otras terminales que pueden estar a la derecha de esta terminal.
        /// </summary>
        public string[] Others { get; }

        /// <summary>
        /// Obtiene el valor de el terminal.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Devuelve un valor que indica si la palabra se encuentra en el texto.
        /// </summary>
        /// <param name="sourceCode">Código fuente.</param>
        /// <returns>Devuelve verdadero si esta terminal se encuentra en el código fuente.</returns>
        public bool IsMatch(string sourceCode)
        {
            return IsRegularExpression ? Regex.IsMatch(sourceCode, $"^{Value}$") : (sourceCode == Value);
        }
    }
}