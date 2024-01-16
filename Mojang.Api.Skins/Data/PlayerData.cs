namespace Mojang.Api.Skins.Data;
/// <summary>
/// Represents the data associated with a Minecraft player.
/// </summary>
public sealed class PlayerData
{
    /// <summary>
    /// The name of the player.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier of the player.
    /// </summary>
    public Guid UUID { get; set; }

    /// <summary>
    /// The skin data associated with the player.
    /// </summary>
    public SkinData Skin { get; set; } = new SkinData();

    /// <summary>
    /// The cape data associated with the player, if available; otherwise, null.
    /// </summary>
    public CapeData? Cape { get; set; }
}
