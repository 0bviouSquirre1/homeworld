namespace homeworld
{
    public class Growable : IComponent
    {
        public int ComponentID { get; set; }
        public int ProduceID { get; set; }
        public Growable(int produce_id)
        {
            ComponentID = IComponent.NextComponentID();
            ProduceID = produce_id;
        }
    }
}