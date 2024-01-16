namespace Mojang.Api.Skins.Cache;
/// <summary>
/// Defines an interface for caching mechanisms.
/// </summary>
public interface ICache
{
    /// <summary>
    /// Gets or sets the duration for which items are cached.
    /// </summary>
    TimeSpan CacheDuration { get; set; }

    /// <summary>
    /// Asynchronously caches an image data associated with a key.
    /// </summary>
    /// <param name="key">The key associated with the image data.</param>
    /// <param name="data">The image data to cache.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CacheImageAsync(string key, byte[] data);

    /// <summary>
    /// Asynchronously retrieves cached image data associated with a key.
    /// </summary>
    /// <param name="key">The key of the cached image data.</param>
    /// <returns>A task that represents the asynchronous operation and contains the cached image data. Returns null if the data is not in the cache.</returns>
    Task<byte[]?> RetrieveImageAsync(string key);

    /// <summary>
    /// Adds a new item to the cache or updates an existing item with the specified key.
    /// </summary>
    /// <param name="key">The key for the cache item.</param>
    /// <param name="value">The item to cache.</param>
    void AddOrSet(string key, object value);

    /// <summary>
    /// Retrieves an item from the cache with the specified key.
    /// </summary>
    /// <param name="key">The key of the item to retrieve.</param>
    /// <typeparam name="T">The type of the item to retrieve.</typeparam>
    /// <returns>The cached item, or null if it is not found in the cache.</returns>
    T? Get<T>(string key);

    /// <summary>
    /// Clears all items from the cache.
    /// </summary>
    /// <returns>True if the cache was successfully cleared; otherwise, false.</returns>
    bool Clear();
}
