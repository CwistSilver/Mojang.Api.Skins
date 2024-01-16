using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.ImageService.Identifier.Cape;
using Mojang.Api.Skins.ImageService.Identifier.Skin;
using Mojang.Api.Skins.ImageService.SkinConverter;
using Mojang.Api.Skins.Repository.MinecraftProfileInformation;
using Mojang.Api.Skins.Repository.MinecraftProfileProperties;
using Mojang.Api.Skins.Repository.MinecraftProfileTextures;
using Mojang.Api.Skins.Utilities.TextureCropper;
using Moq;
using System.Drawing;

namespace Mojang.Api.Skins.Test;
public class SkinsClientTests
{
    private ProfileProperties _profileProperties = new ProfileProperties
    {
        Name = "FakePlayer",
        Id = Guid.NewGuid(),
        Properties = new ProfileProperty[] {
            new ProfileProperty() {
                Name = "textures",
                Value = "ewogICJ0aW1lc3RhbXAiIDogMTcwNTI0MDM4NTE1MSwKICAicHJvZmlsZUlkIiA6ICJlZGM2MzE5YjQ5NjM0ZDhmYmZkNTI1N2QxNzg5N2I0NSIsCiAgInByb2ZpbGVOYW1lIiA6ICJDd2lzdFNpbHYzciIsCiAgInRleHR1cmVzIiA6IHsKICAgICJTS0lOIiA6IHsKICAgICAgInVybCIgOiAiaHR0cDovL3RleHR1cmVzLm1pbmVjcmFmdC5uZXQvdGV4dHVyZS83ZTNjNWQ1MTM4MTE1YTRmNjBjYTRmMGMwMTEyZjk3NmFmYmJjZjk3MGNmY2Y5ZWM1NDk0NDMyNTQ1Njg0NWIxIgogICAgfSwKICAgICJDQVBFIiA6IHsKICAgICAgInVybCIgOiAiaHR0cDovL3RleHR1cmVzLm1pbmVjcmFmdC5uZXQvdGV4dHVyZS8yMzQwYzBlMDNkZDI0YTExYjE1YThiMzNjMmE3ZTllMzJhYmIyMDUxYjI0ODFkMGJhN2RlZmQ2MzVjYTdhOTMzIgogICAgfQogIH0KfQ=="
            }
        }
    };
    private SkinData _defaultSkinData = new SkinData { SkinType = SkinType.Slim, TextureBytes = new byte[] { 1, 2, 3, 4, 5, 6 }, TextureSize = new Size(64, 64) };
    private CapeData _defaultCapeData = new CapeData { CapeName = "TestCape", TextureBytes = new byte[] { 1, 2, 3 }, TextureSize = new Size(64, 32) };


    private readonly Mock<IProfileInformationRepository> _profileInformationRepositoryMock;
    private readonly Mock<IProfilePropertiesRepository> _profilePropertiesRepositoryMock;
    private readonly Mock<IProfileTexturesRepository> _profileTexturesRepositoryMock;
    private readonly Mock<ICapeTextureIdentifier> _capeTextureIdentifierMock;
    private readonly Mock<IImageUtilities> _imageUtilitiesMock;
    private readonly Mock<ITextureCropper> _textureCropperMock;
    private readonly Mock<IModernSkinConverter> _modernSkinConverterMock;
    private readonly Mock<ISkinTypeIdentifier> _skinTypeIdentifierMock;
    public SkinsClientTests()
    {
        _profileInformationRepositoryMock = new Mock<IProfileInformationRepository>();
        _profilePropertiesRepositoryMock = new Mock<IProfilePropertiesRepository>();
        _profileTexturesRepositoryMock = new Mock<IProfileTexturesRepository>();
        _capeTextureIdentifierMock = new Mock<ICapeTextureIdentifier>();
        _imageUtilitiesMock = new Mock<IImageUtilities>();
        _textureCropperMock = new Mock<ITextureCropper>();
        _modernSkinConverterMock = new Mock<IModernSkinConverter>();
        _skinTypeIdentifierMock = new Mock<ISkinTypeIdentifier>();
    }   

