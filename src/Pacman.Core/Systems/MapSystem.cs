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
        private readonly static string Pacman = "Pacman";
        private readonly static string Inky = "Inky";
        private readonly static string Pinky = "Pinky";
        private readonly static string Clyde = "Clyde";
        private readonly static string Blinky = "Blinky";
        private readonly static string Life = "Life";
        private readonly static string Score = "Score";

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
                else if (layer is ObjectLayer objectLayer)
                {
                    InitObjectLayer(objectLayer);
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

                if (!string.IsNullOrWhiteSpace(definition.Type) && Enum.TryParse<TileType>(definition.Type, out var type))
                {
                    if (type is TileType.Solid)
                    {
                        var tileEntity = _world.CreateEntity();
                        tileEntity.Set(new BodyComponent { Position = new Vector2((i % _map.Width) * _map.TileWidth, (i / _map.Width) * _map.TileHeight) });
                        tileEntity.Set(new AabbComponent { Size = new Vector2(_map.TileWidth, _map.TileHeight) });
                        tileEntity.Set(new TileComponent { Value = tileLayer.Tiles[i].Value, Type = type });
                    }
                    else if (type is TileType.Dot)
                    {
                        var dotEntity = _world.CreateEntity();
                        dotEntity.Set(new BodyComponent { Position = new Vector2((i % _map.Width) * _map.TileWidth + 8, (i / _map.Width) * _map.TileHeight + 8) });
                        dotEntity.Set(new AabbComponent { Size = new Vector2(4, 4) });
                        dotEntity.Set(new DotComponent { IsBig = false });
                        dotEntity.Set(new TextureComponent { Value = PacmanTextureAtlas.Dot });
                    }
                    else if (type is TileType.BigDot)
                    {
                        var bigDotEntity = _world.CreateEntity();
                        bigDotEntity.Set(new BodyComponent { Position = new Vector2((i % _map.Width) * _map.TileWidth + 8, (i / _map.Width) * _map.TileHeight + 8) });
                        bigDotEntity.Set(new AabbComponent { Size = new Vector2(16, 16) });
                        bigDotEntity.Set(new DotComponent { IsBig = true });
                        bigDotEntity.Set(new TextureComponent { Value = PacmanTextureAtlas.BigDot });
                    }
                }
            }
        }

        private void InitObjectLayer(ObjectLayer objectLayer)
        {
            foreach (var o in objectLayer.Objects)
            {
                if (o.Name.Equals(Pacman) && o is TiledMap.Rectangle pacman)
                {
                    var entity = _world.CreateEntity();
                    entity.Set(new PacmanComponent { CurrentMovement = MovementType.Idle, NextMovement = MovementType.Idle });
                    entity.Set(new BodyComponent { Position = new Vector2(pacman.X, pacman.Y) });
                    entity.Set(new AabbComponent { Size = new Vector2(pacman.Width, pacman.Height) });
                    entity.Set(new TextureComponent { Value = PacmanTextureAtlas.Pacman });
                    entity.Set(new VelocityComponent());
                    entity.Set(new CollisionComponent());
                }
                else if (o.Name.Equals(Inky) && o is TiledMap.Rectangle inky)
                {
                    var entity = _world.CreateEntity();
                    entity.Set<GhostComponent>();
                    entity.Set(new BodyComponent { Position = new Vector2(inky.X, inky.Y) });
                    entity.Set(new AabbComponent { Size = new Vector2(inky.Width, inky.Height) });
                    entity.Set(new TextureComponent { Value = PacmanTextureAtlas.Inky });
                }
                else if (o.Name.Equals(Pinky) && o is TiledMap.Rectangle pinky)
                {
                    var entity = _world.CreateEntity();
                    entity.Set<GhostComponent>();
                    entity.Set(new BodyComponent { Position = new Vector2(pinky.X, pinky.Y) });
                    entity.Set(new AabbComponent { Size = new Vector2(pinky.Width, pinky.Height) });
                    entity.Set(new TextureComponent { Value = PacmanTextureAtlas.Pinky });
                }
                else if (o.Name.Equals(Clyde) && o is TiledMap.Rectangle clyde)
                {
                    var entity = _world.CreateEntity();
                    entity.Set<GhostComponent>();
                    entity.Set(new BodyComponent { Position = new Vector2(clyde.X, clyde.Y) });
                    entity.Set(new AabbComponent { Size = new Vector2(clyde.Width, clyde.Height) });
                    entity.Set(new TextureComponent { Value = PacmanTextureAtlas.Clyde });
                }
                else if (o.Name.Equals(Blinky) && o is TiledMap.Rectangle blinky)
                {
                    var entity = _world.CreateEntity();
                    entity.Set<GhostComponent>();
                    entity.Set(new BodyComponent { Position = new Vector2(blinky.X, blinky.Y) });
                    entity.Set(new AabbComponent { Size = new Vector2(blinky.Width, blinky.Height) });
                    entity.Set(new TextureComponent { Value = PacmanTextureAtlas.Blinky });
                }
                else if (o.Name.Equals(Score) && o is Text score)
                {
                    var entity = _world.CreateEntity();
                    entity.Set<ScoreComponent>();
                    entity.Set(new BodyComponent { Position = new Vector2(score.X, score.Y) });
                    entity.Set(new FontComponent { Value = PacmanFontAtlas.General });
                }
                else if (o.Name.Equals(Life) && o is Text life)
                {
                    var entity = _world.CreateEntity();
                    entity.Set(new LifeComponent { Value = life.Value });
                    entity.Set(new BodyComponent { Position = new Vector2(life.X, life.Y) });
                    entity.Set(new FontComponent { Value = PacmanFontAtlas.General });
                }
            }
        }
    }
}
