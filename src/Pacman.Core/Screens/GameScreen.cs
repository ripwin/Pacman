using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Core.Enums;
using Pacman.Core.Graphics;
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
                new MapSystem(_world, map),
                new PacmanSystem(_world, game.Input),
                new CollisionSystem(_world));

            var pacmanTextureAtlas = new TextureAtlas<PacmanTextureAtlas>(_content.Load<Texture2D>("pacman_tileset"));
            pacmanTextureAtlas.AddRegion(PacmanTextureAtlas.Blinky, new Microsoft.Xna.Framework.Rectangle(32, 32, 32, 32));
            pacmanTextureAtlas.AddRegion(PacmanTextureAtlas.Clyde, new Microsoft.Xna.Framework.Rectangle(32, 128, 32, 32));
            pacmanTextureAtlas.AddRegion(PacmanTextureAtlas.Inky, new Microsoft.Xna.Framework.Rectangle(32, 96, 32, 32));
            pacmanTextureAtlas.AddRegion(PacmanTextureAtlas.Pinky, new Microsoft.Xna.Framework.Rectangle(32, 64, 32, 32));
            pacmanTextureAtlas.AddRegion(PacmanTextureAtlas.Pacman, new Microsoft.Xna.Framework.Rectangle(32, 0, 32, 32));
            pacmanTextureAtlas.AddRegion(PacmanTextureAtlas.Dot, new Microsoft.Xna.Framework.Rectangle(166, 166, 4, 4));
            pacmanTextureAtlas.AddRegion(PacmanTextureAtlas.BigDot, new Microsoft.Xna.Framework.Rectangle(176, 160, 16, 16));

            _onRenderSystem = new SequentialSystem<GameTime>(
                new MapGraphicsSystem(_world, map, _spriteBatch),
                new GraphicsSystem(_world, _spriteBatch, pacmanTextureAtlas));
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
