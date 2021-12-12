using Pacman.ContentPipelineExtension.TiledMap.Serialization.Tsx;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;
using System.Xml.Serialization;

namespace Pacman.ContentPipelineExtension.TiledMap
{
    [ContentImporter(".tsx", DefaultProcessor = "TsxProcessor", DisplayName = "TSX Importer - Pacman")]
    public class TsxImporter : ContentImporter<Tileset>
    {
        public override Tileset Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage($"Importing TSX file: {filename}");

            using var streamReader = new StreamReader(filename);
            var deserializer = new XmlSerializer(typeof(Tileset));
            var tileset = (Tileset)deserializer.Deserialize(streamReader);

            tileset.Image.Source = Path.Combine(Path.GetDirectoryName(filename), tileset.Image.Source);
            //context.AddDependency(tileset.Image.Source);

            return tileset;
        }
    }
}
