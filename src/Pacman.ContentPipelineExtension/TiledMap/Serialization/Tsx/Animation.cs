using System.Collections.Generic;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class Animation
    {
        [XmlElement("frame")]
        public List<Frame> Frames { get; set; }
    }
}
