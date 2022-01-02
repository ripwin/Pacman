using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Pacman.Core.Collision;
using Pacman.Core.Components;
using Pacman.Core.Utils;

namespace Pacman.Core.Systems
{
    internal sealed class TileCollisionSystem : AEntitySetSystem<GameTime>
    {
        private const int _precisionDigits = 3;

        private readonly World _world;
        private readonly Quadtree _quadtree;

        private readonly EntitySet _movingEntitiesSet;

        public TileCollisionSystem(World world, Utils.Rectangle bound)
            : base(world.GetEntities().With<AabbComponent>().With<BodyComponent>().With<TileComponent>().AsSet())
        {
            _world = world;
            _quadtree = new Quadtree(bound);
            _movingEntitiesSet = world.GetEntities().With<AabbComponent>().With<BodyComponent>().With<VelocityComponent>().AsSet();

            // Load collision 
            _world.Subscribe(new PacmanCollision());
            //_world.Subscribe(new MobCollision());
        }

        protected override void Update(GameTime gameTime, ReadOnlySpan<Entity> entities)
        {
            _quadtree.Clear();

            foreach (var entity in entities)
            {
                _quadtree.Insert(entity);
            }

            foreach (var entity in _movingEntitiesSet.GetEntities())
            {
                var aabb = entity.Get<AabbComponent>();
                ref var body = ref entity.Get<BodyComponent>();
                ref var velocity = ref entity.Get<VelocityComponent>();

                // Prevent float imprecision
                velocity.Value.X = (float)Math.Round(velocity.Value.X, _precisionDigits);
                velocity.Value.Y = (float)Math.Round(velocity.Value.Y, _precisionDigits);

                // Broad phase (using Quadtree)
                var region = new Utils.Rectangle(
                    velocity.Value.X > 0 ? body.Position.X : body.Position.X + velocity.Value.X,
                    velocity.Value.Y > 0 ? body.Position.Y : body.Position.Y + velocity.Value.Y,
                    aabb.Size.X + Math.Abs(velocity.Value.X),
                    aabb.Size.Y + Math.Abs(velocity.Value.Y));

                var targets = _quadtree.Retrieve(region);

                // Narrow phase (using Swept AABB)
                var overlappingEntities = new List<(Entity, float)>();

                foreach (var target in targets)
                {
                    if (target == entity)
                    {
                        continue;
                    }

                    var (isOverlapping, _, contactNormal, contactTime) = IsOverlapping(entity, target);

                    if (isOverlapping && contactTime < 1)
                    {
                        overlappingEntities.Add((target, contactTime));
                    }
                }

                // Order by contact time to resolve the smallest contact time first
                overlappingEntities.Sort((x, y) => x.Item2.CompareTo(y.Item2));

                foreach (var (e, _) in overlappingEntities)
                {
                    var (isOverlapping, contactPoint, contactNormal, contactTime) = IsOverlapping(entity, e);

                    if (isOverlapping && contactTime < 1)
                    {
                        DispatchCollision(entity, e, contactPoint, contactNormal, contactTime);
                    }
                }

                // Update position after resolving collision (maybe not the right place)
                body.Position += velocity.Value;
            }
        }

        private void DispatchCollision(Entity source, Entity target, Vector2 contactPoint, Vector2 contactNormal, float contactTime)
        {
            if (source.Has<PacmanComponent>())
            {
                _world.Publish((source.Get<PacmanComponent>(), source, target, contactPoint, contactNormal, contactTime));
            }
            else if (source.Has<GhostComponent>())
            {
                _world.Publish((source.Get<GhostComponent>(), source, target, contactPoint, contactNormal, contactTime));
            }
        }

        private (bool IsOverlapping, Vector2 ContactPoint, Vector2 ContactNormal, float ContactTime) IsOverlapping(Entity source, Entity target)
        {
            var sourceVelocity = source.Get<VelocityComponent>();

            var sourceBody = source.Get<BodyComponent>();
            var sourceAabb = source.Get<AabbComponent>();

            var targetBody = target.Get<BodyComponent>();
            var targetAabb = target.Get<AabbComponent>();

            // Create an expanded AABB (target)
            var expandedTargetPosition = new BodyComponent { Position = targetBody.Position - sourceAabb.Size / 2 };
            var expandedTargetSize = new AabbComponent { Size = targetAabb.Size + sourceAabb.Size };

            return RayComponent.IsOverlapping(
                (new RayComponent { To = sourceBody.Position + sourceAabb.Size / 2 + sourceVelocity.Value },
                    new BodyComponent { Position = sourceBody.Position + sourceAabb.Size / 2 }),
                (expandedTargetSize, expandedTargetPosition));
        }
    }
}
