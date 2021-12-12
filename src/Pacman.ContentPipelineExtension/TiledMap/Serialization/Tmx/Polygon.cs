using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public sealed class Polygon : ObjectType
    {
        [XmlAttribute("points")]
        public string Points { get; set; }
    }
}
