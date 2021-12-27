using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public sealed class TileLayer : Layer
    {
        [XmlAttribute("width")]
        public int Width { get; }

        [XmlAttribute("height")]
        public int Height { get; }

        [XmlElement("data")]
        public Data Data { get; } = null!;
    }
}
