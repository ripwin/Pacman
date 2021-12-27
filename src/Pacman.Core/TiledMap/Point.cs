using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public sealed class Point : IObject
    {
        public string Name { get; }
        public string Type { get; }
        public float X { get; }
        public float Y { get; }
        public ReadOnlyCollection<Property> Properties { get; }

        public Point(string name, string type, float x, float y)
            : this(name, type, x, y, new())
        { 
        }

        public Point(string name, string type, float x, float y, List<Property> properties)
        {
            Name = name;
            Type = type;
            X = x;
            Y = y;
            Properties = new(new List<Property>(properties));
        }
    }
}
