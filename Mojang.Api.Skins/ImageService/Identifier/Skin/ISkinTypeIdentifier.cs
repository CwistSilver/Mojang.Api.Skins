using Mojang.Api.Skins.Data;

namespace Mojang.Api.Skins.ImageService.Identifier.Skin;
/// <summary>
/// Defines an interface for identifying the skin type.
/// </summary>
public interface ISkinTypeIdentifier
{
    /// <summary>
    /// Identifies the skin type by the skin bytes
    /// </summary>
    /// <param name="skinBytes"> The image bytes of the skin.</param>
    /// <returns> The skin type.</returns>
    SkinType Identify(ReadOnlySpan<byte> skinBytes);
}