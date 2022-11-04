namespace homeworld
{
    public interface IComponent
    {
        public int PlantID { get; set; }
        public int ComponentID { get; set; }
        private static int last_component_id = 0;

        public static int NextComponentID()
        {
            last_component_id++;
            return last_component_id;
        }
    }
}