using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public interface IObject
    {
        public string Name { get; }
        public string Type { get; }
        public float X { get; }
        public float Y { get; }
        public ReadOnlyCollection<Property> Properties { get; }
    }
}
