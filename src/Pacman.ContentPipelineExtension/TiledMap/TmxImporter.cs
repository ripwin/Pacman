using Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap
{
    [ContentImporter(".tmx", DisplayName = "TmxProcessor", DefaultProcessor = "TMX Importer - Pacman")]
    public sealed class TmxImporter : ContentImporter<Map>
    {
        public override Map Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage($"Importing TMX file: {filename}");

            using var tmxStream = new StreamReader(filename);
            var tmxSerializer = new XmlSerializer(typeof(Map));
            var tmx = (Map)tmxSerializer.Deserialize(tmxStream);

            foreach (var tileset in tmx.Tilesets)
            {
                // TODO : Check for external tilesets
                tileset.Image.Source = Path.Combine(Path.GetDirectoryName(filename), tileset.Image.Source);
                context.AddDependency(tileset.Image.Source);
            }

            return tmx;
        }
    }
}
