using UnityEngine;

namespace Modules.TickModule
{
    public class TickProcessor : MonoBehaviour
    {
        public GameObject Processor => gameObject;

        public readonly VirtualTickController<ITickUpdate> TickUpdates 
            = new VirtualTickController<ITickUpdate>();
        public readonly VirtualTickController<ITickLateUpdate> TickLateUpdates
            = new VirtualTickController<ITickLateUpdate>();
        public readonly VirtualTickController<ITickFixedUpdate> TickFixedUpdates 
            = new VirtualTickController<ITickFixedUpdate>();

        private void Update()
        {
            TickUpdates.Tick();
        }

        private void LateUpdate()
        {
            TickLateUpdates.Tick();
        }
        
        private void FixedUpdate()
        {
            TickFixedUpdates.Tick();
        }
        
        public void Dispose()
        {
            TickUpdates.Clear();
            TickFixedUpdates.Clear();
            TickLateUpdates.Clear();
        }
    }
}