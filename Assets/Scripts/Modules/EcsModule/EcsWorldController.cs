using System;
using System.Collections.Generic;
using Modules.EcsModule.Groups;
using Unity.Entities;

namespace Modules.EcsModule
{
    public class EcsWorldController : IWorldController
    {
        private readonly World _world;
        private readonly List<CustomComponentSystemGroup> _systemGroups = new List<CustomComponentSystemGroup>();

        public EcsWorldController(World world)
        {
            _world = world;
        }
        
        public Entity CreateEntity(params ComponentType[] components)
        {
            ValidateWorld();
            return _world.EntityManager.CreateEntity(components);
        }
        
        public void AddGroupToWorld<T>(World world, CustomComponentSystemGroup group) where T : ComponentSystemGroup
        {
            ValidateWorld();

            _systemGroups.Add(group);

            world.GetExistingSystem<T>().AddSystemToUpdateList(world.AddSystem(group));
        }
        
        public void SetEntityComponent<T>(Entity entity, T component) where T : struct, IComponentData
        {
            _world.EntityManager.SetComponentData(entity, component);
        }
        
        public void SetWorldSystemsEnable(bool isEnable)
        {
            ValidateWorld();

            foreach (var system in _world.Systems)
            {
                system.Enabled = isEnable;
            }
        }
        
        public void DisposeWorld()
        {
            ValidateWorld();

            var entityManager = _world.EntityManager;
            entityManager.DestroyEntity(entityManager.UniversalQuery);

            foreach (var systemGroup in _systemGroups)
            {
                systemGroup.Clear();
            }

            _systemGroups.Clear();

            World.DisposeAllWorlds();
        }
        
        private void ValidateWorld()
        {
            if (_world == null)
            {
                throw new Exception("World not created or already destroyed");
            }
        }

    }
}