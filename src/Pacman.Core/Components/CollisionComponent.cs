using Microsoft.Xna.Framework;

namespace Pacman.Core.Components
{
    internal struct CollisionComponent
    {
        public bool CollisionTop;
        public bool CollisionBottom;
        public bool CollisionLeft;
        public bool CollisionRight;

        public Vector2 Velocity;
    }
}
