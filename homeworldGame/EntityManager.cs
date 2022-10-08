namespace homeworld {
    public class EntityManager
    {
        public int lowestUnassignedEntityID = 1;
        public List<int> allEntities;
        
        // Dictionary of (ComponentType, Dictionary)
        // ComponentType is the type class: XY, T is a generic object that implements the interface
        // Dictionary contains (UUID, the Component itself)
        public Dictionary<Type, Dictionary<int, IComponent>> ComponentStores { get; set; }

        public EntityManager()
        {
            allEntities = new List<int>(); // Entities live here
            // the int is how they are associated with the components in the dictionary below
            // sample table looks like this?
            //      XY - (1 - PositionComponent1) // entity numbers are unique Keys
            //           (2 - PositionComponent2) // only components are non-unique
            //           (3 - PositionComponent3) // but they're instantiated data bags
            //  string - (1 - Name1)              // and so distinguished by the data in
            //           (2 - Name2)              // them? not sure
            //           (3 - Name3)
            //     int - (1 - Health1)
            //           (2 - Health2)
            //           (3 - Health3)

            ComponentStores = new Dictionary<Type, Dictionary<int, IComponent>>();
            // componentStores rows look like this:
            //      XY - (#)  // unique rows in the outer dictionary that track 
            //  string - (#)  // components of their type assigned to entities
            //     int - (#)
            //   etc..
        }

        // METHODS
        public void AddComponent(int entity, IComponent component)
        {
            // in order to add a component
            // we need to get the store of this class of component
            Type componentClass = component.GetType();
            if(ComponentStores.TryGetValue(componentClass, out Dictionary<int, IComponent> store))
            {
                // we need to see if the entity already has an entry in this store
                if(store.ContainsKey(entity))
                {
                    // entity already has at least one component of this type
                    Console.WriteLine($"{entity} already has a {component}");
                }
                else // store does not contain an entry for this entity
                {
                    store.Add(entity, component);
                }
            }
            else // ComponentStores does not have a store for this component class
            {
                Dictionary<int, IComponent> nextStore = new Dictionary<int, IComponent>();
                nextStore.Add(entity, component);
                ComponentStores.Add(componentClass, nextStore);
            }
        }
        
        public int CreateEntity()
        {
            int newID = GenerateNewEntityID();

            if (newID < 1)
            {
                // fatal error
                return 0;
            }
            else
            {
                allEntities.Add(newID);
                return newID;
            }
        }

        public int GenerateNewEntityID()
        {
            if(lowestUnassignedEntityID < Int32.MaxValue)
            {
                return lowestUnassignedEntityID++;
            }
            else
            {
                for (int i = 1; i < Int32.MaxValue; i++)
                {
                    if (!allEntities.Contains(i))
                        return i;
                }

                throw new Exception("ERROR: No availalbe Entity IDs; too many entities!");
            }
        }

        public List<IComponent> GetAllComponentsOfType(Type type)
        {
            List<IComponent> result = new List<IComponent>();

            if (ComponentStores.TryGetValue(type, out Dictionary<int, IComponent> store))
            {
                result = store.Values.ToList();
            }

            return result;
        }

        public List<int> GetAllEntitiesPossessingComponent(Type type)
        {
            List<int> result = new List<int>();

            if (ComponentStores.TryGetValue(type, out Dictionary<int, IComponent> store))
            {
                result = store.Keys.ToList();
            }

            return result;
        }

        public IComponent GetComponent(int entity, Type componentType)
        {
            // if we have any of these components registered already
            if (ComponentStores.TryGetValue(componentType, out Dictionary<int, IComponent> store))
            {
                if (store.TryGetValue(entity, out IComponent component))
                    return component;
                return null;
            }
            else
            {
                return null;
                throw new ArgumentException($"Entity {entity} does not contain any {componentType} components!");
            }
        }

        public void KillEntity(int entity)
        {
            allEntities.Remove(entity);
            foreach (Dictionary<int, IComponent> store in ComponentStores.Values)
            {
                store.Remove(entity);
            }
        }
        
        public void RemoveComponent(int entity, IComponent component)
        {
            Type componentClass = component.GetType();   
            // if we have any of these components registered already
            if (ComponentStores.TryGetValue(componentClass, out Dictionary<int, IComponent> store))
            {
                // if that register of components has this entity
                if (store.ContainsKey(entity))
                {
                    store.Remove(entity);
                }
                else
                {
                    throw new ArgumentException($"No entities!");
                }
            }
            else
            {
                throw new ArgumentException($"No components!");
            }
        }
    }
}