using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pacman.Core.Components;
using Pacman.Core.Utils;

namespace Pacman.Core.Systems
{
    internal class PacmanSystem : AEntitySetSystem<GameTime>
    {
        private const int Speed = 2;

        private readonly Input _input;

        public PacmanSystem(World world, Input input)
            : base(world
                .GetEntities()
                .With<PacmanComponent>()
                .With<BodyComponent>()
                .With<AabbComponent>()
                .With<VelocityComponent>()
                .AsSet())
        {
            _input = input;
        }

        protected override void Update(GameTime state, in Entity entity)
        {
            ref var aabb = ref entity.Get<AabbComponent>();
            ref var body = ref entity.Get<BodyComponent>();
            ref var velocity = ref entity.Get<VelocityComponent>();
            ref var collision = ref entity.Get<CollisionComponent>();
            ref var pacman = ref entity.Get<PacmanComponent>();

            if (pacman.CurrentMovement == pacman.NextMovement)
            {
                if (collision.IsCollidingOnXAxis)
                {
                    body.Position.X = collision.ContactPointOnXAxis + (velocity.Value.X > 0 ? -aabb.Size.X : 0f);
                    velocity.Value.X = 0;
                    pacman.CurrentMovement = Enums.MovementType.Idle;
                }

                if (collision.IsCollidingOnYAxis)
                {
                    body.Position.Y = collision.ContactPointOnYAxis + (velocity.Value.Y > 0 ? -aabb.Size.Y : 0);
                    velocity.Value.Y = 0;
                    pacman.CurrentMovement = Enums.MovementType.Idle;
                }
            }
            else
            {
                if (pacman.NextMovement == Enums.MovementType.Left || pacman.NextMovement == Enums.MovementType.Right)
                {
                    if (!collision.IsCollidingOnXAxis)
                    {
                        pacman.CurrentMovement = pacman.NextMovement;
                        velocity.Value.Y = 0;
                    }
                }

                if (pacman.NextMovement == Enums.MovementType.Up || pacman.NextMovement == Enums.MovementType.Down)
                {
                    if (!collision.IsCollidingOnYAxis)
                    {
                        pacman.CurrentMovement = pacman.NextMovement;
                        velocity.Value.X = 0;
                    }
                }
            }

            if (!collision.IsCollidingOnXAxis)
            {
                body.Position.X += velocity.Value.X;
            }

            if (!collision.IsCollidingOnYAxis)
            { 
                body.Position.Y += velocity.Value.Y;
            }

            if (_input.IsKeyJustPressed(Keys.Left) && pacman.CurrentMovement != Enums.MovementType.Left)
            {
                velocity.Value = new Vector2(-Speed, velocity.Value.Y);
                pacman.NextMovement = Enums.MovementType.Left;
            }
            else if (_input.IsKeyJustPressed(Keys.Right) && pacman.CurrentMovement != Enums.MovementType.Right)
            {
                velocity.Value = new Vector2(Speed, velocity.Value.Y);
                pacman.NextMovement = Enums.MovementType.Right;
            }
            else if (_input.IsKeyJustPressed(Keys.Up) && pacman.CurrentMovement != Enums.MovementType.Up)
            {
                velocity.Value = new Vector2(velocity.Value.X, -Speed);
                pacman.NextMovement = Enums.MovementType.Up;
            }
            else if (_input.IsKeyJustPressed(Keys.Down) && pacman.CurrentMovement != Enums.MovementType.Down)
            {
                velocity.Value = new Vector2(velocity.Value.X, Speed);
                pacman.NextMovement = Enums.MovementType.Down;
            }

            collision.IsCollidingOnXAxis = false;
            collision.IsCollidingOnYAxis = false;
            collision.ContactPointOnXAxis = 0f;
            collision.ContactPointOnYAxis = 0f;
        }
    }
}
