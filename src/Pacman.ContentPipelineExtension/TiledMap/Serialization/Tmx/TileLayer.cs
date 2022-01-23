using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public sealed class TileLayer : Layer
    {
        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlElement("data")]
        public Data Data { get; set; } = null!;
    }
}
