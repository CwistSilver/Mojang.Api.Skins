namespace Mojang.Api.Skins.ImageService.SkinConverter;
/// <summary>
/// Defines an interface for converting legacy skin formats to modern skin formats.
/// </summary>
public interface IModernSkinConverter
{
    /// <summary>
    /// Converts legacy Minecraft skin data to the modern skin format.
    /// </summary>
    /// <param name="skinDataBytes">The byte array of the legacy skin data.</param>
    /// <returns>A byte array representing the skin data in the modern format.</returns>
    byte[] ConvertToModernSkin(byte[] skinDataBytes);
}