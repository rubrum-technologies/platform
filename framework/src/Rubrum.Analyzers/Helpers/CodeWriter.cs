using System.Text;

namespace Rubrum.Analyzers.Helpers;

public class CodeWriter(TextWriter writer) : TextWriter
{
    private int _indent;

    public CodeWriter(StringBuilder text)
        : this(new StringWriter(text))
    {
    }

    public static string Indent { get; } = new(' ', 4);

    public override Encoding Encoding { get; } = Encoding.UTF8;

    public override void Write(char value) =>
        writer.Write(value);

    public void WriteStringValue(string value)
    {
        Write('"');
        Write(value);
        Write('"');
    }

    public void WriteIndent()
    {
        if (_indent > 0)
        {
            var spaces = _indent * 4;
            for (var i = 0; i < spaces; i++)
            {
                Write(' ');
            }
        }
    }

    public string GetIndentString()
    {
        if (_indent > 0)
        {
            return new string(' ', _indent * 4);
        }

        return string.Empty;
    }

    public void WriteIndentedLine(string format, params object?[] args)
    {
        WriteIndent();

        if (args.Length == 0)
        {
            Write(format);
        }
        else
        {
            Write(format, args);
        }

        WriteLine();
    }

    public void WriteIndented(string format, params object?[] args)
    {
        WriteIndent();

        if (args.Length == 0)
        {
            Write(format);
        }
        else
        {
            Write(format, args);
        }
    }

    public void WriteSpace() => Write(' ');

    public IDisposable IncreaseIndent()
    {
        _indent++;
        return new Block(DecreaseIndent);
    }

    public IDisposable WriteMethod(
        string accessModifier,
        string returnType,
        string methodName,
        params string[] parameters)
    {
        WriteIndented("{0} {1} {2}(", accessModifier, returnType, methodName);

        if (parameters.Length > 0)
        {
            Write(string.Join(", ", parameters));
        }

        Write(")");
        WriteLine();
        return WithCurlyBrace();
    }

    public IDisposable WriteForEach(string item, string collection)
    {
        WriteIndentedLine("foreach(var {0} in {1})", item, collection);
        return WithCurlyBrace();
    }

    public IDisposable WriteIfClause(string condition, params object[] args)
    {
        WriteIndentedLine("if({0})", args.Length == 0 ? condition : string.Format(condition, args));
        return WithCurlyBrace();
    }

    public IDisposable WithCurlyBrace()
    {
        WriteIndentedLine("{");
        _indent++;
        return new Block(() =>
        {
            DecreaseIndent();
            WriteIndentedLine("}");
        });
    }

    public void DecreaseIndent()
    {
        if (_indent > 0)
        {
            _indent--;
        }
    }

    public IDisposable WriteBraces()
    {
        WriteLeftBrace();
        WriteLine();

        var indent = IncreaseIndent();

        return new Block(() =>
        {
            WriteLine();
            indent.Dispose();
            WriteIndent();
            WriteRightBrace();
        });
    }

    public void WriteLeftBrace() => Write('{');

    public void WriteRightBrace() => Write('}');

    private sealed class Block(Action close) : IDisposable
    {
        public void Dispose() => close();
    }
}
