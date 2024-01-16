using SkiaSharp;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;

namespace Mojang.Api.Skins.ImageService.General;
public sealed class SkiaImageUtilities : IImageUtilities
{
    public byte[] CreateImage(List<byte[]> inputs, List<Vector2> positions)
    {
        if (inputs.Count != positions.Count)
            throw new ArgumentException("The number of inputs must match the number of positions");

        int maxWidth = 0, maxHeight = 0;
        var bitmaps = new List<SKBitmap>();
        for (int i = 0; i < inputs.Count; i++)
        {
            using var stream = new MemoryStream(inputs[i]);
            var bitmap = SKBitmap.Decode(stream);
            if (bitmap == null)
                throw new InvalidOperationException($"Unable to decode image at index {i}.");

            bitmaps.Add(bitmap);

            int potentialWidth = (int)positions[i].X + bitmap.Width;
            int potentialHeight = (int)positions[i].Y + bitmap.Height;

            if (potentialWidth > maxWidth) maxWidth = potentialWidth;
            if (potentialHeight > maxHeight) maxHeight = potentialHeight;
        }

        using var combinedBitmap = new SKBitmap(maxWidth, maxHeight);
        using var canvas = new SKCanvas(combinedBitmap);
        canvas.Clear(SKColors.Transparent);

        for (int i = 0; i < bitmaps.Count; i++)
        {
            canvas.DrawBitmap(bitmaps[i], new SKPoint(positions[i].X, positions[i].Y));
            bitmaps[i].Dispose();
        }

        canvas.Flush();

        using var skiaImage = SKImage.FromBitmap(combinedBitmap);
        using var skiaData = skiaImage.Encode(SKEncodedImageFormat.Png, 100);

        return skiaData.ToArray();
    }

    public bool IsAreaFilledWithColor(ReadOnlySpan<byte> inputBytes, Rectangle area, Color color)
    {
        var checkArea = new SKRectI(area.Left, area.Top, area.Right, area.Bottom);
        var skColor = new SKColor(color.R, color.G, color.B, color.A);

        if (inputBytes.IsEmpty)
            throw new ArgumentException("Input bytes are empty", nameof(inputBytes));

        using var bitmap = SKBitmap.Decode(inputBytes) ?? throw new InvalidOperationException("Unable to decode the image.");

        if (!IsAreaValid(checkArea, bitmap))
            throw new ArgumentException("Area is out of the image bounds");

        for (int y = area.Top; y < area.Bottom; y++)
        {
            for (int x = area.Left; x < area.Right; x++)
            {
                var pixelColor = bitmap.GetPixel(x, y);

                if (pixelColor != skColor)
                    return false;
            }
        }

        return true;
    }

