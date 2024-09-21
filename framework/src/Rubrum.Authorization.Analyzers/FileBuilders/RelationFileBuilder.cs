using System.Text;
using Microsoft.CodeAnalysis.Text;
using Rubrum.Authorization.Analyzers.Helpers;
using Rubrum.Authorization.Analyzers.Models;

namespace Rubrum.Authorization.Analyzers.FileBuilders;

public sealed class RelationFileBuilder : IDisposable
{
    private readonly StringBuilder _sb;
    private readonly CodeWriter _writer;

    public RelationFileBuilder()
    {
        _sb = new StringBuilder();
        _writer = new CodeWriter(_sb);
    }

    public void WriteBeginClass(RelationInfo relation)
    {
        var name = relation.PropertyName;
        var className = relation.ClassName;

        var types = string.Join(", ", relation.Values.Select(v => $"typeof({v.ToDisplayString()})"));

        _writer.WriteIndentedLine(
            "public sealed class {0}() : Relation(\"{1}\", {2})",
            className,
            name,
            types);
        _writer.WriteIndentedLine("{");
        _writer.IncreaseIndent();
    }

    public void WriteProperty(string propertyName, string type)
    {
        _writer.WriteIndentedLine(
            "public RelationProperty {0} {{ get; }} = new RelationProperty(\"{0}\", typeof({1}));",
            propertyName,
            type);
    }

    public void WriteEndClass()
    {
        _writer.DecreaseIndent();
        _writer.WriteIndentedLine("}");
    }

    public override string ToString()
        => _sb.ToString();

    public SourceText ToSourceText()
        => SourceText.From(ToString(), Encoding.UTF8);

    public void Dispose()
    {
        _writer.Dispose();
    }
}
