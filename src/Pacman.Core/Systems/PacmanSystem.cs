using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pacman.Core.Components;
using Pacman.Core.Utils;

namespace Pacman.Core.Systems
{
    internal sealed class PacmanSystem : AEntitySetSystem<GameTime>
    {
        private const int Speed = 5;

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
            ref var pacman = ref entity.Get<PacmanComponent>();
            ref var velocity = ref entity.Get<VelocityComponent>();

            if (_input.IsKeyDown(Keys.Left))
            {
                pacman.Movement = Enums.MovementType.Left;
                velocity.Value = new Vector2(-Speed, 0);
            }
            else if (_input.IsKeyDown(Keys.Right))
            {
                pacman.Movement = Enums.MovementType.Right;
                velocity.Value = new Vector2(Speed, 0);
            }
            else if (_input.IsKeyDown(Keys.Up))
            {
                pacman.Movement = Enums.MovementType.Up;
                velocity.Value = new Vector2(0, -Speed);
            }
            else if (_input.IsKeyDown(Keys.Down))
            {
                pacman.Movement = Enums.MovementType.Down;
                velocity.Value = new Vector2(0, Speed);
            }
        }
    }
}
