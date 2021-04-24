using System;
using System.Collections.Generic;
using Modules.ActorModule;
using Modules.ActorModule.Components;
using Modules.AnimationModule;
using Modules.BattleModule.Stats;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;
using Modules.GridModule.Cells;

namespace Modules.BattleModule
{
    public class BattleActor
    {
        public EventHandler<int> HealthChanged;
        public EventHandler<BattleActor> ActorDeath;
        
        public readonly Actor Actor;
        public readonly StatsContainer Stats;
        public readonly CharacterAnimator Animator;

        public BattleActor(Actor actor, IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IEnumerable<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats)
        {
            Actor = actor;
            Stats = new StatsContainer(primaryStats, primaryUpgrades, secondaryStats);
            Animator = new CharacterAnimator(actor.GetActorComponent<ActorAnimationComponent>());
        }

        public void TakeDamage(int damageAmount)
        {
            Stats.ChangeSecondaryStat(SecondaryStat.Health, damageAmount * -1);
            
            HealthChanged?.Invoke(this, Stats[SecondaryStat.Health]);
            if (Stats[SecondaryStat.Health] <= 0)
            {
                Animator.AnimateDeath();
                ActorDeath?.Invoke(this,this);
            }
        }
        
        public Cell Placement
        {
            get =>_placement;
            set
            {
                if (_placement != null)
                {
                    _placement.IsEmpty = true;
                }
                _placement = value;
                _placement.IsEmpty = false;
            }
        }
        
        private Cell _placement;
    } 
}
