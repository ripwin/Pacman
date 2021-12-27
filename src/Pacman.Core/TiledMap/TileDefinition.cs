using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public class TileDefinition
    {
        public string Type { get; }
        public ReadOnlyCollection<Frame> Frames { get; }
        public ReadOnlyCollection<Property> Properties { get; }

        public TileDefinition()
            : this(string.Empty)
        {
        }

        public TileDefinition(string type)
            : this(type, new(), new())
        {
        }

        public TileDefinition(string type, List<Frame> frames, List<Property> properties)
        {
            Type = type;
            Frames = new(new List<Frame>(frames));
            Properties = new(new List<Property>(properties));
        }
    }
}
