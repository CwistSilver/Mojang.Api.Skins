using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;
using System.Net.Http.Json;
using System.Text.Json;

namespace Mojang.Api.Skins.Repository.MinecraftProfileInformation;
public sealed class ProfileInformationRepository : IProfileInformationRepository
{
    private const string MinecraftProfilesURL = "https://api.mojang.com/users/profiles/minecraft/{0}";

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
    public ProfileInformationRepository(IHttpClientFactory httpClientFactory) => _httpClient = httpClientFactory.CreateClient(nameof(Skins));

    public async Task<ProfileInformation> Get(string playerName)
    {
        string cacheKey = $"ProfileInformation-{playerName}";


        if (Options.Cache is not null)
        {
            Guid cachedPlayerUUID;
            lock (_lock)
                cachedPlayerUUID = Options.Cache.Get<Guid>(cacheKey);

            if (cachedPlayerUUID != default)
                return new ProfileInformation() { Name = playerName, Id = cachedPlayerUUID };
        }

        var response = await _httpClient.GetAsync(string.Format(MinecraftProfilesURL, playerName)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>().ConfigureAwait(false); ;
            var errorMessage = errorResponse != null ? errorResponse.ErrorMessage : "Unknown error occurred.";
            throw new HttpRequestException(errorMessage);
        }

        var profileInformation = await response.Content.ReadFromJsonAsync<ProfileInformation>().ConfigureAwait(false);

        lock (_lock)
            Options.Cache?.AddOrSet(cacheKey, profileInformation!.Id);

        return profileInformation!;
    }
}
