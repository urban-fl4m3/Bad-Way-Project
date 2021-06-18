using Common;
using Modules.ActorModule.Components;
using UnityEngine;

namespace Modules.ActorModule
{
    public class Actor : MonoBehaviour
    {
        public Transform Transform => transform;

        private readonly TypeContainer _container = new TypeContainer();

        private void Awake()
        {
            var components = GetComponents<IActorComponent>();
            foreach (var actorComponent in components)
            {
                actorComponent.Initialize(_container);
            }
        }

        public void Reset()
        {
            var components = _container.GetList<IActorComponent>();
            foreach (var actorComponent in components)
            {
                actorComponent.Reset();
            }
        }

        public T GetActorComponent<T>() where T : class, IActorComponent
        {
            return _container.Resolve<T>(); 
        }
    }
}