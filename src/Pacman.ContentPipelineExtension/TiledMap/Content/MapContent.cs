using System.Collections.Generic;

namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class MapContent
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int Orientation { get; set; }
        public List<TileLayer> TileLayers { get; set; }
        public List<ObjectLayer> ObjectsLayers { get; set; }
        public List<TilesetContent> Tilesets { get; set; }

        public MapContent()
        {
            TileLayers = new List<TileLayer>();
            ObjectsLayers = new List<ObjectLayer>();
            Tilesets = new List<TilesetContent>();
        }
    }
}