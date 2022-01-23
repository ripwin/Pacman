namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TileDefinition
    {
        public int Tile { get; set; }

        public string Type { get; set; } = string.Empty;

        public List<(int tile, float duration)> Frames { get; set; } = new();

        public List<(string name, string value)> Properties { get; set; } = new();
    }
}
