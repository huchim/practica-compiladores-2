using System;

namespace Compiladores.US.LexicalAnalyzer
{
    /// <summary>
    /// Representa los argumentos del evento <see cref="Lexer.OnTokenFound"/>.
    /// </summary>
    internal class OnTokenFoundEventArgs : EventArgs
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="OnTokenFoundEventArgs"/>.
        /// </summary>
        /// <param name="token">Información del token.</param>
        public OnTokenFoundEventArgs(Token token)
        {
            Token = token;
        }

        /// <summary>
        /// Obtiene la información del token.
        /// </summary>
        public Token Token { get; }
    }
}