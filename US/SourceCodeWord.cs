using System.Text;

namespace Compiladores.US
{
    internal class SourceCodeWord
    {
        public SourceCodeWord(string value, int position)
        {
            Value = value;
            TokenPosition = new TokenPosition(position, value.Length);
        }

        public string Value { get; }

        public TokenPosition TokenPosition { get; }
    }
}
