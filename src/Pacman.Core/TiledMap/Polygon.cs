using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public sealed class Polygon : IObject
    {
        public string Name { get; }
        public string Type { get; }
        public float X { get; }
        public float Y { get; }
        public ReadOnlyCollection<(float x, float y)> Points { get; }
        public ReadOnlyCollection<Property> Properties { get; }

        public Polygon(string name, string type, float x, float y, List<(float x, float y)> points)
            : this(name, type, x, y, points, new())
        { 
        }

        public Polygon(string name, string type, float x, float y, List<(float x, float y)> points, List<Property> properties)
        {
            Name = name;
            Type = type;
            X = x;
            Y = y;
            Points = new(points.ToArray());
            Properties = new(new List<Property>(properties));
        }
    }
}
