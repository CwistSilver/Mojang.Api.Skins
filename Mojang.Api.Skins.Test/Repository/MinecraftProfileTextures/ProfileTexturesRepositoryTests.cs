using Mojang.Api.Skins.Cache;
using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.ImageService.SkinConverter;
using Mojang.Api.Skins.Repository.MinecraftProfileTextures;
using Mojang.Api.Skins.Test.Mocks;
using Mojang.Api.Skins.Utilities.TextureCropper;
using Moq;
using System.Drawing;
using System.Net;

namespace Mojang.Api.Skins.Test.Repository.MinecraftProfileTextures;
public class ProfileTexturesRepositoryTests
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

    private readonly MockImageUtilities _mockImageUtilities;
    private readonly Mock<IModernSkinConverter> _modernSkinConverterMock;
    private readonly Mock<ITextureCropper> _textureCropperMock;
    private readonly MockCapeTextureIdentifier _capeTextureIdentifierMock;
    private readonly MockSkinTypeIdentifier _mockSkinTypeIdentifier;
    public ProfileTexturesRepositoryTests()
    {
        _mockImageUtilities = new MockImageUtilities();
        _modernSkinConverterMock = new Mock<IModernSkinConverter>();
        _textureCropperMock = new Mock<ITextureCropper>();
        _capeTextureIdentifierMock = new MockCapeTextureIdentifier();
        _mockSkinTypeIdentifier = new MockSkinTypeIdentifier();

        _mockSkinTypeIdentifier.SetIdentifyResult(SkinType.Slim);
        _capeTextureIdentifierMock.SetTextureNameResult(_defaultCapeData.CapeName);
    }

    [Fact]
    public async Task GetSkin_ReturnsSkinData()
    {
        // Arrange
        var repository = SetupRepository(_defaultSkinData);

        // Act
        var result = await repository.GetSkin(_profileProperties);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultSkinData.TextureSize, result.TextureSize);
        Assert.Equal(_defaultSkinData.TextureBytes, result.TextureBytes);
        Assert.Equal(_defaultSkinData.SkinType, result.SkinType);
    }

    [Fact]
    public async Task GetSkin_ReturnsSkinDataFromCache()
    {
        // Arrange
        var fakeResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new ByteArrayContent(Array.Empty<byte>())
        };

        var cacheMock = new Mock<ICache>();
        cacheMock.Setup(r => r.RetrieveImageAsync(It.IsAny<string>())).ReturnsAsync(_defaultSkinData.TextureBytes);

        var repository = SetupRepository(_defaultSkinData, fakeResponse, cacheMock);

        // Act
        var result = await repository.GetSkin(_profileProperties);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultSkinData.TextureSize, result.TextureSize);
        Assert.Equal(_defaultSkinData.TextureBytes, result.TextureBytes);
        Assert.Equal(_defaultSkinData.SkinType, result.SkinType);
    }

    [Fact]
    public async Task GetSkin_AddSkinDataToCache()
    {
        // Arrange       
        var cacheMock = new Mock<ICache>();
        cacheMock.Setup(r => r.RetrieveImageAsync(It.IsAny<string>())).ReturnsAsync(() => null);

        var repository = SetupRepository(_defaultSkinData, cacheMock: cacheMock);
        // Act
        var result = await repository.GetSkin(_profileProperties);

        // Assert
        cacheMock.Verify(c => c.CacheImageAsync(It.IsAny<string>(), It.IsAny<byte[]>()), Times.AtLeastOnce());

        Assert.NotNull(result);
        Assert.Equal(_defaultSkinData.TextureSize, result.TextureSize);
        Assert.Equal(_defaultSkinData.TextureBytes, result.TextureBytes);
        Assert.Equal(_defaultSkinData.SkinType, result.SkinType);
    }


    [Fact]
    public async Task GetSkin_ReturnsConvertedSkinData()
    {
        // Arrange
        var expectedSkinData = new SkinData { SkinType = SkinType.Slim, TextureBytes = new byte[] { 1, 2, 3, 4, 5, 6 }, TextureSize = new Size(64, 32) };
        var convertedSkinBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        _modernSkinConverterMock.Setup(r => r.ConvertToModernSkin(It.IsAny<byte[]>())).Returns(convertedSkinBytes);

        var repository = SetupRepository(expectedSkinData);
        repository.Options.ConvertLegacySkin = true;

        // Act
        var result = await repository.GetSkin(_profileProperties);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedSkinData.TextureSize, result.TextureSize);
        Assert.Equal(convertedSkinBytes, result.TextureBytes);
        Assert.Equal(expectedSkinData.SkinType, result.SkinType);
    }


    [Fact]
    public async Task GetCape_ReturnsCapeData()
    {
        // Arrange
        var repository = SetupRepository(_defaultCapeData);

        // Act
        var result = await repository.GetCape(_profileProperties);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultCapeData.TextureSize, result.TextureSize);
        Assert.Equal(_defaultCapeData.TextureBytes, result.TextureBytes);
        Assert.Equal(_defaultCapeData.CapeName, result.CapeName);
    }

    [Fact]
    public async Task GetCape_ReturnsCapeDataFromCache()
    {
        // Arrange
        var fakeResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new ByteArrayContent(Array.Empty<byte>())
        };

        var cacheMock = new Mock<ICache>();
        cacheMock.Setup(r => r.RetrieveImageAsync(It.IsAny<string>())).ReturnsAsync(_defaultCapeData.TextureBytes);

        var repository = SetupRepository(_defaultCapeData, fakeResponse, cacheMock);

        // Act
        var result = await repository.GetCape(_profileProperties);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultCapeData.TextureSize, result.TextureSize);
        Assert.Equal(_defaultCapeData.TextureBytes, result.TextureBytes);
        Assert.Equal(_defaultCapeData.CapeName, result.CapeName);
    }

    [Fact]
    public async Task GetCape_AddCapeDataToCache()
    {
        // Arrange
        var cacheMock = new Mock<ICache>();
        cacheMock.Setup(r => r.RetrieveImageAsync(It.IsAny<string>())).ReturnsAsync(() => null);

        var repository = SetupRepository(_defaultCapeData, cacheMock: cacheMock);

        // Act
        var result = await repository.GetCape(_profileProperties);

        // Assert
        cacheMock.Verify(c => c.CacheImageAsync(It.IsAny<string>(), It.IsAny<byte[]>()), Times.AtLeastOnce());

        Assert.NotNull(result);
        Assert.Equal(_defaultCapeData.TextureSize, result.TextureSize);
        Assert.Equal(_defaultCapeData.TextureBytes, result.TextureBytes);
        Assert.Equal(_defaultCapeData.CapeName, result.CapeName);
    }


    [Fact]
    public void GetCape_ReturnsLocalCapeData()
    {
        // Arrange
        var repository = SetupRepository(_defaultCapeData);

        // Act
        var result = repository.GetCapeLocal(_defaultCapeData.TextureBytes);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultCapeData.TextureSize, result.TextureSize);
        Assert.Equal(_defaultCapeData.TextureBytes, result.TextureBytes);
        Assert.Equal(_defaultCapeData.CapeName, result.CapeName);
    }

    [Fact]
    public void GetSkin_ReturnsLocalSkinData()
    {
        // Arrange
        var repository = SetupRepository(_defaultSkinData);

        // Act
        var result = repository.GetSkinLocal(_defaultSkinData.TextureBytes);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_defaultSkinData.TextureSize, result.TextureSize);
        Assert.Equal(_defaultSkinData.TextureBytes, result.TextureBytes);
        Assert.Equal(_defaultSkinData.SkinType, result.SkinType);
    }

    [Fact]
    public void GetSkin_ReturnsConvertedLocalSkinData()
    {
        // Arrange
        var expectedSkinData = new SkinData { SkinType = SkinType.Slim, TextureBytes = new byte[] { 1, 2, 3, 4, 5, 6 }, TextureSize = new Size(64, 32) };
        var convertedSkinBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        _modernSkinConverterMock.Setup(r => r.ConvertToModernSkin(It.IsAny<byte[]>())).Returns(convertedSkinBytes);

        var repository = SetupRepository(expectedSkinData);
        repository.Options.ConvertLegacySkin = true;

        // Act
        var result = repository.GetSkinLocal(expectedSkinData.TextureBytes);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedSkinData.TextureSize, result.TextureSize);
        Assert.Equal(convertedSkinBytes, result.TextureBytes);
        Assert.Equal(expectedSkinData.SkinType, result.SkinType);
    }

    private ProfileTexturesRepository SetupRepository(TextureData textureData, HttpResponseMessage? httpResponseMessage = null, Mock<ICache>? cacheMock = null)
    {

        httpResponseMessage ??= new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new ByteArrayContent(textureData.TextureBytes)
        };

        var fakeHandler = new FakeHttpMessageHandler(httpResponseMessage);
        var httpClientFactory = new MockHttpClientFactory(fakeHandler);
        _mockImageUtilities.SetCalculateSizeReturnValue(textureData.TextureSize);

        var repository = new ProfileTexturesRepository(httpClientFactory, _mockImageUtilities, _modernSkinConverterMock.Object, _textureCropperMock.Object, _capeTextureIdentifierMock, _mockSkinTypeIdentifier);

        if (cacheMock is not null)
            repository.Options.Cache = cacheMock.Object;

        return repository;
    }
}