    public byte[] CropImage(ReadOnlySpan<byte> inputBytes, Rectangle rectangle)
    {
        var cropArea = new SKRectI(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

        if (inputBytes.IsEmpty)
            throw new ArgumentException("Input bytes are empty", nameof(inputBytes));

        using var original = SKBitmap.Decode(inputBytes) ?? throw new InvalidOperationException("Unable to decode the image.");
        if (!IsAreaValid(cropArea, original))
            throw new ArgumentException("Crop area is invalid", nameof(cropArea));

        using var cropped = new SKBitmap(cropArea.Width, cropArea.Height);
        if (!original.ExtractSubset(cropped, cropArea))
            throw new InvalidOperationException("Failed to extract image subset.");

        using var skiaImage = SKImage.FromBitmap(cropped);
        using var skiaData = skiaImage.Encode(SKEncodedImageFormat.Png, 100);

        return skiaData.ToArray();
    }

    public IReadOnlyList<byte[]> CropImage(ReadOnlySpan<byte> inputBytes, Rectangle[] regions)
    {
        if (inputBytes.IsEmpty)
            throw new ArgumentException("Input bytes are empty", nameof(inputBytes));

        using var original = SKBitmap.Decode(inputBytes) ?? throw new InvalidOperationException("Unable to decode the image.");

        var images = new List<byte[]>(regions.Length);
        for (int i = 0; i < regions.Length; i++)
        {
            var rectangle = regions[i];
            var cropArea = new SKRectI(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);

            if (!IsAreaValid(cropArea, original))
                throw new ArgumentException("Crop area is invalid", nameof(cropArea));

            using var cropped = new SKBitmap(cropArea.Width, cropArea.Height);
            if (!original.ExtractSubset(cropped, cropArea))
                throw new InvalidOperationException("Failed to extract image subset.");

            using var skiaImage = SKImage.FromBitmap(cropped);
            using var skiaData = skiaImage.Encode(SKEncodedImageFormat.Png, 100);
            images.Add(skiaData.ToArray());
        }

        return images;
    }

    private static bool IsAreaValid(SKRectI cropArea, SKBitmap image) => cropArea.Left >= 0 && cropArea.Top >= 0 && cropArea.Width > 0 && cropArea.Height > 0 && cropArea.Right <= image.Width && cropArea.Bottom <= image.Height;

    public byte[] Combine(ReadOnlySpan<byte> layer1, ReadOnlySpan<byte> layer2, Vector2? layer1Offset = null, Vector2? layer2Offset = null)
    {
        if (layer1.IsEmpty || layer2.IsEmpty)
            throw new ArgumentException("Input bytes are empty");

        using var bitmap1 = SKBitmap.Decode(layer1) ?? throw new InvalidOperationException("Unable to decode the first image.");
        using var bitmap2 = SKBitmap.Decode(layer2) ?? throw new InvalidOperationException("Unable to decode the second image.");

        layer1Offset ??= Vector2.Zero;
        layer2Offset ??= Vector2.Zero;

        int combinedWidth = Math.Max(bitmap1.Width + (int)layer1Offset.Value.X, bitmap2.Width + (int)layer2Offset.Value.X);
        int combinedHeight = Math.Max(bitmap1.Height + (int)layer1Offset.Value.Y, bitmap2.Height + (int)layer2Offset.Value.Y);

        using var combinedBitmap = new SKBitmap(combinedWidth, combinedHeight);
        using var canvas = new SKCanvas(combinedBitmap);

        canvas.DrawBitmap(bitmap1, new SKPoint(layer1Offset.Value.X, layer1Offset.Value.Y));
        canvas.DrawBitmap(bitmap2, new SKPoint(layer2Offset.Value.X, layer2Offset.Value.Y));

        canvas.Flush();

        using var skiaImage = SKImage.FromBitmap(combinedBitmap);
        using var skiaData = skiaImage.Encode(SKEncodedImageFormat.Png, 100);

        return skiaData.ToArray();
    }

    public byte[] Resize(ReadOnlySpan<byte> inputBytes, int width, int height)
    {
        if (inputBytes.IsEmpty)
            throw new ArgumentException("Input bytes are empty");

        using var bitmap = SKBitmap.Decode(inputBytes) ?? throw new InvalidOperationException("Unable to decode the image.");

        using var combinedBitmap = new SKBitmap(width, height);
        using var canvas = new SKCanvas(combinedBitmap);

        canvas.DrawBitmap(bitmap, new SKPoint(0, 0));

        canvas.Flush();

        using var skiaImage = SKImage.FromBitmap(combinedBitmap);
        using var skiaData = skiaImage.Encode(SKEncodedImageFormat.Png, 100);

        return skiaData.ToArray();
    }

    public Size CalculateSize(ReadOnlySpan<byte> inputBytes)
    {
        if (inputBytes.IsEmpty)
            throw new ArgumentException("Input bytes are empty");

        using var bitmap = SKBitmap.Decode(inputBytes) ?? throw new InvalidOperationException("Unable to decode the image.");
        return new Size(bitmap.Width, bitmap.Height);
    }

    public Color GetPixel(ReadOnlySpan<byte> inputBytes, int x, int y)
    {
        if (inputBytes.IsEmpty)
            throw new ArgumentException("Input bytes are empty");

        using var bitmap = SKBitmap.Decode(inputBytes) ?? throw new InvalidOperationException("Unable to decode the image.");
        var pixel = bitmap.GetPixel(x, y);
        return Color.FromArgb(pixel.Alpha, pixel.Red, pixel.Green, pixel.Blue);
    }

    public ReadOnlySpan<byte> GetPixels(ReadOnlySpan<byte> fileBytes)
    {
        if (fileBytes.IsEmpty)
            throw new ArgumentException("Input bytes are empty");

        using var bitmap = SKBitmap.Decode(fileBytes) ?? throw new InvalidOperationException("Unable to decode the image.");
        return bitmap.GetPixelSpan();
    }

    public Memory<byte> HashImage(ReadOnlySpan<byte> fileBytes)
    {
        if (fileBytes.IsEmpty)
            throw new ArgumentException("Input bytes are empty");

        using var bitmap = SKBitmap.Decode(fileBytes) ?? throw new InvalidOperationException("Unable to decode the image.");

        return SHA256.HashData(bitmap.GetPixelSpan());
    }
}