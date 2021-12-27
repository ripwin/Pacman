using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public class Data
    {
        [XmlAttribute("encoding")]
        public Encoding? Encoding { get; }

        [XmlAttribute("compression")]
        public Compression? Compression { get; }

        [XmlText]
        public string? EncodedData { get; }

        [XmlElement("tile")]
        public List<ReferenceTile> Tiles { get; } = new();
    }
}
