using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.ImageService.Identifier.Cape;
using Mojang.Api.Skins.ImageService.Identifier.Skin;
using Mojang.Api.Skins.ImageService.SkinConverter;
using Mojang.Api.Skins.Repository.MinecraftProfileInformation;
using Mojang.Api.Skins.Repository.MinecraftProfileProperties;
using Mojang.Api.Skins.Repository.MinecraftProfileTextures;
using Mojang.Api.Skins.Utilities;
using Mojang.Api.Skins.Utilities.TextureCropper;

namespace Mojang.Api.Skins;
public sealed class SkinsClient : ISkinsClient
{
    private readonly object _lock = new();
    private ClientOptions _options = ClientOptions.Default;
    public ClientOptions Options
    {
        get
        {
            lock (_lock)
                return _options;
        }
        set
        {
            if (value is null)
                return;

            lock (_lock)
            {
                _options = value;
                _profileInformationRepository.Options = _options;
                _profilePropertiesRepository.Options = _options;
                _profileTexturesRepository.Options = _options;
            }
        }
    }

    private readonly IProfileInformationRepository _profileInformationRepository;
    private readonly IProfilePropertiesRepository _profilePropertiesRepository;
    private readonly IProfileTexturesRepository _profileTexturesRepository;
    private readonly ICapeTextureIdentifier _capeTextureIdentifier;
    private readonly IImageUtilities _imageUtilities;
    private readonly ITextureCropper _textureCropper;
    private readonly IModernSkinConverter _modernSkinConverter;
    private readonly ISkinTypeIdentifier _skinTypeIdentifier;
    private readonly IHttpClientFactory? _httpClientFactory;

    public SkinsClient(IProfileInformationRepository profileInformationRepository, IProfilePropertiesRepository profilePropertiesRepository, IProfileTexturesRepository profileTexturesRepository, ICapeTextureIdentifier capeTextureIdentifier, IImageUtilities imageUtilities, ITextureCropper skinPartCropper, IModernSkinConverter modernSkinConverter, ISkinTypeIdentifier skinTypeIdentifier)
    {
        _profileInformationRepository = profileInformationRepository;
        _profileInformationRepository.Options = Options;

        _profilePropertiesRepository = profilePropertiesRepository;
        _profilePropertiesRepository.Options = Options;

        _profileTexturesRepository = profileTexturesRepository;
        _profileTexturesRepository.Options = Options;

        _capeTextureIdentifier = capeTextureIdentifier;
        _imageUtilities = imageUtilities;
        _textureCropper = skinPartCropper;
        _modernSkinConverter = modernSkinConverter;
        _skinTypeIdentifier = skinTypeIdentifier;
    }

    public SkinsClient()
    {
        _httpClientFactory = new DefaultHttpClientFactory();
        _imageUtilities = new SkiaImageUtilities();
        _capeTextureIdentifier = new CapeTextureIdentifier(_imageUtilities);
        _skinTypeIdentifier = new SkinTypeIdentifier(_imageUtilities);
        _textureCropper = new TextureCropper(_imageUtilities);
        _modernSkinConverter = new ModernSkinConverter(_imageUtilities, _textureCropper);
        _profileInformationRepository = new ProfileInformationRepository(_httpClientFactory) { Options = Options };
        _profilePropertiesRepository = new ProfilePropertiesRepository(_httpClientFactory) { Options = Options };
        _profileTexturesRepository = new ProfileTexturesRepository(_httpClientFactory, _imageUtilities, _modernSkinConverter, _textureCropper, _capeTextureIdentifier, _skinTypeIdentifier) { Options = Options };
    }

    public async Task<PlayerData> GetAsync(string playerName)
    {
        var profileInformation = await _profileInformationRepository.Get(playerName).ConfigureAwait(false);
        var profileProperties = await _profilePropertiesRepository.Get(profileInformation.Id).ConfigureAwait(false);
        return await GetSkinData(profileProperties);
    }

    public async Task<PlayerData> GetAsync(Guid playerUUID)
    {
        var profileProperties = await _profilePropertiesRepository.Get(playerUUID).ConfigureAwait(false);
        return await GetSkinData(profileProperties);
    }

    public PlayerData GetLocal(byte[] skinFileBytes, byte[]? capeFileBytes = null)
    {
        var skinData = _profileTexturesRepository.GetSkinLocal(skinFileBytes);

        CapeData? capeData = null;
        if (capeFileBytes is not null)
            capeData = _profileTexturesRepository.GetCapeLocal(capeFileBytes);

        return new PlayerData()
        {
            Skin = skinData,
            Cape = capeData
        };
    }

    private async Task<PlayerData> GetSkinData(ProfileProperties profileProperties)
    {
        var skinTask = _profileTexturesRepository.GetSkin(profileProperties);
        var capeTask = _profileTexturesRepository.GetCape(profileProperties);

        await Task.WhenAll(skinTask, capeTask).ConfigureAwait(false);

        var skinData = await skinTask;
        var capeData = await capeTask;


        return new PlayerData()
        {
            Name = profileProperties.Name,
            UUID = profileProperties.Id,
            Skin = skinData,
            Cape = capeData
        };
    }
}
