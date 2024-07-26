using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
                // Identificadores
                new Symbol("ID", "[_a-zA-Z][_a-zA-Z0-9]*"),

                // Datos compuestos
                new Symbol("FLOAT", "{DIGIT}+(.{DIGIT}+)?", true, new string[] { "DOT", "NUMBER" }),
                new Symbol("NUMBER", "{DIGIT}+", true, new string[] { "DOT", "NUMBER" }),
                
                // Datos primitivos
                new Symbol("DIGIT", "[0-9]", true, new string[] { "DOT", "NUMBER" }),
                new Symbol("CARACTER", "[a-zA-Z]", true, new string[] { "NUMBER", "CARACTER" }),
                
                // Operadores
                new Symbol("EQUAL", "=", isRegularExpression: false),
                new Symbol("PLUS", "+", isRegularExpression: false) { IsOperator = true },
                new Symbol("MINUS", "-", isRegularExpression: false) { IsOperator = true },
                new Symbol("MULTIPLY", "*", isRegularExpression: false) { IsOperator = true },
                new Symbol("DIVIDE", "/", isRegularExpression: false) { IsOperator = true },
                new Symbol("DOT", ".", isRegularExpression: false) { IsOperator = true },
                
                // Parentesis
                new Symbol("PAR_OPEN", "(", isRegularExpression: false) { IsOpen = true },
                new Symbol("PAR_CLOSE", ")", isRegularExpression: false) { IsClose = true },

                // Terminadores
                new Symbol("WHITESPACE", " ", isRegularExpression: false) { IsWhiteSpace = true },
                new Symbol("NEW_LINE", "{CR}{LF}", isRegularExpression: false),
                new Symbol("LF", "\n", isRegularExpression: false) { IsWhiteSpace = true },
                new Symbol("CR", "\r", isRegularExpression: false, new string[] { "LF" }) { IsWhiteSpace = true },
                new Symbol("EOF", "\0", isRegularExpression: false),
            };
        }

        /// <summary>
        /// Devuelve la lista de simbolos.
        /// </summary>
        /// <remarks>
        /// Los simbolos son definidos referenciando a otros simbolos, por lo que este
        /// método expande los simbolos. Ejemplo: {DIGIT}+ se expande a [0-9]+.
        /// </remarks>
        /// <returns></returns>
        internal List<Symbol> GetSymbols()
        {
            var symbols = new List<Symbol>();

            foreach (var symbol in _grammar)
            {
                var value = symbol.Value;

                foreach (var other in _grammar)
                {
                    value = value.Replace("{" + other.Name + "}", other.Value);
                }

                symbols.Add(new Symbol(
                    symbol.Name,
                    value,
                    symbol.IsRegularExpression,
                    symbol.Others));
            }

            return symbols;
        }

        /// <summary>
        ///  Crea los tokens sin contexto de acuerdo al texto de entrada.
        /// </summary>
        /// <param name="sourceCode">Código fuente.</param>
        /// <returns>Lista de tokens.</returns>
        internal IEnumerable<Token> GetLexemas(SourceCode sourceCode)
        {
            // Obtenemos la lista de simbolos disponibles.
            var symbols = GetSymbols();

            // Cuando detectamos un simbolo, almacenamos el cursor donde fue detectado.
            // Esta posición es reiniciada cuando se detecta un simbolo distinto.
            var startIndex = 0;

            // Almacena el simbolo actual, este se va acumulando hasta que se detecte un simbolo distinto.
            Symbol currentSymbol = new Symbol();

            // Recorremos el código fuente hasta que se termine.
            while (!sourceCode.IsEOF())
            {
                // Indica si al final el texto acumulado es válido dentro de la lista de simbolos.
                var tokenFound = false;

                // Recorremos cada elemento del vocabulario.
                foreach (var symbol in symbols)
                {
                    // Evaluaremos el texto acumulado con el simbolo actual.
                    var accText = sourceCode.Extract(startIndex, sourceCode.Position - startIndex + 1);
                    var isMatch = symbol.IsMatch(accText);

                    Console.WriteLine("-> \"{0}\" ({1})", accText, symbol.Name);

                    if (!isMatch)
                    {
                        // Si el texto acumulado no coincide con el simbolo actual, entonces evaluaremos un solo carácter.
                        // ya que este puede ser un simbolo terminados como un espacio o un salto de línea.
                        accText = sourceCode.Extract(sourceCode.Position, 1);
                        Console.WriteLine("?? \"{0}\" ({1})", accText, symbol.Name);

                        if (!symbol.IsMatch(accText))
                        {
                            // No se encontró un simbolo, continuamos con el siguiente.
                            continue;
                        }
                    }

                    // Si no existe un simbolo en la primera vez, se asigna este.
                    // La primera vez que se recorre la lista de símbolos, este no se ha definido.
                    if (currentSymbol.Name == string.Empty)
                    {
                        // Inicializamos los datos del simbolo.
                        currentSymbol = symbol;
                    }

                    // Este simbolo tiene una coincidencia con el código, hay que verificar si pertenece
                    // al mismo simbolo que estamos evaluando o este otro que lo termine.
                    // ? Verificar que el simbolo actual coincida con el simbolo acumulado.
                    if (currentSymbol.Name == symbol.Name)
                    {
                        // Este simbolo ha coincidido con el actual. Mantenemos el caracter inicial.
                        Console.Write("--");
                        tokenFound = true;
                        sourceCode.Move();

                        break;
                    }

                    // Este es un nuevo símbolo, debemos devolver el actual y asignar el contador.
                    Console.Write("**");
                    yield return new Token(startIndex, sourceCode.Position, currentSymbol);

                    // Definimos el nuevo caracter inicial.
                    currentSymbol = symbol;
                    startIndex = sourceCode.Position;
                    tokenFound = true;
                    sourceCode.Move();
                    break;
                }

                // Verificamos si queda pendiente un token
                // En este punto no existen carácteres que puedan ser evaluados, así que el último (activo)
                // es el que se devolverá.
                if (currentSymbol.Name != string.Empty && sourceCode.IsEOF())
                {
                    yield return new Token(startIndex, sourceCode.Position, currentSymbol);
                }

                if (!tokenFound)
                {
                    throw new Exception($"Token no reconocido en la posición {sourceCode.Position}: {sourceCode.Current}");
                }
            }

        }
    }
}
