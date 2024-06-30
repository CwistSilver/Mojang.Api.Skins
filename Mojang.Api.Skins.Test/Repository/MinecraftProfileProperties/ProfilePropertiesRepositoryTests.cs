using Mojang.Api.Skins.Cache;
using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.Repository.MinecraftProfileProperties;
using Mojang.Api.Skins.Test.Mocks;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Mojang.Api.Skins.Test.Repository.MinecraftProfileProperties;
public class ProfilePropertiesRepositoryTests
{
    private readonly Mock<ICache> _mockCache = new();
    private readonly ProfileProperties _profileProperties = new()
    {
        Name = "FakePlayer",
        Id = Guid.NewGuid(),
        Properties = [
            new ProfileProperty() {
                Name = "textures",
                Value = "ewogICJ0aW1lc3RhbXAiIDogMTcwNTI0MDM4NTE1MSwKICAicHJvZmlsZUlkIiA6ICJlZGM2MzE5YjQ5NjM0ZDhmYmZkNTI1N2QxNzg5N2I0NSIsCiAgInByb2ZpbGVOYW1lIiA6ICJDd2lzdFNpbHYzciIsCiAgInRleHR1cmVzIiA6IHsKICAgICJTS0lOIiA6IHsKICAgICAgInVybCIgOiAiaHR0cDovL3RleHR1cmVzLm1pbmVjcmFmdC5uZXQvdGV4dHVyZS83ZTNjNWQ1MTM4MTE1YTRmNjBjYTRmMGMwMTEyZjk3NmFmYmJjZjk3MGNmY2Y5ZWM1NDk0NDMyNTQ1Njg0NWIxIgogICAgfSwKICAgICJDQVBFIiA6IHsKICAgICAgInVybCIgOiAiaHR0cDovL3RleHR1cmVzLm1pbmVjcmFmdC5uZXQvdGV4dHVyZS8yMzQwYzBlMDNkZDI0YTExYjE1YThiMzNjMmE3ZTllMzJhYmIyMDUxYjI0ODFkMGJhN2RlZmQ2MzVjYTdhOTMzIgogICAgfQogIH0KfQ=="
            }
        ]
    };

    [Fact]
    public async Task Get_ReturnsProfilePropertiesFromCache()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(string.Empty)
        };

        _mockCache.Setup(c => c.Get<ProfileProperties>(It.IsAny<string>())).Returns((ProfileProperties?)_profileProperties);
        var repository = SetupRepository(httpResponseMessage, _mockCache);

        // Act
        var result = await repository.Get(_profileProperties.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_profileProperties.Name, result.Name);
        Assert.Equal(_profileProperties.Id, result.Id);
        Assert.Equal(_profileProperties.Properties!.Length, result.Properties!.Length);
        Assert.Equal(_profileProperties.Properties[0].Name, result.Properties[0].Name);
        Assert.Equal(_profileProperties.Properties[0].Value, result.Properties[0].Value);
    }

    [Fact]
    public async Task Get_AddProfilePropertiesToCache()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(_profileProperties)
        };

        _mockCache.Setup(c => c.Get<ProfileProperties>(It.IsAny<string>())).Returns((ProfileProperties?)null);
        var repository = SetupRepository(httpResponseMessage, _mockCache);

        // Act
        var result = await repository.Get(_profileProperties.Id);

        // Assert
        _mockCache.Verify(c => c.AddOrSet(It.IsAny<string>(), It.IsAny<ProfileProperties>()), Times.AtLeastOnce());

        Assert.NotNull(result);
        Assert.Equal(_profileProperties.Name, result.Name);
        Assert.Equal(_profileProperties.Id, result.Id);
        Assert.Equal(_profileProperties.Properties!.Length, result.Properties!.Length);
        Assert.Equal(_profileProperties.Properties[0].Name, result.Properties[0].Name);
        Assert.Equal(_profileProperties.Properties[0].Value, result.Properties[0].Value);
    }

    [Fact]
    public async Task Get_ReturnsProfileProperties()
    {
        // Arrange
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(_profileProperties)
        };

        var repository = SetupRepository(httpResponseMessage);

        // Act
        var result = await repository.Get(_profileProperties.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_profileProperties.Name, result.Name);
        Assert.Equal(_profileProperties.Id, result.Id);
        Assert.Equal(_profileProperties.Properties!.Length, result.Properties!.Length);
        Assert.Equal(_profileProperties.Properties[0].Name, result.Properties[0].Name);
        Assert.Equal(_profileProperties.Properties[0].Value, result.Properties[0].Value);
    }

    [Fact]
    public async Task Get_ThrowsHttpRequestExceptionForNonexistentPlayerUUID()
    {
        // Arrange
        var errorResponse = new ApiErrorResponse { ErrorMessage = $"Couldn't find profile with UUID {_profileProperties.Id:N}" };

        var fakeResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = JsonContent.Create(errorResponse)
        };

        var repository = SetupRepository(fakeResponse);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => repository.Get(_profileProperties.Id));
        Assert.Contains($"Couldn't find profile with UUID {_profileProperties.Id:N}", exception.Message);
    }


    private static ProfilePropertiesRepository SetupRepository(HttpResponseMessage httpResponseMessage, Mock<ICache>? cacheMock = null)
    {
        var fakeHandler = new FakeHttpMessageHandler(httpResponseMessage);
        var httpClientFactory = new MockHttpClientFactory(fakeHandler);
        var mockClientOptionsRepository = new MockClientOptionsRepository();
        mockClientOptionsRepository.Options.Cache = cacheMock?.Object;

        var repository = new ProfilePropertiesRepository(httpClientFactory, mockClientOptionsRepository);

        return repository;
    }
}

