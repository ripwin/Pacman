using DefaultEcs;
using Pacman.Core.Components;
using Pacman.Core.Systems;

namespace Pacman.Core.Collision
{
    internal class PacmanCollision
    {
        [Subscribe]
        private void On(in (PacmanComponent, Entity, Entity, CollisionAxis) message)
        {
            var (_, player, target, collisionAxis) = message;
            ref var playerComponent = ref player.Get<PacmanComponent>();
            ref var playerPosition = ref player.Get<BodyComponent>();
            ref var playerVelocity = ref player.Get<VelocityComponent>();

            if (target.Has<DotComponent>())
            {
                CollidedWithDot(player, target, collisionAxis);
            }
            else if (target.Has<TileComponent>())
            {
                CollidedWithTile(player, target, collisionAxis);
            }
        }

        private static void CollidedWithDot(Entity pacman, Entity dot, CollisionAxis collisionAxis)
        {
            dot.Dispose();
        }

        private static void CollidedWithTile(Entity pacman, Entity tile, CollisionAxis collisionAxis)
        {
            var aabbTile = tile.Get<AabbComponent>();
            var bodyTile = tile.Get<BodyComponent>();

            var velocityMovingEntity = pacman.Get<VelocityComponent>();
            ref var collisionMovingEntity = ref pacman.Get<CollisionComponent>();

            if (collisionAxis == CollisionAxis.X)
            {
                collisionMovingEntity.IsCollidingOnXAxis = true;

                if (velocityMovingEntity.Value.X < 0)
                {
                    collisionMovingEntity.ContactPointOnXAxis = bodyTile.Position.X + aabbTile.Size.X;
                }
                else
                {
                    collisionMovingEntity.ContactPointOnXAxis = bodyTile.Position.X;
                }

            }
            else
            {
                collisionMovingEntity.IsCollidingOnYAxis = true;

                if (velocityMovingEntity.Value.Y < 0)
                {
                    collisionMovingEntity.ContactPointOnYAxis = bodyTile.Position.Y + aabbTile.Size.Y;
                }
                else
                {
                    collisionMovingEntity.ContactPointOnYAxis = bodyTile.Position.Y;
                }
            }
        }
    }
}
