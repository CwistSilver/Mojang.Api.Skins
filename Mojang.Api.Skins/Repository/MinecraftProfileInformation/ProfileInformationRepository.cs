using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.Repository.Options;
using System.Net.Http.Json;

namespace Mojang.Api.Skins.Repository.MinecraftProfileInformation;
public sealed class ProfileInformationRepository(IHttpClientFactory httpClientFactory, IClientOptionsRepository clientOptionsRepository) : IProfileInformationRepository
{
    private const string MinecraftProfilesURL = "https://api.mojang.com/users/profiles/minecraft/{0}";

    private readonly IClientOptionsRepository _clientOptionsRepository = clientOptionsRepository;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<ProfileInformation> Get(string playerName)
    {
        string cacheKey = $"ProfileInformation-{playerName}";
        var options = await _clientOptionsRepository.GetOptionsAsync().ConfigureAwait(false);
        var cache = options.Cache;

        if (cache is not null)
        {
            Guid cachedPlayerUUID = cache.Get<Guid>(cacheKey);

            if (cachedPlayerUUID != default)
                return new ProfileInformation() { Name = playerName, Id = cachedPlayerUUID };
        }

        using var httpClient = _httpClientFactory.CreateClient(nameof(Skins));
        var response = await httpClient.GetAsync(string.Format(MinecraftProfilesURL, playerName)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync(JsonContext.Default.ApiErrorResponse).ConfigureAwait(false);
            var errorMessage = errorResponse != null ? errorResponse.ErrorMessage : "Unknown error occurred.";
            throw new HttpRequestException(errorMessage);
        }

        var profileInformation = await response.Content.ReadFromJsonAsync(JsonContext.Default.ProfileInformation).ConfigureAwait(false);

        cache?.AddOrSet(cacheKey, profileInformation!.Id);

        return profileInformation!;
    }
}
