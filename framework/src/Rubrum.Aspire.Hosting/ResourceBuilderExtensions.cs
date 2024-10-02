using System.Reflection;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;

namespace Rubrum.Aspire.Hosting;

public static class ResourceBuilderExtensions
{
    private static readonly Type EndpointReferenceAnnotationType = AppDomain
        .CurrentDomain
        .GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Single(t => t.Name == "EndpointReferenceAnnotation");

    private static readonly Type Http2ServiceAnnotationType = AppDomain
        .CurrentDomain
        .GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Single(t => t.Name == "Http2ServiceAnnotation"); // Убрать когда станет публичным

    private static readonly PropertyInfo EndpointReferenceAnnotationResource =
        EndpointReferenceAnnotationType.GetProperty("Resource")!;

    public static IReadOnlyList<IResource> GetReferenceProjects<T>(this IResourceBuilder<T> project)
        where T : IResource
    {
        var annotations = project.Resource.Annotations
            .Where(a => EndpointReferenceAnnotationType.IsInstanceOfType(a))
            .ToList();

        return annotations
            .Select(a => (IResource)EndpointReferenceAnnotationResource.GetValue(a)!)
            .ToList();
    }

    public static bool IsHttp2<T>(this T resource)
        where T : IResource
    {
        return resource.Annotations.Any(a => Http2ServiceAnnotationType.IsInstanceOfType(a));
    }

    public static bool IsHttp2<T>(this IResourceBuilder<T> project)
        where T : IResource
    {
        return project.Resource.IsHttp2();
    }

    public static string GetUrl<T>(this IResourceBuilder<T> project, string name)
        where T : IResourceWithEndpoints
    {
        var endpoint = project.GetEndpoint(name);

        if (project.ApplicationBuilder.ExecutionContext.IsPublishMode)
        {
            return ((IManifestExpressionProvider)endpoint).ValueExpression;
        }

        return endpoint.IsAllocated ? endpoint.Url : string.Empty;
    }
}
