using Mojang.Api.Skins.Data;
using Mojang.Api.Skins.ImageService.General;
using Mojang.Api.Skins.Utilities.TextureCropper;
using System.Drawing;
using System.Numerics;

namespace Mojang.Api.Skins.ImageService.SkinConverter;
public sealed class ModernSkinConverter : IModernSkinConverter
{
    private readonly IImageUtilities _imageUtilities;
    private readonly ITextureCropper _skinPartCropper;
    public ModernSkinConverter(IImageUtilities imageUtilities, ITextureCropper skinPartCropper)
    {
        _imageUtilities = imageUtilities;
        _skinPartCropper = skinPartCropper;
    }

    public byte[] ConvertToModernSkin(byte[] skinDataBytes)
    {
        var dataSpan = (ReadOnlySpan<byte>)skinDataBytes.AsSpan();
        var skinData = new SkinData()
        {
            TextureBytes = skinDataBytes,
            SkinType = SkinType.Classic
        };

        var legacySkinPartsKeys = TextureComponentsDictionary.LegacySkinParts.Keys.ToArray();
        var legacySkinPartsRegions = TextureComponentsDictionary.LegacySkinParts.Values.ToArray();
        var legacySkinParts = _skinPartCropper.Cut(skinData, legacySkinPartsKeys).ToList();
        AddMissingLegacySkinParts(legacySkinParts);
        RemoveUnusedPart(dataSpan, legacySkinParts, SkinPart.HeadAccesory_LeftSide, SkinPart.HeadAccesory_FrontSide, SkinPart.HeadAccesory_RightSide, SkinPart.HeadAccesory_TopSide, SkinPart.HeadAccesory_BottomSide, SkinPart.HeadAccesory_BackSide);

        var positions = new List<Vector2>();
        for (int i = 0; i < legacySkinParts.Count; i++)
        {
            var region = TextureComponentsDictionary.ClassicMappings[legacySkinParts[i].SkinPart];
            positions.Add(new Vector2(region.X, region.Y));
        }

        var legacySkinPartsData = legacySkinParts.Where(spd => spd.TextureBytes is not null).Select(spd => spd.TextureBytes).ToList();
        var newImage = _imageUtilities.CreateImage(legacySkinPartsData, positions);
        return _imageUtilities.Resize((ReadOnlySpan<byte>)newImage.AsSpan(), 64, 64);
    }

    private void AddMissingLegacySkinParts(List<SkinPartData> legacySkinParts)
    {
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightArm_LeftSide, SkinPart.LeftArm_LeftSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightArm_FrontSide, SkinPart.LeftArm_FrontSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightArm_RightSide, SkinPart.LeftArm_RightSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightArm_TopSide, SkinPart.LeftArm_TopSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightArm_BottomSide, SkinPart.LeftArm_BottomSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightArm_BackSide, SkinPart.LeftArm_BackSide));

        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightLeg_LeftSide, SkinPart.LeftLeg_LeftSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightLeg_FrontSide, SkinPart.LeftLeg_FrontSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightLeg_RightSide, SkinPart.LeftLeg_RightSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightLeg_TopSide, SkinPart.LeftLeg_TopSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightLeg_BottomSide, SkinPart.LeftLeg_BottomSide));
        legacySkinParts.Add(CreateNewPartFrom(legacySkinParts, SkinPart.RightLeg_BackSide, SkinPart.LeftLeg_BackSide));
    }

    private void RemoveUnusedPart(ReadOnlySpan<byte> skinData, List<SkinPartData> legacySkinParts, params SkinPart[] parts)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            var part = legacySkinParts.Find(skinPartData => skinPartData.SkinPart == parts[i]);
            var area = TextureComponentsDictionary.ClassicMappings[parts[i]];
            if (_imageUtilities.IsAreaFilledWithColor(skinData, area, Color.Black))
                legacySkinParts.Remove(part);
        }

    }

    private SkinPartData CreateNewPartFrom(List<SkinPartData> legacySkinParts, SkinPart copyFrom, SkinPart toCreate) => new SkinPartData(_imageUtilities) { SkinPart = toCreate, TextureBytes = legacySkinParts.Find(skinPartData => skinPartData.SkinPart == copyFrom)!.TextureBytes };
}
