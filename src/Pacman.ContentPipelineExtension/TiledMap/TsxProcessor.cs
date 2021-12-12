using Pacman.ContentPipelineExtension.TiledMap.Content;
using Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace Pacman.ContentPipelineExtension.TiledMap
{
    [ContentProcessor(DisplayName = "TSX Processor - Pacman")]
    public sealed class TsxProcessor : ContentProcessor<Tileset, TilesetContent>
    {
        public override TilesetContent Process(Tileset tsx, ContentProcessorContext context)
        {
            context.Logger.LogMessage("Processing TMX");

            var tileset = new TilesetContent
            {
                
            };

            return tileset;
        }
    }
}
