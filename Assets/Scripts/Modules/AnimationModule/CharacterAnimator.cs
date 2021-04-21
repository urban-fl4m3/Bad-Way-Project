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

        public void AnimateMoving()
        {
            _animationComponent.Animator.SetBool("IsRunning", true);
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