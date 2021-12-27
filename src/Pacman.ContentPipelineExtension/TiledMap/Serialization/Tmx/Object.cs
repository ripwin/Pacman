using Pacman.ContentPipelineExtension.TiledMap.Serialization.Common;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public class Object
    {
        [XmlAttribute("id")]
        public int Id { get; }

        [XmlAttribute("gid")]
        public int Gid { get; }

        [XmlAttribute("name")]
        public string? Name { get; }

        [XmlAttribute("type")]
        public string? Type { get; }

        [XmlAttribute("x")]
        public float X { get; }

        [XmlAttribute("y")]
        public float Y { get; }

        [XmlAttribute("width")]
        public float Width { get; }

        [XmlAttribute("height")]
        public float Height { get; }

        [XmlElement("point", Type = typeof(Point))]
        [XmlElement("ellipse", Type = typeof(Ellipse))]
        [XmlElement("polygon", Type = typeof(Polygon))]
        public ObjectType? ObjectType { get; }

        [XmlElement("properties")]
        public CustomProperties? CustomProperties { get; }
    }
}
