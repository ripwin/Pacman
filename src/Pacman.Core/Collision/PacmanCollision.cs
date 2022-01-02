using DefaultEcs;
using Microsoft.Xna.Framework;
using Pacman.Core.Components;

namespace Pacman.Core.Collision
{
    internal class PacmanCollision
    {
        [Subscribe]
        void On(in (PacmanComponent, Entity, Entity, Vector2, Vector2, float) message)
        {
            var (_, pacman, target, _, _, _) = message;
            ref var pacmanComponent = ref pacman.Get<PacmanComponent>();
            ref var pacmanPosition = ref pacman.Get<BodyComponent>();
            ref var pacmanVelocity = ref pacman.Get<VelocityComponent>();

            if (target.Has<TileComponent>())
            {
                CollidedWithTile(pacman, target);
            }
        }

        private void CollidedWithTile(Entity pacman, Entity tile)
        {
            ref var tileComponent = ref tile.Get<TileComponent>();

            switch (tileComponent.Type)
            {
                case Enums.TileType.TopSolid:
                    TopSolidTileCollision(pacman, tile);
                    break;
                case Enums.TileType.BottomSolid:
                    BottomSolidTileCollision(pacman, tile);
                    break;
                case Enums.TileType.LeftSolid:
                    LeftSolidTileCollision(pacman, tile);
                    break;
                case Enums.TileType.RightSolid:
                    RightSolidTileCollision(pacman, tile);
                    break;
                case Enums.TileType.LeftTopSolid:
                    if (!TopSolidTileCollision(pacman, tile))
                    {
                        LeftSolidTileCollision(pacman, tile);
                    }
                    break;
                case Enums.TileType.RightTopSolid:
                    if (!TopSolidTileCollision(pacman, tile))
                    {
                        RightSolidTileCollision(pacman, tile);
                    }
                    break;
                case Enums.TileType.LeftBottonSolid:
                    if (!BottomSolidTileCollision(pacman, tile))
                    {
                        LeftSolidTileCollision(pacman, tile);
                    }
                    break;
                case Enums.TileType.RightBottonSolid:
                    if (!BottomSolidTileCollision(pacman, tile))
                    {
                        RightSolidTileCollision(pacman, tile);
                    }
                    break;
                case Enums.TileType.TopBottomSolid:
                    if (!BottomSolidTileCollision(pacman, tile))
                    {
                        TopSolidTileCollision(pacman, tile);
                    }
                    break;
                case Enums.TileType.TopLeftBottomSolid:
                    if (!TopSolidTileCollision(pacman, tile))
                    {
                        if (!LeftSolidTileCollision(pacman, tile))
                        {
                            BottomSolidTileCollision(pacman, tile);
                        }
                    }
                    break;
                case Enums.TileType.TopRightBottomSolid:
                    if (!TopSolidTileCollision(pacman, tile))
                    {
                        if (!RightSolidTileCollision(pacman, tile))
                        {
                            BottomSolidTileCollision(pacman, tile);
                        }
                    }
                    break;
                case Enums.TileType.LeftTopRightSolid:
                    if (!LeftSolidTileCollision(pacman, tile))
                    {
                        if (!TopSolidTileCollision(pacman, tile))
                        {
                            RightSolidTileCollision(pacman, tile);
                        }
                    }
                    break;
                case Enums.TileType.LeftBottomRightSolid:
                    if (!LeftSolidTileCollision(pacman, tile))
                    {
                        if (!BottomSolidTileCollision(pacman, tile))
                        {
                            RightSolidTileCollision(pacman, tile);
                        }
                    }
                    break;
                case Enums.TileType.LeftRightSolid:
                    if (!LeftSolidTileCollision(pacman, tile))
                    {
                        RightSolidTileCollision(pacman, tile);
                    }
                    break;
            }
        }

