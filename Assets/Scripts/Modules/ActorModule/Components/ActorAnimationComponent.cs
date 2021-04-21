using Common;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorAnimationComponent : MonoBehaviour, IActorComponent
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;
        
        public void Initialize(TypeContainer container)
        {
            container.Add<ActorAnimationComponent>(this);    
        }
    }
}