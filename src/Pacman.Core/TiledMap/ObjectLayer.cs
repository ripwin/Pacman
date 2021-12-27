using System.Collections.ObjectModel;

namespace Pacman.Core.TiledMap
{
    public sealed class ObjectLayer : ILayer
    {
        public string Name { get; }

        public ReadOnlyCollection<IObject> Objects { get; }

        public ObjectLayer(string name, List<IObject> objects)
        {
            Name = name;
            Objects = new(objects.ToArray()); 
        }

        public List<IObject> GetObjectByName(string name)
            => Objects.Where(o => o.Name.Equals(name)).ToList();

        public List<IObject> GetObjectType(string type)
            => Objects.Where(o => o.Type.Equals(type)).ToList();

        public List<IObject> GetObjectByNameAndType(string name, string type)
            => Objects.Where(o => o.Name.Equals(name) && o.Type.Equals(type)).ToList();
    }
}
