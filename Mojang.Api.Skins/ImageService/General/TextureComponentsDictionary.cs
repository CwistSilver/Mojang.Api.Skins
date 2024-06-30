using Mojang.Api.Skins.Data;
using System.Drawing;

namespace Mojang.Api.Skins.ImageService.General;
/// <summary>
/// Provides static dictionaries to map skin and cape parts to their respective texture regions.
/// </summary>
public static class TextureComponentsDictionary
{
    /// <summary>
    /// A dictionary containing mappings for each part of a classic skin to its corresponding texture region.
    /// </summary>
    public static Dictionary<SkinPart, Rectangle> ClassicMappings => _classicMappings;
    private static readonly Dictionary<SkinPart, Rectangle> _classicMappings = [];

    /// <summary>
    /// A dictionary containing mappings for each part of a slim skin to its corresponding texture region.
    /// </summary>
    public static Dictionary<SkinPart, Rectangle> SlimMappings => _slimMappings;
    private static readonly Dictionary<SkinPart, Rectangle> _slimMappings = [];

    /// <summary>
    /// A dictionary containing mappings for legacy skin parts to their corresponding texture regions.
    /// </summary>
    public static Dictionary<SkinPart, Rectangle> LegacySkinParts => _legacySkinParts;
    private static readonly Dictionary<SkinPart, Rectangle> _legacySkinParts = [];

    /// <summary>
    /// A dictionary containing mappings for each part of a cape to its corresponding texture region.
    /// </summary>
    public static Dictionary<CapePart, Rectangle> CapeMappings => _capeMappings;
    private static readonly Dictionary<CapePart, Rectangle> _capeMappings = [];

    private static readonly Rectangle Cape_Left = new(0, 1, 1, 16);
    private static readonly Rectangle Cape_Front = new(1, 1, 10, 16);
    private static readonly Rectangle Cape_Right = new(11, 1, 1, 16);
    private static readonly Rectangle Cape_Top = new(1, 0, 10, 1);
    private static readonly Rectangle Cape_Bottom = new(11, 0, 10, 1);
    private static readonly Rectangle Cape_Back = new(12, 1, 10, 16);

    private static readonly Rectangle Elytra_Left = new(34, 2, 2, 9);
    private static readonly Rectangle Elytra_Front = new(36, 2, 10, 20);
    private static readonly Rectangle Elytra_Right = new(22, 11, 1, 11);
    private static readonly Rectangle Elytra_Top = new(32, 0, 8, 2);
    private static readonly Rectangle Elytra_Bottom = new(36, 2, 6, 1);


    private static readonly Rectangle Head_Left = new(0, 8, 8, 8);
    private static readonly Rectangle Head_Front = new(8, 8, 8, 8);
    private static readonly Rectangle Head_Right = new(16, 8, 8, 8);
    private static readonly Rectangle Head_Top = new(8, 0, 8, 8);
    private static readonly Rectangle Head_Bottom = new(16, 0, 8, 8);
    private static readonly Rectangle Head_Back = new(24, 8, 8, 8);

    private static readonly Rectangle Head_Accesory_Left = new(32, 8, 8, 8);
    private static readonly Rectangle Head_Accesory_Front = new(40, 8, 8, 8);
    private static readonly Rectangle Head_Accesory_Right = new(48, 8, 8, 8);
    private static readonly Rectangle Head_Accesory_Top = new(40, 0, 8, 8);
    private static readonly Rectangle Head_Accesory_Bottom = new(48, 0, 8, 8);
    private static readonly Rectangle Head_Accesory_Back = new(56, 8, 8, 8);

    private static readonly Rectangle LegRight_Left = new(0, 20, 4, 12);
    private static readonly Rectangle LegRight_Front = new(4, 20, 4, 12);
    private static readonly Rectangle LegRight_Right = new(8, 20, 4, 12);
    private static readonly Rectangle LegRight_Top = new(4, 16, 4, 4);
    private static readonly Rectangle LegRight_Bottom = new(8, 16, 4, 4);
    private static readonly Rectangle LegRight_Back = new(12, 20, 4, 12);

    private static readonly Rectangle Body_Left = new(16, 20, 4, 12);
    private static readonly Rectangle Body_Front = new(20, 20, 8, 12);
    private static readonly Rectangle Body_Right = new(28, 20, 4, 12);
    private static readonly Rectangle Body_Top = new(20, 16, 8, 4);
    private static readonly Rectangle Body_Bottom = new(28, 16, 8, 4);
    private static readonly Rectangle Body_Back = new(32, 20, 8, 12);

