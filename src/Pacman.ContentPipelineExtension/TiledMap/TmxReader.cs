using Pacman.Core.TiledMap;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Pacman.ContentPipelineExtension.TiledMap
{
    public sealed class TmxReader : ContentTypeReader<Map>
    {
        protected override Map Read(ContentReader input, Map existingInstance)
        {
            // Map information
            var width = input.ReadInt32();
            var height = input.ReadInt32();
            var tileWidth = input.ReadInt32();
            var tileHeight = input.ReadInt32();
            var oritentation = input.ReadInt32();

            var orientation = oritentation switch
            {
                (int)Serialization.Tmx.Orientation.Orthogonal => Orientation.Orthogonal,
                (int)Serialization.Tmx.Orientation.Isometric => Orientation.Isometric,
                _ => throw new ArgumentException("Unknow map orientation type")
            };

            var map = new Map(width, height, tileWidth, tileHeight, orientation);

            ReadTilesets(input, map);
            ReadLayers(input, map);

            return map;
        }

        private void ReadLayers(ContentReader input, Map map)
        {
            // Tile layer
            var tileLayerCount = input.ReadInt32();

            for (var i = 0; i < tileLayerCount; i++)
            {
                var name = input.ReadString();
                var width = input.ReadInt32();
                var height = input.ReadInt32();

                // Tiles
                var tileCount = input.ReadInt32();
                var tiles = new List<Tile>();

                for (var j = 0; j < tileCount; j++)
                {
                    tiles.Add(new Tile(input.ReadInt32()));
                }

                map.Add(new TileLayer(name, width, height, tiles));
            }

            // Object layer
            var objectLayerCount = input.ReadInt32();

            for (var i = 0; i < objectLayerCount; i++)
            {
                var layerName = input.ReadString();

                var objects = new List<IObject>();

                // Ellipses
                var ellipeCount = input.ReadInt32();
                for (var j = 0; j < ellipeCount; j++)
                {
                    var name = input.ReadString();
                    var type = input.ReadString();
                    var x = input.ReadSingle();
                    var y = input.ReadSingle();
                    var width = input.ReadSingle();
                    var height = input.ReadSingle();

                    // properties
                    var propertyCount = input.ReadInt32();
                    var properties = new List<Property>();

                    for (var k = 0; k < propertyCount; k++)
                    {
                        properties.Add(new Property(input.ReadString(), input.ReadString()));
                    }

                    objects.Add(new Ellipse(name, type, x, y, width, height, properties));
                }

                // Points
                var pointCount = input.ReadInt32();
                for (var j = 0; j < pointCount; j++)
                {
                    var name = input.ReadString();
                    var type = input.ReadString();
                    var x = input.ReadSingle();
                    var y = input.ReadSingle();

                    // properties
                    var propertyCount = input.ReadInt32();
                    var properties = new List<Property>();

                    for (var k = 0; k < propertyCount; k++)
                    {
                        properties.Add(new Property(input.ReadString(), input.ReadString()));
                    }

                    objects.Add(new Point(name, type, x, y, properties));
                }

                // Polygons
                var polygonCount = input.ReadInt32();
                for (var j = 0; j < polygonCount; j++)
                {
                    var name = input.ReadString();
                    var type = input.ReadString();
                    var x = input.ReadSingle();
                    var y = input.ReadSingle();

                    // Points
                    var pointsCount = input.ReadInt32();
                    var points = new List<(float x, float y)>();

                    for (var k = 0; k < pointsCount; k++)
                    {
                        points.Add((input.ReadSingle(), input.ReadSingle()));
                    }

                    // properties
                    var propertyCount = input.ReadInt32();
                    var properties = new List<Property>();

                    for (var k = 0; k < propertyCount; k++)
                    {
                        properties.Add(new Property(input.ReadString(), input.ReadString()));
                    }

                    objects.Add(new Polygon(name, type, x, y, points, properties));
                }

                // Rectangles
                var rectangleCount = input.ReadInt32();
                for (var j = 0; j < rectangleCount; j++)
                {
                    var name = input.ReadString();
                    var type = input.ReadString();
                    var x = input.ReadSingle();
                    var y = input.ReadSingle();
                    var width = input.ReadSingle();
                    var height = input.ReadSingle();

                    // properties
                    var propertyCount = input.ReadInt32();
                    var properties = new List<Property>();

                    for (var k = 0; k < propertyCount; k++)
                    {
                        properties.Add(new Property(input.ReadString(), input.ReadString()));
                    }

                    objects.Add(new Rectangle(name, type, x, y, width, height, properties));
                }

                map.Add(new ObjectLayer(layerName, objects));
            }
        }

        private void ReadTilesets(ContentReader input, Map map)
        {
            var tilesetCount = input.ReadInt32();

            for (var i = 0; i < tilesetCount; i++)
            {
                var name = input.ReadString();
                var firstGid = new Tile(input.ReadInt32());
                var tileWidth = input.ReadInt32();
                var tileHeight = input.ReadInt32();
                var texture = input.ReadObject<Texture2D>();

                // Tile definition
                var tileDefinitionCount = input.ReadInt32();
                var tileDefinitions = new Dictionary<Tile, TileDefinition>();

                for (var j = 0; j < tileDefinitionCount; j++)
                {
                    var tile = new Tile(input.ReadInt32());
                    var type = input.ReadString();

                    // frames
                    var frameCount = input.ReadInt32();
                    var frames = new List<Frame>();

                    for (var k = 0; k < frameCount; k++)
                    {
                        frames.Add(new Frame(new Tile(input.ReadInt32()), input.ReadSingle()));
                    }

                    // properties
                    var propertyCount = input.ReadInt32();
                    var properties = new List<Property>();

                    for (var k = 0; k < propertyCount; k++)
                    {
                        properties.Add(new Property(input.ReadString(), input.ReadString()));
                    }

                    tileDefinitions.Add(tile, new TileDefinition(type, frames, properties));
                }

                map.Add(new Tileset(name, texture, firstGid, tileWidth, tileHeight, tileDefinitions));
            }
        }
    }
}
