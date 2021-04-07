using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorCollisionActorComponent : MonoBehaviour, IActorComponent
    {
        [SerializeField] private Collider _collider;
        
        public void Initialize(TypeContainer container)
        {
            container.Add<ActorCollisionActorComponent>(this);    
        }
    }
}