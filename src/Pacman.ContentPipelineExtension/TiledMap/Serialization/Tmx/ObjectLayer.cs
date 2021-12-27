using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public sealed class ObjectLayer : Layer
    {
        [XmlElement("object")]
        public List<Object> Objects { get; } = new();
    }
}
