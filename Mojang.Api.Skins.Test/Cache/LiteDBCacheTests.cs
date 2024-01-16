using Mojang.Api.Skins.Cache;
using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.Test.Mocks;
using Moq;

namespace Mojang.Api.Skins.Test.Cache;
public class LiteDBCacheTests : IDisposable
{
    private LiteDBCache _cache;
    private Mock<IImageUtilities> _mockImageUtilities;

    public LiteDBCacheTests()
    {
        _mockImageUtilities = new Mock<IImageUtilities>();
        _cache = new LiteDBCache(_mockImageUtilities.Object);
    }

    [Fact]
    public void Constructor_CreatesDirectories()
    {
        // Assert
        Assert.True(Directory.Exists(_cache.CacheDirectory));
        Assert.True(Directory.Exists(Path.Combine(_cache.CacheDirectory, "images")));
    }

    [Fact]
    public void AddOrSet_AddsItemToCache()
    {
        // Arrange
        string key = "testKey";
        object value = "testValue";

        // Act
        _cache.AddOrSet(key, value);

        // Assert
        var result = _cache.Get<object>(key);
        Assert.Equal(value, result);
    }

    [Fact]
    public void Get_ReturnsNullForNonExistentKey()
    {
        // Arrange
        string key = "nonExistentKey";

        // Act
        var result = _cache.Get<object>(key);

        // Assert
        Assert.Null(result);
    }



    [Fact]
    public async Task CacheImageAsync_CachesImageCorrectly()
    {
        // Arrange
        var mockImageUtilities = new MockImageUtilities();
        string key = "imageKey";
        byte[] imageData = new byte[] { 0, 1, 2, 3, 4 };

        var cache = new LiteDBCache(mockImageUtilities);

        // Act
        await cache.CacheImageAsync(key, imageData);

        // Assert
        var imagePath = Path.Combine(cache.CacheDirectory, "images", BitConverter.ToString(imageData).Replace("-", "") + ".png");
        Assert.True(File.Exists(imagePath));
    }




    [Fact]
    public async Task RetrieveImageAsync_ReturnsNullForNonExistentKey()
    {
        // Arrange
        string key = "nonExistentKey";

        // Act
        var result = await _cache.RetrieveImageAsync(key);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Clear_ClearsCacheAsync()
    {
        // Arrange
        string key = "testKey";
        byte[] testData = new byte[] { 1, 2, 3, 4, 5 };

        var mockImageUtilities = new MockImageUtilities();
        byte[] imageData = new byte[] { 0, 1, 2, 3, 4 };

        var cache = new LiteDBCache(mockImageUtilities);
        cache.AddOrSet(key, testData);
        await cache.CacheImageAsync(key, imageData);
        // Act

        var result = cache.Clear();

        // Assert
        Assert.True(result);

        var directories = Directory.GetDirectories(_cache.CacheDirectory);
        foreach (var directory in directories)
        {
            Assert.Empty(Directory.GetFiles(directory));
        }    
    }

    [Fact]
    public void CacheDuration_IsSetCorrectly()
    {
        // Arrange
        var duration = TimeSpan.FromMinutes(30);

        // Act
        _cache.CacheDuration = duration;

        // Assert
        Assert.Equal(duration, _cache.CacheDuration);
    }

    public void Dispose()
    {
        var directories = Directory.GetDirectories(_cache.CacheDirectory);
        foreach (var directory in directories)
        {
            Directory.Delete(directory, true);
        }
    }
}
