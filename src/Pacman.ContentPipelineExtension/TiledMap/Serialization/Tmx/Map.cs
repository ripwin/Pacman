using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    [XmlRoot("map")]
    public class Map
    {
        [XmlAttribute("version")]
        public string Version { get; } = null!;

        [XmlAttribute("tiledversion")]
        public string TiledVersion { get; } = null!;

        [XmlAttribute("orientation")]
        public Orientation Orientation { get; }

        [XmlAttribute("renderorder")]
        public RenderOrder RenderOrder { get; }

        [XmlAttribute("width")]
        public int Width { get; }

        [XmlAttribute("height")]
        public int Height { get; }

        [XmlAttribute("tilewidth")]
        public int TileWidth { get; }

        [XmlAttribute("tileheight")]
        public int TileHeight { get; }

        [XmlAttribute("infinite")]
        public int Infinite { get; }

        [XmlAttribute("nextlayerid")]
        public int NextLayerId { get; }

        [XmlAttribute("nextobjectid")]
        public int NextObjectId { get; }

        [XmlElement("tileset")]
        public List<Tsx.Tileset> Tilesets { get; } = new();

        [XmlElement("layer", Type = typeof(TileLayer))]
        [XmlElement("objectgroup", Type = typeof(ObjectLayer))]
        public List<Layer> Layers { get; } = new();
    }
}
