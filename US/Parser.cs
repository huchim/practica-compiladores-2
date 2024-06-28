using System.Collections.Generic;
using System.Linq;

namespace Compiladores.US
{
    internal class Parser
    {
        /// <summary>
        /// Devuelve todos los tokens contiguos en un solo token.
        /// </summary>
        /// <param name="lexemas">Tokens.</param>
        /// <returns></returns>
        internal IEnumerable<Token> Tokenize(IEnumerable<Token> lexemas)
        {
            Token lastToken = null;

            foreach (var token in lexemas)
            {
                // Si este es el primer token, entonces lo guardamos.
                if (lastToken == null)
                {
                    lastToken = token;
                    continue;
                }

                // Si este token es del mismo tipo que el último, actualizamos la longitud del token.
                if (lastToken.Symbol.Others.Contains(token.Name))
                {
                    lastToken.UpdateLength(lastToken.Length + token.Length);
                    continue;
                }

                // Si este token es diferente al último, entonces lo devolvemos.
                yield return lastToken;

                // Actualizamos el último token.
                lastToken = token;
            }

            // Devolvemos el último token.
            yield return lastToken;
        }
    }
}
