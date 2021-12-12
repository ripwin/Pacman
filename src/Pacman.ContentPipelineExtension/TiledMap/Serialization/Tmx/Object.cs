using Pacman.ContentPipelineExtension.TiledMap.Serialization.Common;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public class Object
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("gid")]
        public int Gid { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("x")]
        public float X { get; set; }

        [XmlAttribute("y")]
        public float Y { get; set; }

        [XmlAttribute("width")]
        public float Width { get; set; }

        [XmlAttribute("height")]
        public float Height { get; set; }

        [XmlElement("point", Type = typeof(Point))]
        [XmlElement("ellipse", Type = typeof(Ellipse))]
        [XmlElement("polygon", Type = typeof(Polygon))]
        public ObjectType ObjectType { get; set; }

        [XmlElement("properties")]
        public CustomProperties CustomProperties { get; set; }
    }
}
