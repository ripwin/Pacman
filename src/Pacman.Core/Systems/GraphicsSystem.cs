using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Core.Components;
using Pacman.Core.Enums;
using Pacman.Core.Utils;

namespace Pacman.Core.Systems
{
    internal sealed class GraphicsSystem : AEntitySetSystem<GameTime>
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly TextureAtlas<PacmanTextureAtlas> _pacmanTextureAtlas;

        private readonly Matrix _matrix;

        public GraphicsSystem(
            World world,
            SpriteBatch spriteBatch,
            TextureAtlas<PacmanTextureAtlas> pacmanTextureAtlas)
            : base(world.GetEntities().With<BodyComponent>().With<AabbComponent>().WithEither<TextureComponent>().AsSet())
        {
            _spriteBatch = spriteBatch;
            _pacmanTextureAtlas = pacmanTextureAtlas;

            _matrix =
                Matrix.CreateTranslation(0, 0, 0) *
                Matrix.CreateRotationZ(0) *
                Matrix.CreateScale(new Vector3(Game.Scale, Game.Scale, 1));
        }

        protected override void PreUpdate(GameTime state)
            => _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _matrix);

        protected override void Update(GameTime state, in Entity entity)
        {
            ref var ridigBody = ref entity.Get<BodyComponent>();
            ref var aabb = ref entity.Get<AabbComponent>();
            ref var textureComponent = ref entity.Get<TextureComponent>();

            var sourceRectangle = _pacmanTextureAtlas.GetRegion(textureComponent.Value);
            var texture = _pacmanTextureAtlas.Texture();

            _spriteBatch.Draw(
                texture,
                ridigBody.Position - new Vector2(aabb.Size.X / 2, aabb.Size.Y / 2),
                sourceRectangle,
                Color.White);
        }

        protected override void PostUpdate(GameTime state)
            => _spriteBatch.End();
    }
}
