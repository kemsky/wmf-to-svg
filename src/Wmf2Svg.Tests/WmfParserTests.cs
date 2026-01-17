using System.Text;
using Wmf2Svg.Wmf;
using Xunit;

namespace Wmf2Svg.Tests;

public class WmfParserTests
{
    private const string ResourceWmf = "Wmf2Svg.Tests.Data.sample.wmf";
    private const string ResourceSvg = "Wmf2Svg.Tests.Data.sample.svg";

    [Fact]
    public void Parse_Stream()
    {
        using var stream = Helper.GetStream(ResourceWmf);

        var parser = new WmfParser();

        var gdi = parser.Parse(stream, compatible: true);

        using var svgStream = new MemoryStream();

        gdi.Write(svgStream);

        svgStream.Position = 0;

        using var reader = new StreamReader(svgStream, Encoding.UTF8);

        var svg = reader.ReadToEnd();

        var targetSvg = Helper.GetSvg(ResourceSvg);

        Assert.Equal(targetSvg, svg);
    }

    [Fact]
    public void Parse_ByteArray()
    {
        var buffer = Helper.GetBytes(ResourceWmf);

        var parser = new WmfParser();

        var gdi = parser.Parse(buffer, compatible: true);

        using var svgStream = new MemoryStream();

        gdi.Write(svgStream);

        svgStream.Position = 0;

        using var reader = new StreamReader(svgStream, Encoding.UTF8);

        var svg = reader.ReadToEnd();

        var targetSvg = Helper.GetSvg(ResourceSvg);

        Assert.Equal(targetSvg, svg);
    }
}