using System.Text;

namespace Mojang.Api.Skins.Utilities;
public static class MemoryExtensions
{
    public static string ToHexString(this Memory<byte> bytes)
    {
        var hex = new StringBuilder(bytes.Length * 2);
        foreach (byte b in (ReadOnlySpan<byte>)bytes.Span)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }
}
