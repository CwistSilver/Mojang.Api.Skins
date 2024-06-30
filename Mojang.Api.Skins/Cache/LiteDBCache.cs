using LiteDB;
using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.Utilities;

namespace Mojang.Api.Skins.Cache;

public sealed class LiteDBCache : ICache
{
    private const string CacheCollectionName = "cache";
    private const string ImageCacheCollectionName = "imagecache";
    private const string FileName = "lite-db.db";
    private const string ImageDirectoryName = "images";

    /// <summary>
    /// The folder in which the cache is saved
    /// </summary>
    public string CacheDirectory => _cacheDirectory;
    public TimeSpan CacheDuration { get; set; } = TimeSpan.FromMinutes(60);

    private readonly string _imageCacheDirectory;
    private readonly string _storagePath;
    private readonly string _cacheDirectory;
    private readonly IImageUtilities _imageUtilities;
    private readonly FileStream _fileStream;
    public LiteDBCache(IImageUtilities imageUtilities)
    {
        _imageUtilities = imageUtilities;
        _cacheDirectory = GetCacheDirectory();
        _storagePath = Path.Combine(_cacheDirectory, FileName);
        _imageCacheDirectory = Path.Combine(_cacheDirectory, ImageDirectoryName);

        Directory.CreateDirectory(_cacheDirectory);
        Directory.CreateDirectory(_imageCacheDirectory);
        _fileStream = new FileStream(_storagePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
    }

    public void AddOrSet(string key, object value)
    {
        using var db = new LiteDatabase(_fileStream);

        var col = db.GetCollection<LiteDBCacheItem>(CacheCollectionName);
        var item = new LiteDBCacheItem { Key = key, Value = value, LastUpdated = DateTime.UtcNow };
        col.Upsert(item);
    }

    public T? Get<T>(string key)
    {
        using var db = new LiteDatabase(_fileStream);

        var col = db.GetCollection<LiteDBCacheItem>(CacheCollectionName);
        var result = col.FindOne(x => x.Key == key);
        if (result == default)
            return default;

        if (DateTime.UtcNow - result.LastUpdated > CacheDuration)
        {
            return default;
        }

        return (T)result.Value;
    }

    public async Task CacheImageAsync(string key, byte[] data)
    {
        var imageHash = _imageUtilities.HashImage(data);
        var hexaHash = imageHash.ToHexString();
        AddImageKey(key, hexaHash);

        var imagePath = Path.Combine(_imageCacheDirectory, $"{hexaHash}.png");
        if (!Directory.Exists(_imageCacheDirectory))
            Directory.CreateDirectory(_imageCacheDirectory);

        await File.WriteAllBytesAsync(imagePath, data);

        File.SetLastWriteTimeUtc(imagePath, DateTime.UtcNow);
    }

    public async Task<byte[]?> RetrieveImageAsync(string key)
    {
        var hexaHash = GetHashKey(key);
        if (hexaHash is null) return null;

        var imagePath = Path.Combine(_imageCacheDirectory, $"{hexaHash}.png");

        if (!File.Exists(imagePath))
            return null;

        var lastWriteTime = File.GetLastWriteTimeUtc(imagePath);
        if (DateTime.UtcNow - lastWriteTime < CacheDuration)
            return await File.ReadAllBytesAsync(imagePath);
        else
            File.Delete(imagePath);

        return null;
    }

    public bool Clear()
    {
        try
        {
            using var db = new LiteDatabase(_fileStream);

            foreach (var collectionName in db.GetCollectionNames())
                db.DropCollection(collectionName);

            Directory.Delete(_imageCacheDirectory, true);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private string? GetHashKey(string key)
    {
        using var db = new LiteDatabase(_fileStream);

        var col = db.GetCollection<LiteDBCacheItem>(ImageCacheCollectionName);
        var result = col.FindOne(x => x.Key == key);
        if (result is null) return null;

        return (string)result.Value;
    }

    private void AddImageKey(string key, string hexaHash)
    {
        using var db = new LiteDatabase(_fileStream);

        var col = db.GetCollection<LiteDBCacheItem>(ImageCacheCollectionName);
        var item = new LiteDBCacheItem { Key = key, Value = hexaHash, LastUpdated = DateTime.UtcNow };
        col.Upsert(item);
    }


    private string GetCacheDirectory() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameof(Skins));
    private class LiteDBCacheItem
    {
        public string Key { get; set; } = string.Empty;
        public object Value { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
    }
}
