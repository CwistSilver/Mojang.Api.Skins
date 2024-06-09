using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;
using System.Net.Http.Json;

namespace Mojang.Api.Skins.Repository.MinecraftProfileProperties;
public sealed class ProfilePropertiesRepository : IProfilePropertiesRepository
{
    private const string MinecraftProfilePropertiesURL = "https://sessionserver.mojang.com/session/minecraft/profile/{0}";

    private readonly object _lock = new();
    private ClientOptions _options = new();
    public ClientOptions Options
    {
        get
        {
            lock (_lock)
                return _options;
        }
        set
        {
            lock (_lock)
                _options = value;
        }
    }


    private readonly HttpClient _httpClient;
    public ProfilePropertiesRepository(IHttpClientFactory httpClientFactory) => _httpClient = httpClientFactory.CreateClient(nameof(Skins));

    public async Task<ProfileProperties> Get(Guid playerUUID)
    {
        string cacheKey = $"ProfileProperties-{playerUUID}";

        if (Options.Cache is not null)
        {
            ProfileProperties? cachedProfileProperties;
            lock (_lock)
                cachedProfileProperties = Options.Cache.Get<ProfileProperties>(cacheKey);

            if (cachedProfileProperties is not null)
                return cachedProfileProperties;
        }

        var response = await _httpClient.GetAsync(string.Format(MinecraftProfilePropertiesURL, playerUUID.ToString("N"))).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync(JsonContext.Default.ApiErrorResponse).ConfigureAwait(false);
            var errorMessage = errorResponse != null ? errorResponse.ErrorMessage : "Unknown error occurred.";
            throw new HttpRequestException(errorMessage);
        }

        var profileProperties = await response.Content.ReadFromJsonAsync(JsonContext.Default.ProfileProperties).ConfigureAwait(false);

        lock (_lock)
            Options.Cache?.AddOrSet(cacheKey, profileProperties!);

        return profileProperties!;
    }
}
