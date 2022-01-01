using Microsoft.Xna.Framework;

namespace Pacman.Core.Components
{
    /// <summary>
    /// This is a simple line.
    /// 
    /// The code collision between an AABB and a line is inspired by https://github.com/noonat/intersect by Nathan Ostgard (zlib License).
    /// And there's also a great explanation on https://www.youtube.com/watch?v=eo_hrg6kVA8 by Gabe Ambrosio.
    /// </summary>
    internal struct RayComponent
    {
        public Vector2 To;

        public static (bool IsOverlapping, Vector2 ContactPoint, Vector2 ContactNormal, float ContactTime) IsOverlapping(
            (RayComponent Ray, BodyComponent Body) left,
            (AabbComponent Aabb, BodyComponent Body) right)
        {
            var delta = left.Ray.To - left.Body.Position;
            var scale = new Vector2(1.0f) / delta;

            var sign = new Vector2(Math.Sign(scale.X), Math.Sign(scale.Y));
            var half = right.Aabb.Size * new Vector2(0.5f);
            var pos = right.Body.Position + half;

            var near = (pos - sign * half - left.Body.Position) * scale;
            var far = (pos + sign * half - left.Body.Position) * scale;

            if (near.X > far.Y || near.Y > far.X)
            {
                return (false, new Vector2(), new Vector2(), 0);
            }

            var nearTime = Math.Max(near.X, near.Y);
            var farTime = Math.Min(far.X, far.Y);

            if (nearTime >= 1 || farTime <= 0)
            {
                return (false, new Vector2(), new Vector2(), 0);
            }

            var time = MathHelper.Clamp(nearTime, 0, 1);

            var normal = new Vector2();

            if (near.X > near.Y)
            {
                normal.X = -sign.X;
                normal.Y = 0;
            }
            else
            {
                normal.X = 0;
                normal.Y = -sign.Y;
            }

            var contact = left.Body.Position + delta * new Vector2(time);

            return (true, contact, normal, time);
        }
    }
}