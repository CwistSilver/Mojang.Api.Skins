using Mojang.Api.Skins.Data;

namespace Mojang.Api.Skins.Utilities.TextureCropper;
/// <summary>
/// Defines an interface for cropping skin and cape textures into specific parts.
/// </summary>
public interface ITextureCropper
{
    /// <summary>
    /// Cuts specified parts from the skin data.
    /// </summary>
    /// <param name="skinData">The skin data to crop.</param>
    /// <param name="skinParts">The skin parts to be cropped.</param>
    /// <returns>An array of <see cref="SkinPartData"/> representing the cropped parts.</returns>
    SkinPartData[] Cut(SkinData skinData, IEnumerable<SkinPart> skinParts);

    /// <summary>
    /// Cuts a specific part from the skin data.
    /// </summary>
    /// <param name="skinData">The skin data to crop.</param>
    /// <param name="skinPart">The specific skin part to be cropped.</param>
    /// <returns>The <see cref="SkinPartData"/> representing the cropped part.</returns>
    SkinPartData Cut(SkinData skinData, SkinPart skinPart);

    /// <summary>
    /// Cuts specified parts from the cape data.
    /// </summary>
    /// <param name="capeData">The cape data to crop.</param>
    /// <param name="capeParts">The cape parts to be cropped.</param>
    /// <returns>An array of <see cref="CapePartData"/> representing the cropped parts.</returns>
    CapePartData[] Cut(CapeData capeData, IEnumerable<CapePart> capeParts);

    /// <summary>
    /// Cuts a specific part from the cape
    /// </summary>
    /// <param name="capeData">The cape data to crop.</param>
    /// <param name="capePart">The specific cape part to be cropped.</param>
    /// <returns>The <see cref="CapePartData"/> representing the cropped part.</returns>
    CapePartData Cut(CapeData capeData, CapePart capePart);
}