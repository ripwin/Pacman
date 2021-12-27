using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public enum RenderOrder
    {
        [XmlEnum(Name = "right-down")]
        RightDown,

        [XmlEnum(Name = "right-up")]
        RightUp,

        [XmlEnum(Name = "left-down")]
        LeftDown,

        [XmlEnum(Name = "left-up")]
        LeftUp,
    }
}
