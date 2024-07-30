using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Compiladores.US.SyntaxAnalyzer
{
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum NodeType
    {
        Program,
        Identifier,
        IntegerLiteral,
        FloatLiteral,
        StringLiteral,
        ExpressionStatement,
        Literal,
        BinaryExpression,
        Empty,
        AssignmentExpression,
    }
}
