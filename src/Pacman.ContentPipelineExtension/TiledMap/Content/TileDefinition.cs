namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TileDefinition
    {
        public int Tile { get; init; }

        public string Type { get; init; } = string.Empty;

        public List<(int tile, float duration)> Frames { get; init; } = new();

        public List<(string name, string value)> Properties { get; init; } = new();
    }
}
