namespace homeworld
{
    public class Entity : IExists
    {
        [Flags]
        public enum Status
        {
            None = 0,
            Dead = 1
        }

        private string _name = "";
        private XY _location = new XY(0,0);

        public string Name { 
            get => _name;
            set {
                 if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _name = value;
            }
        }
        public string Description { get; set; }
        public int Health { get; set; }
        public XY Location { 
            get => _location;
            set {
                 if (value.Equals(null))
                    throw new ArgumentNullException(nameof(value));
                _location = value;
            }
        }
        public List<Item> Inventory { get; }
        public Status EntityStatus { get; set; }

        public Entity(string name = "jort", XY location = new XY(), string description = "")
        {
            Name = name;
            Description = description;
            Health = 10;
            Location = location;
            Inventory = new List<Item>();
            EntityStatus = Status.None;
        }

        // METHODS

        public void Attack(Entity target)
        {
            target.TakeDamage(5);
        }

        public void Die()
        {
            Health = 0;
            EntityStatus = Status.Dead;
        }

        public void DropItem(Item droppedItem)
        {
            Room thisRoom = Room.GetRoom(Location);
            if (Inventory.Contains(droppedItem))
            {
                thisRoom.Inventory.Add(droppedItem);
                Inventory.Remove(droppedItem);
                Console.WriteLine($"{droppedItem.Name}");
            }
        }

        public void GetItem(Item gotItem)
        {
            Room thisRoom = Room.GetRoom(Location);
            if (thisRoom!.Inventory.Contains(gotItem))
            {
                Inventory.Add(gotItem);
                thisRoom.Inventory.Remove(gotItem);
            }
        }

        public void HarvestFrom(Plant plant)
        {
            if (plant.PlantStatus == Plant.Status.Harvestable)
            {
                Item myItem = plant.Harvest();
            }
        }
        
        public void Move(XY newLocation)
        {
            if (Room.Exists(newLocation))
            {
                Location = newLocation;
            }
        }

        public void TakeDamage(int damage)
        {
            if (Health < damage)
            {
                Die();
            } else {
                Health -= damage;
            }
        }
    }
}