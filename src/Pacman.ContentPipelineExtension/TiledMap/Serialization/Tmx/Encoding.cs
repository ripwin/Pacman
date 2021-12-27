using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public enum Encoding
    {
        [XmlEnum(Name = "csv")]
        Csv,

        [XmlEnum(Name = "base64")]
        Base64
    }
}
