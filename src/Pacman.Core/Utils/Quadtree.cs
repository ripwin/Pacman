using DefaultEcs;
using Pacman.Core.Components;

namespace Pacman.Core.Utils
{
    /// <summary>
    /// This is a simple Quadtree implementation.
    /// 
    /// This code is inspired by https://gamedevelopment.tutsplus.com/tutorials/quick-tip-use-quadtrees-to-detect-likely-collisions-in-2d-space--gamedev-374 by Steven Lambert (MIT License).
    /// Please see https://github.com/straker/kontra/blob/main/src/quadtree.js for more information.
    /// 
    /// And also https://github.com/CodingTrain/QuadTree by Coding Train (MIT License).
    /// </summary>
    internal class Quadtree
    {
        private enum QuadRegion
        {
            None = -1,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        private const int _maxEntities = 8;
        private const int _maxLevels = 4;

        private readonly int _level;
        private readonly Rectangle _bound;

        private readonly List<Entity> _entities;
        private readonly Quadtree?[] _nodes;

        public Quadtree(Rectangle bound)
            : this(0, bound)
        {
        }

        public Quadtree(int level, Rectangle bound)
        {
            _level = level;
            _bound = bound;
            _entities = new List<Entity>();
            _nodes = new Quadtree[4];
        }

        public void Clear()
        {
            _entities.Clear();

            for (var i = 0; i < _nodes.Length; i++)
            {
                if (_nodes[i] != null)
                {
                    _nodes[i]!.Clear();
                    _nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            var width = _bound.Width / 2;
            var height = _bound.Height / 2;
            var x = _bound.X;
            var y = _bound.Y;

            _nodes[(int)QuadRegion.TopLeft] = new Quadtree(_level + 1, new Rectangle(x, y, width, height));
            _nodes[(int)QuadRegion.TopRight] = new Quadtree(_level + 1, new Rectangle(x + width, y, width, height));
            _nodes[(int)QuadRegion.BottomLeft] = new Quadtree(_level + 1, new Rectangle(x, y + height, width, height));
            _nodes[(int)QuadRegion.BottomRight] = new Quadtree(_level + 1, new Rectangle(x + width, y + height, width, height));
        }

        private QuadRegion GetIndex(Entity entity)
        {
            var body = entity.Get<BodyComponent>();
            var aabb = entity.Get<AabbComponent>();

            var quadOrigin = new Microsoft.Xna.Framework.Vector2(_bound.X + (_bound.Width / 2), _bound.Y + (_bound.Height / 2));

            var isInTopRegion = body.Position.Y < quadOrigin.Y && body.Position.Y + aabb.Size.Y < quadOrigin.Y;
            var isInBottomRegion = body.Position.Y > quadOrigin.Y;

            var isInLeftRegion = body.Position.X < quadOrigin.X && body.Position.X + aabb.Size.X < quadOrigin.X;
            var isInRightRegion = body.Position.X > quadOrigin.X;

            if (isInTopRegion)
            {
                if (isInLeftRegion)
                {
                    return QuadRegion.TopLeft;
                }
                else if (isInRightRegion)
                {
                    return QuadRegion.TopRight;
                }
            }
            else if (isInBottomRegion)
            {
                if (isInLeftRegion)
                {
                    return QuadRegion.BottomLeft;
                }
                else if (isInRightRegion)
                {
                    return QuadRegion.BottomRight;
                }
            }

            return QuadRegion.None;
        }

        public void Insert(Entity entity)
        {
            if (_nodes[0] != null)
            {
                var index = GetIndex(entity);

                if (index != QuadRegion.None)
                {
                    _nodes[(int)index]!.Insert(entity);
                    return;
                }
            }

            _entities.Add(entity);

            if (_entities.Count > _maxEntities && _level < _maxLevels)
            {
                if (_nodes[0] == null)
                {
                    Split();
                }

                var i = 0;
                while (i < _entities.Count)
                {
                    var index = GetIndex(_entities[i]);

                    if (index != QuadRegion.None)
                    {
                        _nodes[(int)index]!.Insert(_entities[i]);
                        _entities.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public List<Entity> Retrieve(Entity entity)
        {
            var returnObjects = new List<Entity>();
            var index = GetIndex(entity);

            if (index != QuadRegion.None && _nodes[0] != null)
            {
                returnObjects.AddRange(_nodes[(int)index]!.Retrieve(entity));
            }

            returnObjects.AddRange(_entities);

            return returnObjects;
        }

        public List<Entity> Retrieve(Rectangle region)
        {
            var returnObjects = new List<Entity>();

            if (_bound.IsOverlapping(region))
            {
                foreach (var entity in _entities)
                {
                    var body = entity.Get<BodyComponent>();
                    var aabb = entity.Get<AabbComponent>();

                    if (region.IsOverlapping(new Rectangle(body.Position.X, body.Position.Y, aabb.Size.X, aabb.Size.Y)))
                    {
                        returnObjects.Add(entity);
                    }
                }

                if (_nodes[0] != null)
                {
                    returnObjects.AddRange(_nodes[(int)QuadRegion.TopLeft]!.Retrieve(region));
                    returnObjects.AddRange(_nodes[(int)QuadRegion.TopRight]!.Retrieve(region));
                    returnObjects.AddRange(_nodes[(int)QuadRegion.BottomLeft]!.Retrieve(region));
                    returnObjects.AddRange(_nodes[(int)QuadRegion.BottomRight]!.Retrieve(region));
                }
            }

            return returnObjects;
        }
    }
}
