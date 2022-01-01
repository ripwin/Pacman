using DefaultEcs;
using Microsoft.Xna.Framework;
using Pacman.Core.Components;

namespace Pacman.Core.Collision
{
    internal class PacmanCollision
    {
        private const float _offset = 0.001f;

        private readonly World _world;

        public PacmanCollision(World world)
        {
            _world = world;
        }

        [Subscribe]
        void On(in (PacmanComponent, Entity, Entity, Vector2, Vector2, float) message)
        {
            var (_, player, target, _, _, _) = message;
            ref var playerComponent = ref player.Get<PacmanComponent>();
            ref var playerPosition = ref player.Get<BodyComponent>();
            ref var playerVelocity = ref player.Get<VelocityComponent>();

            if (target.Has<TileComponent>())
            {
                CollidedWithTile(player, target);
            }
        }

        private void CollidedWithTile(Entity player, Entity tile)
        {
            ref var tileComponent = ref tile.Get<TileComponent>();

            switch (tileComponent.Type)
            {
                case Enums.TileType.TopSolid:
                    TopSolidTileCollision(player, tile);
                    break;
                case Enums.TileType.BottomSolid:
                    BottomSolidTileCollision(player, tile);
                    break;
                case Enums.TileType.LeftSolid:
                    LeftSolidTileCollision(player, tile);
                    break;
                case Enums.TileType.RightSolid:
                    RightSolidTileCollision(player, tile);
                    break;
                case Enums.TileType.LeftTopSolid:
                    if (!TopSolidTileCollision(player, tile))
                    {
                        LeftSolidTileCollision(player, tile);
                    }
                    break;
                case Enums.TileType.RightTopSolid:
                    if (!TopSolidTileCollision(player, tile))
                    {
                        RightSolidTileCollision(player, tile);
                    }
                    break;
                case Enums.TileType.LeftBottonSolid:
                    if (!BottomSolidTileCollision(player, tile))
                    {
                        LeftSolidTileCollision(player, tile);
                    }
                    break;
                case Enums.TileType.RightBottonSolid:
                    if (!BottomSolidTileCollision(player, tile))
                    {
                        RightSolidTileCollision(player, tile);
                    }
                    break;
                case Enums.TileType.TopBottomSolid:
                    if (!BottomSolidTileCollision(player, tile))
                    {
                        TopSolidTileCollision(player, tile);
                    }
                    break;
                case Enums.TileType.TopLeftBottomSolid:
                    if (!TopSolidTileCollision(player, tile))
                    {
                        if (!LeftSolidTileCollision(player, tile))
                        {
                            BottomSolidTileCollision(player, tile);
                        }
                    }
                    break;
                case Enums.TileType.TopRightBottomSolid:
                    if (!TopSolidTileCollision(player, tile))
                    {
                        if (!RightSolidTileCollision(player, tile))
                        {
                            BottomSolidTileCollision(player, tile);
                        }
                    }
                    break;
                case Enums.TileType.LeftTopRightSolid:
                    if (!LeftSolidTileCollision(player, tile))
                    {
                        if (!TopSolidTileCollision(player, tile))
                        {
                            RightSolidTileCollision(player, tile);
                        }
                    }
                    break;
                case Enums.TileType.LeftBottomRightSolid:
                    if (!LeftSolidTileCollision(player, tile))
                    {
                        if (!BottomSolidTileCollision(player, tile))
                        {
                            RightSolidTileCollision(player, tile);
                        }
                    }
                    break;
                case Enums.TileType.LeftRightSolid:
                    if (!LeftSolidTileCollision(player, tile))
                    {
                        RightSolidTileCollision(player, tile);
                    }
                    break;
            }
        }

        private bool TopSolidTileCollision(Entity player, Entity tile)
        {
            ref var bodyPosition = ref player.Get<BodyComponent>();
            ref var playerAabb = ref player.Get<AabbComponent>();
            ref var tilePosition = ref tile.Get<BodyComponent>();

            var playerOldPositon = bodyPosition.Position;

            if (playerOldPositon.Y + playerAabb.Size.Y <= tilePosition.Position.Y)
            {
                ref var playerVelocity = ref player.Get<VelocityComponent>();

                playerVelocity.Value.Y = tilePosition.Position.Y - playerAabb.Size.Y - bodyPosition.Position.Y - _offset;

                return true;
            }

            return false;
        }

        private bool LeftSolidTileCollision(Entity player, Entity tile)
        {
            ref var bodyPosition = ref player.Get<BodyComponent>();
            ref var playerAabb = ref player.Get<AabbComponent>();
            ref var tilePosition = ref tile.Get<BodyComponent>();

            var playerOldPositon = bodyPosition.Position;

            if (playerOldPositon.X + playerAabb.Size.X <= tilePosition.Position.X)
            {
                ref var playerVelocity = ref player.Get<VelocityComponent>();
                playerVelocity.Value.X = tilePosition.Position.X - playerAabb.Size.X - bodyPosition.Position.X - _offset;
                return true;
            }

            return false;
        }

        private bool RightSolidTileCollision(Entity player, Entity tile)
        {
            ref var bodyPosition = ref player.Get<BodyComponent>();
            ref var tilePosition = ref tile.Get<BodyComponent>();
            ref var tileAabb = ref tile.Get<AabbComponent>();

            var playerOldPositon = bodyPosition.Position;

            if (playerOldPositon.X >= tilePosition.Position.X + tileAabb.Size.X)
            {
                ref var playerVelocity = ref player.Get<VelocityComponent>();
                playerVelocity.Value.X = tilePosition.Position.X + tileAabb.Size.X - bodyPosition.Position.X + _offset;
                return true;
            }

            return false;
        }

        private bool BottomSolidTileCollision(Entity player, Entity tile)
        {
            ref var bodyPosition = ref player.Get<BodyComponent>();
            ref var tilePosition = ref tile.Get<BodyComponent>();
            ref var tileAabb = ref tile.Get<AabbComponent>();

            var playerOldPositon = bodyPosition.Position;

            if (playerOldPositon.Y >= tilePosition.Position.Y + tileAabb.Size.Y)
            {
                ref var playerVelocity = ref player.Get<VelocityComponent>();
                playerVelocity.Value.Y = tilePosition.Position.Y + tileAabb.Size.Y - bodyPosition.Position.Y + _offset;
                return true;
            }

            return false;
        }
    }
}
