﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Compiladores.US.LexicalAnalyzer
{
    /// <summary>
    /// Representa el código fuente que se va a analizar.
    /// </summary>
    internal class SourceCode
    {
        private readonly StreamReader _stream;

        public SourceCode(Stream stream)
        {
            _stream = new StreamReader(stream);
        }

        public static SourceCode CreateFromString(string content)
        {
            // Throw an exception when content is empty.
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("Content is empty.");
            }

            // Convertir el string a un stream.
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

            return new SourceCode(stream);
        }

        internal IEnumerable<Lexema> GetLexemas()
        {
            // Iterar sobre cada carácter del contenido para recuperar todas las palabras,
            // cada palabra se separa por un espacio en blanco o un salto de línea.
            var currentWord = new StringBuilder();

            // Identifica la posición del cursor dentro del documento.
            var position = -1;

            // Una cadena de texto se delimita por comillas dobles.
            // Un ejemplo: "Hola mundo"
            // Donde "Hola mundo" es una cadena de texto y no dos palabras separadas.
            // El patrón indica que si hay una comilla doble, debe de existir otra para finalizar.
            // En algunos casos se puede encontrar "Hola \"Carlos\"", donde hay un carácter de escape.
            // Por lo que se debe verificar si el carácter anterior es un carácter de escape para no
            // considerarlo como un delimitador.
            var stringLiteralIsOpen = false;

            // Los datos están en un Stream, necesitamos leer carácter por carácter.
            while (_stream.Peek() >= 0)
            {
                position++;
                var c = (char)_stream.Read();

                // Verificar si existe una cadena de texto abierta.
                if (IsStringSeparator(c))
                {
                    if (!stringLiteralIsOpen)
                    {
                        // Evaluar los siguientes carácteres como una cadena de texto.
                        stringLiteralIsOpen = true;

                        continue;
                    }

                    // Hay que verificar si el carácter anterior es un carácter de escape.
                    // Si el carácter anterior es un carácter de escape, entonces no se considera como un delimitador.
                    if (IsEscapeChar(currentWord))
                    {
                        // Incluir en la palabra cuando el carácter anterior es un carácter de escape.
                        currentWord.Append(c);
                    }
                    else
                    {
                        // Finalizar la cadena de texto.
                        stringLiteralIsOpen = false;

                        // Hay que devolver la palabra acumulada.
                        yield return new Lexema(currentWord.ToString(), position - currentWord.Length);
                        currentWord.Clear();
                    }

                    continue;
                }

                // Se incluye todos los carácteres dentro de las comillas dobles.
                if (stringLiteralIsOpen)
                {
                    currentWord.Append(c);

                    continue;
                }

                // Este carácter se debe considerar como una sola palabra
                // así que se debe devolver inmediatamente, pero antes hay que vaciar el búfer.
                if (IsSpecialWord(c))
                {
                    // Antes de devolver la palabra, hay que devolver la palabra actual que se estaba acumulando.
                    if (currentWord.Length > 0)
                    {
                        // La posición debe retroceder con respecto al tamaño de la palabra.
                        yield return new Lexema(currentWord.ToString(), position - currentWord.Length);
                    }

                    // Devolvemos la palabra.
                    yield return new Lexema(c.ToString(), position);

                    // Limpiamos el búfer.
                    currentWord.Clear();

                    continue;
                }

                // Se incluye todos los carácteres dentro de las comillas dobles.
                if (!IsWhiteSpace(c))
                {
                    currentWord.Append(c);

                    continue;
                }

                // Este carácter es un espacio en blanco, por lo que se debe devolver la palabra acumulada.
                // Antes de devolver la palabra, hay que devolver la palabra actual que se estaba acumulando.
                if (currentWord.Length > 0)
                {
                    // La posición debe retroceder con respecto al tamaño de la palabra.
                    yield return new Lexema(currentWord.ToString(), position - currentWord.Length);

                    // Limpiamos el búfer.
                    currentWord.Clear();
                }
            }

            // Devolver la última palabra en caso de que no esté vacía.
            if (currentWord.Length > 0)
            {
                yield return new Lexema(currentWord.ToString(), position - currentWord.Length + 1);
            }

            // Lanzar un error si se ha quedado una cadena abierta.
            if (stringLiteralIsOpen)
            {
                throw new Exception("Se esperaba una comilla doble.");
            }
        }

        /// <summary>
        /// Devuelve un valor que indica si el carácter se considera como una palabra.
        /// </summary>
        /// <remarks>Algunas palabras se conforman de un único carácter.</remarks>
        /// <param name="c">Carácter a evaluar.</param>
        /// <returns>Verdadero si el carácter debe ser tratado como una única palabra.</returns>
        private static bool IsSpecialWord(char c)
        {
            // Cuando el texto es igual a 30+5 se podría considerar que no
            // hay más que una sola palabra porque no hay espacios en blanco entre los operandos.
            // En este caso, 30 y 5 son operandos y + es un operador, por lo que el operador
            // se debe considerar como una palabra completa.
            // Otro ejemplo sería: [hola] donde habría 3 palabras: [, hola y ].
            // Tanto ; como la coma, separan palabras y argumentos.
            // Estas carácteres se consideran como palabras.
            var wordChars = new char[] {
                // Agrupadores
                '(', // Paréntesis abierto
                ')', // Paréntesis cerrado
                '[', // Corchete abierto
                ']', // Corchete cerrado
                '{', // Llave abierta
                '}', // Llave cerrada
                // Operadores ariméticos
                '+', // Suma
                '-', // Resta
                '*', // Multiplicación
                '/', // División
                // Operadores de comparación
                '=', // Igual
                '>', // Mayor que
                '<', // Menor que
                '!', // Negación
                // Operadores lógicos
                '&', // Y
                '|', // O
                '^', // XOR
                // Operadores de asignación
                ':', // Asignación,
                ';',
                ',',
            };

            return wordChars.Contains(c);
        }

        private static bool IsEscapeChar(StringBuilder currentWord)
        {
            return currentWord.Length > 0 && currentWord[currentWord.Length - 1] == '\\';
        }

        private static bool IsStringSeparator(char c)
        {
            return c == '"';
        }

        private static bool IsWhiteSpace(char c)
        {
            // Estos carácteres se consideran como separadores de palabras.
            var breakChars = new char[] { '\n', '\r', '\t', ';', ',' };

            return char.IsWhiteSpace(c) || breakChars.Contains(c);
        }
    }
}