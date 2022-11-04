namespace homeworld
{
    public class NameComponent : IComponent
    {
        public int PlantID     { get; set; }
        public int ComponentID  { get; set; }
        public string Name      { get; set; }

        public NameComponent(string name = "(blank)")
        {
            ComponentID = IComponent.NextComponentID();

            // Component-specific setup
            Name        = name;

        }

        // METHODS

        public override string ToString()
        {
            return Name;
        }
    }
}