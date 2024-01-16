using Mojang.Api.Skins.Data;

namespace Mojang.Api.Skins.ImageService.Identifier.Skin;
public interface ISkinTypeIdentifier
{
    SkinType Identify(ReadOnlySpan<byte> skinBytes);
}