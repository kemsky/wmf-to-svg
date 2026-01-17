# WMF to SVG

WMF to SVG Converting Tool & Library

This is a direct port from java library <https://github.com/hidekatsu-izuno/wmf2svg>.

## Usage

```csharp
using var inputStream = new FileStream(wmfFilePath, FileMode.Open, FileAccess.Read);
using var outputStream = new FileStream(svgFilePath, FileMode.Create, FileAccess.Write);

var parser = new WmfParser();

var gdi = parser.Parse(inputStream);

gdi.Write(outputStream);
```

## License

Apache-2.0

## Copyright

- 2026 Alexander Turtsevich
- 2007-2025 Hidekatsu Izuno, Shunsuke Mori
