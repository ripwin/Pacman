using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Core.Utils
{
    internal class TextureAtlas<T> where T : notnull
    {
        private readonly Texture2D _texture;
        private readonly Dictionary<T, Rectangle> _textureAtlas;

        public TextureAtlas(Texture2D texture)
        {
            _texture = texture;
            _textureAtlas = new Dictionary<T, Rectangle>();
        }

        public void AddRegion(T key, Rectangle region)
        {
            if (region.Size.X < 0 ||
                region.Size.Y < 0 ||
                region.X < 0 ||
                region.Y < 0 ||
                region.X + region.Size.X > _texture.Width ||
                region.Y + region.Size.Y > _texture.Height)
            {
                throw new ArgumentException("The region is not fully within the texture !");
            }

            _textureAtlas.Add(key, region);
        }

        public Rectangle GetRegion(T key)
            => _textureAtlas[key];

        public Texture2D Texture()
            => _texture;
    }
}
