using Wmf2Svg.Gdi;
using Wmf2Svg.Wmf;
using Xunit;

namespace Wmf2Svg.Tests;

public class WmfGdiTests
{
    private const string ResourcePieTestWmf = "Wmf2Svg.Tests.Data.pie_test.wmf";
    private const string ResourceEllipseTestWmf = "Wmf2Svg.Tests.Data.ellipse_test.wmf";
    private const string ResourceFontTestWmf = "Wmf2Svg.Tests.Data.font_test.wmf";

    [Fact]
    public void TestEllipse()
    {
        var gdi = new WmfGdi();
        gdi.PlaceableHeader(0, 0, 9000, 4493, 1440);
        gdi.Header();
        gdi.SetWindowOrgEx(0, 0, null);
        gdi.SetWindowExtEx(200, 200, null);
        gdi.SetBkMode(1);

        var brush1 = gdi.CreateBrushIndirect(1, 0, 0);
        gdi.SelectObject(brush1);
        gdi.Rectangle(0, 0, 200, 200);
        gdi.MoveToEx(10, 10, null);
        gdi.LineTo(100, 100);
        gdi.Footer();

        var targetWmf = Helper.GetBytes(ResourceEllipseTestWmf);

        using var wmfStream = new MemoryStream();

        gdi.Write(wmfStream);

        wmfStream.Position = 0;

        var wmf = Helper.GetBytes(wmfStream);

        Assert.Equal(targetWmf, wmf);
    }

    [Fact]
    public void TestExtTextOut()
    {
        var gdi = new WmfGdi();
        gdi.PlaceableHeader(0, 0, 500, 500, 96);
        gdi.Header();
        gdi.SetWindowOrgEx(0, 0, null);
        gdi.SetWindowExtEx(500, 500, null);
        gdi.SetBkMode(1);

        var brush1 = gdi.CreateBrushIndirect(1, 0, 0);
        gdi.SelectObject(brush1);
        gdi.Rectangle(0, 0, 200, 72);
        gdi.MoveToEx(10, 10, null);
        gdi.LineTo(100, 100);

        var encoding = Gdi.Helper.GetEncoding(GdiFontConstants.ANSI_CHARSET);
        var font1 = gdi.CreateFontIndirect(
            72, 0, 0, 0,
            GdiFontConstants.FW_NORMAL,
            false, false, false,
            GdiFontConstants.ANSI_CHARSET,
            GdiFontConstants.OUT_DEFAULT_PRECIS,
            GdiFontConstants.CLIP_DEFAULT_PRECIS,
            GdiFontConstants.DEFAULT_QUALITY,
            GdiFontConstants.DEFAULT_PITCH,
            encoding.GetBytes("Arial"));
        gdi.SelectObject(font1);

        var fontEncoding = Gdi.Helper.GetEncoding(font1.Charset);
        gdi.ExtTextOut(0, 0, 0, null,
            fontEncoding.GetBytes("ABCdefg"),
            [30, 30, 30, 30, 30, 30, 20]);

        gdi.Footer();

        var targetWmf = Helper.GetBytes(ResourceFontTestWmf);

        using var wmfStream = new MemoryStream();

        gdi.Write(wmfStream);

        wmfStream.Position = 0;

        var wmf = Helper.GetBytes(wmfStream);

        Assert.Equal(targetWmf, wmf);
    }

    [Fact]
    public void TestPie()
    {
        var gdi = new WmfGdi();
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

        var targetWmf = Helper.GetBytes(ResourcePieTestWmf);

        using var wmfStream = new MemoryStream();

        gdi.Write(wmfStream);

        wmfStream.Position = 0;

        var wmf = Helper.GetBytes(wmfStream);

        Assert.Equal(targetWmf, wmf);
    }
}