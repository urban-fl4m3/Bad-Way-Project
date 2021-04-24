using Modules.ActorModule.Components;

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

        public void AnimateCovering(bool isCovering)
        {
            _animationComponent.Animator.SetBool("IsCovering", isCovering);
        }

        public void AnimateDeath()
        {
            _animationComponent.Animator.SetTrigger("Death");
        }
    }
}