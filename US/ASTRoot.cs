namespace Compiladores.US
{
    internal class ASTRoot : ASTExpresionNode
    {
        public ASTRoot() : base(null, new Token(0, new Symbol("EXPR", "")))
        {
        }
    }
}
