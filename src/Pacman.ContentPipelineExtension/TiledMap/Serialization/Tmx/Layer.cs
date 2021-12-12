using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    [XmlInclude(typeof(TileLayer))]
    [XmlInclude(typeof(ObjectLayer))]
    public abstract class Layer
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
