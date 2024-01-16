using Mojang.Api.Skins.ImageService.Identifier.Cape;

namespace Mojang.Api.Skins.Test.Mocks;
public class MockCapeTextureIdentifier : ICapeTextureIdentifier
{
    private string? _identifier;
    public void SetTextureNameResult(string name) => _identifier = name;
    public string? GetTextureName(ReadOnlySpan<byte> fileBytes) => _identifier;
}
