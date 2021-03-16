using Modules.BattleModule.Systems;
using Unity.Entities;

namespace Modules.EcsModule.Groups
{
    public class SimulationSystemsGroup : CustomComponentSystemGroup
    {
        public SimulationSystemsGroup(World world) : base(world)
        {
            
        }

        protected override void HandleCreate()
        {
            base.HandleCreate();
            
            AddSystemToGroup(new TestSystem());
        }
    }
}