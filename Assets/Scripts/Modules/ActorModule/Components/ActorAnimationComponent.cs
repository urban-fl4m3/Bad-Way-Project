using System;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorAnimationComponent : BaseActorComponent<ActorAnimationComponent>
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;

        public bool IsIdlePlaying()
        {
            var idle = _animator.GetCurrentAnimatorStateInfo(0).IsName("Ilde");
            var covered = _animator.GetCurrentAnimatorStateInfo(0).IsName("Taking Cover Idle");
            return (idle || covered);
        }
    }
}