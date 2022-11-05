namespace homeworld
{
    public class Growable : IComponent
    {
        public int EntityID      { get; set; }
        public int ComponentID  { get; set; }
        public int ProduceID    { get; set; }
        public Growable()
        {
            ComponentID = IComponent.NextComponentID();
        }
        public Entity GetPlant()
        {
            return Lookup.EntityById(EntityID);
        }
        public Entity GetProduce()
        {
            return Lookup.EntityById(ProduceID);
        }
    }
}