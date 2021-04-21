using Modules.ActorModule.Components;
using UnityEngine;

namespace Modules.AnimationModule
{
    public class CharacterAnimator
    {
        private readonly ActorAnimationComponent _animationComponent;

        public CharacterAnimator(ActorAnimationComponent animationComponent)
        {
            _animationComponent = animationComponent;
        }

        public void ChangeMovingState(bool state)
        {
            _animationComponent.Animator.SetBool("IsRunning", state);
        }

        public void AnimateShooting()
        {
            _animationComponent.Animator.SetTrigger("Shoot");
        }

        public void AnimateCovering()
        {
            
        }
    }
}