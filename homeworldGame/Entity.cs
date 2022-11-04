namespace homeworld
{
    public class Entity
    {
        public int EntityID { get; set; }
        public List<IComponent> ComponentList { get; set; }
        public Entity(Archetype.States archetype, string name, XY location, Mobility.States mobility)
        {
            EntityID = EntityManager.NextEntityID();
            ComponentList = Lookup.ArchetypeComponents(archetype, name, location, mobility);
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
    }
}