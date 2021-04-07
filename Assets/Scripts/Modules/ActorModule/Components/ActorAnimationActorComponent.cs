using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorAnimationActorComponent : MonoBehaviour, IActorComponent
    {
        [SerializeField] private Animator _animator;
        
        public void Initialize(TypeContainer container)
        {
            container.Add<ActorAnimationActorComponent>(this);    
        }
    }
}