    private static readonly Rectangle ArmRight_Left = new(40, 20, 4, 12);
    private static readonly Rectangle ArmRight_Front = new(44, 20, 4, 12);
    private static readonly Rectangle ArmRight_Right = new(48, 20, 4, 12);
    private static readonly Rectangle ArmRight_Top = new(44, 16, 4, 4);
    private static readonly Rectangle ArmRight_Bottom = new(48, 16, 4, 4);
    private static readonly Rectangle ArmRight_Back = new(52, 20, 4, 12);

    private static readonly Rectangle LegRight_Accesory_Left = new(0, 36, 4, 12);
    private static readonly Rectangle LegRight_Accesory_Front = new(4, 36, 4, 12);
    private static readonly Rectangle LegRight_Accesory_Right = new(8, 36, 4, 12);
    private static readonly Rectangle LegRight_Accesory_Top = new(4, 32, 4, 4);
    private static readonly Rectangle LegRight_Accesory_Bottom = new(8, 32, 4, 4);
    private static readonly Rectangle LegRight_Accesory_Back = new(12, 36, 4, 12);

    private static readonly Rectangle Body_Accesory_Left = new(16, 36, 4, 12);
    private static readonly Rectangle Body_Accesory_Front = new(20, 36, 8, 12);
    private static readonly Rectangle Body_Accesory_Right = new(28, 36, 4, 12);
    private static readonly Rectangle Body_Accesory_Top = new(20, 32, 8, 4);
    private static readonly Rectangle Body_Accesory_Bottom = new(28, 32, 8, 4);
    private static readonly Rectangle Body_Accesory_Back = new(32, 36, 8, 12);

    private static readonly Rectangle ArmRight_Accesory_Left = new(40, 36, 4, 12);
    private static readonly Rectangle ArmRight_Accesory_Front = new(44, 36, 4, 12);
    private static readonly Rectangle ArmRight_Accesory_Right = new(48, 36, 4, 12);
    private static readonly Rectangle ArmRight_Accesory_Top = new(44, 32, 4, 4);
    private static readonly Rectangle ArmRight_Accesory_Bottom = new(48, 32, 4, 4);
    private static readonly Rectangle ArmRight_Accesory_Back = new(52, 36, 4, 12);

    private static readonly Rectangle LegLeft_Accesory_Left = new(0, 52, 4, 12);
    private static readonly Rectangle LegLeft_Accesory_Front = new(4, 52, 4, 12);
    private static readonly Rectangle LegLeft_Accesory_Right = new(8, 52, 4, 12);
    private static readonly Rectangle LegLeft_Accesory_Top = new(4, 48, 4, 4);
    private static readonly Rectangle LegLeft_Accesory_Bottom = new(8, 48, 4, 4);
    private static readonly Rectangle LegLeft_Accesory_Back = new(12, 52, 4, 12);

    private static readonly Rectangle LegLeft_Left = new(16, 52, 4, 12);
    private static readonly Rectangle LegLeft_Front = new(20, 52, 4, 12);
    private static readonly Rectangle LegLeft_Right = new(24, 52, 4, 12);
    private static readonly Rectangle LegLeft_Top = new(20, 48, 4, 4);
    private static readonly Rectangle LegLeft_Bottom = new(24, 48, 4, 4);
    private static readonly Rectangle LegLeft_Back = new(28, 52, 4, 12);

    private static readonly Rectangle ArmLeft_Left = new(32, 52, 4, 12);
    private static readonly Rectangle ArmLeft_Front = new(36, 52, 4, 12);
    private static readonly Rectangle ArmLeft_Right = new(40, 52, 4, 12);
    private static readonly Rectangle ArmLeft_Top = new(36, 48, 4, 4);
    private static readonly Rectangle ArmLeft_Bottom = new(40, 48, 4, 4);
    private static readonly Rectangle ArmLeft_Back = new(44, 52, 4, 12);

