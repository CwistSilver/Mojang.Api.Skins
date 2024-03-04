using Mojang.Api.Skins.ImageService.General;
using System.Runtime.InteropServices;

namespace Mojang.Api.Skins.ImageService.Identifier.Cape;
public sealed class CapeTextureIdentifier : ICapeTextureIdentifier
{
    private static readonly Memory<byte> birthday = new byte[] { 160, 187, 255, 74, 55, 139, 177, 136, 28, 94, 124, 137, 184, 167, 254, 125, 206, 229, 177, 65, 65, 19, 107, 244, 149, 107, 65, 88, 18, 3, 47, 206 };
    private static readonly Memory<byte> cheapsh0t = new byte[] { 95, 172, 162, 204, 67, 241, 96, 120, 187, 84, 63, 92, 35, 133, 18, 96, 219, 69, 72, 243, 136, 207, 87, 68, 185, 42, 76, 180, 205, 117, 49, 156 };
    private static readonly Memory<byte> cherryBlossom = new byte[] { 245, 106, 228, 89, 201, 79, 13, 102, 73, 77, 98, 141, 212, 8, 234, 191, 206, 228, 166, 11, 39, 2, 101, 127, 80, 84, 107, 15, 42, 65, 62, 199 };
    private static readonly Memory<byte> cobalt = new byte[] { 222, 61, 141, 242, 76, 232, 41, 174, 162, 39, 130, 184, 253, 204, 220, 251, 93, 241, 107, 37, 143, 64, 222, 245, 211, 6, 57, 106, 193, 186, 9, 36 };
    private static readonly Memory<byte> dannyBstyle = new byte[] { 216, 167, 108, 190, 187, 114, 39, 178, 78, 20, 174, 168, 56, 40, 12, 231, 2, 155, 169, 238, 104, 166, 154, 209, 22, 245, 167, 118, 1, 175, 149, 112 };
    private static readonly Memory<byte> julianClark = new byte[] { 252, 186, 111, 144, 113, 100, 169, 21, 36, 41, 78, 39, 250, 230, 180, 255, 203, 157, 23, 62, 203, 43, 222, 94, 255, 25, 153, 158, 203, 147, 200, 98 };
    private static readonly Memory<byte> mapMaker = new byte[] { 149, 102, 173, 13, 94, 224, 114, 41, 252, 192, 63, 164, 239, 82, 33, 89, 114, 10, 99, 207, 176, 105, 103, 37, 99, 151, 51, 239, 232, 57, 155, 122 };
    private static readonly Memory<byte> migrator = new byte[] { 75, 245, 67, 157, 230, 61, 3, 46, 200, 104, 113, 154, 87, 229, 50, 240, 209, 27, 4, 192, 33, 192, 241, 255, 203, 206, 179, 138, 141, 166, 116, 78 };
    private static readonly Memory<byte> millionthSale = new byte[] { 94, 158, 196, 130, 173, 210, 238, 154, 7, 80, 76, 97, 82, 161, 130, 199, 36, 82, 178, 133, 191, 144, 39, 126, 55, 77, 15, 139, 128, 25, 222, 18 };
    private static readonly Memory<byte> minecon2013 = new byte[] { 75, 162, 141, 36, 241, 138, 166, 160, 247, 190, 142, 122, 109, 114, 236, 191, 149, 122, 112, 141, 180, 240, 120, 118, 233, 190, 239, 37, 132, 226, 44, 181 };
    private static readonly Memory<byte> minecon2015 = new byte[] { 145, 33, 13, 23, 254, 138, 79, 208, 232, 171, 252, 187, 45, 98, 133, 102, 28, 5, 237, 42, 110, 34, 114, 72, 253, 107, 51, 9, 249, 135, 41, 143 };
    private static readonly Memory<byte> minecon2016 = new byte[] { 222, 10, 70, 62, 169, 146, 6, 12, 240, 73, 161, 14, 208, 126, 120, 60, 171, 108, 78, 118, 194, 218, 64, 2, 115, 68, 153, 40, 122, 173, 28, 238 };
    private static readonly Memory<byte> minecon2011 = new byte[] { 188, 142, 7, 186, 55, 87, 143, 76, 128, 8, 98, 151, 20, 95, 221, 26, 239, 108, 111, 155, 190, 177, 239, 222, 53, 169, 208, 246, 209, 52, 40, 119 };
    private static readonly Memory<byte> minecon2012 = new byte[] { 79, 172, 239, 192, 170, 96, 38, 200, 82, 42, 0, 31, 246, 205, 100, 175, 245, 233, 43, 222, 122, 161, 246, 50, 136, 184, 125, 13, 95, 154, 178, 3 };
    private static readonly Memory<byte> moderator = new byte[] { 81, 226, 122, 210, 226, 23, 225, 246, 115, 9, 252, 57, 216, 122, 84, 5, 48, 13, 231, 150, 201, 32, 220, 167, 196, 182, 196, 253, 218, 102, 118, 126 };
    private static readonly Memory<byte> mojangnew = new byte[] { 119, 5, 62, 224, 133, 215, 251, 115, 103, 83, 184, 19, 119, 31, 176, 226, 149, 54, 137, 43, 47, 84, 138, 42, 165, 229, 21, 25, 195, 12, 252, 111 };
    private static readonly Memory<byte> mojangold = new byte[] { 109, 237, 88, 54, 82, 91, 200, 229, 193, 38, 6, 230, 89, 96, 0, 190, 152, 174, 104, 70, 198, 195, 160, 13, 204, 125, 86, 100, 77, 37, 196, 252 };
    private static readonly Memory<byte> mojang = new byte[] { 74, 36, 246, 131, 216, 242, 29, 242, 75, 23, 88, 183, 161, 30, 154, 161, 158, 28, 161, 64, 141, 36, 10, 68, 239, 228, 168, 84, 66, 76, 165, 60 };
    private static readonly Memory<byte> mrMessiah = new byte[] { 194, 87, 29, 122, 86, 140, 31, 184, 19, 60, 59, 222, 45, 99, 215, 15, 25, 0, 187, 11, 216, 0, 103, 175, 24, 125, 150, 104, 38, 170, 106, 133 };
    private static readonly Memory<byte> prismarine = new byte[] { 216, 243, 246, 116, 73, 110, 130, 98, 167, 144, 168, 48, 17, 126, 21, 221, 155, 247, 28, 108, 253, 0, 146, 133, 105, 88, 43, 43, 143, 135, 111, 169 };
    private static readonly Memory<byte> scrollsChamp = new byte[] { 33, 74, 72, 205, 114, 210, 27, 49, 150, 241, 72, 237, 157, 60, 227, 133, 253, 85, 17, 27, 215, 247, 152, 128, 231, 224, 80, 22, 249, 30, 224, 65 };
    private static readonly Memory<byte> translatorChinese = new byte[] { 252, 176, 199, 156, 33, 245, 50, 122, 247, 22, 239, 236, 52, 225, 148, 117, 63, 245, 188, 206, 202, 74, 57, 242, 88, 133, 210, 115, 27, 15, 43, 101 };
    private static readonly Memory<byte> translator = new byte[] { 56, 7, 244, 28, 175, 152, 84, 58, 71, 205, 217, 12, 165, 211, 222, 183, 42, 98, 113, 191, 124, 232, 169, 183, 9, 251, 246, 36, 112, 106, 209, 207 };
    private static readonly Memory<byte> turtle = new byte[] { 241, 169, 245, 236, 75, 89, 137, 201, 35, 100, 247, 208, 89, 182, 182, 197, 108, 187, 6, 12, 10, 241, 157, 130, 190, 100, 126, 150, 43, 95, 114, 60 };
    private static readonly Memory<byte> valentine = new byte[] { 24, 249, 220, 71, 172, 163, 204, 42, 129, 13, 255, 10, 75, 18, 170, 169, 86, 103, 178, 75, 254, 234, 158, 46, 53, 123, 20, 128, 148, 132, 75, 161 };
    private static readonly Memory<byte> vanilla = new byte[] { 208, 229, 138, 231, 190, 174, 130, 53, 120, 7, 160, 136, 184, 92, 70, 40, 7, 215, 138, 216, 52, 163, 28, 150, 125, 188, 145, 157, 133, 25, 52, 68 };

