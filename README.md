# Mojang.Api.Skins
<img width="160" height="auto" src="https://github.com/CwistSilver/Mojang.Api.Skins/raw/main/icon.png">

[![Mojang.Api.Skins](https://img.shields.io/nuget/vpre/Mojang.Api.Skins.svg?cacheSeconds=3600&label=Mojang.Api.Skins%20nuget)](https://www.nuget.org/packages/Mojang.Api.Skins)
[![NuGet](https://img.shields.io/nuget/dt/Mojang.Api.Skins.svg?cacheSeconds=3600&label=Downloads)](https://www.nuget.org/packages/Mojang.Api.Skins)

## Overview
Mojang.Api.Skins is a .NET library designed to interact with the Mojang API and provides an efficient way to retrieve Minecraft skin and cape textures. It supports retrieving textures by player name or UUID and includes functionality for handling local and remote texture data.

## Features
- **Fetch Minecraft Skins and Capes**: Retrieve textures for Minecraft player skins and capes using names or UUIDs.
- **Legacy Skin Conversion**: Automatically converts legacy Minecraft skins (64x32 format) to the modern format (64x64). For more detailed information on skins, visit the [Minecraft Wiki](https://minecraft.wiki/w/Skin#:~:text=Java%20Edition%20map.-,Templates,-%5Bedit%20%7C).
- **Automatic cape identification**: Automatic name identification of official Minecraft Capes.
- **Caching**: Includes a default caching mechanism and supports custom cache implementations.
- **Smart Texture Storage**: The default cache identifies and stores unique textures only once, even when multiple players share the same texture, saving significant storage space.
- **Texture Processing**: Extract and combine specific parts of skins and capes, enabling detailed texture manipulation.
- **'Dependency Injection Support'**: Easily integrate **'Mojang.Api.Skins'** with .NET Dependency Injection systems, allowing for streamlined management and usage within your projects.

## Godot Showcase
Experience **'Mojang.Api.Skins'** in action with the [Godot](https://github.com/godotengine/godot) Showcase. This demonstration project, named **'Mojang.Api.Skins.Demo.Godot'**, illustrates the practical application of our .NET library in a Godot environment. It demonstrates real-time rendering of Minecraft skins and capes on 3D models, offering an interactive experience for users. This showcase highlights the versatility and user-friendly nature of our library. For a hands-on experience and to learn more, visit the **'Mojang.Api.Skins.Demo.Godot'** [repository](https://github.com/CwistSilver/Mojang.Api.Skins.Demo.Godot).

## Caching
Mojang.Api.Skins includes a versatile caching system, enhancing performance by reducing API calls and storing data locally.

### Using the Default Cache
The library utilizes **'LiteDBCache'** by default, for efficient caching:

#### Efficient Texture Management
- Recognizes duplicate textures across different players and stores a single instance, reducing storage requirements.
- Suitable for scenarios where multiple players might share the same skin or cape texture.

### Using the Memory Cache
For scenarios where in-memory caching is preferred, you can use the MemoryCacheWrapper:
```cs
var client = new SkinsClient();

var cacheOptions = new MemoryCacheOptions() { CompactionPercentage = 0.50 };
var cache = new MemoryCache(cacheOptions);
client.Options.Cache = new MemoryCacheWrapper(cache);
```

### Custom Cache Implementation
Implement the **'ICache'** interface to create and use a custom cache:

```cs
public class YourCustomCache : ICache
{
    // Custom cache implementation
}

var client = new SkinsClient();
client.Options.Cache = new YourCustomCache();
```

### Direct API Interaction (No Caching)
To interact directly with the Mojang API without caching:
```cs
var client = new SkinsClient();
client.Options.Cache = null; // Disables caching
```

## Legacy Skin Conversion
Mojang.Api.Skins automatically detects and converts legacy Minecraft skins (64x32 format) to the modern format (64x64). This feature ensures compatibility with older skins like Notch's original skin.

### Enabling/Disabling Legacy Conversion
Legacy skin conversion is enabled by default but can be toggled off if needed:
```cs
var client = new SkinsClient();
client.Options.ConvertLegacySkin = false; // Disables legacy skin conversion
```

## Usage
**'Mojang.Api.Skins'** enables you to retrieve player data in multiple ways. The returned **'PlayerData'** instance varies based on the method used.
Here are some usage examples:

### Fetch Player Data by UUID
To obtain player data using a UUID:
```cs
Guid playerUUID = Guid.NewGuid(); // Replace this with a real UUID
var client = new SkinsClient();
var playerData = await client.GetAsync(playerUUID);
```

### Fetch Player Data by Name
You can also retrieve player data based on the player's name:
```cs
var client = new SkinsClient();
var playerData = await client.GetAsync("Notch");
```

### Get Local Player Skin Without Cape
To load a local player skin without a cape:
```cs
var client = new SkinsClient();
var skinBytes = File.ReadAllBytes(@"c:\players\notch.png");
var playerData = client.GetLocal(skinBytes);
// playerData will have skin data, but no name or UUID.
```

### Get Local Player Skin With Cape
And to load a local player skin with a cape:
```cs
var client = new SkinsClient();
var skinBytes = File.ReadAllBytes(@"c:\players\notch.png");
var capeBytes = File.ReadAllBytes(@"c:\players\notch-cape.png");
var playerData = client.GetLocal(skinBytes, capeBytes);
// playerData will have both skin and cape data, but no name or UUID.
```
## Integration with Dependency Injection
**'Mojang.Api.Skins'** is designed for use with the .NET Dependency Injection (DI) system. To register the library in your DI container, use **'AddMinecraftApiSkins'**:
```cs
ServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddMinecraftApiSkins();

var serviceProvider = serviceCollection.BuildServiceProvider();
var client = serviceProvider.GetRequiredService<ISkinsClient>();
```

## Texture Handling
**'Mojang.Api.Skins'** offers advanced texture processing capabilities, allowing developers to extract specific parts of skins and capes and combine them.

### Extracting Specific Texture Parts
You can selectively extract individual texture parts from the skin or cape. For example, to get the front image of the head and its accessory:
```cs
var client = new SkinsClient();
var playerData = await client.GetAsync("Notch");

var headImage = playerData.Skin.GetSkinPart(SkinPart.Head_FrontSide);
var headAccessoryImage = playerData.Skin.GetSkinPart(SkinPart.HeadAccessory_FrontSide);
```

### Combining Textures
Extracted textures can then be combined to create a complete image:
```cs
var fullHeadImage = headImage.Combine(headAccessoryImage);
```

### Handling Cape Textures
Similarly, you can extract and process parts of capes:
```cs
var capeFrontImage = playerData.Cape.GetCapePart(CapePart.FrontSide);
var elytraFrontImage = playerData.Cape.GetCapePart(CapePart.Elytra_FrontSide);
```

These features enable flexible and precise handling of Minecraft textures, particularly useful when you need to manipulate or display specific texture elements.

## Contributing
Contributions to Mojang.Api.Skins are welcome. Follow these steps to contribute:

1. Fork the Project
2. Create your Feature Branch (git checkout -b feature/YourFeature)
3. Commit your Changes (git commit -m 'Add YourFeature')
4. Push to the Branch (git push origin feature/YourFeature)
5. Open a Pull Request

## Licence
**'Mojang.Api.Skins'** is licenced under the [MIT licence](LICENSE.txt).

## Dependencies
**'Mojang.Api.Skins'** utilizes the following packages, which need to be included as dependencies in your project:

- [LiteDB](https://github.com/mbdavid/LiteDB)
- [Microsoft.Extensions.Caching.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.Caching.Abstractions/)
- [Microsoft.Extensions.Http](https://github.com/dotnet/runtime/tree/main/src/libraries/Microsoft.Extensions.Http)
- [Microsoft.Extensions.Http.Polly](https://www.nuget.org/packages/Microsoft.Extensions.Http.Polly)
- [SkiaSharp](https://github.com/mono/SkiaSharp)
- [System.Net.Http.Json](https://www.nuget.org/packages/System.Net.Http.Json)
- [System.Text.Json](https://www.nuget.org/packages/System.Text.Json)
