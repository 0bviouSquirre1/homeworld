namespace homeworld
{
    public static class Display
    {
        public static void AllEntities()
        {
            foreach (KeyValuePair<int, Dictionary<int, IComponent>> entity in EntityManager.AllEntities)
            {
                int entity_id = entity.Key;
                Dictionary<int, IComponent> component_list = entity.Value;

                Console.Write(entity_id.ToString("000"));
                foreach (KeyValuePair<int, IComponent> component_node in component_list)
                {
                    IComponent component = component_node.Value;
                    Console.Write(" : ");
                    Console.Write(component.ToString());
                }
                Console.WriteLine();
            }
        }
    }
}