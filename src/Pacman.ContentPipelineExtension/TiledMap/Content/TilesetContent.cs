using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TilesetContent
    {
        public int FirstGid { get; set; }

        public string Name { get; set; } = string.Empty;

        public int TileHeight { get; set; }

        public int TileWidth { get; set; }

        public Texture2DContent Image { get; set; } = null!;

        public List<TileDefinition> TileDefinitions { get; set; }

        public TilesetContent()
        {
            TileDefinitions = new();
        }
    }
}
