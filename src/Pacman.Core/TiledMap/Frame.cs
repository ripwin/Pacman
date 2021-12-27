namespace Pacman.Core.TiledMap
{
    public readonly struct Frame
    {
        public Tile Tile { get; }
        public float Duration { get; }

        public Frame(Tile tile, float duration)
        {
            Tile = tile;
            Duration = duration;
        }
    }
}
