using Mojang.Api.Skins.ImageService.General;
using System.Drawing;
using System.Numerics;

namespace Mojang.Api.Skins.Test.Mocks;
public class MockImageUtilities : IImageUtilities
{
    private Size _calculateSizeReturnValue = new Size(0, 0);
    public void SetCalculateSizeReturnValue(Size size) => _calculateSizeReturnValue = size;
    public Size CalculateSize(ReadOnlySpan<byte> inputBytes) => _calculateSizeReturnValue;

    public bool IsAreaFilledWithColor(ReadOnlySpan<byte> inputBytes, Rectangle area, Color color) => false;
    public byte[] CreateImage(List<byte[]> inputs, List<Vector2> positions) => Array.Empty<byte>();
    public Color GetPixel(ReadOnlySpan<byte> inputBytes, int x, int y) => Color.Empty;
    public ReadOnlySpan<byte> GetPixels(ReadOnlySpan<byte> fileBytes) => Array.Empty<byte>();
    public Memory<byte> HashImage(ReadOnlySpan<byte> fileBytes) => new Memory<byte>(fileBytes.ToArray());
    public IReadOnlyList<byte[]> CropImage(ReadOnlySpan<byte> inputBytes, Rectangle[] regions) => new List<byte[]>();
    public byte[] CropImage(ReadOnlySpan<byte> inputBytes, Rectangle rectangle) => Array.Empty<byte>();
    public byte[] Combine(ReadOnlySpan<byte> layer1, ReadOnlySpan<byte> layer2, Vector2? layer1Offset = null, Vector2? layer2Offset = null) => Array.Empty<byte>();
    public byte[] Resize(ReadOnlySpan<byte> inputBytes, int width, int height) => Array.Empty<byte>();
}
