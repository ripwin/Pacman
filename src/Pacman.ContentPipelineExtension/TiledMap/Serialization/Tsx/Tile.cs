using Pacman.ContentPipelineExtension.TiledMap.Serialization.Common;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class Tile
    {
        [XmlAttribute("id")]
        public int Id { get; }

        [XmlAttribute("type")]
        public string? Type { get; }

        [XmlElement("properties")]
        public CustomProperties? CustomProperties { get; }

        [XmlElement("animation")]
        public Animation? Animation { get; }
    }
}
