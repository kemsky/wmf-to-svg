using Wmf2Svg.Svg;
using Xunit;

namespace Wmf2Svg.Tests;

public class SvgGdiTests
{
    private const string ResourceSvg = "Wmf2Svg.Tests.Data.pie_test.svg";

    [Fact]
    public void TestPie()
    {
        var gdi = new SvgGdi();
        gdi.PlaceableHeader(0, 0, 9000, 9000, 1440);
        gdi.Header();
        gdi.SetWindowOrgEx(0, 0, null);
        gdi.SetWindowExtEx(200, 200, null);
        gdi.SetBkMode(1);

        var brush1 = gdi.CreateBrushIndirect(1, 0, 0);
        gdi.SelectObject(brush1);
        gdi.Rectangle(10, 10, 110, 110);

        var pen2 = gdi.CreatePenIndirect(0, 1, 0x0000FF);
        gdi.SelectObject(pen2);
        gdi.Pie(10, 10, 110, 110, 60, 10, 110, 60);
        gdi.Footer();

        var userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var outputDir = Path.Combine(userHome, "wmf2svg");
        Directory.CreateDirectory(outputDir);

        using var outputStream = new MemoryStream();

        gdi.Write(outputStream);

        var svg = Helper.GetString(outputStream);

        var targetSvg = Helper.GetSvg(ResourceSvg);

        Assert.Equal(targetSvg, svg);
    }
}