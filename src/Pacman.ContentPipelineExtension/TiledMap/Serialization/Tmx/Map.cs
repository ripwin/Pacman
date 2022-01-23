using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    [XmlRoot("map")]
    public class Map
    {
        [XmlAttribute("version")]
        public string Version { get; set; } = null!;

        [XmlAttribute("tiledversion")]
        public string TiledVersion { get; set; } = null!;

        [XmlAttribute("orientation")]
        public Orientation Orientation { get; set; }

        [XmlAttribute("renderorder")]
        public RenderOrder RenderOrder { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }

        [XmlAttribute("tilewidth")]
        public int TileWidth { get; set; }

        [XmlAttribute("tileheight")]
        public int TileHeight { get; set; }

        [XmlAttribute("infinite")]
        public int Infinite { get; set; }

        [XmlAttribute("nextlayerid")]
        public int NextLayerId { get; set; }

        [XmlAttribute("nextobjectid")]
        public int NextObjectId { get; set; }

        [XmlElement("tileset")]
        public List<Tsx.Tileset> Tilesets { get; set; } = new();

        [XmlElement("layer", Type = typeof(TileLayer))]
        [XmlElement("objectgroup", Type = typeof(ObjectLayer))]
        public List<Layer> Layers { get; set; } = new();
    }
}
