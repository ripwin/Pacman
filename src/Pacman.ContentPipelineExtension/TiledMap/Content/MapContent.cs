namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class MapContent
    {
        public int Width { get; init; }

        public int Height { get; init; }

        public int TileWidth { get; init; }

        public int TileHeight { get; init; }

        public int Orientation { get; init; }

        public List<TileLayer> TileLayers { get; init; }

        public List<ObjectLayer> ObjectsLayers { get; init; }

        public List<TilesetContent> Tilesets { get; init; }

        public MapContent()
        {
            TileLayers = new();
            ObjectsLayers = new();
            Tilesets = new();
        }
    }
}