namespace Compiladores.US.LexicalAnalyzer
{
    internal class SourceLocation
    {
        public SourceLocation(string source, int position)
        {
            Source = source;
            Position = new TokenPosition(position, source.Length);
        }

        public string Source { get; }

        public TokenPosition Position { get; }
    }
}