    private static readonly Rectangle ArmLeft_Accesory_Left = new(48, 52, 4, 12);
    private static readonly Rectangle ArmLeft_Accesory_Front = new(52, 52, 4, 12);
    private static readonly Rectangle ArmLeft_Accesory_Right = new(56, 52, 4, 12);
    private static readonly Rectangle ArmLeft_Accesory_Top = new(52, 48, 4, 4);
    private static readonly Rectangle ArmLeft_Accesory_Bottom = new(56, 48, 4, 4);
    private static readonly Rectangle ArmLeft_Accesory_Back = new(60, 52, 4, 12);

    private static readonly Rectangle Slim_ArmRight_Left = new(40, 20, 4, 12);
    private static readonly Rectangle Slim_ArmRight_Front = new(44, 20, 3, 12);
    private static readonly Rectangle Slim_ArmRight_Right = new(47, 20, 4, 12);
    private static readonly Rectangle Slim_ArmRight_Top = new(44, 16, 3, 4);
    private static readonly Rectangle Slim_ArmRight_Bottom = new(47, 16, 3, 4);
    private static readonly Rectangle Slim_ArmRight_Back = new(52, 20, 3, 12);

    private static readonly Rectangle Slim_ArmRight_Accesory_Left = new(40, 36, 4, 12);
    private static readonly Rectangle Slim_ArmRight_Accesory_Front = new(44, 36, 3, 12);
    private static readonly Rectangle Slim_ArmRight_Accesory_Right = new(47, 36, 4, 12);
    private static readonly Rectangle Slim_ArmRight_Accesory_Top = new(44, 32, 3, 4);
    private static readonly Rectangle Slim_ArmRight_Accesory_Bottom = new(47, 32, 3, 4);
    private static readonly Rectangle Slim_ArmRight_Accesory_Back = new(51, 36, 3, 12);

    private static readonly Rectangle Slim_ArmLeft_Left = new(32, 52, 4, 12);
    private static readonly Rectangle Slim_ArmLeft_Front = new(36, 52, 3, 12);
    private static readonly Rectangle Slim_ArmLeft_Right = new(39, 52, 4, 12);
    private static readonly Rectangle Slim_ArmLeft_Top = new(36, 48, 3, 4);
    private static readonly Rectangle Slim_ArmLeft_Bottom = new(39, 48, 3, 4);
    private static readonly Rectangle Slim_ArmLeft_Back = new(43, 52, 3, 12);

    private static readonly Rectangle Slim_ArmLeft_Accesory_Left = new(48, 52, 4, 12);
    private static readonly Rectangle Slim_ArmLeft_Accesory_Front = new(52, 52, 3, 12);
    private static readonly Rectangle Slim_ArmLeft_Accesory_Right = new(55, 52, 4, 12);
    private static readonly Rectangle Slim_ArmLeft_Accesory_Top = new(52, 48, 3, 4);
    private static readonly Rectangle Slim_ArmLeft_Accesory_Bottom = new(55, 48, 3, 4);
    private static readonly Rectangle Slim_ArmLeft_Accesory_Back = new(59, 52, 3, 12);

    static TextureComponentsDictionary()
    {
        AddCommonComponents(_classicMappings);
        AddSteveComponents();

        AddCommonComponents(_slimMappings);
        AddAlexComponents();

        AddLegacyComponents();

        AddCapeComponents();
    }

