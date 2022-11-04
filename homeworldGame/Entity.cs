using static homeworld.Mobility.States;
using static homeworld.Consumable.States;
using static homeworld.Archetype.States;
using Optional;

namespace homeworld
{
    public class Entity
    {
        public int EntityID { get; set; }
        public List<IComponent> ComponentList { get; set; }
        public Entity(Archetype.States archetype, XY location)
        {
            EntityID = EntityManager.NextEntityID();
            ComponentList = Lookup.ArchetypeComponents(archetype, location);
        }

        // METHODS

        public List<Entity> Inventory()
        {
            return Lookup.EntityInventory(EntityID);
        }
        public XY Location()
        {
            return Lookup.EntityLocation(EntityID);
        }

        /*public override string ToString()
        {
            string name = Lookup.EntityName(EntityID);
            return $"{EntityID} - {name}";
        }*/
    }
}