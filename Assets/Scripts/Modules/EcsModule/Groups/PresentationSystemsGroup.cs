using Unity.Entities;

namespace Modules.EcsModule.Groups
{
    public class PresentationSystemsGroup : CustomComponentSystemGroup
    {
        public PresentationSystemsGroup(World world) : base(world)
        {
            
        }

        protected override void HandleCreate()
        {
            base.HandleCreate();
            
            
        }
    }
}