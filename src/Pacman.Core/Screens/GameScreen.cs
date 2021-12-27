using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Core.Systems;
using Pacman.Core.TiledMap;

namespace Pacman.Core.Screens
{
    internal sealed class GameScreen : IScene
    {
        private static readonly string FirstLevel = "level_1";

        private readonly ContentManager _content;
        private readonly SpriteBatch _spriteBatch;
        private readonly World _world;

        private readonly ISystem<GameTime> _onUpdateSystem;
        private readonly ISystem<GameTime> _onRenderSystem;

        public GameScreen(Game game)
        {
            _content = new ContentManager(game.Services, Game.ContentFolderPath);
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
            _world = new World();

            var map = _content.Load<Map>(FirstLevel);

            _onUpdateSystem = new SequentialSystem<GameTime>(
                new MapSystem(_world, map));

            _onRenderSystem = new SequentialSystem<GameTime>(
                new MapGraphicsSystem(_world, map, _spriteBatch));
        }

        public void Draw(GameTime gameTime)
            => _onRenderSystem.Update(gameTime);

        public void Update(GameTime gameTime)
            => _onUpdateSystem.Update(gameTime);

        public void Dispose()
        {
            _world.Dispose();
            _content.Dispose();
            _spriteBatch.Dispose();
        }
    }
}
