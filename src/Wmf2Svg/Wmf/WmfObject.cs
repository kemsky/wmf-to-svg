using Wmf2Svg.Gdi;

namespace Wmf2Svg.Wmf;

public abstract class WmfObject : IGdiObject
{
    public int ID { get; set; }

    protected WmfObject(int id)
    {
        ID = id;
    }
}