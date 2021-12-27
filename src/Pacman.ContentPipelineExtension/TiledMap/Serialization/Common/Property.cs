using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Common
{
    public class Property
    {
        [XmlAttribute("name")]
        public string Name { get; } = null!;

        [XmlAttribute("value")]
        public string Value { get; } = string.Empty;
    }
}
