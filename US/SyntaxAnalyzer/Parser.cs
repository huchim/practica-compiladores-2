using Compiladores.US.LexicalAnalyzer;
using Compiladores.US.SyntaxAnalyzer.Expressions;
using System.Collections.Generic;

namespace Compiladores.US.SyntaxAnalyzer
{
    /// <summary>
    /// Representa el analizador léxico.
    /// </summary>
    internal class Parser
    {
        private readonly IEnumerator<Token> _tokens;

        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = tokens.GetEnumerator();
        }

        public Program Parse()
        {
            return Program();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// program ::= statement*
        /// </remarks>
        /// <returns></returns>
        private Program Program()
        {
            // Creamos el programa.
            var program = new Program();

            _tokens.MoveNext();

            program.Body.Add(Expr());

            return program;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// factor ::= INTEGER | FLOAT | CHAR | ID | LPAREN expr RPAREN
        /// </remarks>
        /// <param name="tokens"></param>
        /// <param name="exp"></param>
        private Node Factor()
        {
            var token = Peek();

            switch (token.Category.TokenType)
            {
                case TokenType.OpenParenthesis:
                    Eat(TokenType.OpenParenthesis);
                    var node = Expr();
                    Eat(TokenType.CloseParenthesis);

                    return node;
                case TokenType.TypeInteger:
                    Eat(TokenType.TypeInteger);
                    return new IntegerLiteral(token);
                case TokenType.TypeFloat:
                    Eat(TokenType.TypeFloat);
                    return new FloatLiteral(token);
                case TokenType.TypeChar:
                    Eat(TokenType.TypeChar);
                    return new StringLiteral(token);
                case TokenType.Identifier:
                    Eat(TokenType.Identifier);
                    return new Identifier(token);

                default:
                    throw new System.Exception("Unexpected token");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// term : factor ((MUL | DIV) factor)*
        /// </remarks>
        /// <returns></returns>
        private Node Term()
        {
            var node = Factor();

            while (Peek().Category.TokenType == TokenType.OperatorMultiply || Peek().Category.TokenType == TokenType.OperatorDivide)
            {
                var token = Pop();
                var right = Factor();

                node = new BinaryExpression()
                {
                    Left = node as Expression,
                    Operator = token.Category.TokenType,
                    Right = right as Expression,
                };
            }

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// expr   : term ((PLUS | MINUS) term)*
        /// term   : factor((MUL | DIV) factor)*
        /// factor : INTEGER | LPAREN expr RPAREN
        /// </remarks>
        /// <returns></returns>
        private Node Expr()
        {
            var node = Term();

            while (Peek().Category.TokenType == TokenType.OperatorPlus || Peek().Category.TokenType == TokenType.OperatorMinus)
            {
                var token = Pop();
                var right = Term();

                node = new BinaryExpression()
                {
                    Left = node as Expression,
                    Operator = token.Category.TokenType,
                    Right = right as Expression,
                };
            }

            return node;
        }

        /// <summary>
        /// Verifica que el token actual sea del tipo especificado y avanza el enumerador.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <exception cref="System.Exception"></exception>
        private void Eat(TokenType tokenType)
        {
            var token = Peek();

            if (token.Category.TokenType != tokenType)
            {
                throw new System.Exception($"Unexpected token, expected {tokenType} but {token.Category.TokenType}");
            }

            Pop();
        }

        /// <summary>
        /// Devuelve el siguiente token sin avanzar el enumerador.
        /// </summary>
        /// <returns></returns>
        private Token Peek()
        {
            return _tokens.Current;
        }

        /// <summary>
        /// Devuelve el token actual y avanza el enumerador.
        /// </summary>
        /// <returns>Token actual antes de avanzar.</returns>
        private Token Pop()
        {
            var token = _tokens.Current;

            _tokens.MoveNext();

            return token;
        }
    }
}