namespace homeworld
{
    public class Archetype : IComponent
    {
        public int EntityID     { get; set; }
        public int ComponentID  { get; set; }
        public States State     { get; set; }
        public enum States
        {
            None,
            Player,
            Produce,
            Cup,
            Kettle,
            Bucket,
            Well,
            Tea,
            Plant,
            Item
        }
        public Archetype(States archetype)
        {
            ComponentID = IComponent.NextComponentID();

            // Component-specific setup
            State       = archetype;
        }
    }
}