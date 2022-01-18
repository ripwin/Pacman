using Microsoft.Xna.Framework;

namespace Pacman.Core.Components
{
    internal struct AabbComponent
    {
        public Vector2 Size;

        public static bool IsOverlapping((AabbComponent Aabb, BodyComponent Body) left, (AabbComponent Aabb, BodyComponent Body) right)
            => (left.Body.Position.X < right.Body.Position.X + right.Aabb.Size.X && left.Body.Position.X + left.Aabb.Size.X > right.Body.Position.X) &&
                (left.Body.Position.Y < right.Body.Position.Y + right.Aabb.Size.Y && left.Body.Position.Y + left.Aabb.Size.Y > right.Body.Position.Y);
    }
}
