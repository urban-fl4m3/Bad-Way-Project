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
        public int Id { get; }
        
        public event EventHandler<BattleActor> ActorDeath;
        public event EventHandler<BattleActor> Selected;
        public event EventHandler<BattleActor> Deselected;

        public Actor Actor { get; private set; }
        public  bool IsEnemy { get; private set; }
        public  StatsContainer Stats { get; private set; }
        public  CharacterAnimator Animator { get; private set; }

        public DynamicValue<int> Health { get; private set; }
        public int MaxHealth { get; private set; }

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
        
        public BattleActor(int id, Actor actor, IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IEnumerable<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats,
            bool isEnemy)
        {
            Id = id;
            Actor = actor;
            
            Initialize(Actor, primaryStats, primaryUpgrades, secondaryStats, isEnemy);
            
            actor.GetActorComponent<ActorCollisionComponent>().Selected += HandleActorSelected;
            actor.GetActorComponent<ActorCollisionComponent>().Deselected += HandleActorDeselected;
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
        
        public void Reset( IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IEnumerable<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats,
            bool isEnemy)
        {
            Stats.SecondaryStatChanged -= HandleHealthChanged;
            
            Initialize(Actor, primaryStats, primaryUpgrades, secondaryStats, isEnemy);
        }

        private void Initialize( Actor actor, IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IEnumerable<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats,
            bool isEnemy)
        {
            IsEnemy = isEnemy;
            Stats = new StatsContainer(primaryStats, primaryUpgrades, secondaryStats);
            Animator = new CharacterAnimator(actor.GetActorComponent<ActorAnimationComponent>());
            Stats.SecondaryStatChanged += HandleHealthChanged;
            
            Health = new DynamicValue<int>(Stats[SecondaryStat.Health]);
            MaxHealth = Health.Value;
        }
        
        private void HandleActorSelected(object sender, EventArgs e)
        {
            Selected?.Invoke(this, this);
        }

        private void HandleActorDeselected(object sender, EventArgs e)
        {
            Deselected?.Invoke(this, this);
        }
        
        private void HandleHealthChanged(object sender, StatChangedEventArgs<SecondaryStat> e)
        {
            if (e.Stat == SecondaryStat.Health)
            {
                Health.Value = e.Amount;

                if (e.Amount <= 0)
                {
                    Animator.AnimateDeath();
                    Actor.GetActorComponent<ActorNavigationComponent>().NavMeshAgent.enabled = false;
                    Actor.GetActorComponent<ActorCollisionComponent>().Collider.enabled = false;
                    _placement.IsEmpty = true;
                    _placement = null;
                    ActorDeath?.Invoke(this,this);
                }
            }
        }
    }
}
