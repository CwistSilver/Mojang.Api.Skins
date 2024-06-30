using Microsoft.Extensions.Caching.Memory;
using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.Utilities;

namespace Mojang.Api.Skins.Cache;

public sealed class MemoryCacheWrapper : ICache
{
    private const string HashKeyLable = "HashKeys-{0}";

    private readonly HashSet<string> _keys = [];
    private readonly IMemoryCache _memoryCache;
    private readonly IImageUtilities _imageUtilities;
    public TimeSpan CacheDuration { get; set; } = TimeSpan.FromMinutes(60);

    public MemoryCacheWrapper(IMemoryCache memoryCache, IImageUtilities imageUtilities)
    {
        _memoryCache = memoryCache;
        _imageUtilities = imageUtilities;
    }

    public MemoryCacheWrapper(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _imageUtilities = new SkiaImageUtilities();
    }

    public void AddOrSet(string key, object value)
    {
        key = key.ToLower();

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = CacheDuration
        };
        _memoryCache.Set(key, value, options);
        _keys.Add(key);
    }

    public T? Get<T>(string key) => _memoryCache.TryGetValue(key.ToLower(), out T? value) ? value : default;


    public Task<byte[]?> RetrieveImageAsync(string key)
    {
        var hexaHash = GetHashKey(key);
        if (hexaHash is null) return Task.FromResult<byte[]?>(null);

        if (_memoryCache.TryGetValue(hexaHash, out byte[]? value))
            return Task.FromResult(value);

        return Task.FromResult<byte[]?>(null);
    }

    public Task CacheImageAsync(string key, byte[] data)
    {
        var imageHash = _imageUtilities.HashImage(data);
        var hexaHash = imageHash.ToHexString();
        AddOrSet(string.Format(HashKeyLable, key), hexaHash);
        AddOrSet(hexaHash, data);

        return Task.CompletedTask;
    }

    public bool Clear()
    {
        foreach (var key in _keys)
            _memoryCache.Remove(key);

        _keys.Clear();
        return true;
    }

    private string? GetHashKey(string key)
    {
        if (!_memoryCache.TryGetValue(string.Format(HashKeyLable, key), out string? hashKey))
            return null;

        return hashKey;
    }
}
