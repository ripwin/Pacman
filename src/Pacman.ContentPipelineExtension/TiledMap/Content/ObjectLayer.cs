namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class ObjectLayer
    {
        public string Name { get; init; } = string.Empty;

        public List<(string name, string type, float x, float y, List<(string name, string value)> properties)> Points { get; init; }

        public List<(string name, string type, float x, float y, float width, float height, List<(string name, string value)> properties)> Ellipses { get; init; }

        public List<(string name, string type, float x, float y, float width, float height, List<(string name, string value)> properties)> Rectangles { get; init; }

        public List<(string name, string type, float x, float y, List<(float x, float y)> points, List<(string name, string value)> properties)> Polygons { get; init; }

        public ObjectLayer()
        {
            Points = new();
            Ellipses = new();
            Rectangles = new();
            Polygons = new();
        }
    }
}
