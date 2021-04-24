using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Modules.ActorModule;
using Modules.ActorModule.Components;
using Modules.AnimationModule;
using Modules.BattleModule.Stats;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;
using Modules.GridModule.Cells;
using UnityEngine.EventSystems;

namespace Modules.BattleModule
{
    public class BattleActor
    {
        public EventHandler<int> HealthChange;
        public EventHandler<BattleActor> ActorDeath;
        
        public readonly Actor Actor;
        public readonly StatsContainer Stats;
        public readonly CharacterAnimator Animator;

        public int Health;
        public int ValueHealth;

        public BattleActor(Actor actor, IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IReadOnlyCollection<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats)
        {
            Actor = actor;
            Stats = new StatsContainer(primaryStats, primaryUpgrades, secondaryStats);
            Animator = new CharacterAnimator(actor.GetActorComponent<ActorAnimationComponent>());

            Health = secondaryStats[0].Value;
            ValueHealth = secondaryStats[0].Value;

            actor.SetHelth(secondaryStats[0].Value,secondaryStats[0].Value);
        }

        public void TakeDamage(int DP)
        {
            Health -= DP;
            HealthChange?.Invoke(this,Health);
            if (Health <= 0)
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