    [Fact]
    public async Task GetAsync_ByName_ReturnsValidPlayerData()
    {
        // Arrange        
        _profileInformationRepositoryMock.Setup(repo => repo.Get(_profileProperties.Name)).ReturnsAsync(_profileProperties);
        _profilePropertiesRepositoryMock.Setup(repo => repo.Get(_profileProperties.Id)).ReturnsAsync(_profileProperties);
        _profileTexturesRepositoryMock.Setup(repo => repo.GetSkin(_profileProperties)).ReturnsAsync(_defaultSkinData);
        _profileTexturesRepositoryMock.Setup(repo => repo.GetCape(_profileProperties)).ReturnsAsync(_defaultCapeData);

        var client = CreateClient();

        // Act
        var result = await client.GetAsync(_profileProperties.Name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_profileProperties.Name, result.Name);
        Assert.Equal(_profileProperties.Id, result.UUID);
        Assert.Equal(_defaultSkinData, result.Skin);
        Assert.Equal(_defaultCapeData, result.Cape);

        _profileInformationRepositoryMock.Verify(repo => repo.Get(_profileProperties.Name), Times.Once());
        _profilePropertiesRepositoryMock.Verify(repo => repo.Get(_profileProperties.Id), Times.Once());
        _profileTexturesRepositoryMock.Verify(repo => repo.GetSkin(_profileProperties), Times.Once());
        _profileTexturesRepositoryMock.Verify(repo => repo.GetCape(_profileProperties), Times.Once());
    }

    [Fact]
    public async Task GetAsync_ByUUID_ReturnsValidPlayerData()
    {
        // Arrange      
        _profilePropertiesRepositoryMock.Setup(repo => repo.Get(_profileProperties.Id)).ReturnsAsync(_profileProperties);
        _profileTexturesRepositoryMock.Setup(repo => repo.GetSkin(_profileProperties)).ReturnsAsync(_defaultSkinData);
        _profileTexturesRepositoryMock.Setup(repo => repo.GetCape(_profileProperties)).ReturnsAsync(_defaultCapeData);

        var client = CreateClient();

        // Act
        var result = await client.GetAsync(_profileProperties.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_profileProperties.Name, result.Name);
        Assert.Equal(_profileProperties.Id, result.UUID);
        Assert.Equal(_defaultSkinData, result.Skin);
        Assert.Equal(_defaultCapeData, result.Cape);

        _profilePropertiesRepositoryMock.Verify(repo => repo.Get(_profileProperties.Id), Times.Once());
        _profileTexturesRepositoryMock.Verify(repo => repo.GetSkin(_profileProperties), Times.Once());
        _profileTexturesRepositoryMock.Verify(repo => repo.GetCape(_profileProperties), Times.Once());
    }

    [Fact]
    public void GetFromFile_WithoutCape_ReturnsValidPlayerData()
    {
        // Arrange      
        _profileTexturesRepositoryMock.Setup(repo => repo.GetSkinLocal(in It.Ref<byte[]>.IsAny)).Returns(_defaultSkinData);

        var client = CreateClient();

        // Act
        var result = client.GetLocal(_defaultSkinData.TextureBytes);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultSkinData, result.Skin);

        _profileTexturesRepositoryMock.Verify(repo => repo.GetSkinLocal(_defaultSkinData.TextureBytes), Times.Once());
    }

    [Fact]
    public void GetFromFile_WithCape_ReturnsValidPlayerData()
    {
        // Arrange      
        _profileTexturesRepositoryMock.Setup(repo => repo.GetSkinLocal(in It.Ref<byte[]>.IsAny)).Returns(_defaultSkinData);
        _profileTexturesRepositoryMock.Setup(repo => repo.GetCapeLocal(in It.Ref<byte[]>.IsAny)).Returns(_defaultCapeData);

        var client = CreateClient();

        // Act
        var result = client.GetLocal(_defaultSkinData.TextureBytes, _defaultCapeData.TextureBytes);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultSkinData, result.Skin);
        Assert.Equal(_defaultCapeData, result.Cape);

        _profileTexturesRepositoryMock.Verify(repo => repo.GetSkinLocal(_defaultSkinData.TextureBytes), Times.Once());
        _profileTexturesRepositoryMock.Verify(repo => repo.GetCapeLocal(_defaultCapeData.TextureBytes), Times.Once());
    }

    private SkinsClient CreateClient() => new SkinsClient(_profileInformationRepositoryMock.Object, _profilePropertiesRepositoryMock.Object, _profileTexturesRepositoryMock.Object, _capeTextureIdentifierMock.Object, _imageUtilitiesMock.Object, _textureCropperMock.Object, _modernSkinConverterMock.Object, _skinTypeIdentifierMock.Object);
}
