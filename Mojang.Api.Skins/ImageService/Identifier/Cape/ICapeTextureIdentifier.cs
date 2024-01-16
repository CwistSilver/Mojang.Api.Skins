namespace Mojang.Api.Skins.ImageService.Identifier.Cape;
/// <summary>
/// Defines an interface for identifying cape textures from file bytes.
/// </summary>
public interface ICapeTextureIdentifier
{
    /// <summary>
    /// Gets the texture name from the provided cape file bytes.
    /// </summary>
    /// <param name="fileBytes">The byte span of the cape file.</param>
    /// <returns>The name of the texture if identified; otherwise, null.</returns>
    string? GetTextureName(ReadOnlySpan<byte> fileBytes);
}
