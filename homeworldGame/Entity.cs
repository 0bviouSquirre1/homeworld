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

        // METHODS

        public string Name()
        {
            return Lookup.EntityName(this);
        }
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