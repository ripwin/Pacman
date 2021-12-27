using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Core.TiledMap
{
    public class Tileset
    {
        public string Name { get; }
        public Texture2D Texture { get; }
        public int TileHeight { get; }
        public int TileWidth { get; }

        private readonly Tile _firstGid;
        private readonly TileDefinition[] _tileDefinitions;

        public Tileset(
            string name,
            Texture2D texture, 
            Tile firstGid, 
            int tileWidth, 
            int tileHeight,
            Dictionary<Tile, TileDefinition> definitions)
        {
            Name = name;
            Texture = texture;
            _firstGid = firstGid;

            TileHeight = tileHeight;
            TileWidth = tileWidth;

            var tileCount = Texture.Width / TileWidth * Texture.Height / TileHeight;
            _tileDefinitions = new TileDefinition[tileCount];

            for (var i = 0; i < tileCount; i++)
            {
                if (definitions.TryGetValue(new Tile(i), out var definition))
                {
                    _tileDefinitions[i] = new TileDefinition(
                        definition.Type,
                        definition.Frames.ToList(),
                        definition.Properties.ToList());
                }
                else
                {
                    _tileDefinitions[i] = new TileDefinition();
                }
            }
        }

        public bool Contains(Tile tile)
        {
            var tileCount = (Texture.Width / TileWidth) * (Texture.Height / TileHeight);
            return tile.Value >= _firstGid.Value && tile.Value <= _firstGid.Value + tileCount;
        }

        public TileDefinition GetDefinition(Tile tile)
        {
            if (!Contains(tile))
            {
                throw new ArgumentOutOfRangeException($"The gid {tile.Value} does not exist on this tileset !");
            }

            return _tileDefinitions[tile.Value - _firstGid.Value];
        }

        public Microsoft.Xna.Framework.Rectangle GetRegion(Tile tile)
        {
            var tileCount = (Texture.Width / TileWidth) * (Texture.Height / TileHeight);

            if (tile.Value >= _firstGid.Value && tile.Value <= _firstGid.Value + tileCount)
            {
                var column = Texture.Width / TileWidth;

                return new Microsoft.Xna.Framework.Rectangle (
                    new Microsoft.Xna.Framework.Point(((tile.Value - 1) % column) * TileWidth, ((tile.Value - 1) / column) * TileHeight),
                    new Microsoft.Xna.Framework.Point(TileWidth, TileHeight)
                );
            }

            throw new ArgumentOutOfRangeException($"The gid {tile.Value} is out of range !");
        }
    }
}
