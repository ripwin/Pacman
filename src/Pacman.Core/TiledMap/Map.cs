using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public class Map
    {
        public int Height { get; }

        public int Width { get; }
        public int TileHeight { get; }
        public int TileWidth { get; }
        public Orientation Orientation { get; }
        public ReadOnlyCollection<ILayer> Layers { get; }
        public ReadOnlyCollection<Tileset> Tilesets { get; }

        private readonly List<ILayer> _layers;
        private readonly List<Tileset> _tilesets;

        public Map(int width, int height, int tileWidth, int tileHeight, Orientation orientation)
        {
            Width = width;
            Height = height;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Orientation = orientation;

            _layers = new();
            Layers = new(_layers);

            _tilesets = new();
            Tilesets = new(_tilesets);
        }

        public void Add(ILayer layer)
        {
            if (layer is TileLayer l && (l.Width != Width || l.Height != Height))
            {
                throw new ArgumentException($"The tiles layer must contains {Width} X {Height} tiles !");
            }

            _layers.Add(layer);
        }

        public void Add(Tileset tileset)
            => _tilesets.Add(tileset);
    }
}
