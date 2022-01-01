namespace Pacman.Core.Utils
{
    internal struct Rectangle
    {
        public float X { get; }
        public float Y { get; }
        public float Width { get; }
        public float Height { get; }

        public Rectangle(float x, float y, float width, float height)
            => (X, Y, Width, Height) = (x, y, width, height);

        public bool IsOverlapping(Rectangle other)
            => (X < other.X + other.Width && X + Width > other.X) &&
                (Y < other.Y + other.Height && Y + Height > other.Y);
    }
}
