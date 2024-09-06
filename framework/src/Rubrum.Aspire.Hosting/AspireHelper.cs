using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Rubrum.Aspire.Hosting;

public static class AspireHelper
{
    public static Dictionary<string, string> ObjectToConfig(object? obj)
    {
        var values = new Dictionary<string, string>();
        var builder = new ConfigurationBuilder();
        var json = JsonSerializer.Serialize(obj);

        builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)));
        var root = builder.Build();

        foreach (var (key, value) in root.AsEnumerable()
                     .Where(pair => !string.IsNullOrEmpty(pair.Value))
                     .OrderBy(pair => pair.Key))
        {
            values.Add(key.Replace(":", "__"), value!);
        }

        return values;
    }
}