        private bool TopSolidTileCollision(Entity pacman, Entity tile)
        {
            ref var bodyPosition = ref pacman.Get<BodyComponent>();
            ref var pacmanAabb = ref pacman.Get<AabbComponent>();
            ref var tilePosition = ref tile.Get<BodyComponent>();

            var pacmanOldPositon = bodyPosition.Position;

            if (pacmanOldPositon.Y + pacmanAabb.Size.Y <= tilePosition.Position.Y)
            {
                ref var pacmanVelocity = ref pacman.Get<VelocityComponent>();

                pacmanVelocity.Value.Y = tilePosition.Position.Y - pacmanAabb.Size.Y - bodyPosition.Position.Y;

                CheckOldDirection(pacman);

                return true;
            }

            return false;
        }

        private bool LeftSolidTileCollision(Entity pacman, Entity tile)
        {
            ref var bodyPosition = ref pacman.Get<BodyComponent>();
            ref var pacmanAabb = ref pacman.Get<AabbComponent>();
            ref var tilePosition = ref tile.Get<BodyComponent>();

            var pacmanOldPositon = bodyPosition.Position;

            if (pacmanOldPositon.X + pacmanAabb.Size.X <= tilePosition.Position.X)
            {
                ref var pacmanVelocity = ref pacman.Get<VelocityComponent>();
                pacmanVelocity.Value.X = tilePosition.Position.X - pacmanAabb.Size.X - bodyPosition.Position.X;

                CheckOldDirection(pacman);

                return true;
            }

            return false;
        }

        private bool RightSolidTileCollision(Entity pacman, Entity tile)
        {
            ref var bodyPosition = ref pacman.Get<BodyComponent>();
            ref var tilePosition = ref tile.Get<BodyComponent>();
            ref var tileAabb = ref tile.Get<AabbComponent>();

            var pacmanOldPositon = bodyPosition.Position;

            if (pacmanOldPositon.X >= tilePosition.Position.X + tileAabb.Size.X)
            {
                ref var pacmanVelocity = ref pacman.Get<VelocityComponent>();
                pacmanVelocity.Value.X = tilePosition.Position.X + tileAabb.Size.X - bodyPosition.Position.X;

                CheckOldDirection(pacman);

                return true;
            }

            return false;
        }

        private bool BottomSolidTileCollision(Entity pacman, Entity tile)
        {
            ref var bodyPosition = ref pacman.Get<BodyComponent>();
            ref var tilePosition = ref tile.Get<BodyComponent>();
            ref var tileAabb = ref tile.Get<AabbComponent>();

            var pacmanOldPositon = bodyPosition.Position;

            if (pacmanOldPositon.Y >= tilePosition.Position.Y + tileAabb.Size.Y)
            {
                ref var pacmanVelocity = ref pacman.Get<VelocityComponent>();
                pacmanVelocity.Value.Y = tilePosition.Position.Y + tileAabb.Size.Y - bodyPosition.Position.Y;

                CheckOldDirection(pacman);

                return true;
            }

            return false;
        }

        private void CheckOldDirection(Entity pacman)
        {
            ref var pacmanComponent = ref pacman.Get<PacmanComponent>();
            ref var pacmanVelocity = ref pacman.Get<VelocityComponent>();

            if (pacmanComponent.Movement != pacmanComponent.OldMovement)
            {
                if (pacmanComponent.OldMovement == Enums.MovementType.Left)
                {
                    pacmanVelocity.Value = new Vector2(-2, 0);
                }
                else if (pacmanComponent.OldMovement == Enums.MovementType.Right)
                {
                    pacmanVelocity.Value = new Vector2(2, 0);
                }
                else if (pacmanComponent.OldMovement == Enums.MovementType.Up)
                {
                    pacmanVelocity.Value = new Vector2(0, -2);
                }
                else if (pacmanComponent.OldMovement == Enums.MovementType.Down)
                {
                    pacmanVelocity.Value = new Vector2(0, 2);
                }

                pacmanComponent.Movement = pacmanComponent.OldMovement;
            }
        }
    }
}
