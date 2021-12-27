using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public sealed class Ellipse : IObject
    {
        public string Name { get; }
        public string Type { get; }
        public float X { get; }
        public float Y { get; }
        public float Height { get; }
        public float Width { get; }
        public ReadOnlyCollection<Property> Properties { get; }

        public Ellipse(string name, string type, float x, float y, float width, float height)
            : this(name, type, x, y, width, height, new())
        {
        }

        public Ellipse(string name, string type, float x, float y, float width, float height, List<Property> properties)
        {
            Name = name;
            Type = type;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Properties = new(new List<Property>(properties));
        }
    }
}
