using Unity.Entities;

namespace Modules.EcsModule.Systems
{
    public class CustomComponentSystem : ComponentSystem
    {
        protected bool ShouldUpdate { get; set; } = true;
        
        protected sealed override void OnCreate()
        {
            base.OnCreate();
            HandleCreate();
        }

        protected sealed override void OnStartRunning()
        {
            base.OnStartRunning();
            HandleStartRunning();
        }

        protected sealed override void OnStopRunning()
        {
            base.OnStopRunning();
            HandleStopRunning();
        }

        protected sealed override void OnDestroy()
        {
            base.OnDestroy();
            HandleDestroy();
        }

        protected sealed override void OnUpdate()
        {
            if (ShouldUpdate)
            {
                HandleUpdate();
            }    
        }

        protected virtual void HandleCreate()
        {
            
        }

        protected virtual void HandleStartRunning()
        {
            
        }

        protected virtual void HandleStopRunning()
        {
            
        }

        protected virtual void HandleDestroy()
        {
            
        }

        protected virtual void HandleUpdate()
        {
            
        }
    }
}