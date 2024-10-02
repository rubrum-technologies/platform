namespace Rubrum.Graphql;

internal static class StringExtension
{
    public static string? ReplaceNewLine(this string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        return text
            .Replace("\r\n", "\n")
            .Replace("\r", "\n");
    }
}
