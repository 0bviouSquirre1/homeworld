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
            return Lookup.EntityInventory(this);
        }
        public XY Location()
        {
            return Lookup.EntityLocation(this);
        }

        public override string ToString()
        {
            string name = Lookup.EntityName(this);
            return $"{name}";
        }
    }
}