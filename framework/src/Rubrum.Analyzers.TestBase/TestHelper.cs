using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Basic.Reference.Assemblies;
using CookieCrumble;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Rubrum.Analyzers;

public static class TestHelper
{
    private static readonly HashSet<string> IgnoreCodes = ["CS8652", "CS8632", "CS5001", "CS8019"];

    public static Snapshot GetGeneratedSourceSnapshot<TGenerator>(
        [StringSyntax("csharp")] string sourceText,
        IEnumerable<PortableExecutableReference>? references = null)
        where TGenerator : IIncrementalGenerator, new()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceText);

        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: [syntaxTree],
            ReferenceAssemblies.Net80.Concat(references ?? []));

        IIncrementalGenerator generator = new TGenerator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        return CreateSnapshot(compilation, driver);
    }

    private static Snapshot CreateSnapshot(CSharpCompilation compilation, GeneratorDriver driver)
    {
        var snapshot = new Snapshot();

        foreach (var result in driver.GetRunResult().Results)
        {
            var sources = result.GeneratedSources.OrderBy(s => s.HintName);

            foreach (var source in sources)
            {
                var text = source.SourceText.ToString();

                snapshot.Add(text, source.HintName, MarkdownLanguages.CSharp);
            }

            var diagnostics = compilation.GetDiagnostics();
            if (diagnostics.Length > 0)
            {
                AddDiagnosticsToSnapshot(snapshot, diagnostics, "Compilation Diagnostics");
            }

            if (result.Diagnostics.Any())
            {
                AddDiagnosticsToSnapshot(snapshot, result.Diagnostics, "Generator Diagnostics");
            }
        }

        return snapshot;
    }

    private static void AddDiagnosticsToSnapshot(
        Snapshot snapshot,
        ImmutableArray<Diagnostic> diagnostics,
        string title)
    {
        var hasDiagnostics = false;

        using var stream = new MemoryStream();
        using var jsonWriter = new Utf8JsonWriter(
            stream,
            new JsonWriterOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Indented = true,
            });

        jsonWriter.WriteStartArray();

        foreach (var diagnostic in diagnostics.Where(diagnostic => !IgnoreCodes.Contains(diagnostic.Id)))
        {
            hasDiagnostics = true;

            jsonWriter.WriteStartObject();
            jsonWriter.WriteString(nameof(diagnostic.Id), diagnostic.Id);

            var descriptor = diagnostic.Descriptor;

            jsonWriter.WriteString(nameof(descriptor.Title), descriptor.Title.ToString());
            jsonWriter.WriteString(nameof(diagnostic.Severity), diagnostic.Severity.ToString());
            jsonWriter.WriteNumber(nameof(diagnostic.WarningLevel), diagnostic.WarningLevel);

            jsonWriter.WriteString(
                nameof(diagnostic.Location),
                diagnostic.Location.GetMappedLineSpan().ToString());

            var description = descriptor.Description.ToString();
            if (!string.IsNullOrWhiteSpace(description))
            {
                jsonWriter.WriteString(nameof(descriptor.Description), description);
            }

            var help = descriptor.HelpLinkUri;
            if (!string.IsNullOrWhiteSpace(help))
            {
                jsonWriter.WriteString(nameof(descriptor.HelpLinkUri), help);
            }

            jsonWriter.WriteString(
                nameof(descriptor.MessageFormat),
                descriptor.MessageFormat.ToString());

            jsonWriter.WriteString("Message", diagnostic.GetMessage());
            jsonWriter.WriteString(nameof(descriptor.Category), descriptor.Category);

            jsonWriter.WritePropertyName(nameof(descriptor.CustomTags));

            jsonWriter.WriteStartArray();

            foreach (var tag in descriptor.CustomTags)
            {
                jsonWriter.WriteStringValue(tag);
            }

            jsonWriter.WriteEndArray();

            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndArray();
        jsonWriter.Flush();

        if (hasDiagnostics)
        {
            snapshot.Add(Encoding.UTF8.GetString(stream.ToArray()), title, MarkdownLanguages.Json);
        }
    }
}
