using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;
using System.Net.Http.Json;

namespace Mojang.Api.Skins.Repository.MinecraftProfileProperties;
public sealed class ProfilePropertiesRepository : IProfilePropertiesRepository
{
    private const string MinecraftProfilePropertiesURL = "https://sessionserver.mojang.com/session/minecraft/profile/{0}";

    public ClientOptions Options { get; set; } = new();


    private readonly HttpClient _httpClient;
    public ProfilePropertiesRepository(IHttpClientFactory httpClientFactory) => _httpClient = httpClientFactory.CreateClient(nameof(Skins));

    public async Task<ProfileProperties> Get(Guid playerUUID)
    {
        string cacheKey = $"ProfileProperties-{playerUUID}";

        if (Options.Cache is not null)
        {
            var cachedProfileProperties = Options.Cache.Get<ProfileProperties>(cacheKey);
            if (cachedProfileProperties is not null)
                return cachedProfileProperties;
        }

        var response = await _httpClient.GetAsync(string.Format(MinecraftProfilePropertiesURL, playerUUID.ToString("N"))).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
            var errorMessage = errorResponse != null ? errorResponse.ErrorMessage : "Unknown error occurred.";
            throw new HttpRequestException(errorMessage);
        }

        var profileProperties = await response.Content.ReadFromJsonAsync<ProfileProperties>();

        Options.Cache?.AddOrSet(cacheKey, profileProperties!);

        return profileProperties!;
    }
}
