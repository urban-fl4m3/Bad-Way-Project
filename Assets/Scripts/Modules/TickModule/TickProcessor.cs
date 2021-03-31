using System.Collections.Generic;
using UnityEngine;

namespace Modules.TickModule
{
    //Todo create generic update controller for each tick type
    public class TickProcessor : MonoBehaviour
    {
        public GameObject Processor => gameObject;

        public readonly List<ITickUpdate> TickUpdates = new List<ITickUpdate>();
        public readonly List<ITickLateUpdate> TickLateUpdates = new List<ITickLateUpdate>();
        public readonly List<ITickFixedUpdate> TickFixedUpdates = new List<ITickFixedUpdate>();

        private void Update()
        {
            foreach (var tick in TickUpdates)
            {
                tick.Tick();
            }
        }

        private void LateUpdate()
        {
            foreach (var tick in TickLateUpdates)
            {
                tick.Tick();
            }
        }
        
        private void FixedUpdate()
        {
            foreach (var tick in TickFixedUpdates)
            {
                tick.Tick();
            }
        }
        
        public void Dispose()
        {
            TickUpdates.Clear();
            TickFixedUpdates.Clear();
            TickLateUpdates.Clear();
        }
    }
}