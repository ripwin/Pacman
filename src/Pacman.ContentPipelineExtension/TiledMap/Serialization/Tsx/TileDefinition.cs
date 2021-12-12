using System.Collections.Generic;

namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class TileDefinition
    {
        public string Type { get; set; }
        public List<(int tile, float duration)> Fames { get; set; }
        public List<(string name, string value)> Properties { get; set; }
    }
}
