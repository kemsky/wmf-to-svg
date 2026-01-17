using SkiaSharp;

namespace Wmf2Svg.Svg;

internal static class ImageConverter
{
    public static byte[]? Convert(byte[] image, string destType, bool reverse)
    {
        if (destType == null)
        {
            throw new ArgumentNullException(nameof(destType), "dest type is null.");
        }

        destType = destType.ToLowerInvariant();

        SKEncodedImageFormat format;
        switch (destType)
        {
            case "png":
                format = SKEncodedImageFormat.Png;
                break;
            case "jpeg":
            case "jpg":
                format = SKEncodedImageFormat.Jpeg;
                break;
            case "gif":
                format = SKEncodedImageFormat.Gif;
                break;
            case "bmp":
                format = SKEncodedImageFormat.Bmp;
                break;
            case "webp":
                format = SKEncodedImageFormat.Webp;
                break;
            default:
                throw new NotSupportedException($"Unsupported image encoding: {destType}");
        }

        try
        {
            using var inputStream = new MemoryStream(image);
            using var skBitmap = SKBitmap.Decode(inputStream);

            if (skBitmap == null)
            {
                return null;
            }

            var outputBitmap = skBitmap;

            // Convert to 24-bit color (remove alpha channel for consistency with Java version)
            if (skBitmap.ColorType != SKColorType.Rgb888x)
            {
                var info = new SKImageInfo(skBitmap.Width, skBitmap.Height, SKColorType.Rgb888x);
                using var convertedBitmap = new SKBitmap(info);

                using var canvas = new SKCanvas(convertedBitmap);
                canvas.Clear(SKColors.White);
                canvas.DrawBitmap(skBitmap, 0, 0);

                if (outputBitmap != skBitmap)
                {
                    outputBitmap.Dispose();
                }

                outputBitmap = convertedBitmap;
            }

            // Flip vertically if requested
            if (reverse)
            {
                using var flippedBitmap = new SKBitmap(outputBitmap.Width, outputBitmap.Height, outputBitmap.ColorType, outputBitmap.AlphaType);

                using var canvas = new SKCanvas(flippedBitmap);
                canvas.Scale(1, -1, 0, outputBitmap.Height / 2f);
                canvas.DrawBitmap(outputBitmap, 0, 0);

                if (outputBitmap != skBitmap)
                {
                    outputBitmap.Dispose();
                }

                outputBitmap = flippedBitmap;
            }

            // Encode to destination format
            using var outputImage = SKImage.FromBitmap(outputBitmap);
            using var data = outputImage.Encode(format, 100);

            if (outputBitmap != skBitmap)
            {
                outputBitmap.Dispose();
            }

            return data?.ToArray();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to convert image", ex);
        }
    }
}