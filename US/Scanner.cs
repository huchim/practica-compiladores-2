using System.Collections.Generic;

namespace Compiladores.US
{
    /// <summary>
    /// Representa un analizador léxico.
    /// </summary>
    internal class Scanner
    {
        /// <summary>
        /// Define el vocabulario de la gramática.
        /// </summary>
        private readonly List<Symbol> _grammar;

        public Scanner()
        {
            // Definimos la lista de tokens que se pueden reconocer.
            _grammar = new List<Symbol>() {

                new Symbol("WHITESPACE", " ") { IsWhiteSpace = true },
                new Symbol("DOT", "."),
                new Symbol("NUMBER", "[0-9]+", true, new string[] { "DOT", "NUMBER" }),
                new Symbol("PAR_OPEN", "(") { IsOpen = true },
                new Symbol("PAR_CLOSE", ")") { IsClose = true },
                
                // Operaciones permitidas
                new Symbol("PLUS", "+") { IsOperator = true },
                new Symbol("MINUS", "-") { IsOperator = true },
                new Symbol("MULTIPLY", "*") { IsOperator = true },
                new Symbol("DIVIDE", "/") { IsOperator = true },

                // Sin usar, solo de ejemplo.
                new Symbol("PRINT", "print"),
                new Symbol("COMMA", ","),
                new Symbol("EQUAL", "="),
                new Symbol("TERMINATOR", ";"),
                new Symbol("CARACTER", "[a-zA-Z]+", true, new string[] { "NUMBER", "CARACTER" }),
                new Symbol("LF", "\n") { IsWhiteSpace = true },
                new Symbol("CR", "\r", false, new string[] { "LF" }) { IsWhiteSpace = true },
            };
        }

        /// <summary>
        ///  Crea los tokens sin contexto de acuerdo al texto de entrada.
        /// </summary>
        /// <param name="sourceCode">Código fuente.</param>
        /// <returns>Lista de tokens.</returns>
        internal IEnumerable<Token> GetLexemas(SourceCode sourceCode)
        {
            // Recorremos el código fuente hasta que se termine.
            while (!sourceCode.IsEOF())
            {
                // Marca si se encontró la palabra o la palabra no forma parte del vocabulario.
                var tokenFound = false;

                // Recorremos cada elemento del vocabulario.
                foreach (var gr in _grammar)
                {
                    if (!gr.IsMatch(sourceCode))
                    {
                        continue;
                    }

                    var token = new Token(sourceCode.Position, gr);

                    // Movemos el cursor a donde finaliza este token.
                    // El cursor es la posición del caracter que estamos
                    // evaluando.
                    sourceCode.Move(token.Length);

                    tokenFound = true;
                    yield return token;

                    // Como las palabras están ordenadas y las primeras
                    // tienen más prioridad que las últimas y ya encontramos
                    // un token, vamos a reiniciar la búsqueda de tokens.
                    break;
                }

                // Los espacios en blanco son ignorados.
                if (!tokenFound)
                {
                    // Si la posición no tiene algún token, entonces arrojamos una excepción.
                    throw new System.Exception($"Token no reconocido en la posición {sourceCode.Position}");
                }
            }

        }
    }
}
