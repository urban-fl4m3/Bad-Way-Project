using System.Collections.Generic;
using Modules.EcsModule.Systems;
using Unity.Entities;

[assembly: DisableAutoCreation]
namespace Modules.EcsModule.Groups
{
    public class CustomComponentSystemGroup : ComponentSystemGroup
    {
        protected bool ShouldUpdate { get; set; }
        
        private readonly World _world;
        private readonly HashSet<CustomComponentSystem> _systems = new HashSet<CustomComponentSystem>();
        
        protected CustomComponentSystemGroup(World world)
        {
            _world = world;
        }

        protected void AddSystemToGroup(CustomComponentSystem system, bool isEnabled = true)
        {
            _systems.Add(system);
            
            AddSystemToUpdateList(system);
            system.Enabled = true;
        }
        
        protected sealed override void OnCreate()
        {
            base.OnCreate();
            HandleCreate();
        }
        
        protected sealed override void OnUpdate()
        {
            if (ShouldUpdate)
            {
                base.OnUpdate();
            }
        }

        protected virtual void HandleCreate()
        {
            
        }

        public void Clear()
        {
            foreach (var system in _systems)
            {
                //Some unsubscribings
            }
        }
    }
}