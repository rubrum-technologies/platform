using System.Diagnostics.CodeAnalysis;
using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Volo.Abp.DependencyInjection;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Rubrum.Platform.DataSourceService;

public class DataSourceCompiler : IDataSourceCompiler, ITransientDependency
{
    public bool TryCompile(DataSource dataSource, [NotNullWhen(true)] out Stream? dll)
    {
        var namespaceDeclaration = CreateNamespace(dataSource)
            .AddMembers([..dataSource.Entities.Select(e => CreateClass(dataSource, e))]);

        var root = CompilationUnit()
            .AddMembers(namespaceDeclaration)
            .AddUsings(UsingDirective(ParseName("System")));

        var compilation = CSharpCompilation
            .Create(
                dataSource.Id.ToString(),
                syntaxTrees: [root.SyntaxTree],
                references: ReferenceAssemblies.Net80,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var stream = new MemoryStream();
        var result = compilation.Emit(stream);
        stream.Position = 0;

        if (!result.Success)
        {
            stream.Dispose();
            dll = null;
            return false;
        }

        dll = stream;
        return true;
    }

    protected static NamespaceDeclarationSyntax CreateNamespace(DataSource dataSource)
    {
        return NamespaceDeclaration(ParseName($"Rubrum.Platform.DataSourceService.{dataSource.Prefix}"));
    }

    protected static ClassDeclarationSyntax CreateClass(DataSource dataSource, DataSourceEntity entity)
    {
        var relations = dataSource.InternalRelations
            .Where(x => x.Left.EntityId == entity.Id);

        return ClassDeclaration(entity.Name)
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddMembers([..entity.Properties.Select(CreateProperty)])
            .AddMembers([..relations.Select(r => CreateLink(dataSource, r))]);
    }

    protected static PropertyDeclarationSyntax CreateProperty(DataSourceEntityProperty property)
    {
        var propertySyntax = property.ToPropertySyntax()
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration));

        return propertySyntax;
    }

    protected static PropertyDeclarationSyntax CreateLink(DataSource dataSource, DataSourceInternalRelation relation)
    {
        var entity = dataSource.GetEntityById(relation.Right.EntityId);

        var propertySyntax = PropertyDeclaration(ParseName(entity.Name), entity.Name)
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration));

        return propertySyntax;
    }
}
