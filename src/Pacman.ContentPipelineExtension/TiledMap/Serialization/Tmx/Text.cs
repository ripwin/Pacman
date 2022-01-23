using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public sealed class Text : ObjectType
    {
        [XmlAttribute("wrap")]
        public string Wrap { get; set; } = null!;

        [XmlText]
        public string Value { get; set; } = string.Empty;
    }
}
