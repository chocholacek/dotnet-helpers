using System.Text;

namespace DotnetHelperTool.Helpers;

public enum EncodingType
{
    UTF8,
    Unicode,
    ASCII
}

public static class EncodingTypeExtensions
{
    public static Encoding ToEncoding(this EncodingType encoding) => encoding switch
    {
        EncodingType.UTF8 => Encoding.UTF8,
        EncodingType.Unicode => Encoding.Unicode,
        EncodingType.ASCII => Encoding.ASCII,
        _ => throw new ArgumentException($"Unknown value {encoding} for type {nameof(EncodingType)}")
    };
}