using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class Tileset
    {
        [XmlAttribute("firstgid")]
        public int FirstGid { get; set; }

        [XmlAttribute("source")]
        public string Source { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlAttribute("tiledversion")]
        public string TiledVersion { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("tilewidth")]
        public int TileWidth { get; set; }

        [XmlAttribute("tileheight")]
        public int TileHeight { get; set; }

        [XmlAttribute("spacing")]
        public int Spacing { get; set; }

        [XmlAttribute("tilecount")]
        public int TileCount { get; set; }

        [XmlAttribute("columns")]
        public int Columns { get; set; }

        [XmlElement("image")]
        public Image Image { get; set; }

        [XmlElement("tile")]
        public List<Tile> Tiles { get; set; }
    }
}
