using Mojang.Api.Skins.ImageService.General;
using System.Drawing;
using System.Numerics;

namespace Mojang.Api.Skins.Data;
/// <summary>
/// Provides the base class for texture data, including methods for saving and encoding textures.
/// </summary>
public class TextureData
{
    /// <summary>
    /// Gets the texture data as a byte array.
    /// </summary>
    public byte[] TextureBytes { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// Gets the size of the texture.
    /// </summary>
    public Size TextureSize { get; set; }

    private readonly IImageUtilities? _imageUtilities;
    public TextureData(IImageUtilities imageUtilities) => _imageUtilities = imageUtilities;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextureData"/> class.
    /// </summary>
    public TextureData() { }

    /// <summary>
    /// Saves the texture as a PNG file to the specified path. If the file already exists, the save operation is skipped.
    /// </summary>
    /// <param name="path">The file path where the texture will be saved as a PNG file.</param>
    public void SaveAsPNG(string path)
    {
        if (File.Exists(path))
            return;

        File.WriteAllBytes(path, TextureBytes);
    }

    /// <summary>
    /// Combines the current texture data with another texture data, applying optional offsets to each layer, and returns a new <see cref="TextureData"/> instance containing the combined result.
    /// </summary>
    /// <param name="textureData">The texture data to combine with the current instance.</param>
    /// <param name="layer1Offset">Optional offset for the first layer of the current texture data. Defaults to null.</param>
    /// <param name="layer2Offset">Optional offset for the second layer of the texture data. Defaults to null.</param>
    /// <returns>A new <see cref="TextureData"/> instance representing the combined texture data, or null if the image utilities are not available.</returns>
    public TextureData? Combine(TextureData textureData, Vector2? layer1Offset = null, Vector2? layer2Offset = null)
    {
        if (_imageUtilities is null) return null;

        var combinedImage = _imageUtilities.Combine(TextureBytes, textureData.TextureBytes, layer1Offset, layer2Offset);
        return new TextureData(_imageUtilities)
        {
            TextureBytes = combinedImage,
            TextureSize = _imageUtilities.CalculateSize(combinedImage),
        };
    }

    /// <summary>
    /// Converts the texture data to a Base64 encoded string.
    /// </summary>
    /// <returns>A Base64 encoded string representing the texture data.</returns>
    public string ToBase64() => Convert.ToBase64String((ReadOnlySpan<byte>)TextureBytes.AsSpan());
}
