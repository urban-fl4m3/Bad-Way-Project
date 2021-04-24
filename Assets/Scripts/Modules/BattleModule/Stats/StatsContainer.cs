using System.Collections.Generic;
using Modules.BattleModule.Stats.Helpers;
using Modules.BattleModule.Stats.Models;
using UnityEngine;

namespace Modules.BattleModule.Stats
{
    public class StatsContainer
    {
        private readonly IReadOnlyDictionary<PrimaryStat, int> _primaryStats;
        private readonly IReadOnlyCollection<int> _primaryUpgrades;
        private readonly IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> _secondaryStats;

        public StatsContainer(IReadOnlyDictionary<PrimaryStat, int> primaryStats,
            IReadOnlyCollection<int> primaryUpgrades, IReadOnlyDictionary<SecondaryStat, StatAndUpgrades> secondaryStats)
        {
            _primaryStats = primaryStats;
            _primaryUpgrades = primaryUpgrades;
            _secondaryStats = secondaryStats;
        }

        private void UpdateStats()
        {
           
        }
    }
}