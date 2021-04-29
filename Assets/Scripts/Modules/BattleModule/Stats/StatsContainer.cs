using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Modules.BattleModule.Stats.EventArgs;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;
using UnityEngine;

namespace Modules.BattleModule.Stats
{
    public class StatsContainer
    {
        public event EventHandler<StatChangedEventArgs<PrimaryStat>> PrimaryStatChanged;
        public event EventHandler<StatChangedEventArgs<SecondaryStat>> SecondaryStatChanged;
        
        public readonly int MaxHealth;
        public readonly DynamicValue<int> Health = new DynamicValue<int>(0);

        public int this[SecondaryStat stat] => _secondaryStats[stat];
        public int this[PrimaryStat stat] => _primaryStats[stat];

        
        private readonly IReadOnlyDictionary<PrimaryStat, int> _defaultPrimaryStats;
        private readonly IReadOnlyList<int> _defaultPrimaryUpgrades;
        private readonly IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> _defaultSecondaryStats;

        private readonly Dictionary<PrimaryStat, int> _primaryStats = new Dictionary<PrimaryStat, int>();
        private readonly Dictionary<SecondaryStat, int> _secondaryStats = new Dictionary<SecondaryStat, int>();
        
        public StatsContainer(IReadOnlyDictionary<PrimaryStat, int> defaultPrimaryStats,
            IEnumerable<int> defaultPrimaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> defaultSecondaryStats)
        {
            _defaultPrimaryStats = defaultPrimaryStats;
            _defaultPrimaryUpgrades = defaultPrimaryUpgrades.ToList();
            _defaultSecondaryStats = defaultSecondaryStats;

            RecalculatePrimaryStats();
            RecalculateSecondaryStats();

            MaxHealth = _secondaryStats[SecondaryStat.Health];
            Health.Value = _secondaryStats[SecondaryStat.Health];
        }
        

        public void ChangeSecondaryStat(SecondaryStat stat, int amount)
        {
            if (stat == SecondaryStat.Health)
                Health.Value += amount;
            
            _secondaryStats[stat] += amount;
            SecondaryStatChanged?.Invoke(this, 
                new StatChangedEventArgs<SecondaryStat>(stat, _secondaryStats[stat]));
        }

        public void ChangePrimaryState(PrimaryStat stat, int amount)
        {
            _primaryStats[stat] += amount;
            RecalculateSecondaryStats();
            PrimaryStatChanged?.Invoke(this, 
                new StatChangedEventArgs<PrimaryStat>(stat, _primaryStats[stat] += amount));
        }

        private void RecalculatePrimaryStats()
        {
            var i = 0;
            
            foreach (var defaultPrimaryStat in _defaultPrimaryStats)
            {
                var upgrade = _defaultPrimaryUpgrades[i];

                if (!_primaryStats.ContainsKey(defaultPrimaryStat.Key))
                {
                    _primaryStats.Add(defaultPrimaryStat.Key, 0);
                }

                _primaryStats[defaultPrimaryStat.Key] = defaultPrimaryStat.Value + upgrade;
                i++;
            }
        }

        private void RecalculateSecondaryStats()
        {
            foreach (var defaultSecondaryStat in _defaultSecondaryStats)
            {
                var upgrades = defaultSecondaryStat.Value.UpgradeList;
                
                if (_secondaryStats.ContainsKey(defaultSecondaryStat.Key))
                {
                    _secondaryStats.Add(defaultSecondaryStat.Key, 0);
                }

                var totalUpgradeSum = upgrades.Sum(
                    upgrade => Mathf.FloorToInt(
                        _primaryStats[upgrade.Stat] * upgrade.UpgradeValue));

                _secondaryStats[defaultSecondaryStat.Key] = defaultSecondaryStat.Value.Value + totalUpgradeSum;
            }
        }
    }
}