using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.Data.MojangApi;
using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.ImageService.Identifier.Cape;
using Mojang.Api.Skins.ImageService.Identifier.Skin;
using Mojang.Api.Skins.ImageService.SkinConverter;
using Mojang.Api.Skins.Repository.MinecraftProfileInformation;
using Mojang.Api.Skins.Repository.MinecraftProfileProperties;
using Mojang.Api.Skins.Repository.MinecraftProfileTextures;
using Mojang.Api.Skins.Repository.Options;
using Mojang.Api.Skins.Utilities;
using Mojang.Api.Skins.Utilities.TextureCropper;

namespace Mojang.Api.Skins;
public sealed class SkinsClient : ISkinsClient
{
    public ClientOptions Options
    {
        get
        {
            return _clientOptionsRepository.GetOptions();
        }
        set
        {
            _clientOptionsRepository.SetOptionsAsync(value).ConfigureAwait(false);
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
    private readonly IClientOptionsRepository _clientOptionsRepository;

    public SkinsClient(IProfileInformationRepository profileInformationRepository, IProfilePropertiesRepository profilePropertiesRepository, IProfileTexturesRepository profileTexturesRepository, ICapeTextureIdentifier capeTextureIdentifier, IImageUtilities imageUtilities, ITextureCropper skinPartCropper, IModernSkinConverter modernSkinConverter, ISkinTypeIdentifier skinTypeIdentifier, IClientOptionsRepository clientOptionsRepository)
    {
        _clientOptionsRepository = clientOptionsRepository;
        _clientOptionsRepository.SetOptionsAsync(ClientOptions.Default).ConfigureAwait(false);
        _profileInformationRepository = profileInformationRepository;
        _profilePropertiesRepository = profilePropertiesRepository;
        _profileTexturesRepository = profileTexturesRepository;

        _capeTextureIdentifier = capeTextureIdentifier;
        _imageUtilities = imageUtilities;
        _textureCropper = skinPartCropper;
        _modernSkinConverter = modernSkinConverter;
        _skinTypeIdentifier = skinTypeIdentifier;
    }

    public SkinsClient()
    {
        _clientOptionsRepository = new ClientOptionsRepository();
        _clientOptionsRepository.SetOptionsAsync(ClientOptions.Default).ConfigureAwait(false);

        _httpClientFactory = new DefaultHttpClientFactory();
        _imageUtilities = new SkiaImageUtilities();
        _capeTextureIdentifier = new CapeTextureIdentifier(_imageUtilities);
        _skinTypeIdentifier = new SkinTypeIdentifier(_imageUtilities);
        _textureCropper = new TextureCropper(_imageUtilities);
        _modernSkinConverter = new ModernSkinConverter(_imageUtilities, _textureCropper);
        _profileInformationRepository = new ProfileInformationRepository(_httpClientFactory, _clientOptionsRepository);
        _profilePropertiesRepository = new ProfilePropertiesRepository(_httpClientFactory, _clientOptionsRepository);
        _profileTexturesRepository = new ProfileTexturesRepository(_httpClientFactory, _imageUtilities, _modernSkinConverter, _textureCropper, _capeTextureIdentifier, _skinTypeIdentifier, _clientOptionsRepository);
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
