using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public sealed class Text : IObject
    {
        public string Name { get; }
        public string Type { get; }
        public float X { get; }
        public float Y { get; }
        public float Height { get; }
        public float Width { get; }
        public string Value { get; }
        public ReadOnlyCollection<Property> Properties { get; }

        public Text(string name, string type, float x, float y, float width, float height, string value)
            : this(name, type, x, y, width, height, value, new())
        {
        }

        public Text(string name, string type, float x, float y, float width, float height, string value, List<Property> properties)
        {
            Name = name;
            Type = type;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Value = value;
            Properties = new(new List<Property>(properties));
        }
    }
}
