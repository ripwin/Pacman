using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class Tileset
    {
        [XmlAttribute("firstgid")]
        public int FirstGid { get; }

        [XmlAttribute("source")]
        public string? Source { get; }

        [XmlAttribute("version")]
        public string? Version { get; }

        [XmlAttribute("tiledversion")]
        public string? TiledVersion { get; }

        [XmlAttribute("name")]
        public string Name { get; } = null!;

        [XmlAttribute("tilewidth")]
        public int TileWidth { get; }

        [XmlAttribute("tileheight")]
        public int TileHeight { get; }

        [XmlAttribute("spacing")]
        public int Spacing { get; }

        [XmlAttribute("tilecount")]
        public int TileCount { get; }

        [XmlAttribute("columns")]
        public int Columns { get; }

        [XmlElement("image")]
        public Image Image { get; } = null!;

        [XmlElement("tile")]
        public List<Tile> Tiles { get; } = new();
    }
}
