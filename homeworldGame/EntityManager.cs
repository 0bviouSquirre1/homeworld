namespace homeworld {
    public class EntityManager
    {
        public List<Guid> allEntities; // this is the table of entities (table 4 )
        public Dictionary<Type, Dictionary<Guid, IComponent>> ComponentStores { get; set; } // this is the table of entity_components (table 5)
        // table n+5 are the actual component objects?

        public EntityManager()
        {
            allEntities = new List<Guid>(); // Entities live here
            // the Guid is how they are associated with the components in the dictionary below
            // sample table looks like this
            //      XY - (1 - PositionComponent1) // entity numbers are unique Keys
            //           (2 - PositionComponent2) // only components are non-unique
            //           (3 - PositionComponent3) // but they're instantiated data bags
            //  string - (1 - Name1)              // and so distinguished by the data in
            //           (2 - Name2)              // them? not sure
            //           (3 - Name3)
            //     int - (1 - Health1)
            //           (2 - Health2)
            //           (3 - Health3)

            ComponentStores = new Dictionary<Type, Dictionary<Guid, IComponent>>();
            // componentStores rows look like this:
            //      XY - (#)  // unique rows in the outer dictionary that track 
            //  string - (#)  // components of their type assigned to entities
            //     Guid - (#)
            //   etc..
        }

        // METHODS
        public void AddComponent(Guid entity, IComponent component)
        {
            // in order to add a component
            // we need to get the store of this class of component
            Type componentClass = component.GetType();
            if(ComponentStores.TryGetValue(componentClass, out Dictionary<Guid, IComponent> store))
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
                Dictionary<Guid, IComponent> nextStore = new Dictionary<Guid, IComponent>();
                nextStore.Add(entity, component);
                ComponentStores.Add(componentClass, nextStore);
            }
        }
        
        public Guid CreateEntity()
        {
            Guid newID = Guid.NewGuid();
        
            allEntities.Add(newID);
            return newID;
        }

        public List<IComponent> GetAllComponentsOfType(Type type)
        {
            List<IComponent> result = new List<IComponent>();

            if (ComponentStores.TryGetValue(type, out Dictionary<Guid, IComponent> store))
            {
                result = store.Values.ToList();
            }

            return result;
        }

        public List<Guid> GetAllEntitiesPossessingComponent(Type type)
        {
            List<Guid> result = new List<Guid>();

            if (ComponentStores.TryGetValue(type, out Dictionary<Guid, IComponent> store))
            {
                result = store.Keys.ToList();
            }

            return result;
        }

        public IComponent GetComponent(Guid entity, Type componentType)
        {
            // if we have any of these components registered already
            if (ComponentStores.TryGetValue(componentType, out Dictionary<Guid, IComponent> store))
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

        public void KillEntity(Guid entity)
        {
            allEntities.Remove(entity);
            foreach (Dictionary<Guid, IComponent> store in ComponentStores.Values)
            {
                store.Remove(entity);
            }
        }
        
        public void RemoveComponent(Guid entity, IComponent component)
        {
            Type componentClass = component.GetType();   
            // if we have any of these components registered already
            if (ComponentStores.TryGetValue(componentClass, out Dictionary<Guid, IComponent> store))
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