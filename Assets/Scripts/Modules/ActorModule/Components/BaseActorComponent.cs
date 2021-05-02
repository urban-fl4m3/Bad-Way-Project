using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public abstract class BaseActorComponent<TComponent> : MonoBehaviour, IActorComponent 
        where TComponent : IActorComponent
    {
        public void Initialize(TypeContainer container)
        {
            container.Add<TComponent>(this);
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
            
        }
    }
}