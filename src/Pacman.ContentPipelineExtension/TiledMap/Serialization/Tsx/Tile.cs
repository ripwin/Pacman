using Pacman.ContentPipelineExtension.TiledMap.Serialization.Common;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class Tile
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("type")]
        public string? Type { get; set; }

        [XmlElement("properties")]
        public CustomProperties? CustomProperties { get; set; }

        [XmlElement("animation")]
        public Animation? Animation { get; set; }
    }
}
