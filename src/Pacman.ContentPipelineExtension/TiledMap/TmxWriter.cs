using Pacman.ContentPipelineExtension.TiledMap.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Pacman.ContentPipelineExtension.TiledMap
{
    [ContentTypeWriter]
    public sealed class TmxWriter : ContentTypeWriter<MapContent>
    {
        protected override void Write(ContentWriter output, MapContent map)
        {
            // Map information
            output.Write(map.Width);
            output.Write(map.Height);
            output.Write(map.TileWidth);
            output.Write(map.TileHeight);
            output.Write(map.Orientation);

            // Tilesets
            output.Write(map.Tilesets.Count);

            foreach (var tileset in map.Tilesets)
            {
                output.Write(tileset.Name);
                output.Write(tileset.FirstGid);
                output.Write(tileset.TileWidth);
                output.Write(tileset.TileHeight);
                output.WriteObject(tileset.Image);

                // Tiles
                output.Write(tileset.TileDefinitions.Count);

                foreach (var tile in tileset.TileDefinitions)
                {
                    output.Write(tile.Tile);
                    output.Write(tile.Type);

                    // Frames
                    output.Write(tile.Frames.Count);

                    foreach (var frame in tile.Frames)
                    {
                        output.Write(frame.tile);
                        output.Write(frame.duration);
                    }

                    // Properties
                    output.Write(tile.Properties.Count);

                    foreach (var (name, value) in tile.Properties)
                    {
                        output.Write(name);
                        output.Write(value);
                    }
                }
            }

            // Tile layer
            output.Write(map.TileLayers.Count);

            foreach (var layer in map.TileLayers)
            {
                output.Write(layer.Name);
                output.Write(layer.Width);
                output.Write(layer.Height);
                
                // Tiles
                output.Write(layer.Tiles.Count);

                foreach (var tile in layer.Tiles)
                {
                    output.Write(tile);
                }
            }

            // Object layer
            output.Write(map.ObjectsLayers.Count);

            foreach (var layer in map.ObjectsLayers)
            {
                output.Write(layer.Name);

                // Ellipses
                output.Write(layer.Ellipses.Count);
                
                foreach (var (name, type, x, y, width, height, properties) in layer.Ellipses)
                {
                    output.Write(name);
                    output.Write(type);
                    output.Write(x);
                    output.Write(y);
                    output.Write(width);
                    output.Write(height);

                    // Properties
                    output.Write(properties.Count);

                    foreach (var property in properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
                    }
                }

                // Points
                output.Write(layer.Points.Count);

                foreach (var (name, type, x, y, properties) in layer.Points)
                {
                    output.Write(name);
                    output.Write(type);
                    output.Write(x);
                    output.Write(y);

                    // Properties
                    output.Write(properties.Count);

                    foreach (var property in properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
                    }
                }

                // Polygons
                output.Write(layer.Polygons.Count);

                foreach (var (name, type, x, y, points, properties) in layer.Polygons)
                {
                    output.Write(name);
                    output.Write(type);
                    output.Write(x);
                    output.Write(y);

                    // Points
                    output.Write(points.Count);

                    foreach (var point in points)
                    {
                        output.Write(point.x);
                        output.Write(point.y);
                    }

                    // Properties
                    output.Write(properties.Count);

                    foreach (var property in properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
                    }
                }

                // Rectangles
                output.Write(layer.Rectangles.Count);

                foreach (var (name, type, x, y, width, height, properties) in layer.Rectangles)
                {
                    output.Write(name);
                    output.Write(type);
                    output.Write(x);
                    output.Write(y);
                    output.Write(width);
                    output.Write(height);

                    // Properties
                    output.Write(properties.Count);

                    foreach (var property in properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
                    }
                }
            }
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
            => "Pacman.ContentPipelineExtension.TiledMap.TmxReader, Pacman.ContentPipelineExtension";
    }
}
