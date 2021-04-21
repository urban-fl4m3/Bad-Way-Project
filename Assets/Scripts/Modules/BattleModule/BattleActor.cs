using System.Collections.Generic;
using Modules.ActorModule;
using Modules.ActorModule.Components;
using Modules.AnimationModule;
using Modules.BattleModule.Stats;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;
using Modules.GridModule.Cells;
using UnityEngine;

namespace Modules.BattleModule
{
    public class BattleActor
    {
        public readonly Actor Actor;
        public StatsContainer Stats { get; }

        private CharacterAnimator _characterAnimator;
        
        public BattleActor(Actor actor, IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IReadOnlyCollection<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats)
        {
            Actor = actor;
            Stats = new StatsContainer(primaryStats, primaryUpgrades, secondaryStats);

            _characterAnimator = new CharacterAnimator(Actor.GetActorComponent<ActorAnimationComponent>());
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
