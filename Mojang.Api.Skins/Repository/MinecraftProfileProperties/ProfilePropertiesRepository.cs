using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.Repository.Options;
using System.Net.Http.Json;

namespace Mojang.Api.Skins.Repository.MinecraftProfileProperties;
public sealed class ProfilePropertiesRepository(IHttpClientFactory httpClientFactory, IClientOptionsRepository clientOptionsRepository) : IProfilePropertiesRepository
{
    private const string MinecraftProfilePropertiesURL = "https://sessionserver.mojang.com/session/minecraft/profile/{0}";

    private readonly IClientOptionsRepository _clientOptionsRepository = clientOptionsRepository;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task<ProfileProperties> Get(Guid playerUUID)
    {
        string cacheKey = $"ProfileProperties-{playerUUID}";
        var options = await _clientOptionsRepository.GetOptionsAsync().ConfigureAwait(false);
        var cache = options.Cache;

        if (cache is not null)
        {
            ProfileProperties? cachedProfileProperties = cache.Get<ProfileProperties>(cacheKey);

            if (cachedProfileProperties is not null)
                return cachedProfileProperties;
        }

        using var httpClient = _httpClientFactory.CreateClient(nameof(Skins));
        var response = await httpClient.GetAsync(string.Format(MinecraftProfilePropertiesURL, playerUUID.ToString("N"))).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync(JsonContext.Default.ApiErrorResponse).ConfigureAwait(false);
            var errorMessage = errorResponse != null ? errorResponse.ErrorMessage : "Unknown error occurred.";
            throw new HttpRequestException(errorMessage);
        }

        var profileProperties = await response.Content.ReadFromJsonAsync(JsonContext.Default.ProfileProperties).ConfigureAwait(false);

        cache?.AddOrSet(cacheKey, profileProperties!);

        return profileProperties!;
    }
}
