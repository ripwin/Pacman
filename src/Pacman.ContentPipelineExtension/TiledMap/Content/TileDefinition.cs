using System.Collections.Generic;

namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TileDefinition
    {
        public int Tile { get; set; }
        public string Type { get; set; }
        public List<(int tile, float duration)> Frames { get; set; }
        public List<(string name, string value)> Properties { get; set; }
    }
}
