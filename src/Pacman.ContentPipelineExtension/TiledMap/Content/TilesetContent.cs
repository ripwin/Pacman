using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.Collections.Generic;

namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TilesetContent
    {
        public string Name { get; set; }
        public int FirstGid { get; set; }
        public Texture2DContent Image { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public List<TileDefinition> TileDefinitions { get; set; }

        public TilesetContent()
        {
            TileDefinitions = new List<TileDefinition>();
        }
    }
}
