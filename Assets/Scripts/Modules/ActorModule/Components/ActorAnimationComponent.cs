using System;
using UnityEngine;

namespace Modules.ActorModule.Components
{
    public class ActorAnimationComponent : BaseActorComponent<ActorAnimationComponent>
    {
        [SerializeField] private Animator _animator;

        public Animator Animator => _animator;

        public override void OnReset()
        {
            _animator.Play("Idle");
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsCovering", false);
        }
    }
}