using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class Image
    {
        [XmlAttribute("source")]
        public string Source { get; set; } = null!;

        [XmlAttribute("width")]
        public int Width { get; }

        [XmlAttribute("height")]
        public int Height { get; }
    }
}
