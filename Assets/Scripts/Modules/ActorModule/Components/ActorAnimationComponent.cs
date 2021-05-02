using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorAnimationComponent : BaseActorComponent<ActorAnimationComponent>
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;
    }
}