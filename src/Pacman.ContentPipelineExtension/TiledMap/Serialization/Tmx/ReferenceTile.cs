using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    public class ReferenceTile
    {
        [XmlAttribute("gid")]
        public int Gid { get; set; }
    }
}
