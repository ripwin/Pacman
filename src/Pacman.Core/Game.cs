using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pacman.Core.Screens;
using Pacman.Core.Utils;

namespace Pacman.Core
{
    public sealed class Game : Microsoft.Xna.Framework.Game
    {
        public readonly static string ContentFolderPath = "Content";

        public const int Height = 36 * 16;
        public const int Width = 28 * 16;
        public const int Scale = 2;

        private readonly GraphicsDeviceManager _graphics;
        private IScene? _scene;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = ContentFolderPath;
            IsMouseVisible = true;

            Input = new Input();
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Width * Scale;
            _graphics.PreferredBackBufferHeight = Height * Scale;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _scene = new GameScreen(this);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update(Keyboard.GetState());

            if (Input.IsKeyDown(Keys.Escape))
            { 
                Exit();
            }

            _scene!.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _scene!.Draw(gameTime);

            base.Draw(gameTime);
        }

        internal void ChangeScene(IScene scene)
        {
            _scene!.Dispose();

            _scene = scene;
        }

        internal Input Input { get; }
    }
}
