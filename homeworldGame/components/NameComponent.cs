namespace homeworld
{
    public class NameComponent : IComponent
    {
        private string entity_name = "(blank)";
        public int ComponentID { get; set; }
        public string Name
        {
            get => entity_name;
            set => entity_name = value;
        }

        public NameComponent(string name = "(blank)")
        {
            Name = name;
            ComponentID = IComponent.NextComponentID();
        }

        // METHODS

        public override string ToString()
        {
            return Name;
        }
    }
}