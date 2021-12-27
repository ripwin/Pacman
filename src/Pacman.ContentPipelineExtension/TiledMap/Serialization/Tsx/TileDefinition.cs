namespace Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx
{
    public class TileDefinition
    {
        public string? Type { get; }

        public List<(int tile, float duration)> Fames { get; } = new();

        public List<(string name, string value)> Properties { get; } = new();
    }
}
