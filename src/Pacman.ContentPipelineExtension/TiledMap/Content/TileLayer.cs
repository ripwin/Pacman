namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class TileLayer
    {
        public string Name { get; set; } = string.Empty;

        public int Height { get; set; }

        public int Width { get; set; }

        public List<int> Tiles { get; set; } = new();
    }
}
