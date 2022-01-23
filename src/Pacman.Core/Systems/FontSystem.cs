using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Core.Components;
using Pacman.Core.Enums;
using Pacman.Core.Utils;

namespace Pacman.Core.Systems
{
    internal sealed class FontSystem : AEntitySetSystem<GameTime>
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly FontAtlas<PacmanFontAtlas> _pacmanFontAtlas;

        private readonly Matrix _matrix;

        public FontSystem(
            World world,
            SpriteBatch spriteBatch,
            FontAtlas<PacmanFontAtlas> pacmanFontAtlas) 
            : base(world.GetEntities().With<FontComponent>().WithEither<ScoreComponent>().Or<LifeComponent>().AsSet())
        {
            _spriteBatch = spriteBatch;
            _pacmanFontAtlas = pacmanFontAtlas;

            _matrix =
                Matrix.CreateTranslation(0, 0, 0) *
                Matrix.CreateRotationZ(0) *
                Matrix.CreateScale(new Vector3(Game.Scale, Game.Scale, 1));
        }

        protected override void PreUpdate(GameTime state)
            => _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _matrix);

        protected override void Update(GameTime state, in Entity entity)
        {
            var ridigBody = entity.Get<BodyComponent>();
            var fontComponent = entity.Get<FontComponent>();

            var font = _pacmanFontAtlas.GetFont(fontComponent.Value);
            string text = string.Empty;

            if (entity.Has<ScoreComponent>())
            {
                var scoreComponent = entity.Get<ScoreComponent>();
                text = scoreComponent.Score.ToString();
            }
            else if (entity.Has<LifeComponent>())
            {
                var lifeComponent = entity.Get<LifeComponent>();
                text = lifeComponent.Value;
            }

            _spriteBatch.DrawString(
                font,
                text,
                ridigBody.Position,
                Color.White);
        }

        protected override void PostUpdate(GameTime state)
            => _spriteBatch.End();
    }
}
