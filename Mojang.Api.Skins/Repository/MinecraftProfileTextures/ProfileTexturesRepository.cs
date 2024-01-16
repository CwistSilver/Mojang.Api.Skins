using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.ImageService.Identifier.Cape;
using Mojang.Api.Skins.ImageService.Identifier.Skin;
using Mojang.Api.Skins.ImageService.SkinConverter;
using Mojang.Api.Skins.Utilities.TextureCropper;
using System.Text.Json;

namespace Mojang.Api.Skins.Repository.MinecraftProfileTextures;
public sealed class ProfileTexturesRepository : IProfileTexturesRepository
{
    private const string TexturesProperty = "textures";
    private const int LegacySkinHeight = 32;

    public ClientOptions Options { get; set; } = new();


    private readonly HttpClient _httpClient;
    private readonly IModernSkinConverter _modernSkinConverter;
    private readonly IImageUtilities _imageUtilities;
    private readonly ITextureCropper _textureCropper;
    private readonly ICapeTextureIdentifier _capeTextureIdentifier;
    private readonly ISkinTypeIdentifier _skinTypeIdentifier;
    public ProfileTexturesRepository(IHttpClientFactory httpClientFactory, IImageUtilities imageUtilities, IModernSkinConverter modernSkinConverter, ITextureCropper textureCropper, ICapeTextureIdentifier capeTextureIdentifier, ISkinTypeIdentifier skinTypeIdentifier)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(Skins));
        _imageUtilities = imageUtilities;
        _modernSkinConverter = modernSkinConverter;
        _textureCropper = textureCropper;
        _capeTextureIdentifier = capeTextureIdentifier;
        _skinTypeIdentifier = skinTypeIdentifier;
    }


    public async Task<SkinData> GetSkin(ProfileProperties profileProperties) => await FetchSkinBytes(profileProperties).ConfigureAwait(false);
    public async Task<CapeData?> GetCape(ProfileProperties profileProperties) => await FetchCapeBytes(profileProperties).ConfigureAwait(false);

    public SkinData GetSkinLocal(in byte[] skinBytes)
    {
        var textureSize = _imageUtilities.CalculateSize((ReadOnlySpan<byte>)skinBytes.AsSpan());
        if (Options.ConvertLegacySkin && textureSize.Height == 32)
            return CreateSkinData(_modernSkinConverter.ConvertToModernSkin(skinBytes));

        return CreateSkinData(skinBytes);
    }

    public CapeData GetCapeLocal(in byte[] capeData) => CreateCapeData(capeData);

    private async Task<SkinData> FetchSkinBytes(ProfileProperties profileProperties)
    {
        var profileTextureInformation = DecodeProfileTextureInformation(profileProperties);
        if (profileTextureInformation.Textures?.Skin is null)
            throw new InvalidOperationException("Skin information is missing in the texture data.");

        var skinKey = $"{profileTextureInformation.ProfileId}-skin";

        byte[]? skinImage;
        if (Options.Cache is not null)
        {
            skinImage = await Options.Cache!.RetrieveImageAsync(skinKey);
            if (skinImage is not null)
                return CreateSkinData(skinImage);
        }

        skinImage = await FetchTextureBytes(profileTextureInformation.Textures.Skin.Url!).ConfigureAwait(false);

        if (Options.ConvertLegacySkin)
        {
            var textureSize = _imageUtilities.CalculateSize((ReadOnlySpan<byte>)skinImage.AsSpan());
            if (textureSize.Height == LegacySkinHeight)
                skinImage = _modernSkinConverter.ConvertToModernSkin(skinImage);
        }

        if (Options.Cache is not null)
            await Options.Cache.CacheImageAsync(skinKey, skinImage);

        var skinData = CreateSkinData(skinImage);
        skinData.TextureUrl = profileTextureInformation.Textures.Skin.Url;
        return skinData;
    }

    private async Task<CapeData?> FetchCapeBytes(ProfileProperties profileProperties)
    {
        var profileTextureInformation = DecodeProfileTextureInformation(profileProperties);
        if (profileTextureInformation.Textures?.Cape is null)
            return null;

        var playerCapeKey = $"{profileTextureInformation.ProfileId}-cape";
        byte[]? cachedCapeBytes;
        if (Options.Cache is not null)
        {
            cachedCapeBytes = await Options.Cache!.RetrieveImageAsync(playerCapeKey);
            if (cachedCapeBytes is not null)
                return CreateCapeData(cachedCapeBytes);
        }

        var capeBytes = await FetchTextureBytes(profileTextureInformation.Textures.Cape.Url!).ConfigureAwait(false);

        if (Options.Cache is not null)
            await Options.Cache.CacheImageAsync(playerCapeKey, capeBytes);

        var capeData = CreateCapeData(capeBytes);
        capeData.TextureUrl = profileTextureInformation.Textures.Cape.Url;
        return capeData;
    }

    private CapeData CreateCapeData(in byte[] capeData)
    {
        return new CapeData(_textureCropper, _imageUtilities)
        {
            TextureBytes = capeData,
            TextureSize = _imageUtilities.CalculateSize(capeData),
            CapeName = _capeTextureIdentifier.GetTextureName(capeData) ?? "Unknown"
        };
    }

    private SkinData CreateSkinData(in byte[] skinBytes)
    {
        var skinType = _skinTypeIdentifier.Identify(skinBytes);

        return new SkinData(_textureCropper, _imageUtilities)
        {
            TextureSize = _imageUtilities.CalculateSize((ReadOnlySpan<byte>)skinBytes.AsSpan()),
            TextureBytes = skinBytes,
            SkinType = skinType
        };
    }

    private async Task<byte[]> FetchTextureBytes(string textureUrl)
    {
        if (string.IsNullOrEmpty(textureUrl))
            throw new ArgumentException("Texture URL is missing or empty.", nameof(textureUrl));

        var response = await _httpClient.GetAsync(textureUrl).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch texture. Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");

        return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
    }

    private ProfileTextureInformation DecodeProfileTextureInformation(ProfileProperties profileProperties)
    {
        if (profileProperties.Properties is null)
            throw new ArgumentNullException(nameof(profileProperties), "Properties collection is null.");

        var texturesProperty = profileProperties.Properties.FirstOrDefault(property => property.Name == TexturesProperty) ??
            throw new InvalidOperationException($"Textures property is missing inside {nameof(ProfileProperties)}.");

        var base64Bytes = Convert.FromBase64String(texturesProperty.Value);
        var decodedString = System.Text.Encoding.UTF8.GetString(base64Bytes);

        var profileTextureInformation = JsonSerializer.Deserialize<ProfileTextureInformation>(decodedString);

        return profileTextureInformation ?? throw new InvalidOperationException("Failed to deserialize the profile texture information.");
    }
}
