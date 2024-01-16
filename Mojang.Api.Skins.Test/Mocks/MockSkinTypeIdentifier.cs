using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.ImageService.Identifier.Skin;

namespace Mojang.Api.Skins.Test.Mocks;
public class MockSkinTypeIdentifier : ISkinTypeIdentifier
{
    private SkinType _skinType;

    public void SetIdentifyResult(SkinType skinType) => _skinType = skinType;
    public SkinType Identify(ReadOnlySpan<byte> skinBytes) => _skinType;
}
