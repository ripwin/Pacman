using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx
{
    [XmlInclude(typeof(Ellipse))]
    [XmlInclude(typeof(Point))]
    public abstract class ObjectType
    {
    }
}
