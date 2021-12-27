using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Common
{
    public class CustomProperties
    {
        [XmlElement("property")]
        public List<Property> Properties { get; } = new();
    }
}
