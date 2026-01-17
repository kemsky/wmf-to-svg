using System.Reflection;
using System.Text;

namespace Wmf2Svg.Tests;

internal static class Helper
{
    public static byte[] GetBytes(Stream stream)
    {
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }

        using var memoryStream = new MemoryStream();

        stream.CopyTo(memoryStream);

        return memoryStream.ToArray();
    }

    public static string GetString(Stream stream)
    {
        if (stream.CanSeek)
        {
            stream.Position = 0;
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);

        return reader.ReadToEnd();
    }

    public static byte[] GetBytes(string resource)
    {
        using var stream = GetStream(resource);

        return GetBytes(stream);
    }

    public static string GetSvg(string resource)
    {
        using var stream = GetStream(resource);

        using var reader = new StreamReader(stream, Encoding.UTF8);

        return reader.ReadToEnd();
    }

    public static Stream GetStream(string resource)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var stream = assembly.GetManifestResourceStream(resource);

        if (stream == null)
        {
            throw new InvalidOperationException($"Could not load resource: {resource}");
        }

        return stream;
    }
}