using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public sealed class TileLayer : ILayer
    {
        public int Height { get; }
        public int Width { get; }
        public string Name { get; }
        public ReadOnlyCollection<Tile> Tiles { get; }

        public TileLayer(string name, int width, int height, List<Tile> tiles)
        {
            Name = name;
            Width = width;
            Height = height;

            if (tiles.Count > Width * Height)
            {
                throw new ArgumentException($"The number of tiles must be {Width * Height} ({Width} x {Height}) !");
            }

            Tiles = new(tiles.ToArray());
        }

        public Tile GetTile(int x, int y)
        {
            if (x < 0 || x > Width || y < 0 || y > Height)
            {
                throw new ArgumentOutOfRangeException($"The x or y coordinates is out of range !");
            }

            return Tiles[y * Width + x];
        }
    }
}
