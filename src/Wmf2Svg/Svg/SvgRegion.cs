using System.Xml;
using Wmf2Svg.Gdi;

namespace Wmf2Svg.Svg;

public abstract class SvgRegion : SvgObject, IGdiRegion
{
    protected SvgRegion(SvgGdi gdi) : base(gdi)
    {
    }

    public abstract XmlElement CreateElement();
}