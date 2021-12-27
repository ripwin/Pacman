namespace Pacman.Core.TiledMap
{
    public readonly struct Property
    {
        public string Name { get; }
        public string Value { get; }

        public Property(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
