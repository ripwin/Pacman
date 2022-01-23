namespace Pacman.ContentPipelineExtension.TiledMap.Content
{
    public class ObjectLayer
    {
        public string Name { get; set; } = string.Empty;

        public List<(string name, string type, float x, float y, List<(string name, string value)> properties)> Points { get; set; }

        public List<(string name, string type, float x, float y, float width, float height, List<(string name, string value)> properties)> Ellipses { get; set; }

        public List<(string name, string type, float x, float y, float width, float height, List<(string name, string value)> properties)> Rectangles { get; set; }

        public List<(string name, string type, float x, float y, List<(float x, float y)> points, List<(string name, string value)> properties)> Polygons { get; set; }

        public List<(string name, string type, float x, float y, float width, float height, string value, List<(string name, string value)> properties)> Texts { get; set; }

        public ObjectLayer()
        {
            Points = new();
            Ellipses = new();
            Rectangles = new();
            Polygons = new();
            Texts = new();
        }
    }
}