    private readonly List<(Memory<byte>, string)> _capeIdentitiesList = new();

    private readonly IImageUtilities _imageUtilities;
    public CapeTextureIdentifier(IImageUtilities imageUtilities)
    {
        _imageUtilities = imageUtilities;
        AddDefaultCapeIdentitiesList();
    }

    public string? GetTextureName(ReadOnlySpan<byte> fileBytes)
    {
        var hashValue = _imageUtilities.HashImage(fileBytes);
        return FindName(hashValue.Span);
    }

    private void AddDefaultCapeIdentitiesList()
    {
        _capeIdentitiesList.Add((birthday, "Birthday cape"));
        _capeIdentitiesList.Add((migrator, "Migrator cape"));
        _capeIdentitiesList.Add((vanilla, "Vanilla cape"));
        _capeIdentitiesList.Add((cherryBlossom, "Cherry Blossom cape"));
        _capeIdentitiesList.Add((minecon2016, "MINECON 2016 cape"));
        _capeIdentitiesList.Add((minecon2013, "MINECON 2013 cape"));
        _capeIdentitiesList.Add((minecon2015, "MINECON 2015 cape"));
        _capeIdentitiesList.Add((minecon2012, "MINECON 2012 cape"));
        _capeIdentitiesList.Add((minecon2011, "MINECON 2011 cape"));
        _capeIdentitiesList.Add((mapMaker, "Realms MapMaker cape"));
        _capeIdentitiesList.Add((mojang, "Mojang cape"));
        _capeIdentitiesList.Add((mojangnew, "Mojang Studios cape"));
        _capeIdentitiesList.Add((translator, "Translator cape"));
        _capeIdentitiesList.Add((moderator, "Mojira Moderator cape"));
        _capeIdentitiesList.Add((mojangold, "Mojang cape (Classic)"));
        _capeIdentitiesList.Add((cobalt, "Cobalt cape"));
        _capeIdentitiesList.Add((scrollsChamp, "Scrolls Champion cape"));
        _capeIdentitiesList.Add((translatorChinese, "Chinese Translator cape"));
        _capeIdentitiesList.Add((valentine, "Valentine cape"));
        _capeIdentitiesList.Add((turtle, "Turtle cape"));
        _capeIdentitiesList.Add((julianClark, "Julian Clark"));
        _capeIdentitiesList.Add((cheapsh0t, "cheapsh0t's cape"));
        _capeIdentitiesList.Add((dannyBstyle, "dannyBstyle's cape"));
        _capeIdentitiesList.Add((julianClark, "JulianClark's cape"));
        _capeIdentitiesList.Add((millionthSale, "Millionth Customer cape"));
        _capeIdentitiesList.Add((mrMessiah, "MrMessiah's cape"));
        _capeIdentitiesList.Add((prismarine, "Prismarine cape"));
        _capeIdentitiesList.Add((dannyBstyle, "danny Bstyle"));
    }

    public string? FindName(ReadOnlySpan<byte> imageHash)
    {
        foreach (var capeIdentity in _capeIdentitiesList)
        {
            if (imageHash.SequenceEqual(capeIdentity.Item1.Span))
                return capeIdentity.Item2;
        }

        return null;
    }
}