﻿using Microsoft.Extensions.DependencyInjection;
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
/// <summary>
/// Provides extension methods for IServiceCollection to add Minecraft skin fetcher related services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds services related to Minecraft skin fetching to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added services.</returns>
    public static IServiceCollection AddMinecraftApiSkins(this IServiceCollection services)
    {
        services.AddScoped<IClientOptionsRepository, ClientOptionsRepository>();
        services.AddScoped<IProfileInformationRepository, ProfileInformationRepository>();
        services.AddScoped<IProfilePropertiesRepository, ProfilePropertiesRepository>();
        services.AddScoped<IProfileTexturesRepository, ProfileTexturesRepository>();
        services.AddScoped<ICapeTextureIdentifier, CapeTextureIdentifier>();
        services.AddScoped<IModernSkinConverter, ModernSkinConverter>();
        services.AddScoped<IImageUtilities, SkiaImageUtilities>();
        services.AddScoped<ITextureCropper, TextureCropper>();
        services.AddScoped<ISkinTypeIdentifier, SkinTypeIdentifier>();
        services.AddScoped<ISkinsClient, SkinsClient>();

        AddHttpClient(services);

        return services;
    }

    private static void AddHttpClient(IServiceCollection services)
    {
        services.AddHttpClient(nameof(Skins), client => client.AddSignature()).AddPolicyHandler(HttpClientExtension.GetRetryPolicy());
    }
}
