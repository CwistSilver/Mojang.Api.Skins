using System.Drawing;
using System.Numerics;

namespace Mojang.Api.Skins.ImageService.General;
/// <summary>
/// Defines an interface for various image utility operations.
/// </summary>
public interface IImageUtilities
{
    /// <summary>
    /// Checks if a specified area in an image is filled with a given color.
    /// </summary>
    /// <param name="inputBytes">The byte span of the image.</param>
    /// <param name="area">The area to check within the image.</param>
    /// <param name="color">The color to check for.</param>
    /// <returns>True if the specified area is filled with the specified color; otherwise, false.</returns>
    bool IsAreaFilledWithColor(ReadOnlySpan<byte> inputBytes, Rectangle area, Color color);

    /// <summary>
    /// Creates a composite image from multiple images and their respective positions.
    /// </summary>
    /// <param name="inputs">The list of byte arrays representing the images.</param>
    /// <param name="positions">The list of positions for each image.</param>
    /// <returns>A byte array representing the combined image.</returns>
    byte[] CreateImage(List<byte[]> inputs, List<Vector2> positions);

    /// <summary>
    /// Gets the color of a specific pixel in an image.
    /// </summary>
    /// <param name="inputBytes">The byte span of the image.</param>
    /// <param name="x">The x-coordinate of the pixel.</param>
    /// <param name="y">The y-coordinate of the pixel.</param>
    /// <returns>The color of the specified pixel.</returns>
    Color GetPixel(ReadOnlySpan<byte> inputBytes, int x, int y);

    /// <summary>
    /// Retrieves all pixels from the image file bytes.
    /// </summary>
    /// <param name="fileBytes">The byte span of the image file.</param>
    /// <returns>A ReadOnlySpan of bytes representing the pixels of the image.</returns>
    ReadOnlySpan<byte> GetPixels(ReadOnlySpan<byte> fileBytes);

    /// <summary>
    /// Generates a hash for an image from its file bytes.
    /// </summary>
    /// <param name="fileBytes">The byte span of the image file.</param>
    /// <returns>A Memory of bytes representing the hashed value of the image.</returns>
    Memory<byte> HashImage(ReadOnlySpan<byte> fileBytes);

    /// <summary>
    /// Calculates the size of an image from its bytes.
    /// </summary>
    /// <param name="inputBytes">The byte span of the image.</param>
    /// <returns>The size of the image.</returns>
    Size CalculateSize(ReadOnlySpan<byte> inputBytes);

    /// <summary>
    /// Crops an image into multiple regions.
    /// </summary>
    /// <param name="inputBytes">The byte span of the image to crop.</param>
    /// <param name="regions">The array of rectangles defining the regions to crop.</param>
    /// <returns>A list of byte arrays, each representing a cropped region of the image.</returns>
    IReadOnlyList<byte[]> CropImage(ReadOnlySpan<byte> inputBytes, Rectangle[] regions);

    /// <summary>
    /// Crops a specified region from an image.
    /// </summary>
    /// <param name="inputBytes">The byte span of the image to crop.</param>
    /// <param name="rectangle">The rectangle defining the region to crop.</param>
    /// <returns>A byte array representing the cropped region of the image.</returns>
    byte[] CropImage(ReadOnlySpan<byte> inputBytes, Rectangle rectangle);

    /// <summary>
    /// Combines two image layers, optionally with offsets.
    /// </summary>
    /// <param name="layer1">The byte span of the first layer.</param>
    /// <param name="layer2">The byte span of the second layer.</param>
    /// <param name="layer1Offset">The optional offset for the first layer.</param>
    /// <param name="layer2Offset">The optional offset for the second layer.</param>
    /// <returns>A byte array representing the combined image.</returns>
    byte[] Combine(ReadOnlySpan<byte> layer1, ReadOnlySpan<byte> layer2, Vector2? layer1Offset = null, Vector2? layer2Offset = null);

    /// <summary>
    /// Resizes an image to the specified width and height.
    /// </summary>
    /// <param name="inputBytes">The byte span of the image to resize.</param>
    /// <param name="width">The desired width of the resized image.</param>
    /// <param name="height">The desired height of the resized image.</param>
    /// <returns>A byte array representing the resized image.</returns>
    byte[] Resize(ReadOnlySpan<byte> inputBytes, int width, int height);
}