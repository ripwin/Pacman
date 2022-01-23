using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Core.Utils
{
    internal class FontAtlas<T> where T : notnull
    {
        private readonly Dictionary<T, SpriteFont> _fontAtlas;

        public FontAtlas()
            => _fontAtlas = new Dictionary<T, SpriteFont>();

        public void AddFont(T key, SpriteFont font)
            => _fontAtlas.Add(key, font);


        public SpriteFont GetFont(T key)
            => _fontAtlas[key];
    }
}