    private static void AddAlexComponents()
    {
        SlimMappings.Add(SkinPart.LeftArm_LeftSide, Slim_ArmLeft_Left);
        SlimMappings.Add(SkinPart.LeftArm_FrontSide, Slim_ArmLeft_Front);
        SlimMappings.Add(SkinPart.LeftArm_RightSide, Slim_ArmLeft_Right);
        SlimMappings.Add(SkinPart.LeftArm_TopSide, Slim_ArmLeft_Top);
        SlimMappings.Add(SkinPart.LeftArm_BottomSide, Slim_ArmLeft_Bottom);
        SlimMappings.Add(SkinPart.LeftArm_BackSide, Slim_ArmLeft_Back);

        SlimMappings.Add(SkinPart.LeftArmAccesory_LeftSide, Slim_ArmLeft_Accesory_Left);
        SlimMappings.Add(SkinPart.LeftArmAccesory_FrontSide, Slim_ArmLeft_Accesory_Front);
        SlimMappings.Add(SkinPart.LeftArmAccesory_RightSide, Slim_ArmLeft_Accesory_Right);
        SlimMappings.Add(SkinPart.LeftArmAccesory_TopSide, Slim_ArmLeft_Accesory_Top);
        SlimMappings.Add(SkinPart.LeftArmAccesory_BottomSide, Slim_ArmLeft_Accesory_Bottom);
        SlimMappings.Add(SkinPart.LeftArmAccesory_BackSide, Slim_ArmLeft_Accesory_Back);

        SlimMappings.Add(SkinPart.RightArm_LeftSide, Slim_ArmRight_Left);
        SlimMappings.Add(SkinPart.RightArm_FrontSide, Slim_ArmRight_Front);
        SlimMappings.Add(SkinPart.RightArm_RightSide, Slim_ArmRight_Right);
        SlimMappings.Add(SkinPart.RightArm_TopSide, Slim_ArmRight_Top);
        SlimMappings.Add(SkinPart.RightArm_BottomSide, Slim_ArmRight_Bottom);
        SlimMappings.Add(SkinPart.RightArm_BackSide, Slim_ArmRight_Back);

        SlimMappings.Add(SkinPart.RightArmAccesory_LeftSide, Slim_ArmRight_Accesory_Left);
        SlimMappings.Add(SkinPart.RightArmAccesory_FrontSide, Slim_ArmRight_Accesory_Front);
        SlimMappings.Add(SkinPart.RightArmAccesory_RightSide, Slim_ArmRight_Accesory_Right);
        SlimMappings.Add(SkinPart.RightArmAccesory_TopSide, Slim_ArmRight_Accesory_Top);
        SlimMappings.Add(SkinPart.RightArmAccesory_BottomSide, Slim_ArmRight_Accesory_Bottom);
        SlimMappings.Add(SkinPart.RightArmAccesory_BackSide, Slim_ArmRight_Accesory_Back);
    }

    private static void AddSteveComponents()
    {
        ClassicMappings.Add(SkinPart.LeftArm_LeftSide, ArmLeft_Left);
        ClassicMappings.Add(SkinPart.LeftArm_FrontSide, ArmLeft_Front);
        ClassicMappings.Add(SkinPart.LeftArm_RightSide, ArmLeft_Right);
        ClassicMappings.Add(SkinPart.LeftArm_TopSide, ArmLeft_Top);
        ClassicMappings.Add(SkinPart.LeftArm_BottomSide, ArmLeft_Bottom);
        ClassicMappings.Add(SkinPart.LeftArm_BackSide, ArmLeft_Back);

        ClassicMappings.Add(SkinPart.LeftArmAccesory_LeftSide, ArmLeft_Accesory_Left);
        ClassicMappings.Add(SkinPart.LeftArmAccesory_FrontSide, ArmLeft_Accesory_Front);
        ClassicMappings.Add(SkinPart.LeftArmAccesory_RightSide, ArmLeft_Accesory_Right);
        ClassicMappings.Add(SkinPart.LeftArmAccesory_TopSide, ArmLeft_Accesory_Top);
        ClassicMappings.Add(SkinPart.LeftArmAccesory_BottomSide, ArmLeft_Accesory_Bottom);
        ClassicMappings.Add(SkinPart.LeftArmAccesory_BackSide, ArmLeft_Accesory_Back);

        ClassicMappings.Add(SkinPart.RightArm_LeftSide, ArmRight_Left);
        ClassicMappings.Add(SkinPart.RightArm_FrontSide, ArmRight_Front);
        ClassicMappings.Add(SkinPart.RightArm_RightSide, ArmRight_Right);
        ClassicMappings.Add(SkinPart.RightArm_TopSide, ArmRight_Top);
        ClassicMappings.Add(SkinPart.RightArm_BottomSide, ArmRight_Bottom);
        ClassicMappings.Add(SkinPart.RightArm_BackSide, ArmRight_Back);

        ClassicMappings.Add(SkinPart.RightArmAccesory_LeftSide, ArmRight_Accesory_Left);
        ClassicMappings.Add(SkinPart.RightArmAccesory_FrontSide, ArmRight_Accesory_Front);
        ClassicMappings.Add(SkinPart.RightArmAccesory_RightSide, ArmRight_Accesory_Right);
        ClassicMappings.Add(SkinPart.RightArmAccesory_TopSide, ArmRight_Accesory_Top);
        ClassicMappings.Add(SkinPart.RightArmAccesory_BottomSide, ArmRight_Accesory_Bottom);
        ClassicMappings.Add(SkinPart.RightArmAccesory_BackSide, ArmRight_Accesory_Back);
    }

