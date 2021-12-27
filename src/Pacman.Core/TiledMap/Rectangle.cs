using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public sealed class Rectangle : IObject
    {
        public string Name { get; }
        public string Type { get; }
        public float X { get; }
        public float Y { get; }
        public float Height { get; }
        public float Width { get; }
        public ReadOnlyCollection<Property> Properties { get; }

        public Rectangle(string name, string type, float x, float y, float width, float height)
            : this(name, type, x, y, width, height, new())
        { 
        
        }

        public Rectangle(string name, string type, float x, float y, float width, float height, List<Property> properties)
        {
            Name = name;
            Type = type;
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Properties = new(new List<Property>(properties));
        }
    }
}
