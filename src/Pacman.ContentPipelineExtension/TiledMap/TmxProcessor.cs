using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Pacman.ContentPipelineExtension.TiledMap.Content;
using Pacman.ContentPipelineExtension.TiledMap.Serialization.Tmx;

namespace Pacman.ContentPipelineExtension.TiledMap
{
    [ContentProcessor(DisplayName = "TMX Processor - Pacman")]
    public sealed class TmxProcessor : ContentProcessor<Map, MapContent>
    {
        public override MapContent Process(Map tmx, ContentProcessorContext context)
        {
            context.Logger.LogMessage("Processing TMX");

            var map = new MapContent
            {
                Width = tmx.Width,
                Height = tmx.Height,
                TileWidth = tmx.TileWidth,
                TileHeight = tmx.TileHeight,
                Orientation = (int)tmx.Orientation
            };

            LoadTilesets(tmx, map, context);
            LoadLayers(tmx, map);

            return map;
        }

        private static void LoadTilesets(Map tmx, MapContent map, ContentProcessorContext context)
        {
            foreach (var tsx in tmx.Tilesets)
            {
                // TODO : Check for external tilesets

                // Get image texture
                var source = new ExternalReference<Texture2DContent>(tsx.Image.Source);
                var texture = context.BuildAndLoadAsset<Texture2DContent, Texture2DContent>(source, string.Empty);

                // Get tile definitions
                var tileDefinitions = new List<TileDefinition>();

                foreach (var tile in tsx.Tiles)
                {
                    tileDefinitions.Add(new TileDefinition()
                    {
                        Tile = tile.Id,
                        Type = tile.Type ?? string.Empty,
                        Frames = tile.Animation?.Frames.Select(f => ((f.TileId + 1), (float)f.Duration)).ToList() ?? new(),
                        Properties = tile.CustomProperties?.Properties.Select(p => (p.Name, p.Value)).ToList() ?? new()
                    });
                }

                map.Tilesets.Add(new TilesetContent
                {
                    Name = tsx.Name,
                    FirstGid = tsx.FirstGid,
                    TileWidth = tsx.TileWidth,
                    TileHeight = tsx.TileHeight,
                    Image = texture,
                    TileDefinitions = tileDefinitions
                });
            }
        }

        private static void LoadLayers(Map tmx, MapContent map)
        {
            foreach (var layer in tmx.Layers)
            {
                if (layer is Serialization.Tmx.TileLayer tileLayer)
                {
                    // TODO : Check for encoding and compression
                    map.TileLayers.Add(new Content.TileLayer
                    {
                        Name = tileLayer.Name ?? string.Empty,
                        Width = tileLayer.Width,
                        Height = tileLayer.Height,
                        Tiles = tileLayer.Data.Tiles.Select(t => t.Gid).ToList()
                    });
                }
                else if (layer is Serialization.Tmx.ObjectLayer objectLayer)
                {
                    var objects = new Content.ObjectLayer
                    {
                        Name = objectLayer.Name ?? string.Empty
                    };

                    foreach (var o in objectLayer.Objects)
                    {
                        var name = o.Name ?? string.Empty;
                        var type = o.Type ?? string.Empty;

                        var properties = o.CustomProperties?.Properties.Select(p => (p.Name, p.Value)).ToList() ?? new();

                        if (o.ObjectType is Ellipse)
                        {
                            objects.Ellipses.Add((name, type, o.X, o.Y, o.Width, o.Height, properties));
                        }
                        else if (o.ObjectType is Point)
                        {
                            objects.Points.Add((name, type, o.X, o.Y, properties));
                        }
                        else if (o.ObjectType is Polygon polygon)
                        {
                            var points = new List<(float x, float y)>();

                            foreach (var point in polygon.Points.Split(' '))
                            {
                                var p = point.Split(',');
                                points.Add((float.Parse(p[0]), float.Parse(p[1])));
                            }

                            objects.Polygons.Add((name, type, o.X, o.Y, points, properties));
                        }
                        else if (o.ObjectType is Text text)
                        {
                            objects.Texts.Add((name, type, o.X, o.Y, o.Width, o.Height, text.Value, properties));
                        }
                        else
                        {
                            objects.Rectangles.Add((name, type, o.X, o.Y, o.Width, o.Height, properties));
                        }
                    }

                    map.ObjectsLayers.Add(objects);
                }
            }
        }
    }
}