    private static void AddLegacyComponents()
    {
        LegacySkinParts.Add(SkinPart.Head_LeftSide, Head_Left);
        LegacySkinParts.Add(SkinPart.Head_FrontSide, Head_Front);
        LegacySkinParts.Add(SkinPart.Head_RightSide, Head_Right);
        LegacySkinParts.Add(SkinPart.Head_TopSide, Head_Top);
        LegacySkinParts.Add(SkinPart.Head_BottomSide, Head_Bottom);
        LegacySkinParts.Add(SkinPart.Head_BackSide, Head_Back);

        LegacySkinParts.Add(SkinPart.HeadAccesory_LeftSide, Head_Accesory_Left);
        LegacySkinParts.Add(SkinPart.HeadAccesory_FrontSide, Head_Accesory_Front);
        LegacySkinParts.Add(SkinPart.HeadAccesory_RightSide, Head_Accesory_Right);
        LegacySkinParts.Add(SkinPart.HeadAccesory_TopSide, Head_Accesory_Top);
        LegacySkinParts.Add(SkinPart.HeadAccesory_BottomSide, Head_Accesory_Bottom);
        LegacySkinParts.Add(SkinPart.HeadAccesory_BackSide, Head_Accesory_Back);

        LegacySkinParts.Add(SkinPart.Body_LeftSide, Body_Left);
        LegacySkinParts.Add(SkinPart.Body_FrontSide, Body_Front);
        LegacySkinParts.Add(SkinPart.Body_RightSide, Body_Right);
        LegacySkinParts.Add(SkinPart.Body_TopSide, Body_Top);
        LegacySkinParts.Add(SkinPart.Body_BottomSide, Body_Bottom);
        LegacySkinParts.Add(SkinPart.Body_BackSide, Body_Back);

        LegacySkinParts.Add(SkinPart.RightLeg_LeftSide, LegRight_Left);
        LegacySkinParts.Add(SkinPart.RightLeg_FrontSide, LegRight_Front);
        LegacySkinParts.Add(SkinPart.RightLeg_RightSide, LegRight_Right);
        LegacySkinParts.Add(SkinPart.RightLeg_TopSide, LegRight_Top);
        LegacySkinParts.Add(SkinPart.RightLeg_BottomSide, LegRight_Bottom);
        LegacySkinParts.Add(SkinPart.RightLeg_BackSide, LegRight_Back);

        LegacySkinParts.Add(SkinPart.RightArm_LeftSide, ArmRight_Left);
        LegacySkinParts.Add(SkinPart.RightArm_FrontSide, ArmRight_Front);
        LegacySkinParts.Add(SkinPart.RightArm_RightSide, ArmRight_Right);
        LegacySkinParts.Add(SkinPart.RightArm_TopSide, ArmRight_Top);
        LegacySkinParts.Add(SkinPart.RightArm_BottomSide, ArmRight_Bottom);
        LegacySkinParts.Add(SkinPart.RightArm_BackSide, ArmRight_Back);
    }

