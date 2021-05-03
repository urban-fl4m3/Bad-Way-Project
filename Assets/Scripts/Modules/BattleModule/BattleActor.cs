using System;
using System.Collections.Generic;
using Common;
using Modules.ActorModule;
using Modules.ActorModule.Components;
using Modules.AnimationModule;
using Modules.BattleModule.Stats;
using Modules.BattleModule.Stats.EventArgs;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;
using Modules.GridModule.Cells;
using Modules.GunModule;

namespace Modules.BattleModule
{
    public class BattleActor
    {
        public event EventHandler<BattleActor> ActorDeath;
        public event EventHandler<BattleActor> Selected;
        public event EventHandler<BattleActor> Deselected;
        
        public readonly Actor Actor;
        public readonly bool IsEnemy;
        public readonly StatsContainer Stats;
        public readonly CharacterAnimator Animator;

        public readonly DynamicValue<int> Health;
        public readonly int MaxHealth;
        
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
        public WeaponInfo _weaponInfo { get; private set; }
        
        public BattleActor(Actor actor, IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IEnumerable<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats,
            bool isEnemy)
        {
            Actor = actor;
            IsEnemy = isEnemy;
            Stats = new StatsContainer(primaryStats, primaryUpgrades, secondaryStats);
            Animator = new CharacterAnimator(actor.GetActorComponent<ActorAnimationComponent>());
            Stats.SecondaryStatChanged += HandleHealthChanged;
            
            Health = new DynamicValue<int>(Stats[SecondaryStat.Health]);
            MaxHealth = Health.Value;
            
            actor.ActorSelected += HandleActorSelected;
            actor.ActorDeselected += HandleActorDeselected;
        }

        public void SetWeapon(WeaponInfo weaponInfo)
        {
            _weaponInfo = weaponInfo;
            Actor.GetActorComponent<WeaponPlaceholderComponent>().SetWeapon(weaponInfo.Prefab);
        }
        
        public void TakeDamage(int damageAmount)
        {
            Stats.ChangeSecondaryStat(SecondaryStat.Health, damageAmount * -1);
        }

        private void HandleHealthChanged(object sender, StatChangedEventArgs<SecondaryStat> e)
        {
            if (e.Stat == SecondaryStat.Health)
            {
                Health.Value = e.Amount;

                if (e.Amount <= 0)
                {
                    Animator.AnimateDeath();
                    ActorDeath?.Invoke(this,this);
                }
            }
        }
        
        private void HandleActorSelected(object sender, EventArgs e)
        {
            Selected?.Invoke(this, this);
        }

        private void HandleActorDeselected(object sender, EventArgs e)
        {
            Deselected?.Invoke(this, this);
        }
    }
}
