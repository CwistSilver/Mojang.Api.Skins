using Mojang.Api.Skins.Cache;
using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.Repository.MinecraftProfileInformation;
using Mojang.Api.Skins.Test.Mocks;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Mojang.Api.Skins.Test.Repository.MinecraftProfileInformation;
public class ProfileInformationRepositoryTests
{
    private readonly Mock<ICache> _mockCache = new Mock<ICache>();
    private readonly ProfileInformation _profileInformation = new ProfileInformation() { Id = Guid.NewGuid(), Name = "FakePlayer" };

    [Fact]
    public async Task Get_ReturnsProfileInformation()
    {
        // Arrange
        var fakeResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(_profileInformation)
        };

        var repository = SetupRepository(fakeResponse);

        // Act
        var result = await repository.Get(_profileInformation.Name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_profileInformation.Name, result.Name);
        Assert.Equal(_profileInformation.Id, result.Id);
    }

    [Fact]
    public async Task Get_ReturnsProfileInformationFromCache()
    {
        // Arrange
        var fakeResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(string.Empty)
        };
        _mockCache.Setup(c => c.Get<Guid>(It.IsAny<string>())).Returns(_profileInformation.Id);

        var repository = SetupRepository(fakeResponse, _mockCache);

        // Act
        var result = await repository.Get(_profileInformation.Name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_profileInformation.Name, result.Name);
        Assert.Equal(_profileInformation.Id, result.Id);
    }

    [Fact]
    public async Task Get_AddProfileInformationToCache()
    {
        // Arrange
        var fakeResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = JsonContent.Create(_profileInformation)
        };
        _mockCache.Setup(c => c.Get<Guid>(It.IsAny<string>())).Returns(default(Guid));
        var repository = SetupRepository(fakeResponse, _mockCache);

        // Act
        var result = await repository.Get(_profileInformation.Name);

        // Assert
        _mockCache.Verify(c => c.AddOrSet(It.IsAny<string>(), It.IsAny<Guid>()), Times.AtLeastOnce());

        Assert.NotNull(result);
        Assert.Equal(_profileInformation.Name, result.Name);
        Assert.Equal(_profileInformation.Id, result.Id);
    }

    [Fact]
    public async Task Get_ThrowsHttpRequestExceptionForNonexistentPlayer()
    {
        // Arrange
        string playerName = "nonexistentPlayer";
        var errorResponse = new
        {
            path = $"/users/profiles/minecraft/{playerName}",
            errorMessage = $"Couldn't find any profile with name {playerName}"
        };

        var fakeResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = JsonContent.Create(errorResponse)
        };

        var repository = SetupRepository(fakeResponse);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(() => repository.Get(playerName));
        Assert.Contains(playerName, exception.Message);
    }

    private static ProfileInformationRepository SetupRepository(HttpResponseMessage httpResponseMessage, Mock<ICache>? cacheMock = null)
    {
        var fakeHandler = new FakeHttpMessageHandler(httpResponseMessage);
        var httpClientFactory = new MockHttpClientFactory(fakeHandler);

        var repository = new ProfileInformationRepository(httpClientFactory);

        if (cacheMock is not null)
            repository.Options.Cache = cacheMock.Object;

        return repository;
    }
}
