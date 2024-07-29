using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores.US.Grammar
{
    /// <summary>
    /// Representa el proveedor de la gramática.
    /// </summary>
    /// <remarks>
    /// Aquí se deine las reglas sintáctias del lenguaje.
    /// </remarks>
    internal class GrammarProvider
    {
        private readonly IDictionary<string, List<SymbolDefinition>> _grammar;

        public GrammarProvider()
        {
            _grammar = new Dictionary<string, List<SymbolDefinition>>()
            {
                ["NUMBER"] = new List<SymbolDefinition>() {
                    new SymbolDefinition(new [] { TokenType.TypeFloat }),
                    new SymbolDefinition(new [] { TokenType.TypeInt }),
                    new SymbolDefinition(new [] { TokenType.Number }),
                },

                ["VAR"] = new List<SymbolDefinition>()
                {
                    new SymbolDefinition(new [] { TokenType.Identifier }),
                },

                // Terminales
                ["WHILE"] = new List<SymbolDefinition>()
                {
                    new SymbolDefinition(new [] { TokenType.KeywordWhile }),
                },

                ["ID"] = new List<SymbolDefinition>()
                {
                    new SymbolDefinition(new [] { TokenType.Identifier }),
                },

                // No terminales
                ["WHILE_STM"] = new List<SymbolDefinition>()
                {
                    new SymbolDefinition(new [] { TokenType.GreaterThan, TokenType.Identifier, TokenType.Assignment, TokenType.TypeInt }),
                    new SymbolDefinition(new [] { TokenType.Var, TokenType.Identifier, TokenType.Assignment, TokenType.TypeFloat }),
                    new SymbolDefinition(new [] { TokenType.Var, TokenType.Identifier, TokenType.Assignment, TokenType.TypeChar }),
                },
            };
        }
    }
}
