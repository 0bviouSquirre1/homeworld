using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;

namespace homeworld
{
    public class Entity
    {
        public int EntityID { get; set; }
        public List<IComponent> ComponentList { get; set; }
        public Entity(Archetype.States archetype)
        {
            EntityID = EntityManager.NextEntityID();
            ComponentList = Lookup.ArchetypeComponents(archetype);
        }
    }
}