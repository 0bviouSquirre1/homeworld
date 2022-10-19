namespace homeworld
{
    public class Container : Item
    {
        public bool isFull;
        public bool isLit;
        public Container(string name) : base (name)
        {
        }
        public Container(string name, XYComponent location) : base (name, location)
        {
        }

        // Methods

        
    }
}