namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TileLayer
    {
        public string Name { get; init; } = string.Empty;

        public int Height { get; init; }

        public int Width { get; init; }

        public List<int> Tiles { get; init; } = new();
    }
}