    private static void AddCommonComponents(Dictionary<SkinPart, Rectangle> dictionary)
    {
        dictionary.Add(SkinPart.Head_LeftSide, Head_Left);
        dictionary.Add(SkinPart.Head_FrontSide, Head_Front);
        dictionary.Add(SkinPart.Head_RightSide, Head_Right);
        dictionary.Add(SkinPart.Head_TopSide, Head_Top);
        dictionary.Add(SkinPart.Head_BottomSide, Head_Bottom);
        dictionary.Add(SkinPart.Head_BackSide, Head_Back);

        dictionary.Add(SkinPart.HeadAccesory_LeftSide, Head_Accesory_Left);
        dictionary.Add(SkinPart.HeadAccesory_FrontSide, Head_Accesory_Front);
        dictionary.Add(SkinPart.HeadAccesory_RightSide, Head_Accesory_Right);
        dictionary.Add(SkinPart.HeadAccesory_TopSide, Head_Accesory_Top);
        dictionary.Add(SkinPart.HeadAccesory_BottomSide, Head_Accesory_Bottom);
        dictionary.Add(SkinPart.HeadAccesory_BackSide, Head_Accesory_Back);

        dictionary.Add(SkinPart.Body_LeftSide, Body_Left);
        dictionary.Add(SkinPart.Body_FrontSide, Body_Front);
        dictionary.Add(SkinPart.Body_RightSide, Body_Right);
        dictionary.Add(SkinPart.Body_TopSide, Body_Top);
        dictionary.Add(SkinPart.Body_BottomSide, Body_Bottom);
        dictionary.Add(SkinPart.Body_BackSide, Body_Back);

        dictionary.Add(SkinPart.BodyAccesory_LeftSide, Body_Accesory_Left);
        dictionary.Add(SkinPart.BodyAccesory_FrontSide, Body_Accesory_Front);
        dictionary.Add(SkinPart.BodyAccesory_RightSide, Body_Accesory_Right);
        dictionary.Add(SkinPart.BodyAccesory_TopSide, Body_Accesory_Top);
        dictionary.Add(SkinPart.BodyAccesory_BottomSide, Body_Accesory_Bottom);
        dictionary.Add(SkinPart.BodyAccesory_BackSide, Body_Accesory_Back);

        dictionary.Add(SkinPart.LeftLeg_LeftSide, LegLeft_Left);
        dictionary.Add(SkinPart.LeftLeg_FrontSide, LegLeft_Front);
        dictionary.Add(SkinPart.LeftLeg_RightSide, LegLeft_Right);
        dictionary.Add(SkinPart.LeftLeg_TopSide, LegLeft_Top);
        dictionary.Add(SkinPart.LeftLeg_BottomSide, LegLeft_Bottom);
        dictionary.Add(SkinPart.LeftLeg_BackSide, LegLeft_Back);

        dictionary.Add(SkinPart.LeftLegAccesory_LeftSide, LegLeft_Accesory_Left);
        dictionary.Add(SkinPart.LeftLegAccesory_FrontSide, LegLeft_Accesory_Front);
        dictionary.Add(SkinPart.LeftLegAccesory_RightSide, LegLeft_Accesory_Right);
        dictionary.Add(SkinPart.LeftLegAccesory_TopSide, LegLeft_Accesory_Top);
        dictionary.Add(SkinPart.LeftLegAccesory_BottomSide, LegLeft_Accesory_Bottom);
        dictionary.Add(SkinPart.LeftLegAccesory_BackSide, LegLeft_Accesory_Back);

        dictionary.Add(SkinPart.RightLeg_LeftSide, LegRight_Left);
        dictionary.Add(SkinPart.RightLeg_FrontSide, LegRight_Front);
        dictionary.Add(SkinPart.RightLeg_RightSide, LegRight_Right);
        dictionary.Add(SkinPart.RightLeg_TopSide, LegRight_Top);
        dictionary.Add(SkinPart.RightLeg_BottomSide, LegRight_Bottom);
        dictionary.Add(SkinPart.RightLeg_BackSide, LegRight_Back);

        dictionary.Add(SkinPart.RightLegAccesory_LeftSide, LegRight_Accesory_Left);
        dictionary.Add(SkinPart.RightLegAccesory_FrontSide, LegRight_Accesory_Front);
        dictionary.Add(SkinPart.RightLegAccesory_RightSide, LegRight_Accesory_Right);
        dictionary.Add(SkinPart.RightLegAccesory_TopSide, LegRight_Accesory_Top);
        dictionary.Add(SkinPart.RightLegAccesory_BottomSide, LegRight_Accesory_Bottom);
        dictionary.Add(SkinPart.RightLegAccesory_BackSide, LegRight_Accesory_Back);
    }

    private static void AddCapeComponents()
    {
        CapeMappings.Add(CapePart.FrontSide, Cape_Front);
        CapeMappings.Add(CapePart.LeftSide, Cape_Left);
        CapeMappings.Add(CapePart.RightSide, Cape_Right);
        CapeMappings.Add(CapePart.TopSide, Cape_Top);
        CapeMappings.Add(CapePart.BottomSide, Cape_Bottom);
        CapeMappings.Add(CapePart.BackSide, Cape_Back);

        CapeMappings.Add(CapePart.Elytra_LeftSide, Elytra_Left);
        CapeMappings.Add(CapePart.Elytra_FrontSide, Elytra_Front);
        CapeMappings.Add(CapePart.Elytra_RightSide, Elytra_Right);
        CapeMappings.Add(CapePart.Elytra_TopSide, Elytra_Top);
        CapeMappings.Add(CapePart.Elytra_BottomSide, Elytra_Bottom);
    }
}
