using System;
using System.Collections.Generic;

namespace Modules.TickModule
{
    public class TickManager : ITickManager
    {
        private readonly TickProcessor _processor;

        private readonly Dictionary<object, List<ITick>> _allTicks = new Dictionary<object, List<ITick>>();

        public TickManager(TickProcessor tickProcessor)
        {
            if (_processor != null)
            {
                UnityEngine.Object.Destroy(tickProcessor.Processor);
                tickProcessor.Dispose();
                
                throw new Exception($"Tick processor is already exists");
            }
            
            _processor = tickProcessor;
        }

        public void AddTick(object owner, ITickUpdate tick)
        {
            AddTickInternal(owner, tick);
            _processor.TickUpdates.Add(tick);
        }

        public void AddTick(object owner, ITickLateUpdate tick)
        {
            AddTickInternal(owner, tick);
            _processor.TickLateUpdates.Add(tick);
        }

        public void AddTick(object owner, ITickFixedUpdate tick)
        {
            AddTickInternal(owner, tick);
            _processor.TickFixedUpdates.Add(tick);
        }

        public void RemoveTick(ITickUpdate tickUpdate)
        {
            _processor.TickUpdates.Remove(tickUpdate);
        }

        public void RemoveTick(ITickLateUpdate tickUpdate)
        {
            _processor.TickLateUpdates.Remove(tickUpdate);
        }

        public void RemoveTick(ITickFixedUpdate tickFixedUpdate)
        {
            _processor.TickFixedUpdates.Remove(tickFixedUpdate);
        }
        
        private void AddTickInternal(object owner, ITick tick)
        {
            var hasOwner = _allTicks.TryGetValue(owner, out var tickList);

            if (hasOwner)
            {
                tickList.Add(tick);
            }
            else
            {
                tickList = new List<ITick> {tick};
                _allTicks.Add(owner, tickList);
            }
        }
    }
}