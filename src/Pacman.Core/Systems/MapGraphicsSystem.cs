using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.Core.Components;
using Pacman.Core.TiledMap;

namespace Pacman.Core.Systems
{
    internal sealed class MapGraphicsSystem : AEntitySetSystem<GameTime>
    {
        private readonly Map _map;
        private readonly SpriteBatch _spriteBatch;

        private readonly Matrix _matrix;

        public MapGraphicsSystem(World world, Map map, SpriteBatch spriteBatch)
            : base(world.GetEntities().With<BodyComponent>().With<TileComponent>().AsSet())
        {
            _map = map;
            _spriteBatch = spriteBatch;

            _matrix =
                Matrix.CreateTranslation(0, 0, 0) *
                Matrix.CreateRotationZ(0) *
                Matrix.CreateScale(new Vector3(Game.Scale, Game.Scale, 1));
        }

        protected override void PreUpdate(GameTime state)
            => _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _matrix);

        protected override void Update(GameTime state, in Entity entity)
        {
            var body = entity.Get<BodyComponent>();

            var tileComponent = entity.Get<TileComponent>();
            var tile = tileComponent.Value;

            var tileset = _map.Tilesets.Where(t => t.Contains(new Tile(tile))).Single();

            var sourceRectangle = tileset.GetRegion(new Tile(tile));

            _spriteBatch.Draw(
                tileset.Texture,
                body.Position,
                sourceRectangle,
                Color.White);
        }

        protected override void PostUpdate(GameTime state)
            => _spriteBatch.End();
    }
}
