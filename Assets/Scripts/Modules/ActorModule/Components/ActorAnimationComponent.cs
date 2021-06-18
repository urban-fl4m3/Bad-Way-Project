using UnityEngine;

namespace Modules.ActorModule.Components
{
    /// <summary>
    /// Добавить конфиг, в котором настраивается начальное состтояние анимации.
    /// Enum{ idle , run , fire , reset}
    ///
    /// _animationConfig.OnReset(_animator)
    // AnimationConfig: 
    //  foreach (IAnimationContext context in animations)
    //{
   // context.Process(_animator)
  //  }
  //  abstract AnimationContext -> abstract void Process(Animator animator):
  //animator.Play("idle")   / animator.SetBool("isRunning", false);
   // Enum:   { Play/ SetBool / SetTrigger }
    /// </summary>
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