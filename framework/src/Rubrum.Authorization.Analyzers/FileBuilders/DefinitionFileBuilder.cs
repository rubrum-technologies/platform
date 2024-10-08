﻿using System.Text;
using Microsoft.CodeAnalysis.Text;
using Rubrum.Authorization.Analyzers.Helpers;
using Rubrum.Authorization.Analyzers.Models;

namespace Rubrum.Authorization.Analyzers.FileBuilders;

public sealed class DefinitionFileBuilder : IDisposable
{
    private readonly StringBuilder _sb;
    private readonly CodeWriter _writer;

    public DefinitionFileBuilder()
    {
        _sb = new StringBuilder();
        _writer = new CodeWriter(_sb);
    }

    public void WriteHeader()
    {
        _writer.WriteIndentedLine("// <auto-generated/>");
        _writer.WriteLine();
        _writer.WriteIndentedLine("#nullable enable");
        _writer.WriteIndentedLine("#pragma warning disable");
        _writer.WriteLine();
        _writer.WriteIndentedLine("using System;");
        _writer.WriteIndentedLine("using Rubrum.Authorization.Relations;");
        _writer.WriteLine();
    }

    public void WriteBeginNamespace(string ns)
    {
        _writer.WriteIndentedLine("namespace {0}", ns);
        _writer.WriteIndentedLine("{");
        _writer.IncreaseIndent();
    }

    public void WriteBeginClass(string name)
    {
        _writer.WriteIndentedLine("public static partial class {0}", name);
        _writer.WriteIndentedLine("{");
        _writer.IncreaseIndent();
    }

    public void WriteRelationProperty(RelationInfo relation)
    {
        _writer.WriteIndentedLine(
            "public static {0} {1} {{ get; }} = new {0}();",
            relation.ClassName,
            relation.PropertyName);
        _writer.WriteLine();
    }

    public void WritePermissionProperty(PermissionInfo permission)
    {
        _writer.WriteIndentedLine(
            "public static PermissionLink {0} {{ get; }} = new PermissionLink(Permissions.{0}, {0}Configure);",
            permission.PropertyName);
        _writer.WriteLine();
    }

    public void WritePermissionMethod(PermissionInfo permission)
    {
        _writer.WriteIndentedLine("public static partial Permission {0}Configure();", permission.PropertyName);
        _writer.WriteLine();
    }

    public void WriteRefClass(DefinitionInfo definition)
    {
        _writer.WriteIndentedLine(
            "public sealed record Ref() : DefinitionReference(\"{0}\", false)",
            definition.ClassName);
        _writer.WriteIndentedLine("{");

        using (_writer.IncreaseIndent())
        {
            _writer.WriteIndentedLine(
                "public sealed record All() : DefinitionReference(\"{0}\", true);",
                definition.ClassName);

            foreach (var propertyName in definition.Relations.Select(r => r.PropertyName))
            {
                _writer.WriteIndentedLine(
                    "public sealed record {0}() : DefinitionReference(\"{1}\", false, \"{0}\");",
                    propertyName,
                    definition.ClassName);
            }
        }

        _writer.WriteIndentedLine("}");
        _writer.WriteLine();
    }

    public void WritePermissionsClass(DefinitionInfo definition)
    {
        _writer.WriteIndentedLine("public static class Permissions");
        _writer.WriteIndentedLine("{");

        using (_writer.IncreaseIndent())
        {
            foreach (var name in definition.Permissions.Select(p => p.PropertyName))
            {
                _writer.WriteIndentedLine("public const string {0} = \"{0}\";", name);
            }
        }

        _writer.WriteIndentedLine("}");
        _writer.WriteLine();
    }

    public void WriteRelationClass(SourceText sourceText)
    {
        foreach (var line in sourceText.ToString().Split('\n'))
        {
            _writer.WriteIndentedLine(line);
        }
    }

    public void WriteEndClass()
    {
        _writer.DecreaseIndent();
        _writer.WriteIndentedLine("}");
    }

    public void WriteEndNamespace()
    {
        _writer.DecreaseIndent();
        _writer.WriteIndentedLine("}");
        _writer.WriteLine();
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
