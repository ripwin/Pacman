using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TilesetContent
    {
        public int FirstGid { get; init; }

        public string Name { get; init; } = string.Empty;

        public int TileHeight { get; init; }

        public int TileWidth { get; init; }

        public Texture2DContent Image { get; init; } = null!;

        public List<TileDefinition> TileDefinitions { get; init; }

        public TilesetContent()
        {
            TileDefinitions = new();
        }
    }
}
