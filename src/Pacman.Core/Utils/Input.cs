using Microsoft.Xna.Framework.Input;

namespace Pacman.Core.Utils
{
    internal class Input
    {
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;

        public bool IsKeyUp(Keys key)
            =>_currentKeyboardState.IsKeyUp(key);

        public bool IsKeyDown(Keys key)
            => _currentKeyboardState.IsKeyDown(key);

        public bool IsKeyJustPressed(Keys key)
            => _currentKeyboardState.IsKeyDown(key) && !_previousKeyboardState.IsKeyDown(key);

        public void Update(KeyboardState state)
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = state;
        }
    }
}
