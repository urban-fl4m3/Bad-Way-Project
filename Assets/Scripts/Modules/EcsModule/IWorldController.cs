using Modules.EcsModule.Groups;
using Unity.Entities;

namespace Modules.EcsModule
{
    public interface IWorldController
    {
        Entity CreateEntity(params ComponentType[] components);
        void AddGroupToWorld<T>(World world, CustomComponentSystemGroup group) where T : ComponentSystemGroup;
        void SetEntityComponent<T>(Entity entity, T component) where T : struct, IComponentData;
        void SetWorldSystemsEnable(bool isEnable);
        void DisposeWorld();
    }
}