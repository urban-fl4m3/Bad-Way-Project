using UnityEngine;

namespace Modules.ActorModule.Concrete
{
    public class CameraActor : Actor
    {
        [SerializeField] private Camera _component;
        public Camera Component => _component;
    }
}