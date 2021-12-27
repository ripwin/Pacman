using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Pacman.Core.Components;
using Pacman.Core.Enums;
using Pacman.Core.TiledMap;

namespace Pacman.Core.Systems
{
    internal sealed class MapSystem : AComponentSystem<GameTime, TileComponent>
    {
        private readonly Map _map;
        private readonly World _world;

        public MapSystem(World world, Map map)
            : base(world)
        {
            _map = map;
            _world = world;

            // Set the enities
            foreach (var layer in map.Layers)
            {
                if (layer is TileLayer tileLayer)
                {
                    InitTileLayer(tileLayer);
                }
            }
        }

        private void InitTileLayer(TileLayer tileLayer)
        {
            // Create tiles
            for (var i = 0; i < tileLayer.Tiles.Count; i++)
            {
                // Skip the empty tile
                if (tileLayer.Tiles[i].Value == Tile.Empty)
                {
                    continue;
                }

                var tile = new Tile(tileLayer.Tiles[i].Value);
                var tileset = _map.Tilesets.Where(t => t.Contains(tile)).Single();
                var definition = tileset.GetDefinition(tile);

                var tileEntity = _world.CreateEntity();

                tileEntity.Set(new BodyComponent { Position = new Vector2((i % _map.Width) * _map.TileWidth, (i / _map.Width) * _map.TileHeight) });

                var tileComponent = new TileComponent { Value = tileLayer.Tiles[i].Value, Type = TileType.None };

                if (!string.IsNullOrWhiteSpace(definition.Type) && Enum.TryParse<TileType>(definition.Type, out var type))
                {
                    tileComponent.Type = type;
                    tileEntity.Set(new AabbComponent { Size = new Vector2(_map.TileWidth, _map.TileHeight) });
                }

                tileEntity.Set(tileComponent);
            }
        }
    }
}
