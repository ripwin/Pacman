using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public enum Orientation
    {
        [XmlEnum(Name = "orthogonal")]
        Orthogonal,

        [XmlEnum(Name = "isometric")]
        Isometric
    }
}
