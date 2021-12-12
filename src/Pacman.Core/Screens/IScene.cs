using Microsoft.Xna.Framework;

namespace Pacman.Core.Screens
{
    public interface IScene : IDisposable
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
