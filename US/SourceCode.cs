using System;
using System.Text.RegularExpressions;

namespace Compiladores.US
{
    /// <summary>
    /// Representa el código fuente que se va a analizar.
    /// </summary>
    internal class SourceCode
    {
        /// <summary>
        ///  Posición actual.
        /// </summary>
        private int _chx;

        /// <summary>
        ///  Inicializa una nueva instancia de la clase <see cref="SourceCode"/>.
        /// </summary>
        /// <param name="code">Código fuente de entrada.</param>
        public SourceCode(string code)
        {
            Code = code;
            _chx = 0;

            // Verificar si el código fuente termina con \0.
            if (code[code.Length - 1] != '\0')
            {
                Code += '\0';
            }
        }

        /// <summary>
        ///  Obtiene el código.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Obtiene el carácter actual.
        /// </summary>
        public char Current => CharAt(_chx);

        /// <summary>
        ///  Obtiene la posición actual del cursor.
        /// </summary>
        internal int Position => _chx;

        /// <summary>
        ///  Extrae una porción del código de acuerdo al token proporcionado.
        /// </summary>
        /// <param name="token">Información del token.</param>
        /// <returns>Una porción del código.</returns>
        internal string Extract(Token token)
        {
            int startIndex = token.StartIndex;

            // Devolver un subconjunto del código, de acuerdo a la posición inicial y la longitud.
            return Code.Substring(startIndex, token.Length);
        }

        /// <summary>
        ///  Extrae una porción del código de acuerdo al token proporcionado.
        /// </summary>
        /// <param name="startIndex">Posición inicial.</param>
        /// <param name="length">Longitud esperada.</param>
        /// <returns>Una porción del código.</returns>
        internal string Extract(int startIndex, int length = 1)
        {
            // Devolver un subconjunto del código, de acuerdo a la posición inicial y la longitud.
            return Code.Substring(startIndex, length);
        }

        /// <summary>
        ///  Mueve el cursor una posición hacia adelante.
        /// </summary>
        internal void Move()
        {
            _chx++;
        }

        /// <summary>
        ///  Mueve el cursor hacia adelante.
        /// </summary>
        /// <param name="positions">Posiciones a mover.</param>
        internal void Move(int positions)
        {
            _chx += positions;
        }

        [Obsolete("Se debe mover la lógica de este elemento.")]
        internal bool Contains(string pattern)
        {
            // Verificar si el código fuente aún tiene suficientes caracteres...
            // Como las expresiones regulares en esta versión únicamente evaluarán un carácter, entonces
            // se verificará sobre 1.
            if (!R(1))
            {
                return false;
            }

            return Regex.IsMatch(CharAt(_chx).ToString(), pattern);
        }

        /// <summary>
        ///  Devuelve un valor que indica si los carácteres son los esperados.
        /// </summary>
        /// <param name="c">Carácteres a comparar.</param>
        /// <returns>Devuelve <see langword="true"/> si coincide.</returns>
        [Obsolete("Se debe mover la lógica de este elemento.")]
        internal bool Contains(char[] c)
        {
            // Verificar si el código fuente aún tiene suficientes caracteres para esta palabra.
            if (!R(c.Length))
            {
                // El texto ya no tiene suficientes caracteres para esta palabra.
                return false;
            }

            // Extraemos la porción del token para poder comparar.
            var val = Code.Substring(_chx, c.Length);

            return val == new string(c);
        }

        /// <summary>
        /// Devuelve un valor que indica si el carácter actual es un espacio en blanco.
        /// </summary>
        /// <returns>Devuelve <see langword="true"/> si el carácter es un espacio en blanco.</returns>
        internal bool IsWhiteSpace()
        {
            return char.IsWhiteSpace(CharAt(_chx));
        }

        /// <summary>
        ///  Devuelve un valor que indica si ha finalizado el documento.
        /// </summary>
        /// <returns>Devuelve <see langword="true"/> si el documento ha finalizado.</returns>
        internal bool IsEOF()
        {
            return CharAt(_chx) == '\0';
        }

        /// <summary>
        ///  Devuelve un valor que indica si aún queda espacio para el siguiente elemento.
        /// </summary>
        /// <param name="v">Cantidad de caracteres</param>
        /// <returns>Devuelve <see langword="true"/> si el documento aún tiene espacio.</returns>
        internal bool R(int v)
        {
            return Code.Length - _chx >= v;
        }

        /// <summary>
        ///  Devuelve el caracter en la posición especificada en <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Posicióne esperada.</param>
        /// <returns>El carácter en la posición esperada.</returns>
        internal char CharAt(int index)
        {
            if (index > Code.Length - 1 || index < 0)
            {
                return '\0';
            }

            return Code[index];
        }
    }
}
