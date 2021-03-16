using Modules.EcsModule.Groups;
using Unity.Entities;

namespace Modules.EcsModule
{
    public class WorldBuilder
    {
        public IWorldController Build(string worldName)
        {
            DefaultWorldInitialization.Initialize(worldName, false);

            var world = World.DefaultGameObjectInjectionWorld;
            var worldController = new EcsWorldController(world);

            var simulationSystemsGroup = new SimulationSystemsGroup(world);
            var presentationSystemsGroup = new PresentationSystemsGroup(world);
            
            worldController.AddGroupToWorld<SimulationSystemGroup>(world, simulationSystemsGroup);
            worldController.AddGroupToWorld<PresentationSystemGroup>(world, presentationSystemsGroup);
            
            return worldController;
        }
    }
}