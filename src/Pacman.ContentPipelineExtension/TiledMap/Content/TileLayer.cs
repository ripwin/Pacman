using System.Collections.Generic;

namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TileLayer
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<int> Tiles { get; set; }
    }
}
