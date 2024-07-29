namespace Compiladores.US
{
    internal class TokenPosition
    {
        public TokenPosition(int start, int length)
        {
            Length = length;
            StartIndex = start;
        }

        public int Length { get; private set; }

        public int StartIndex { get; }
    }
}
