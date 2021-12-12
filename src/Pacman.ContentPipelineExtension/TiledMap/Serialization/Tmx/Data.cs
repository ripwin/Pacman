using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public class Data
    {
        [XmlAttribute("encoding")]
        public Encoding Encoding { get; set; }

        [XmlAttribute("compression")]
        public Compression Compression { get; set; }

        [XmlText]
        public string EncodedData;

        [XmlElement("tile")]
        public List<ReferenceTile> Tiles { get; set; }
    }
}
