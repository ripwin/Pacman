namespace Pacman.Core.TiledMap
{
    public readonly struct Tile
    {
        public const int Empty = 0;

        public int Value { get; }

        public Tile(int value)
        {
            Value = value;
        }
    }
}
