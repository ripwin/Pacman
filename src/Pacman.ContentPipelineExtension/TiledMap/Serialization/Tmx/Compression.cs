using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public enum Compression
    {
        [XmlEnum(Name = "zlib")]
        Zlib,

        [XmlEnum(Name = "gzip")]
        Gzip
    }
}
