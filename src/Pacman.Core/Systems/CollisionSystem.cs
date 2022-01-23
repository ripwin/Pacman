using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Pacman.Core.Collision;
using Pacman.Core.Components;

namespace Pacman.Core.Systems
{
    internal enum CollisionAxis
    { 
        X,
        Y
    }

    internal sealed class CollisionSystem : AEntitySetSystem<GameTime>
    {
        private readonly EntitySet _movingEntitiesSet;
        private readonly World _world;

        public CollisionSystem(World world)
            : base(world.GetEntities().With<AabbComponent>().With<BodyComponent>().WithEither<TileComponent>().Or<DotComponent>().AsSet())
        {
            _world = world;

            _movingEntitiesSet = _world
                .GetEntities()
                .With<AabbComponent>()
                .With<BodyComponent>()
                .With<VelocityComponent>()
                .With<CollisionComponent>()
                .AsSet();

            _world.Subscribe(new PacmanCollision(_world));
        }

        protected override void Update(GameTime state, ReadOnlySpan<Entity> entities) 
        {
            foreach (var tileEntity in entities)
            {
                var aabbTile = tileEntity.Get<AabbComponent>();
                var bodyTile = tileEntity.Get<BodyComponent>();

                if (tileEntity.Has<DotComponent>())
                { 
                
                }

                foreach (var movingEntity in _movingEntitiesSet.GetEntities())
                {
                    var aabbMovingEntity = movingEntity.Get<AabbComponent>();
                    var bodyMovingEntity = movingEntity.Get<BodyComponent>();
                    var velocityMovingEntity = movingEntity.Get<VelocityComponent>();

                    if (velocityMovingEntity.Value.X != 0 && AabbComponent.IsOverlapping(
                        (aabbTile, bodyTile),
                        (aabbMovingEntity,
                            new BodyComponent
                            {
                                Position = new Vector2(bodyMovingEntity.Position.X + velocityMovingEntity.Value.X, bodyMovingEntity.Position.Y)
                            })))
                    {
                        DispatchCollision(movingEntity, tileEntity, CollisionAxis.X);
                    }

                    if (velocityMovingEntity.Value.Y != 0 && AabbComponent.IsOverlapping(
                        (aabbTile, bodyTile),
                        (aabbMovingEntity,
                            new BodyComponent
                            {
                                Position = new Vector2(bodyMovingEntity.Position.X, bodyMovingEntity.Position.Y + velocityMovingEntity.Value.Y)
                            })))
                    {
                        DispatchCollision(movingEntity, tileEntity, CollisionAxis.Y);
                    }
                }
            }
        }

        private void DispatchCollision(Entity source, Entity target, CollisionAxis collisionAxis)
        {
            if (source.Has<PacmanComponent>())
            {
                _world.Publish((source.Get<PacmanComponent>(), source, target, collisionAxis));
            }
        }
    }
}
