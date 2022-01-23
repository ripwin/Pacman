using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class Frame
    {
        [XmlAttribute("tileid")]
        public int TileId { get; set; }

        [XmlAttribute("duration")]
        public int Duration { get; set; }
    }
}
