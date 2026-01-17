[![Build](https://github.com/kemsky/wmf-to-svg/actions/workflows/build.yml/badge.svg)](https://github.com/kemsky/wmf-to-svg/actions/workflows/build.yml)

# WMF to SVG

WMF to SVG Converting Tool & Library

This is a direct port from java library <https://github.com/hidekatsu-izuno/wmf2svg>.

Package is available on [Nuget](https://www.nuget.org/packages/Wmf2Svg/).

Install using dotnet CLI:
```commandline
dotnet add package Wmf2Svg
```
Install using Package-Manager console:
```commandline
PM> Install-Package Wmf2Svg
```

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
