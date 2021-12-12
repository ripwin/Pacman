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
            output.Write((int)map.Orientation);

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

                    foreach (var property in tile.Properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
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
                
                foreach (var ellipse in layer.Ellipses)
                {
                    output.Write(ellipse.name);
                    output.Write(ellipse.type);
                    output.Write(ellipse.x);
                    output.Write(ellipse.y);
                    output.Write(ellipse.width);
                    output.Write(ellipse.height);

                    // Properties
                    output.Write(ellipse.properties.Count);

                    foreach (var property in ellipse.properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
                    }
                }

                // Points
                output.Write(layer.Points.Count);

                foreach (var point in layer.Points)
                {
                    output.Write(point.name);
                    output.Write(point.type);
                    output.Write(point.x);
                    output.Write(point.y);

                    // Properties
                    output.Write(point.properties.Count);

                    foreach (var property in point.properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
                    }
                }

                // Polygons
                output.Write(layer.Polygons.Count);

                foreach (var polygon in layer.Polygons)
                {
                    output.Write(polygon.name);
                    output.Write(polygon.type);
                    output.Write(polygon.x);
                    output.Write(polygon.y);

                    // Points
                    output.Write(polygon.points.Count);

                    foreach (var point in polygon.points)
                    {
                        output.Write(point.x);
                        output.Write(point.y);
                    }

                    // Properties
                    output.Write(polygon.properties.Count);

                    foreach (var property in polygon.properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
                    }
                }

                // Rectangles
                output.Write(layer.Rectangles.Count);

                foreach (var rectangle in layer.Rectangles)
                {
                    output.Write(rectangle.name);
                    output.Write(rectangle.type);
                    output.Write(rectangle.x);
                    output.Write(rectangle.y);
                    output.Write(rectangle.width);
                    output.Write(rectangle.height);

                    // Properties
                    output.Write(rectangle.properties.Count);

                    foreach (var property in rectangle.properties)
                    {
                        output.Write(property.name);
                        output.Write(property.value);
                    }
                }
            }
        }

        //public override string GetRuntimeType(TargetPlatform targetPlatform)
        //{
        //    return typeof(BitmapFont).AssemblyQualifiedName;
        //}

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Pacman.ContentPipelineExtension.TiledMap.TmxReader, Pacman.ContentPipelineExtension";
        }
    }
}
