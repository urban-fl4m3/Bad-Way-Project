using System;
using System.Collections.Generic;
using Modules.ActorModule;
using Modules.ActorModule.Components;
using Modules.AnimationModule;
using Modules.BattleModule.Stats;
using Modules.BattleModule.Stats.EventArgs;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;
using Modules.GridModule.Cells;

namespace Modules.BattleModule
{
    public class BattleActor
    {
        public event EventHandler<int> HealthChanged;
        public event EventHandler<BattleActor> ActorDeath;
        public event EventHandler<BattleActor> Selected;
        public event EventHandler Deselected;
        
        public readonly Actor Actor;
        public readonly StatsContainer Stats;
        public readonly CharacterAnimator Animator;

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
        
        public BattleActor(Actor actor, IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IEnumerable<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats)
        {
            Actor = actor;
            Stats = new StatsContainer(primaryStats, primaryUpgrades, secondaryStats);
            Animator = new CharacterAnimator(actor.GetActorComponent<ActorAnimationComponent>());
            
            Stats.SecondaryStatChanged += HandleHealthChanged;
            
            actor.ActorSelected += HandleActorDeselected;
            actor.ActorDeselected += HandleActorSelected;
        }

        public void TakeDamage(int damageAmount)
        {
            Stats.ChangeSecondaryStat(SecondaryStat.Health, damageAmount * -1);
        }

        private void HandleHealthChanged(object sender, StatChangedEventArgs<SecondaryStat> e)
        {
            if (e.Stat == SecondaryStat.Health)
            {
                HealthChanged?.Invoke(this, e.Amount);

                if (e.Amount <= 0)
                {
                    Animator.AnimateDeath();
                    ActorDeath?.Invoke(this,this);
                }
            }
        }
        
        private void HandleActorSelected(object sender, EventArgs e)
        {
            Deselected?.Invoke(this, e);
        }

        private void HandleActorDeselected(object sender, EventArgs e)
        {
            Selected?.Invoke(this, this);
        }
    }
}
