using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class Frame
    {
        [XmlAttribute("tileid")]
        public int TileId { get; }

        [XmlAttribute("duration")]
        public int Duration { get; }
    }
}
