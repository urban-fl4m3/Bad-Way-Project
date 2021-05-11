using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorTargetComponent: BaseActorComponent<ActorTargetComponent>
    {
        public Transform TargetForUI;
        public Transform ThirdPersonCamera;
    }
}